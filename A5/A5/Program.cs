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
        static void Main(string[] args) { }

        public static long[] BinarySearch1(long[] a , long [] b)
        {
            //write your code here
            return new long[] { 0 };
        }

        public static string ProcessBinarySearch1(string inStr) =>
            TestTools.Process(inStr, BinarySearch1);



        public static long MajorityElement2(long n, long[] a)
        {
            //write your code here
            return 0;
        }

        public static string ProcessMajorityElement2(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long>)MajorityElement2);

        public static long[] ImprovingQuickSort3(long n, long[] a)
        {
            //write your code here          
            return new long[] { 0 };
        }

        public static string ProcessImprovingQuickSort3(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long[]>)ImprovingQuickSort3);

        public static long NumberofInversions4(long n, long[] a)
        {
            //write your code here
            return 0;
        }

        public static string ProcessNumberofInversions4(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long>)NumberofInversions4);

        public static long[] OrganizingLottery5(long[] points, long[] startSegments,
            long[] endSegment)
        {
            //write your code here
            return new long[] { 0 };
        }

        public static string ProcessOrganizingLottery5(string inStr) =>
            TestTools.Process(inStr,OrganizingLottery5);

        public static double ClosestPoints6(long n, long[] xPoints, long[] yPoints)
        {
            //write your code here
            return 0;
        }

        public static string ProcessClosestPoints6(string inStr) =>
           TestTools.Process(inStr, (Func<long, long[], long[], double>)
               ClosestPoints6);

    }
}
