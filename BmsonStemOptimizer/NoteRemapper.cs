using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace BmsonStemOptimizer
{
    struct Note
    {
        public ulong x;
        public ulong y;
        public bool c;
        public ulong l; // will go unchanged.
        public Note(ulong x, ulong y, bool c, ulong l)
        {
            this.x = x;
            this.y = y;
            this.c = c;
            this.l = l;
        }
    }

    

    class SoundChannel
    {
        public string name;
        public Note[] notes;

        public SoundChannel(string name, Note[] notes)
        {
            this.name = name;
            this.notes = notes;
        }
    }

    class NoteRemapper
    {
        class RestartChannelEvent {
            public SoundChannel channel;
            public ulong RestartPulse;
            public RestartChannelEvent(SoundChannel ch, ulong pulse)
            {
                channel = ch;
                RestartPulse = pulse;
            }
        }

        private List<SoundChannel> StemNotes;
        private Dictionary<string, StemTimeMap[]> OptimizedStems;

        private List<SoundChannel> NewStemNotes;
        
        
        private Dictionary<string, bool> MappedOptimizedStems;
        private BmsonTimingData timing;
        private JObject bmsonOutput;

        private StemTimeMap GetNewStemForNote(string oldstem, Note note, ulong stemoffset)
        {
            double t = timing.TimeAtPulse(note.y) - timing.TimeAtPulse(stemoffset);

            var candidates = OptimizedStems[oldstem];

            foreach (var interval in candidates)
            {
                if (interval.InRange(t))
                {
                    return interval;
                }
            }

            return null;
        }

        private void CopyNotesToNewStems()
        {
            NewStemNotes = new List<SoundChannel>();
            // Assuming that... bmson stems are only used once, and never restarted!
            var OptimizedStemHasStart = new Dictionary<string, bool>();
            var OptimizedStemReferences = new Dictionary<string, bool>();
            var OldStemOffset = new Dictionary<string, ulong>();

            foreach (var channel in StemNotes)
            {
                var file = channel.name;
                var NewStems = new Dictionary<string, List<Note>>();
                int ignoredNotes = 0;

                ulong stemPulseOffset = 0;
                foreach (var note in channel.notes)
                {
                    if (note.c == false)
                    {
                        stemPulseOffset = note.y;
                        OldStemOffset[channel.name] = stemPulseOffset;
                        break;
                    }
                }

                foreach (var note in channel.notes)
                {
                    // get remapped stem from old stem
                    StemTimeMap optimizedstem = GetNewStemForNote(file, note, stemPulseOffset);

                    if (optimizedstem != null)
                    {
                        // if the list doesn't exist for the remapped stem, create it
                        if (!NewStems.ContainsKey(optimizedstem.file))
                        {
                            NewStems[optimizedstem.file] = new List<Note>();
                        }

                        var list = NewStems[optimizedstem.file];

                        // add note to the new stem

                        ulong pulse = (timing.PulseAtTimeRound(optimizedstem.originalStart) + stemPulseOffset);
                        bool ContinuationFlag = pulse != note.y;

                        if (!ContinuationFlag)
                            OptimizedStemHasStart[optimizedstem.file] = true;

                        OptimizedStemReferences[optimizedstem.file] = true;

                        list.Add(new Note(note.x, note.y, ContinuationFlag, note.l));
                    }
                    else
                        ignoredNotes++;
                }

                foreach (var stem in NewStems) {
                    // Add BGM start note if neccesary...

                    if (!OptimizedStemHasStart.ContainsKey(stem.Key))
                    {
                        var opstem = OptimizedStems[file].Where(x => x.file == stem.Key).First();
                        ulong pulse = timing.PulseAtTimeRound(opstem.originalStart) + stemPulseOffset;
                        Logger.Log("Adding new BGM start at {0} for stem {1}", pulse, stem.Key);
                        stem.Value.Add(new Note(0, pulse, false, 0));
                    }

                    NewStemNotes.Add(new SoundChannel(stem.Key, stem.Value.OrderBy(x => x.y).ToArray()));
                }

                if (ignoredNotes > 0)
                    Logger.Log("Sound channel {0} had {1} notes ignored as they mapped to silence.", 
                        file, ignoredNotes);
            }

            // By this point, there are, or might be, loose stem parts that map to no note.
            foreach (var opstems in OptimizedStems)
            {
                ulong offset = OldStemOffset[opstems.Key];
                foreach (var stem in opstems.Value)
                {
                    if (!OptimizedStemReferences.ContainsKey(stem.file))
                    {
                        ulong pulse = timing.PulseAtTimeRound(stem.originalStart) + offset;
                        var notelist = new Note[1];
                        notelist[0] = new Note(0, pulse, false, 0);

                        Logger.Log("Adding unreferenced stem {0} at {1}", stem.file, pulse);
                        NewStemNotes.Add(new SoundChannel(stem.file, notelist));
                    }
                }
            }

            // OK, now we're really done.
        }

        private void RegenerateJsonNotes()
        {
            JArray jchannelarray = new JArray();
            foreach (SoundChannel sc in NewStemNotes)
            {
                JArray jnotesarray = new JArray();
                foreach (var note in sc.notes)
                {
                    JObject jnote = new JObject();
                    jnote.Add("x", note.x);
                    jnote.Add("y", note.y);
                    jnote.Add("c", note.c);
                    if (note.l > 0)
                        jnote.Add("l", note.l);
                    jnotesarray.Add(jnote);
                }

                JObject channel = new JObject();
                channel.Add("name", sc.name);
                channel.Add("notes", jnotesarray);
                jchannelarray.Add(channel);
            }

            bmsonOutput["sound_channels"] = jchannelarray;
        }

        private void LoadNotes(JObject root)
        {
            StemNotes = new List<SoundChannel>();
            var soundchannels = root["sound_channels"];
            if (soundchannels == null)
            {
                Logger.Log("INFO: sound_channels is null. No notes were loaded.");
            }

            foreach (var sc in soundchannels)
            {
                string stem = (string)sc["name"];

                var notes = new List<Note>();
                foreach (var note in sc["notes"])
                {
                    notes.Add(new Note(
                        note["x"] != null ? (ulong)note["x"] : 0,
                        note["y"] != null ? (ulong)note["y"] : 0,
                        note["c"] != null ? (bool)note["c"] : true,
                        note["l"] != null ? (ulong)note["l"] : 0));
                }

                StemNotes.Add(new SoundChannel(stem, notes.ToArray()));
            }
        }

        public string GetRemappedJSON()
        {
            CopyNotesToNewStems();
            RegenerateJsonNotes();
            return bmsonOutput.ToString();
        }

        public NoteRemapper(JObject root, Dictionary<string, StemTimeMap[]> timemap)
        {
            bmsonOutput = (JObject)root.DeepClone();
            timing = new BmsonTimingData(root);
            OptimizedStems = timemap;

            LoadNotes(root);
        }
    }
}
