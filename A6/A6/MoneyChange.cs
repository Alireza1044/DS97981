using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A6
{
    public class MoneyChange: Processor
    {
        private static readonly int[] COINS = new int[] {1, 3, 4};

        public MoneyChange(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long>) Solve);

        public long Solve(long n)
        {
            List<int> coins = new List<int> { 1, 3, 4 };

            long[] total = new long[n + 1];
            total[0] = 0;

            for (int i = 1; i <= n; i++)
            {
                total[i] = long.MaxValue;
            }

            foreach(var coin in coins)
            {
                for (int i = 1; i <= n; i++)
                {
                    if (i >= coin)
                    {
                        if (1 + total[i - coin] < total[i])
                        {
                            total[i] = total[i - coin] + 1;
                        }
                    }
                }
            }
            return total[n];
        }
    }
}
