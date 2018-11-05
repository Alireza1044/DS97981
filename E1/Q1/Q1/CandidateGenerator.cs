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
            return candidates.ToArray();
        }

        private static string Insert(string word, int pos, char c)
        {
            char[] wordChars = word.ToCharArray();
            char[] newWord = new char[wordChars.Length+1];
            //TODO
            return new string(newWord);
        }

        private static string Delete(string word, int pos)
        {
            char[] wordChars = word.ToCharArray();
            char[] newWord = new char[wordChars.Length-1];
            //TODO
            return new string(newWord);
        }

        private static string Substitute(string word, int pos, char c)
        {
            char[] wordChars = word.ToCharArray();
            char[] newWord = new char[wordChars.Length];
            //TODO
            return new string(newWord);
        }

    }
}
