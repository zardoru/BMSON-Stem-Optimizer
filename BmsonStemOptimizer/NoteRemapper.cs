using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace BmsonStemOptimizer
{
   

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

        private Dictionary<string, StemTimeMap[]> OptimizedStems;

        private List<SoundChannel> StemNotes;
        private List<SoundChannel> NewStemNotes;
        
        
        private Dictionary<string, bool> MappedOptimizedStems;
        private BmsonTimingData timing;
        private JObject bmsonOutput;

        private BmsonNoteLoader notedata;

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
            notedata = new BmsonNoteLoader(root);

            StemNotes = notedata.GetNotes();
            OptimizedStems = timemap;
        }
    }
}
