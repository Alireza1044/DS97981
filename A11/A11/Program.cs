using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A11
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BinaryTreeTraversals b = new BinaryTreeTraversals("a1");
            List<long[]> node = new List<long[]>();
            List<long> z = new List<long>();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    z.Add(long.Parse(Console.ReadLine()));
                }
                node.Add(z.ToArray());
                z = new List<long>();
            }
            long[][] s = b.Solve(node.ToArray());
            for (int i = 0; i < 1; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Console.Write(s[i][j]);
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }
    }
}
