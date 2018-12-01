using TestCommon;
using System;
using System.Collections.Generic;
using System.Linq;

namespace A9
{
    public class ConvertIntoHeap : Processor
    {
        public ConvertIntoHeap(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long[], Tuple<long, long>[]>)Solve);

        public Tuple<long, long>[] Solve(
            long[] array)
        {
            //Write your code here
            int size = array.Length;
            List<long> arr = new List<long>();
            arr = array.ToList();
            List<Tuple<long, long>> res = new List<Tuple<long, long>>();
            int start = (array.Length - 2) / 2;
            for (int i = start; i >= 0; i--)
            {
                if (2 * i + 2 <= arr.Count - 1 && arr[2 * i + 2] < arr[i] && arr[2 * i + 2] < arr[2 * i + 1])
                {
                    res.Add(Swap(ref arr, i, 2 * i + 2));
                    DeepSearch(ref arr, 2 * i + 2, ref res);
                }
                else if (2 * i + 2 <= arr.Count - 1 && arr[2 * i + 1] < arr[i] && arr[2 * i + 1] < arr[2 * i + 2]
                    || 2 * i + 1 == arr.Count - 1 && arr[2 * i + 1] < arr[i])
                {
                    res.Add(Swap(ref arr, i, 2 * i + 1));
                    DeepSearch(ref arr, 2 * i + 1, ref res);
                }
            }
            return res.ToArray();
        }

        public static void DeepSearch(ref List<long> arr, int i, ref List<Tuple<long, long>> res)
        {
            if (2 * i + 2 <= arr.Count - 1 && arr[2 * i + 2] < arr[i] && arr[2 * i + 2] < arr[2 * i + 1])
            {
                res.Add(Swap(ref arr, i, 2 * i + 2));
                DeepSearch(ref arr, 2 * i + 2, ref res);
            }
            else if (2 * i + 2 <= arr.Count - 1 && arr[2 * i + 1] < arr[i] && arr[2 * i + 1] < arr[2 * i + 2]
                    || 2 * i + 1 == arr.Count - 1 && arr[2 * i + 1] < arr[i])
            {
                res.Add(Swap(ref arr, i, 2 * i + 1));
                DeepSearch(ref arr, 2 * i + 1, ref res);
            }
        }

        public static Tuple<long, long> Swap(ref List<long> arr, int i, int j)
        {
            long temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
            return Tuple.Create((long)i, (long)j);
        }
    }

}