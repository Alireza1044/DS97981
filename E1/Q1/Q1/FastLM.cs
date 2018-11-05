using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    public class FastLM
    {
        public readonly WordCount[] WordCounts;


        public FastLM(WordCount[] wordCounts)
        {
            this.WordCounts = wordCounts.OrderBy(wc => wc.Word).ToArray();
        }

        public bool GetCount(string word, out ulong count)
        {
            count = 0;
            //TODO
            return false;
        }
    }
}
