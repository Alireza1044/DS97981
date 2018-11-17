using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A8
{
    public class CheckBrackets : Processor
    {
        public CheckBrackets(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<string, long>)Solve);

        public long Solve(string str)
        {
            Stack<(char, int)> brackets = new Stack<(char, int)>();
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '(' || str[i] == '{' || str[i] == '[')
                    brackets.Push((str[i], i));
                else if (str[i] == ')' || str[i] == '}' || str[i] == ']')
                {
                    if (brackets.Count != 0)
                    {
                        var temp = brackets.Pop();
                        switch (str[i])
                        {
                            case ']':
                                if (temp.Item1 != '[')
                                    return i + 1;
                                break;
                            case '}':
                                if (temp.Item1 != '{')
                                    return i + 1;
                                break;
                            case ')':
                                if (temp.Item1 != '(')
                                    return i + 1;
                                break;
                        }
                    }
                    else
                        return i + 1;
                }
            }
            if (brackets.Count != 0)
                return brackets.Peek().Item2 + 1;
            return -1;
        }
    }
}
