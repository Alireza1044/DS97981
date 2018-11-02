using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A6
{
    public class LCSOfThree: Processor
    {
        public LCSOfThree(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long[], long[], long[], long>)Solve);

        public long Solve(long[] seq1, long[] seq2, long[] seq3)
        {
            int[,,] table = new int[seq1.Length + 1, seq2.Length + 1, seq3.Length + 1];

            for (int i = 0; i <= seq1.Length; i++)
            {
                table[i, 0, 0] = 0;
            }
            for (int i = 0; i <= seq2.Length; i++)
            {
                table[0, i, 0] = 0;
            }
            for (int i = 0; i <= seq3.Length; i++)
            {
                table[0, 0, i] = 0;
            }

            for (int i = 1; i < table.GetLength(0); i++)
            {
                for (int j = 1; j < table.GetLength(1); j++)
                {
                    for (int k = 1; k < table.GetLength(2); k++)
                    {
                        if (seq1[i-1] == seq2[j-1] && seq2[j-1] == seq3[k-1])
                        {
                            table[i, j, k] = table[i - 1, j - 1, k - 1] + 1;
                        }
                        else
                        {
                            table[i, j, k] = Max(table[i - 1, j, k], table[i, j - 1, k], table[i, j, k - 1]);
                        }
                    }
                }
            }

            return table[seq1.Length, seq2.Length, seq3.Length];
        }

        private int Max(int v1, int v2,int v3)
        {
            List<int> arr = new List<int> { v1, v2 ,v3};
            return arr.Max();
        }
    }
}
