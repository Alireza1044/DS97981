using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    public class SlowLM
    {
        public readonly WordCount[] WordCounts;

        public SlowLM(WordCount[] wordCounts)
        {
            this.WordCounts = wordCounts;
        }

        public bool GetCount(string word, out ulong count)
        {
            count = 0;
            for (int i = 0; i < WordCounts.Length; i++)
            {
                if (string.Compare(WordCounts[i].Word, word) == 0)
                {
                    count = WordCounts[i].Count;
                    return true;
                }
            }
            return false;
        }
    }
}
