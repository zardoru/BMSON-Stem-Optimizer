using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using NAudio.Wave;

namespace BmsonStemOptimizer
{
    public class StemTimeMap
    {
        public string outputFile;
        public double originalStart;
        public double originalEnd;

        public bool Use
        {
            get { return originalEnd - originalStart > StemOptimizer.MinPlaybackTime || !StemOptimizer.UseMinimumPlayback; }
        }

        public bool InRange(double time)
        {
            return time >= originalStart && time < originalEnd;
        }

        public StemTimeMap(string inputFile, double os, double oe)
        {
            originalEnd = oe;
            originalStart = os;
            outputFile = string.Format("{1} - {0}{2}",
                os,
                Path.GetFileNameWithoutExtension(inputFile),
                Path.GetExtension(inputFile));
        }
    }

    public static class StemOptimizer
    {
        // Minimum time of silence required to perform a cut
        public static double MinSilenceTime = .2f;

        // Minimum time of playback required to consider this cut
        public static double MinPlaybackTime = 0.15f;
        public static bool UseLoudEpsilon = true;
        public static bool UseMinimumPlayback = true;

        // Minimum value to be considered "hearable"
        private static readonly float LOUD_EPSILON = 1e-2f;

        internal class TimeCut
        {
            public double start;
            public double end;

            public TimeCut(double s, double e)
            {
                start = s;
                end = e;
            }
        };

        static string[] BMSONVersionsSupported = new string[]
        {
            "1.0.0"
        };

        static List<string> GetStemPathsFromBMSONRoot(string bmson_file, JObject root)
        {
            var folder = Path.GetDirectoryName(bmson_file);
            var ret = new List<string>();

            foreach(JObject sc in root["sound_channels"])
            {
                string path = sc["name"].ToString();
                if (!ret.Contains(path))
                    ret.Add(path);
                else
                {
                    Logger.Log("INFO: Stem {0} already in optimization list, skipping", path);
                }
            }

            return ret;
        }

        static bool CheckBMSONVersionFromBMSONRoot(JObject root)
        {
            return BMSONVersionsSupported.Contains(root["version"].ToString());
        }

        static StemTimeMap[] SilenceToCuts(string file, List<TimeCut> silences, double duration)
        {
            List<StemTimeMap> ret = new List<StemTimeMap>();
            double startTime = 0;

            if (silences.Count() == 0)
            {
                ret.Add(new StemTimeMap(file, 0, duration));
                return ret.ToArray();
            }

            if (silences.ElementAt(0).start == startTime)
            {
                startTime = silences.ElementAt(0).end;
            }

            foreach(var silence in (startTime == 0 ? silences : silences.Skip(1)))
            {
                ret.Add(new StemTimeMap(file, startTime, silence.start));
                startTime = silence.end;
            }

            if (startTime < duration)
                ret.Add(new StemTimeMap(file, startTime, duration));

            return ret.ToArray();
        }

        static StemTimeMap[] OptimizeStem(string bmson, BmsonTimingData timing, string inp)
        {
            string file = Path.GetDirectoryName(bmson) + Path.DirectorySeparatorChar + inp;
            List<TimeCut> silences = new List<TimeCut>();

            Logger.Log("Starting optimization for stem {0}", Path.GetFileName(file));

            var reader = new WaveFileReader(file);

            long silenceCutThreshold = (long)(reader.WaveFormat.SampleRate * MinSilenceTime);

            double sr = reader.WaveFormat.SampleRate;
            float[] frame;
            long silenceFrames = 0;
            long currentFrame = 0;

            Action addCut = () =>
            {
                if (silenceFrames > silenceCutThreshold)
                {
                    TimeCut tc = new TimeCut((currentFrame - silenceFrames) / sr, (currentFrame) / sr);
                    tc.start = timing.SnappedTimeToPulse(tc.start);
                    tc.end = timing.SnappedTimeToPulse(tc.end);
                    silences.Add(tc);
                }
            };

            while ( (frame = reader.ReadNextSampleFrame()) != null ) {
                bool allZero = true;
                foreach (var f in frame)
                {
                    if ( (Math.Abs(f) >= LOUD_EPSILON && UseLoudEpsilon) ||
                         (f != 0 && !UseLoudEpsilon) )
                    {
                        allZero = false;
                        break;
                    }
                }

                if (allZero)
                {
                    silenceFrames++;
                } else
                {

                    // got some noise? if so check if silence lasted for a significant time
                    // if so add a cut
                    addCut();

                    silenceFrames = 0;
                }

                currentFrame++;
            }


            addCut();

            string message = "Silence periods found for stem {0}: " + Environment.NewLine;
            foreach(var silence in silences)
            {
                message += string.Format("\tstart: {0} to {1}{2}", 
                    silence.start, silence.end, Environment.NewLine);
            }

            Logger.Log(message, Path.GetFileName(file));
            return SilenceToCuts(file, silences.OrderBy(x => x.start).ToList(), reader.TotalTime.TotalSeconds);
        }

