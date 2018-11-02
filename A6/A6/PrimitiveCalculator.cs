using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A6
{
    public class PrimitiveCalculator : Processor
    {
        public PrimitiveCalculator(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[]>)Solve);

        public long[] Solve(long n)
        {
            int[] ops = new int[] { 1, 2, 3 };
            long[] total = new long[n + 1];
            total[0] = 0;
            total[1] = 1;

            for (int i = 2; i < total.Length; i++)
            {
                total[i] = long.MaxValue;
            }

            for (int i = 1; i < total.Length; i++)
            {

                if (i % 3 == 0 && total[i / 3] + 1 < total[i])
                {
                    total[i] = total[i / 3] + 1;
                }
                if (i % 2 == 0 && total[i / 2] + 1 < total[i])
                {
                    total[i] = total[i / 2] + 1;
                }
                if(total[i-1] +1 < total[i])
                {
                    total[i] = total[i - 1] + 1;
                }
            }

            long[] result = new long[(int)total[n]];
            int divider = 0;
            int index = result.Length - 2;
            result[result.Length - 1] = n;

            for (int i = (int)n; i > 0 && index>=0;index--)
            {
                long temp = long.MaxValue;
                if(i % 3 == 0)
                {
                    if (total[i / 3] < temp)
                    {
                        temp = total[i / 3];
                        divider = 3;
                        result[index] = i / 3;
                    }
                }
                if(i % 2 == 0)
                {
                    if (total[i / 2] < temp)
                    {
                        temp = total[i / 2];
                        divider = 2;
                        result[index] = i / 2;
                    }
                }
                if (total[i - 1] < temp)
                {
                    temp = total[i - 1];
                    divider = 1;
                    result[index] = i - 1;
                }
                switch (divider)
                {
                    case 3:
                        {
                            i /= 3;
                            break;
                        }
                    case 2:
                        {
                            i /= 2;
                            break;
                        }
                    case 1:
                        {
                            i--;
                            break;
                        }
                }
            }
            result.Reverse();

            return result.ToArray();
        }
    }
}
