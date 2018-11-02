using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A6
{
    public class EditDistance: Processor
    {
        public EditDistance(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<string, string, long>)Solve);

        public long Solve(string str1, string str2)
        {
            str1 = " " + str1;
            str2 = " " + str2;
            char[] firstString = str1.ToCharArray();
            char[] secondstring = str2.ToCharArray();

            int[,] table = new int[firstString.Length, secondstring.Length];

            for (int i = 0; i < firstString.Length; i++)
            {
                for (int j = 0; j < secondstring.Length; j++)
                {
                    table[i, j] = int.MaxValue;
                }
            }

            for (int i = 0; i < firstString.Length; i++)
            {
                table[i,0] = i;
            }
            for (int i = 0; i < secondstring.Length; i++)
            {
                table[0, i] = i;
            }

            for (int i = 1; i < firstString.Length; i++)
            {
                for (int j = 1; j < secondstring.Length; j++)
                {
                    if (firstString[i] == secondstring[j])
                        table[i, j] = table[i - 1, j - 1];
                    else
                    {
                        table[i, j] = Min(table[i - 1, j], table[i, j - 1], table[i - 1, j - 1]) + 1;
                    }
                }
            }

            return table[firstString.Length-1,secondstring.Length-1];
        }

        private int Min(int v1, int v2, int v3)
        {
            List<int> arr = new List<int> { v1, v2, v3 };
            return arr.Min();
        }
    }
}
