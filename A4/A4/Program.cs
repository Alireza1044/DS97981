using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A4
{
    public class Program
    {
        public static void Main(string[] args)
        {
        }

        public static string ProcessChangingMoney1(string inStr) => TestTools.Process(inStr, (Func<long, long>)ChangingMoney1);

        public static string ProcessMaximizingLoot2(string inStr) => TestTools.Process(inStr, (Func<long, long[], long[], long>)MaximizingLoot2);

        public static string ProcessMaximizingOnlineAdRevenue3(string inStr) => TestTools.Process(inStr, (Func<long, long[], long[], long>)MaximizingOnlineAdRevenue3);

        public static string ProcessCollectingSignatures4(string inStr) => TestTools.Process(inStr, (Func<long, long[], long[], long>)CollectingSignatures4);

        public static string ProcessMaximizeNumberOfPrizePlaces5(string inStr) => TestTools.Process(inStr, (Func<long, long[]>)MaximizeNumberOfPrizePlaces5);

        public static string ProcessMaximizeSalary6(string inStr) => TestTools.Process(inStr, MaximizeSalary6);

        public static long ChangingMoney1(long money)
        {
            long largeCoin = money / 10;
            long mediumCoin = (money % 10) / 5;
            long smallCoin = money % 5;
            return (largeCoin + mediumCoin + smallCoin);
        }

        public static long MaximizingLoot2(long capacity, long[] weights, long[] values)
        {
            double result = 0;
            var infos = values.Zip(weights, (v, w) => new
            {
                value = v,
                weight = w,
                density = v / (double)w,
            }).OrderByDescending(x => x.density).
            ToList();
            
            foreach (var info in infos)
            {
                if (info.weight <= capacity)
                {
                    result += info.value; ;
                    capacity -= info.weight;
                }
                else if (info.weight > capacity)
                {
                    result += (capacity * info.density);
                    break;
                }
            }
            return (long)result;
        }

        public static long MaximizingOnlineAdRevenue3(long slotCount, long[] adRevenue, long[] averageDailyClick)
        => adRevenue.OrderByDescending(x => x).
            Zip(averageDailyClick.OrderByDescending(x => x), (revenue, click) => revenue * click).
            Sum();        

        public static long CollectingSignatures4(long tenantCount, long[] startTimes, long[] endTimes)
        {
            var ranges = startTimes.Zip(endTimes, (s, e) => new
            {
                start = s,
                end = e,
                range = e - s,
                isListed = false,
            }).OrderBy(x => x.range).ToList();

            for (int i = 0; i < ranges.Count; i++)
            {
                for (int j = i + 1; j < ranges.Count; j++)
                {
                    if (ranges[i].start >= ranges[j].start && ranges[i].end <= ranges[j].end)
                    {
                        ranges.RemoveAt(j);
                    }
                }
            }
            ranges = ranges.OrderBy(x => x.start).ToList();
            Dictionary<long, List<dynamic>> groups = new Dictionary<long, List<dynamic>>();
            for (int i = 0; i < ranges.Count; i++)
            {
                List<dynamic> tempGroup = new List<dynamic>();
                for (int j = i; j < ranges.Count; j++)
                {
                    if (ranges[j].isListed == true)
                        break;

                    if (((ranges[j].start >= ranges[i].start && ranges[j].start <= ranges[i].end)
                        || (ranges[j].start >= ranges[i].start && ranges[j].end <= ranges[i].end))
                        && (ranges[j].isListed != true))
                    {
                        tempGroup.Add(ranges[j]);
                        ranges[j] = new { ranges[j].start, ranges[j].end, ranges[j].range, isListed = true };
                    }
                }
                if (tempGroup.Count != 0)
                    groups.Add(i, tempGroup);
            }
            return groups.Keys.Count();
        }

        public static long[] MaximizeNumberOfPrizePlaces5(long n)
        {
            List<long> numbers = new List<long>();
            for (int i = 1; n >= 0; i++)
            {
                if (n - i >= 0)
                {
                    n -= i;
                    numbers.Add(i);
                }
                else
                {
                    numbers[i - 2] += n;
                    break;
                }
            }
            return numbers.ToArray();
        }

        public static string MaximizeSalary6(long n, long[] numbers)
        {
            string result = null;
            for (int i = 0; i < numbers.Length; i++)
            {
                for (int j = i + 1; j < numbers.Length; j++)
                {
                    if (long.Parse(numbers[j].ToString() + numbers[i].ToString()) >=
                            long.Parse(numbers[i].ToString() + numbers[j].ToString()))
                    {
                        long temp = numbers[i];
                        numbers[i] = numbers[j];
                        numbers[j] = temp;
                    }
                }
            }
            for (int i = 0; i < numbers.Length; i++)
            {
                result += numbers[i].ToString();
            }
            return result;
        }
    }
}