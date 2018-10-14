using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TestCommon
{
    public static class TestTools 
    {
        private static readonly char[] IgnoreChars = new char[] { '\n', '\r', ' ' };

        public static void RunLocalTest(string AssignmentName, Func<string,string> Processor, string TestDataName=null)
        {
            string testDataPath = $"{AssignmentName}_TestData";
            if (!string.IsNullOrEmpty(TestDataName))
                testDataPath = Path.Combine(testDataPath, TestDataName);

            Assert.IsTrue(Directory.Exists(testDataPath));
            string[] inFiles = Directory.GetFiles(testDataPath, "*In_*.txt");

            Assert.IsTrue(inFiles.Length > 0 &&
                Directory.GetFiles(testDataPath).Length % 2 == 0);

            List<string> failedTests = new List<string>();
            foreach (var inFile in inFiles)
            {
                string outFile = inFile.Replace("In_", "Out_");
                Assert.IsTrue(File.Exists(outFile));
                try
                {
                    string result = Processor(File.ReadAllText(inFile)).Trim(IgnoreChars);
                    string expectedResult = string.Join("\n", File.ReadAllLines(outFile)
                        .Select(line => line.Trim(IgnoreChars)) // Ignore white spaces 
                        .Where(line => ! string.IsNullOrWhiteSpace(line))); // Ignore empty lines

                    Assert.AreEqual(expectedResult, result);
                    Console.WriteLine($"Test Passed: {inFile}");
                }
                catch (Exception e)
                {
                    failedTests.Add($"Test failed for input {inFile}: {e.Message}");
                    Console.WriteLine($"Test Failed: {inFile}");
                }
            }

            Assert.IsTrue(failedTests.Count == 0,
                $"{failedTests.Count} out of {inFiles.Length} tests failed: {string.Join("\n", failedTests)}");

            Console.WriteLine($"All {inFiles.Length} tests passed.");
        }


        public static string Process(string inStr, Func<long, long> longProcessor)
        {
            long n = long.Parse(inStr);
            return longProcessor(n).ToString();
        }

        public static string Process(string inStr, Func<long, long[]> longProcessor)
        {
            long n = long.Parse(inStr);
            return string.Join("\n", longProcessor(n));
        }

        public static string Process(string inStr, Func<long, long[], string> longProcessor)
        {

            var lines = inStr.Split(IgnoreChars, StringSplitOptions.RemoveEmptyEntries);
            long count = long.Parse(lines.Take(1).First());
            var numbers = lines.Skip(1)
                .Select(n => long.Parse(n))
                .ToArray();

            Assert.AreEqual(count, numbers.Length);

            string result = longProcessor(numbers.Length, numbers);
            Assert.IsTrue(result.All(c => char.IsDigit(c)));
            return result;
        }

        public static string Process(string inStr, Func<long, long, long> longProcessor)
        {
            long a, b;
            ParseTwoNumbers(inStr, out a, out b);
            return longProcessor(a, b).ToString();
        }

        public static string Process(
            string inStr, 
            Func<long, long[], long[], long> longProcessor)
        {
            List<long> list1 = new List<long>(),
                       list2 = new List<long>();

            long firstLine;

            firstLine = ReadParallelArray(inStr, list1, list2);

            return longProcessor(firstLine,
                list1.ToArray(),
                list2.ToArray()).ToString();
        }

        public static string Process(
            string inStr,
            Func<long, long[], long[], long[]> longProcessor)
        {
            List<long> list1 = new List<long>(),
                       list2 = new List<long>();

            long firstLine;

            firstLine = ReadParallelArray(inStr, list1, list2);

            return string.Join("\n", 
                longProcessor(firstLine, list1.ToArray(), list2.ToArray()));
        }

        private static long ReadParallelArray(string inStr, List<long> list1, List<long> list2)
        {
            long firstLine;
            using (StringReader reader = new StringReader(inStr))
            {
                firstLine = long.Parse(reader.ReadLine());

                string line = null;
                while (null != (line = reader.ReadLine()))
                {
                    long a, b;
                    ParseTwoNumbers(line, out a, out b);
                    list1.Add(a);
                    list2.Add(b);
                }

            }

            return firstLine;
        }

        private static void ParseTwoNumbers(string inStr, out long a, out long b)
        {
            var toks = inStr.Split(IgnoreChars, StringSplitOptions.RemoveEmptyEntries);
            a = long.Parse(toks[0]);
            b = long.Parse(toks[1]);
        }

    }
}
