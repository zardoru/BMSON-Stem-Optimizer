using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace BmsonStemOptimizer
{
    /*
        integration methods taken straight out of raindrop
        ith modifications to a few types (double -> ulong)
    */
    class BmsonTimingData
    {
        SortedDictionary<ulong, double> BPM = new SortedDictionary<ulong, double>();
        SortedDictionary<ulong, ulong> Stops = new SortedDictionary<ulong, ulong>();

        // derivative stuff from the above
        SortedDictionary<double, double> timeBPM = new SortedDictionary<double, double>();
        SortedDictionary<double, double> timeStops = new SortedDictionary<double, double>();
        SortedDictionary<double, double> BPS = new SortedDictionary<double, double>();

        ulong resolution = 240;

        public double BPMTimeAtPulse(ulong pulse)
        {
            double time = 0;
            var list = BPM.Where(x => x.Key < pulse);
            for (int i = 0; i < list.Count(); i++)
            {
                var current = list.ElementAt(i);
                var spb = 60.0 / current.Value;
                if (i + 1 < list.Count())
                {
                    var next = list.ElementAt(i + 1);
                    time += ((next.Key - current.Key) / (double)resolution) * spb;
                }
                else
                {
                    time += (pulse - current.Key) / (double)resolution * spb;
                }
            }

            return time;
        }

        public double SPBAtPulse(ulong pulse)
        {
            return 60.0 / BPMAtPulse(pulse);
        }

        // Return accomulated time for stops with stop pulse strictly less than pulse
        public double StopTimeAtPulse(ulong pulse)
        {
            var time = Stops.Where(x => x.Key < pulse)
                               .Select(x => x.Value / resolution * SPBAtPulse(x.Key));

            if (time.Count() > 0) return time.Aggregate((cur, v) => cur + v);

            return 0.0;
        }


        public double TimeAtPulse(ulong pulse)
        {
            return BPMTimeAtPulse(pulse) + StopTimeAtPulse(pulse);
        }

        public double BPMAtPulse(ulong pulse)
        {
            return BPM.Where(x => x.Key >= pulse).First().Value;
        }

        public double BPMAtTime(double time)
        {
            return timeBPM.Where(k => k.Key < time).Last().Value;
        }

        public double BeatAtTime(double time)
        {
            double beat = 0;
            var lst = BPS.Where(x => x.Key < time);
            for (int i = 0; i < lst.Count(); i++)
            {
                var cur = lst.ElementAt(i);
                if (i + 1 < lst.Count())
                {
                    var next = lst.ElementAt(i + 1);
                    beat += (next.Key - cur.Key) * cur.Value;
                } else
                {
                    beat += (time - cur.Key) * cur.Value;
                }
            }

            return beat;
        }

        internal double SnappedTimeToPulse(double time)
        {
            return TimeAtPulse(PulseAtTimeRound(time));
        }

        public double PulseAtTime(double time)
        {
            return BeatAtTime(time) * resolution;
        }

        public ulong PulseAtTimeRound(double time)
        {
            return (ulong)Math.Round(PulseAtTime(time));
        }

        private void AddBPS(KeyValuePair<ulong, double> cur, IEnumerable<KeyValuePair<ulong, ulong>> intermediatestops)
        {
            var bps = cur.Value / 60.0;
            if (intermediatestops.Count() > 0)
            {
                bool skip = false;
                if (intermediatestops.ElementAt(0).Key == cur.Key)
                {
                    double t = TimeAtPulse(cur.Key);
                    double stoplen = intermediatestops.ElementAt(0).Value / (double)resolution * bps;
                    BPS.Add(t, 0);
                    BPS.Add(t + stoplen, bps);
                    skip = true;
                }
                else
                {
                    BPS.Add(TimeAtPulse(cur.Key), bps);
                }

                foreach (var stop in (skip ? intermediatestops.Skip(1) : intermediatestops))
                {
                    double t = TimeAtPulse(cur.Key);
                    double stoplen = stop.Value / (double)resolution * bps;
                    BPS.Add(t, 0);
                    BPS.Add(t + stoplen, bps);
                }
            }
            else
            {
                BPS.Add(TimeAtPulse(cur.Key), bps);
            }
        }

        private void RecalculateTimeChanges()
        {
            foreach (var bpm in BPM)
            {
                timeBPM.Add(TimeAtPulse(bpm.Key), bpm.Value);
            }

            foreach (var stop in Stops)
            {
                timeStops.Add(TimeAtPulse(stop.Key), stop.Value / (double)resolution * SPBAtPulse(stop.Key));
            }

            for (int i = 0; i < BPM.Count(); i++)
            {
                var cur = BPM.ElementAt(i);
                if (i + 1 < BPM.Count())
                {
                    var next = BPM.ElementAt(i + 1);
                    var intermediatestops = Stops
                        .Where(x => x.Key >= cur.Key && x.Key < next.Key);

                    AddBPS(cur, intermediatestops);
                } else
                {
                    var intermediatestops = Stops
                       .Where(x => x.Key >= cur.Key);

                    AddBPS(cur, intermediatestops);
                }
            }
        }

        public BmsonTimingData(JObject root)
        {
            Logger.Log("Adding BPM and STOP events");

            if (root["info"] == null || root["info"]["init_bpm"] == null)
            {
                throw new InvalidOperationException("Timing data requires an init_bpm to be defined on the bmson file.");
            }

            BPM.Add(0, (double)root["info"]["init_bpm"]);

            var res = root["info"]["resolution"];
            if (res != null)
                resolution = (ulong)res;

            if (root["bpm_events"] != null)
            {
                foreach (JObject bpm in root["bpm_events"])
                {
                    BPM.Add((ulong)bpm["y"], (double)bpm["bpm"]);
                }
            } else
            {
                Logger.Log("INFO: bpm_events is null or undefined.");
            }

            if (root["stop_events"] != null)
            {
                foreach (JObject stop in root["stop_events"])
                {
                    Stops.Add((ulong)stop["y"], (ulong)stop["duration"]);
                }
            } else
            {
                Logger.Log("INFO: stop_events is null or undefined.");
            }

            Logger.Log("Calculating SPB and effective BPM/STOP times");
            RecalculateTimeChanges();
        }
    }
}
