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
            long temp = capacity;
            List<(double, long, long)> density = new List<(double, long, long)>();
            for (int i = 0; i < weights.Length; i++)
            {
                density.Add(((values[i] / (double)weights[i]), weights[i], values[i]));
            }
            density.Sort();
            density.Reverse();
            foreach (var i in density)
            {
                if (i.Item2 <= capacity)
                {
                    result += i.Item3;
                    capacity -= i.Item2;
                }
                else if (i.Item2 > capacity)
                {
                    result += (capacity * (double)i.Item1);
                    break;
                }
            }
            return (long)result;
        }

        public static long MaximizingOnlineAdRevenue3(long slotCount, long[] adRevenue, long[] averageDailyClick)
        {
            List<long> Revenue = adRevenue.OrderByDescending(x => x).ToList();
            List<long> Click = averageDailyClick.OrderByDescending(x => x).ToList();
            List<long> orderedAds = new List<long>();
            for (int i = 0; i < Revenue.Count; i++)
            {
                orderedAds.Add(Revenue[i] * Click[i]);
            }
            return orderedAds.Sum();
        }

        public static long CollectingSignatures4(long tenantCount, long[] startTimes, long[] endTimes)
        {
            List<(long, long, long, bool)> ranges = new List<(long, long, long, bool)>();
            for (int i = 0; i < startTimes.Length; i++)
            {
                ranges.Add((startTimes[i], endTimes[i], endTimes[i] - startTimes[i], false));
            }
            ranges = ranges.OrderBy(x => x.Item3).ToList();
            for (int i = 0; i < ranges.Count; i++)
            {
                for (int j = i + 1; j < ranges.Count; j++)
                {
                    if (ranges[i].Item1 >= ranges[j].Item1 && ranges[i].Item2 <= ranges[j].Item2)
                    {
                        ranges.RemoveAt(j);
                    }
                }
            }
            ranges = ranges.OrderBy(x => x.Item1).ToList();
            Dictionary<long, List<(long, long, long, bool)>> groups = new Dictionary<long, List<(long, long, long, bool)>>();
            for (int i = 0; i < ranges.Count; i++)
            {
                List<(long, long, long, bool)> tempGroup = new List<(long, long, long, bool)>();
                for (int j = i; j < ranges.Count; j++)
                {
                    if (ranges[j].Item4 == true)
                        break;

                    if (((ranges[j].Item1 >= ranges[i].Item1 && ranges[j].Item1 <= ranges[i].Item2)
                        || (ranges[j].Item2 >= ranges[i].Item1 && ranges[j].Item2 <= ranges[i].Item2))
                        && (ranges[j].Item4 != true))
                    {
                        tempGroup.Add(ranges[j]);                        
                        ranges[j] = (ranges[j].Item1,ranges[j].Item2,ranges[j].Item3,true);                        
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
                    if(long.Parse(numbers[j].ToString() + numbers[i].ToString()) >=
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