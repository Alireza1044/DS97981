using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    public class SpellChecker
    {
        public readonly FastLM LanguageModel;

        public SpellChecker(FastLM lm)
        {
            this.LanguageModel = lm;
        }

        public string[] Check(string misspelling)
        {
            List<WordCount> candidates = 
                new List<WordCount>();
            // TODO

            candidates = LanguageModel.WordCounts.Where(x => EditDistance(x.Word, misspelling) <= 1).ToList();

            return candidates
                    .OrderByDescending(x => x.Count)
                    .Select(x => x.Word)
                    .Distinct()
                    .ToArray();
        }

        public string[] SlowCheck(string misspelling)
        {
            List<WordCount> candidates =
                new List<WordCount>();

            // TODO
            for (int i = 0; i < LanguageModel.WordCounts.Length; i++)
            {
                if (EditDistance(LanguageModel.WordCounts[i].Word, misspelling,true) <= 1)
                    candidates.Add(LanguageModel.WordCounts[i]);
            }

            return candidates
                    .OrderByDescending(x => x.Count)
                    .Select(x => x.Word)
                    .Distinct()
                    .ToArray();
        }

        public int EditDistance(string str1, string str2,bool isSlow = false)
        {
            int n = str1.Length;
            int m = str2.Length;

            if (str1 == str2)
                return 0;

            int[,] Distance = new int[n + 1, m + 1];
            for (int i = 0; i < n + 1; i++)
            {
                Distance[i, 0] = i;
            }

            for (int j = 0; j < m + 1; j++)
            {
                Distance[0, j] = j;
            }

            char[] chr1 = str1.ToCharArray();
            char[] chr2 = str2.ToCharArray();

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    // TODO
                    if (chr1[i - 1] == chr2[j - 1])
                        Distance[i, j] = Distance[i - 1, j - 1];
                    else
                    {
                        if (isSlow)
                            Distance[i, j] = Min(Distance[i - 1, j], Distance[i, j - 1], Distance[i - 1, j - 1]) + 1;
                        else
                            Distance[i, j] = Math.Min((Math.Min(Distance[i - 1, j], Distance[i, j - 1])), Distance[i - 1, j - 1]) + 1;
                    }
                }
            }
            return Distance[n, m];
        }

        private int Min(int v1, int v2, int v3)
        {
            List<int> arr = new List<int> { v1, v2, v3 };
            return arr.Min();
        }
    }
}
