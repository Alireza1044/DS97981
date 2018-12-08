using System;
using System.Collections.Generic;
using TestCommon;

namespace A10
{
    public class RabinKarp : Processor
    {
        public RabinKarp(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<string, string, long[]>)Solve);

        const long d = 256;
        const long q = 1000000007;
        static long patHash=0, txtHash=0, h = 1, M, N;

        public long[] Solve(string pattern, string text)
        {
            List<long> occurrences = new List<long>();
            M = pattern.Length;
            N = text.Length;
            txtHash = 0;
            patHash = 0;
            h = 1;
            for (int i = 0; i < M - 1; i++)
            {
                h = (h * d) % q;
            }

            GetHash(pattern, text);

            for (int i = 0; i <= N - M; i++)
            {
                if (patHash == txtHash)
                {
                    int j = new int();
                    for (j = 0; j < M; j++)
                    {
                        if (text[i + j] != pattern[j])
                        {
                            break;
                        }

                    }
                    if (j == M)
                        occurrences.Add(i);
                }

                if (i < N - M)
                {
                    txtHash = (d * (txtHash - text[i] * h) + text[i + (int)M]) % q;
                }
                if (txtHash < 0)
                    txtHash += q;
            }

            return occurrences.ToArray();
        }

        public static void GetHash(string pattern, string text)
        {
            for (int i = 0; i < M; i++)
            {
                patHash = (d * patHash + pattern[i]) % q;
                txtHash = (d * txtHash + text[i]) % q;
            }
        }

        public static long[] PreComputeHashes(
            string T,
            int P,
            long p,
            long x)
        {
            return null;
        }
    }
}
