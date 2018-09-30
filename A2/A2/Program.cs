using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2
{
    public class Program
    {
        static void Main(string[] args)
        {
        }

        public static int NaiveMaxPairWiseProduct(List<int> numbers)
        {
            int product = 0;
            for (int i = 0; i < numbers.Count(); i++)
            {
                for (int j = i + 1; j < numbers.Count(); j++)
                {
                    if (numbers[i] * numbers[j] > product)
                        product = numbers[i] * numbers[j];
                }
            }
            return product;
        }

        public static int FastMaxPairWiseProduct(List<int> numbers)
        {
            int firstIndex = 0;
            for(int i = 1; i < numbers.Count(); i++)
            {
                if (numbers[i] > numbers[firstIndex])
                    firstIndex = i;
            }
            int secondIndex;
            if (firstIndex == 0)
                secondIndex = 1;
            else
                secondIndex = 0;
            for (int i = 0; i < numbers.Count(); i++)
            {
                if (i != firstIndex && numbers[i] > numbers[secondIndex])
                    secondIndex = i;
            }
            return numbers[firstIndex] * numbers[secondIndex];
        }

        public static string Process(string input)
        {
            var inData = input.Split(new char[] { '\n', '\r', ' ' },
                StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s))
                .ToList();

            return FastMaxPairWiseProduct(inData).ToString();
        }
    }
}
