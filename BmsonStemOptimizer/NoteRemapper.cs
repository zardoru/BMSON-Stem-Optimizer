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

    class RemappedNote
    {
        public string newStem;
        public double startTime; // start time, relative to stem
        public Note oldNote;
    }

    class NoteRemapper
    {
        private Dictionary<string, Note[]> StemNotes;
        private Dictionary<string, StemTimeMap[]> StemMap;
        private BmsonTimingData timing;

        private void MoveNotesToNewStems()
        {
            foreach (var stem in StemNotes)
            {
                var file = stem.Key;
                var notes = stem.Value;
                foreach (var note in notes)
                {

                }
            }
        }

        public NoteRemapper(JObject root, Dictionary<string, StemTimeMap[]> timemap)
        {
            timing = new BmsonTimingData(root);
        }
    }
}
