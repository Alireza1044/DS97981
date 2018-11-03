using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    public class Program
    {
        private static Dictionary<int, char[]> D =
            new Dictionary<int, char[]>
            {
                [0] = new char[] { '+' },
                [1] = new char[] { '_', ',', '@' },
                [2] = new char[] { 'A', 'B', 'C' },
                [3] = new char[] { 'D', 'E', 'F' },
                [4] = new char[] { 'G', 'H', 'I' },
                [5] = new char[] { 'J', 'K', 'L' },
                [6] = new char[] { 'M', 'N', 'O' },
                [7] = new char[] { 'P', 'Q', 'R', 'S' },
                [8] = new char[] { 'T', 'U', 'V' },
                [9] = new char[] { 'W', 'X', 'Y', 'Z' },
            };


        public static string[] GetNames(int[] phone,int low,int high)
        {
            List<string> result = new List<string>();
            if (low == high)
            {
                foreach (var item in D[phone[low]])
                {
                    result.Add(item.ToString());
                }
                return result.ToArray();
            }
            
                int mid = (low + high) / 2;

                var leftside = GetNames(phone, low, mid);
                var rightside = GetNames(phone, mid + 1, high);

                for (int i = 0; i < leftside.Length; i++)
                {
                    for (int j = 0; j < rightside.Length; j++)
                    {
                        result.Add(leftside[i].ToString() + rightside[j].ToString());
                    }
                }
            return result.ToArray();
        }


        static void Main(string[] args)
        {
            int[] phoneNumber = new int[] {0, 9, 1, 2, 2, 2, 4, 2, 5, 2, 5 };

            GetNames(phoneNumber, 0, phoneNumber.Length);
            Console.WriteLine();
            Console.ReadKey();
        }


    }
}
