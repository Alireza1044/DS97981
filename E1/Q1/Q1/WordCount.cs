using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    public class WordCount
    {        
        public readonly string Word;
        public readonly ulong Count;

        public WordCount(string word, ulong count)
        {
            Word = word;
            this.Count = count;
        }
    }
}
