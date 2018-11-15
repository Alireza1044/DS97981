using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A7
{
    public class MaximizingArithmeticExpression : Processor
    {
        public MaximizingArithmeticExpression(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<string, long>)Solve);

        public long Solve(string expression)
        {
            //Write your code here
            string[] nums = expression.Split('+','-','*');
            List<char> ops = expression.ToCharArray().ToList();
            for (int i = 0; i < ops.Count; i++)
            {
                if (ops[i] != '*' || ops[i] != '+' || ops[i] != '-')
                    ops.RemoveAt(i);
            }
            List<string> expArr = new List<string>();
            for (int i = 0; i < nums.Length; i++)
            {
                expArr.Add(nums[i]);
                if(i < ops.Count)
                    expArr.Add(ops[i].ToString());
            }
            string[] exp = expArr.ToArray();

            int[,] minTable = new int[ nums.Length, nums.Length];
            int[,] maxTable = new int[nums.Length, nums.Length];

            for (int i = 0; i < minTable.GetLength(0); i++)
            {
                minTable[i, i] = int.Parse(exp[2 * i]);
                maxTable[i, i] = int.Parse(exp[2 * i]);
                if (i >= 1)
                {
                    minTable[i - 1, i] = Counter(exp[2 * i-2], exp[2 * i - 1], exp[2 * i ]);//khune balaii
                    maxTable[i - 1, i] = Counter(exp[2 * i-2], exp[2 * i - 1], exp[2 * i ]);
                }
            }
            for (int q = 2; q < minTable.GetLength(0); q++)
            {
                for (int j = q, i = 0; j < minTable.GetLength(0); i++, j++)
                {
                    int min = int.MaxValue;
                    int max = int.MinValue;
                    for (int k = j-1,z=j; k >= i; k--,z--)
                    {
                        int a = Counter(minTable[i, k].ToString(), exp[2 * k + 1], minTable[z, j].ToString());
                        int b = Counter(minTable[i, k].ToString(), exp[2 * k + 1], maxTable[z, j].ToString());
                        int c = Counter(maxTable[i, k].ToString(), exp[2 * k + 1], minTable[z, j].ToString());
                        int d = Counter(maxTable[i, k].ToString(), exp[2 * k + 1], maxTable[z, j].ToString());
                        int temp= new[]
                        {
                            a,b,c,d,
                        }.Min();
                        if (temp < min)
                            min = temp;

                        temp= new[]
                        {
                            a,b,c,d,
                        }.Max();
                        if (temp > max)
                            max = temp;
                    }
                    minTable[i, j] = min;
                    maxTable[i, j] = max;
                }
            }

            return maxTable[0, maxTable.GetLength(0) - 1];
        }

        public int Counter(string f,string op,string s)
        {
            int a = int.Parse(f);
            int b = int.Parse(s);

            switch (op)
            {
                case "*":
                    return a * b;
                case "-":
                    return a - b;
                case "+":
                    return a + b;
                default:
                    return 0;
            }
        }
    }
}
