using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A7
{
    public class MaximumGold : Processor
    {
        public MaximumGold(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long>)Solve);

        public long Solve(long W, long[] goldBars)
        {
            goldBars = goldBars.OrderBy(x => x).ToArray();
            long[,] table = new long[goldBars.Length + 1, W + 1];

            for (int i = 0; i <= W; i++)
            {
                table[0, i] = 0;
            }
            for (int i = 0; i <= goldBars.Length; i++)
            {
                table[i, 0] = 0;
            }
            for (int i = 1; i <= goldBars.Length; i++)
            {
                for (int j = 1; j <= W; j++)
                {
                    if (j < goldBars[i - 1])
                        table[i, j] = table[i - 1, j];
                    else
                        table[i, j] = Math.Max(table[i - 1, j - goldBars[i - 1]] + goldBars[i - 1], table[i - 1, j]);
                }
            }
            
            return table[goldBars.Length,W];
        }
    }
}
