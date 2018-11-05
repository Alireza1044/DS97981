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
            Counter(0, WordCounts.Length - 1, ref count, word);
            if (count > 0)
                return true;
            else
                return false;
        }

        public void Counter(int low,int high,ref ulong count,string word)
        {
            if(low == high)
            {
                if (string.Compare((WordCounts[high].Word), word) < 0)
                {
                    count = WordCounts[high].Count;
                }
            }

            if(low < high)
            {
                int mid = (low + high) / 2;
                if(string.Compare((WordCounts[mid].Word),word) < 0)
                {
                    Counter(mid+1, high, ref count, word);
                }
                else if(string.Compare((WordCounts[mid].Word), word) > 0)
                {
                    Counter( low, mid, ref count, word);
                }
                else if(string.Compare((WordCounts[mid].Word), word) == 0)
                {
                    count = WordCounts[mid].Count;
                }
            } else if(low > high)
            {
                count = 0;
            }
        }
    }
}
