using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3
{
    public class Program
    {
        public static void Main(string[] args)
        {
        }

        public static string Process(string inStr, Func<long, long> longProcessor)
        {
            long n = long.Parse(inStr);
            return longProcessor(n).ToString();
        }

        public static string Process(string inStr, Func<long, long, long> longProcessor)
        {
            var toks = inStr.Split(new char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            long a = long.Parse(toks[0]);
            long b = long.Parse(toks[1]);
            return longProcessor(a, b).ToString();
        }

        public static string ProcessFibonacci_LastDigit(string inStr) => Process(inStr, Fibonacci_LastDigit);

        public static string ProcessGCD(string inStr) => Process(inStr, GCD);

        public static string ProcessLCM(string inStr) => Process(inStr, LCM);

        public static string ProcessFibonacci_Mod(string inStr) => Process(inStr, Fibonacci_Mod);

        public static string ProcessFibonacci_Sum(string inStr) => Process(inStr, Fibonacci_Sum);

        public static string ProcessFibonacci_Partial_Sum(string inStr) => Process(inStr, Fibonacci_Partial_Sum);

        public static string ProcessFibonacci_Sum_Squares(string inStr) => Process(inStr, Fibonacci_Sum_Squares);

        public static long Fibonacci(long n)
        {
            long[] fibNum = new long[] { 1, 1 };
            long temp = 0;
            if (n == 0)
            {
                return 0;
            }
            else if (n <= 2)
            {
                return 1;
            }
            else
            {
                for (int i = 0; i < n - 2; i++)
                {
                    temp = fibNum[1];
                    fibNum[1] = fibNum[0] + fibNum[1];
                    fibNum[0] = temp;
                }
            }
            return fibNum[1];
        }

        public static long Fibonacci_LastDigit(long n)
        {
            long radix = 10;
            List<long> mods = new List<long>();
            mods.Add(0);
            mods.Add(1);
            long element1 = 0;
            long element2 = 1;
            for (int i = 2; i <= n; i++)
            {
                long temp = element2 % radix;
                element2 = (element1 % radix + temp) % radix;
                element1 = temp;
                mods.Add(element2);
                if (mods[i - 1] == 0 && mods[i] == 1)
                {
                    mods.RemoveAt(i);
                    mods.RemoveAt(i - 1);
                    break;
                }
            }
            return mods[(int)(n % (mods.Count))];
        }

        public static string ProcessFibonacci(string inStr) => Process(inStr, Fibonacci);

        public static long GCD(long smallNum, long bigNum)
        {
            if (smallNum == 0)
            {
                return bigNum;
            }
            return GCD(bigNum % smallNum, smallNum);
        }

        public static long LCM(long smallNum, long bigNum)
        {
            return (smallNum * bigNum) / GCD(smallNum, bigNum);
        }

        public static long Fibonacci_Mod(long n, long m)
        {
            List<long> mods = new List<long>();
            mods.Add(0);
            mods.Add(1);
            long element1 = 0;
            long element2 = 1;
            for (int i = 2; i <= n; i++)
            {
                long temp = element2 % m;
                element2 = (element1 % m + temp) % m;
                element1 = temp;
                mods.Add(element2);
                if (mods[i - 1] == 0 && mods[i] == 1)
                {
                    mods.RemoveAt(i);
                    mods.RemoveAt(i - 1);
                    break;
                }
            }
            return mods[(int)(n % (mods.Count))];
        }

        public static long Fibonacci_Sum(long n)
        {
            List<long> mods = new List<long>();
            int radix = 10;
            mods.Add(0);
            mods.Add(1);
            long element1 = 0;
            long element2 = 1;
            for (int i = 2; i <= n; i++)
            {
                long temp = element2 % radix;
                element2 = (element1 % radix + temp) % radix;
                element1 = temp;
                mods.Add(element2);
                if (mods[i - 1] == 0 && mods[i] == 1)
                {
                    mods.RemoveAt(i);
                    mods.RemoveAt(i - 1);
                    break;
                }
            }
            long result = 0;
            long listSum = mods.Sum();
            result += (n / mods.Count) * listSum;
            for (int i = 0; i <= n % mods.Count; i++)
            {
                result += mods[i];
            }
            return result % radix;
        }

        public static long Fibonacci_Partial_Sum(long n, long m)
        {
            if (m > n)
            {
                long temp = m;
                m = n;
                n = temp;
            }
            long result = Fibonacci_Sum(n) - Fibonacci_Sum(m - 1);
            if (result < 0)
            {
                result += 10;
            }
            return result;
        }

        public static long Fibonacci_Sum_Squares(long n)
        {
            long width = Fibonacci_Mod(n,10);
            long length = width + Fibonacci_Mod(n - 1,10);
            return (width * length) % 10;
        }
    }
}
