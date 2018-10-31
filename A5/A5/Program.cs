using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A5
{
    public class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            bool isOK = true;
            while (isOK)
            {
                int a = rnd.Next(1, 3);
                long[] points = new long[a];

                for (int i = 0; i < points.Length; i++)
                {
                    int s = rnd.Next(0, 40);
                    points[i] = s;
                }

                a = rnd.Next(1, 10);

                long[] starts = new long[a];
                long[] ends = new long[a];

                for (int i = 0; i < starts.Length; i++)
                {
                    int s = rnd.Next(-40, 35);
                    starts[i] = s;
                    s = rnd.Next(s + 1, 40);
                    ends[i] = s;
                }
                List<long> resNaive = OrganizingLotteryNaive5(points, starts, ends).ToList();
                List<long> resFast = OrganizingLottery5(points, starts, ends).ToList();
                for (int i = 0; i < points.Length; i++)
                {
                    if (resFast[i] != resNaive[i])
                        isOK = false;
                }

                Console.Write("Point: ");
                for (int i = 0; i < points.Length; i++)
                {
                    Console.Write($"{points[i]} ");
                }
                Console.WriteLine();

                Console.Write("Start: ");
                for (int i = 0; i < starts.Length; i++)
                {
                    Console.Write($"{starts[i]} ");
                }
                Console.WriteLine();

                Console.Write("End:   ");
                for (int i = 0; i < ends.Length; i++)
                {
                    Console.Write($"{ends[i]} ");
                }
                Console.WriteLine();

                Console.Write("Naive: ");
                for (int i = 0; i < resNaive.Count; i++)
                {
                    Console.Write($"{resNaive[i]} ");
                }
                Console.WriteLine();
                Console.Write("Fast:  ");
                for (int i = 0; i < resFast.Count; i++)
                {
                    Console.Write($"{resFast[i]} ");
                }
                Console.WriteLine();
                if (isOK)
                    Console.WriteLine("Ok!");
            }
            Console.WriteLine("Error!");
            Console.ReadKey();
        }

        public static long[] OrganizingLotteryNaive5(long[] points, long[] startSegments,
            long[] endSegment)
        {
            List<long> res = new List<long>();
            foreach (var point in points)
            {
                res.Add(startSegments.Zip(endSegment, (s, e) => new
                {
                    start = s,
                    end = e,
                }).ToList().FindAll(x => x.start <= point && x.end >= point).Count());
            }
            return res.ToArray();
        }

        public static long[] BinarySearch1(long[] a, long[] b)
        {
            List<long> result = new List<long>();

            for (int i = 0; i < b.Length; i++)
            {
                long low = 0;
                long high = a.Length - 1;
                while (true)
                {
                    long mid = (high + low) / 2;
                    if (b[i] > a[mid]) low = mid + 1;
                    if (b[i] < a[mid]) high = mid - 1;
                    if (b[i] == a[mid])
                    {
                        result.Add(mid);
                        break;
                    }
                    if (low > high)
                    {
                        result.Add(-1);
                        break;
                    }
                }
            }
            return result.ToArray();
        }

        public static string ProcessBinarySearch1(string inStr)
            => TestCommon.TestTools.Process(inStr, (Func<long[], long[], long[]>)BinarySearch1);



        public static long MajorityElement2(long n, long[] a)
        {
            //write your code here
            List<long> numbers = new List<long>();
            numbers = a.ToList();
            numbers.Sort();

            long median = numbers[numbers.Count / 2];
            //if (numbers.Count % 2 == 0)
            //    median = numbers[numbers.Count / 2];
            //else
            //    median = numbers[numbers.Count / 2 - 1];

            long low = 0;
            long high = numbers.Count - 1;

            while (true)
            {
                long mid = (high + low) / 2;
                //if (high + low % 2 == 0)
                //    mid = (high + low) / 2;
                //else mid = (high + low) / 2 + 1;
                if (numbers[(int)mid / 2] == median && numbers[(int)mid * 3 / 2] == median) return 1;
                if (numbers[(int)mid / 2] == median) high = mid - 1;
                if (numbers[(int)mid * 3 / 2] == median) low = mid + 1;
                if (low >= high) break;
                if ((numbers[(int)mid * 3 / 2] != median) && (numbers[(int)mid / 2] != median)) break;
                if ((high - low == 1) && (numbers[(int)high] == numbers[(int)low])) return 1;
                if ((high - low == 1) && (numbers[(int)high] != numbers[(int)low])) break;
            }
            return 0;
        }

        public static string ProcessMajorityElement2(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long>)MajorityElement2);

        public static long[] ImprovingQuickSort3(long n, long[] a)
        {
            //write your code here       
            int low = 0;
            int high = a.Length - 1;
            List<long> numbers = a.ToList();
            QuickSort(numbers, low, high);
            return numbers.ToArray();
        }

        static void QuickSort(List<long> arr, int low, int high)
        {
            if (low < high)
            {
                List<int> parts = Partition(ref arr, low, high);
                QuickSort(arr, low, parts[0] - 1);
                QuickSort(arr, parts[1] + 1, high);
            }
        }

        static List<int> Partition(ref List<long> arr, int low, int high)
        {
            long pivot = arr[low];
            int leftMark = low;
            int rightMark = high;
            int pivotIndex = low;
            while (leftMark <= rightMark)
            {
                if (arr[leftMark] < pivot)
                {
                    Swap(leftMark, pivotIndex, ref arr);
                    pivotIndex++;
                    leftMark++;
                }
                else if (arr[leftMark] > pivot)
                {
                    Swap(leftMark, rightMark, ref arr);
                    rightMark--;
                }
                else
                {
                    leftMark++;
                }
            }
            return new List<int> { pivotIndex, rightMark };
        }

        public static void Swap(int a, int b, ref List<long> numbers)
        {
            long temp = numbers[a];
            numbers[a] = numbers[b];
            numbers[b] = temp;
        }

        public static string ProcessImprovingQuickSort3(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long[]>)ImprovingQuickSort3);

        public static long NumberofInversions4(long n, long[] a)
        {
            //write your code here
            long invCounter = 0;
            MergeSort(a, 0, a.Length - 1, ref invCounter);
            return invCounter;
        }

        public static void MergeSort(long[] arr, int low, int high, ref long invCounter)
        {
            int mid = (low + high) / 2;
            if (low < high)
            {
                MergeSort(arr, low, mid, ref invCounter);
                MergeSort(arr, mid + 1, high, ref invCounter);
                Merge(arr, low, mid, high, ref invCounter);
            }
        }

        public static void Merge(long[] arr, int low, int mid, int high, ref long invCounter)
        {
            List<long> left = new List<long>();
            List<long> right = new List<long>();

            long n1 = mid - low + 1;
            long n2 = high - mid;

            for (int z = 0; z < n1; z++)
                left.Add(arr[low + z]);

            for (int y = 0; y < n2; y++)
                right.Add(arr[mid + 1 + y]);


            int i = 0, j = 0;

            while (i < n1 && j < n2)
            {
                if (left[i] <= right[j])
                {
                    arr[low] = left[i++];
                }

                else
                {
                    arr[low] = right[j++];
                    invCounter += left.Count - i;
                }
                low++;
            }

            while (i < n1)
            {
                arr[low++] = left[i++];
            }

            while (j < n2)
            {
                arr[low++] = right[j++];
            }
        }

        public static string ProcessNumberofInversions4(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long>)NumberofInversions4);

        public struct Seg
        {
            public long Value;
            public char Key;
            public int Index;

            public Seg(char s, long e, int i)
            {
                Key = s;
                Value = e;
                Index = i;
            }
        }

        public static long[] OrganizingLottery5(long[] points, long[] startSegments,
            long[] endSegment)
        {
            //write your code here
            long[] result = new long[points.Length];

            List<Seg> datas = new List<Seg>();

            Seg s = new Seg();

            for (int i = 0; i < points.Length; i++)
            {
                s.Key = 'p';
                s.Value = points[i];
                s.Index = i;
                datas.Add(s);
            }

            for (int i = 0; i < startSegments.Length; i++)
            {
                s.Key = 'l';
                s.Value = startSegments[i];
                s.Index = 0;
                datas.Add(s);
                s.Key = 'r';
                s.Value = endSegment[i];
                s.Index = 0;
                datas.Add(s);
            }
            datas = datas.OrderBy(x => x.Key).OrderBy(x => x.Value).ToList();

            int cnt = 0;

            foreach (var data in datas)
            {
                if (data.Key == 'l') cnt++;
                else if (data.Key == 'r') cnt--;
                else if (data.Key == 'p') result[data.Index] = cnt;
            }

            return result;
        }

        public static string ProcessOrganizingLottery5(string inStr)
            => TestCommon.TestTools.Process(inStr, (Func<long[], long[], long[], long[]>)OrganizingLottery5);


        public struct Point
        {
            public long X, Y;
            public Point(long x,long y)
            {
                X = x;
                Y = y;
            }
        }

        public static double ClosestPoints6(long n, long[] xPoints, long[] yPoints)
        {
            //write your code here
            List<Point> points = new List<Point>();
            Point p = new Point();
            for (int i = 0; i < xPoints.Length; i++)
            {
                p.X = xPoints[i];
                p.Y = yPoints[i];
                points.Add(p);
            }
            points = points.OrderBy(x => x.X).ToList();
            double result = Closest(points, 0, points.Count - 1);
            result *= 10000;
            double temp = Math.Round(result);
            result = temp / 10000;
            return result;
        }

        public static double Closest(List<Point> points, int low, int high)
        {
            int n = high - low + 1;
            if (n <= 3)
            {
                //bruteForce!
                return bruteForce(points, low,high);
            }

            int mid = (high + low) / 2;

            double leftDistance = Closest(points, low, mid);
            double rightDistance = Closest(points, mid + 1, high);

            double d = min(leftDistance, rightDistance);

            List<Point> strip = new List<Point>();
            for (int i = low; i <= high; i++)
            {
                if (Math.Abs(points[i].X - points[mid].X) < d)
                    strip.Add(points[i]);
            }
            return min(d, stripClosest(strip, d));
        }

        public static double stripClosest(List<Point> strip, double d)
        {
            double min = d;
            strip = strip.OrderBy(x => x.Y).ToList();

            for (int i = 0; i < strip.Count; i++)
            {
                for (int j = i + 1; j < strip.Count && (strip[j].Y-strip[i].Y) < min; j++) 
                {
                    if (dist(strip[i], strip[j]) <= min)
                        min = dist(strip[i], strip[j]);
                }
            }
            return min;
        }

        public static double bruteForce(List<Point> points, int low,int high)
        {
            double min = double.MaxValue;
            for (int i = low; i <= high; i++)
                for (int j = i + 1; j <= high; j++)
                    if (dist(points[i], points[j]) < min)
                        min = dist(points[i], points[j]);
            return min;
        }

        public static double dist(Point point1, Point point2)
        {
            //return Math.Pow((Math.Pow((point1.X - point2.X), 2)) + (Math.Pow((point1.Y - point2.Y), 2)),0.5);
            return Math.Sqrt(((point1.X - point2.X) * (point1.X - point2.X)) + ((point1.Y - point2.Y) * (point1.Y - point2.Y)));
        }

        public static double min(double x, double y)
        {
            if (x <= y)
                return x;
            else
                return y;
        }

        public static string ProcessClosestPoints6(string inStr) =>
           TestTools.Process(inStr, (Func<long, long[], long[], double>)
               ClosestPoints6);

    }
}
