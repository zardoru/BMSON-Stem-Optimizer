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



    struct SoundChannel
    {
        public string name;
        public Note[] notes;

        public SoundChannel(string name, Note[] notes)
        {
            this.name = name;
            this.notes = notes;
        }

        public SoundChannel Clone()
        {
            var newnotes = notes.Clone();
            var output = new SoundChannel(name, notes);

            return output;
        }
    }

    class BmsonNoteLoader
    {
        private List<SoundChannel> StemNotes;

        // Clones the internal parsed data!
        public List<SoundChannel> GetNotes()
        {
            List<SoundChannel> Output = new List<SoundChannel>(StemNotes);
            return Output;
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

        public BmsonNoteLoader(JObject root)
        {
            LoadNotes(root);
        }
    }
}