        public static async Task<Dictionary<string, StemTimeMap[]>> OptimizeFromBMSON(string bmson, string output_dir)
        {
            JObject root = GetBMSONRoot(bmson);

            var timing = new BmsonTimingData(root);
            var timemap = new Dictionary<string, StemTimeMap[]>();
            await CreateRemappingDataFromBMSON(bmson, root, timing, timemap);
            
            Logger.Log("Remapping Data generated.");
            return timemap;
        }

        private static JObject GetBMSONRoot(string bmson)
        {
            var json_txt = File.ReadAllText(bmson, new UTF8Encoding());
            var root = JObject.Parse(json_txt);

            if (!CheckBMSONVersionFromBMSONRoot(root))
            {
                string msg = string.Format(
                    "BMSON version unsupported. Supported versions are: {0}",
                    string.Join(", ", BMSONVersionsSupported));

                throw new NotSupportedException(msg);
            }

            return root;
        }

        

        private static async Task CreateRemappingDataFromBMSON(string bmson, JObject root, BmsonTimingData timing, Dictionary<string, StemTimeMap[]> timemap)
        {
            var stemPaths = GetStemPathsFromBMSONRoot(bmson, root);
            var _lock = new object();
            var tasks = new List<Task>();
            stemPaths.ToList().ForEach((stemPath) =>
            {
                Action fn = () =>
                {
                    var optimized = OptimizeStem(bmson, timing, stemPath);
                    lock (_lock)
                    {
                        timemap[stemPath] = optimized;
                    }
                };
                //fn();
                tasks.Add(Task.Run(fn));
            });

            await Task.WhenAll(tasks);
            Logger.Log("Done locating silence periods and remapping to nonsilent periods.");
        }

        public static void SerializeRemappingDataToJSON(Dictionary<string, StemTimeMap[]> timemap, string filepath)
        {
            Logger.Log("Writing remapping data for possible user usage to {0}", filepath);

            StreamWriter output = new StreamWriter(filepath);
            string json = JsonConvert.SerializeObject(timemap, Formatting.Indented);
            output.WriteLine(json);
            output.Flush();
        }

        public static void WriteNewStemsFromRemappingData(string bmson, Dictionary<string, StemTimeMap[]> timemap, string output_dir)
        {
            Logger.Log("Outputting new audio data and time remapping data.");

            foreach (var stem in timemap)
            {
                var reader = new WaveFileReader(Path.GetDirectoryName(bmson) + Path.DirectorySeparatorChar + stem.Key);
                foreach (var period in stem.Value)
                {
                    // 1e7 ticks = 1s
                    char sep = Path.DirectorySeparatorChar;
                    long startTick = (long)(period.originalStart * 1e7);
                    long endTick = (long)(period.originalEnd * 1e7);
                    string outfile = output_dir + sep + period.outputFile;

                    double len = period.originalEnd - period.originalStart;
                    if ( len < MinPlaybackTime && UseMinimumPlayback)
                    {
                        Logger.Log("Skipping {0} as it is too short ({1} < {2}", outfile, len, MinPlaybackTime);
                        continue;
                    }

                    reader.CurrentTime = new TimeSpan(startTick);

                    Logger.Log("Outputting period {0} to {1} to {2}...", period.originalStart, period.originalEnd, outfile);
                    var output = new WaveFileWriter(outfile, reader.WaveFormat);

                    while (reader.CurrentTime.Ticks < endTick)
                    {
                        float[] frame = reader.ReadNextSampleFrame();
                        foreach(var samp in frame)
                            output.WriteSample(samp);
                    }

                    output.SetLength(reader.WaveFormat.BitsPerSample / 8 * reader.Length);
                    output.Close();
                }
            }
        }
    }
}
