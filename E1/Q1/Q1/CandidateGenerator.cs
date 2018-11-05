using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    public static class CandidateGenerator
    {
        public static readonly char[] Alphabet =
            Enumerable.Range('a', 'z' - 'a' + 1)
                      .Select(c => (char)c)
                      .ToArray();

        public static string[] GetCandidates(string word)
        {
            List<string> candidates = new List<string>();
            //TODO
            for (int i = 0; i < Alphabet.Length; i++)
            {
                for (int j = 0; j <= word.Length; j++)
                {
                    candidates.Add((Insert(word, j, Alphabet[i])));
                }
            }

            for (int i = 0; i < word.Length; i++)
            {
                candidates.Add(Delete(word, i));
            }

            for (int i = 0; i < Alphabet.Length; i++)
            {
                for (int j = 0; j <= word.Length; j++)
                {
                    candidates.Add((Substitute(word, j, Alphabet[i])));
                }
            }

            return candidates.ToArray();
        }

        private static string Insert(string word, int pos, char c)
        {
            char[] wordChars = word.ToCharArray();
            char[] newWord = new char[wordChars.Length + 1];
            //TODO
            for (int i = 0; i <= wordChars.Length; i++)
            {
                if(i == pos)
                {
                    newWord[i] = c;
                }
                else if(i > pos)
                {
                    newWord[i] = wordChars[i-1];
                }
                else
                {
                    newWord[i] = wordChars[i];
                }
            }
            return new string(newWord);
        }

        private static string Delete(string word, int pos)
        {
            char[] wordChars = word.ToCharArray();
            char[] newWord = new char[wordChars.Length - 1];
            //TODO
            for (int i = 0; i < wordChars.Length; i++)
            {
                if (i == pos)
                    continue;
                else if (i < pos) 
                {
                    newWord[i] = wordChars[i];
                }
                else
                {
                    newWord[i - 1] = wordChars[i];
                }
            }

            return new string(newWord);
        }

        private static string Substitute(string word, int pos, char c)
        {
            char[] wordChars = word.ToCharArray();
            char[] newWord = new char[wordChars.Length];
            //TODO

            for (int i = 0; i < wordChars.Length; i++)
            {
                if (i == pos)
                    newWord[i] = c;
                else
                {
                    newWord[i] = wordChars[i];
                }
            }

            return new string(newWord);
        }

        //public static int EditDistance(string str1 , string str2 ,ref int count)
        //{
        //    int[,] table = new int[str1.Length + 1, str2.Length + 1];

        //    for (int i = 0; i < table.GetLength(0); i++)
        //    {
        //        table[i, 0] = i;
        //    }
        //    for (int i = 0; i < table.GetLength(1); i++)
        //    {
        //        table[0, i] = i;
        //    }

        //    char[] chr1 = str1.ToCharArray();
        //    char[] chr2 = str2.ToCharArray();

        //    for (int i = 1; i < table.GetLength(0); i++)
        //    {
        //        for (int j = 1; j < table.GetLength(1); j++)
        //        {
        //            if (chr1[i] == chr2[j])
        //                table[i, j] = table[i - 1, j - 1];
        //            else
        //            {
        //                table[i, j] = Min(table[i - 1, j], table[i, j - 1]);
        //            }
        //        }
        //    }
        //    return table[str1.Length, str2.Length];
        //}

        //private static int Min(int v1, int v2)
        //{
        //    List<int> arr = new List<int> { v1, v2 };
        //    return arr.Min();
        //}
    }
}
