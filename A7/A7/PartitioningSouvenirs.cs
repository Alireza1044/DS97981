using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A7
{
    public class PartitioningSouvenirs : Processor
    {
        public PartitioningSouvenirs(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long>)Solve);

        public long Solve(long souvenirsCount, long[] souvenirs)
        {
            //Write your code here
            bool firstIsTrue = false;
            bool secondIsTrue = false;
            if (souvenirs.Sum() % 3 != 0)
                return 0;
            long sum = souvenirs.Sum() / 3;

            souvenirs = souvenirs.OrderByDescending(x => x).ToArray();
            long[,] table = new long[souvenirs.Length + 1, 2 * sum + 1];

            for (int i = 0; i < table.GetLength(1); i++)
            {
                table[0, i] = 0;
            }
            for (int i = 0; i < table.GetLength(0); i++)
            {
                table[i, 0] = 0;
            }
            for (int i = 1; i < table.GetLength(0); i++)
            {
                for (int j = 1; j < table.GetLength(1); j++)
                {
                    if (j < souvenirs[i - 1])
                        table[i, j] = table[i - 1, j];
                    else
                        table[i, j] = Math.Max(table[i - 1, j - souvenirs[i - 1]] + souvenirs[i - 1], table[i - 1, j]);
                    if (j == sum && table[i, j] == sum)
                        firstIsTrue = true;
                    if (j == 2 * sum && table[i, j] == 2 * sum)
                        secondIsTrue = true;
                }
            }

            if (firstIsTrue && secondIsTrue)
                return 1;
            else
                return 0;
        }
    }
}
