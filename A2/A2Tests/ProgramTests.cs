﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using A2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace A2.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod()]
        public void NaiveMaxPairWiseProductTest()
        {
            int mpwp = Program.NaiveMaxPairWiseProduct(new List<int>() { 10, 14, 15, 9 });
            Assert.AreEqual(mpwp, 14 * 15);
        }

        [TestMethod()]
        [DeploymentItem("TestData", "A1_TestData")]
        public void GradedTest_Correctness() //Graded:A2:100
        {
            TestCommon.TestTools.RunLocalTest("A1", Program.Process);
        }

        [TestMethod(), Timeout(500)]
        [DeploymentItem("TestData", "A1_TestData")]
        public void GradedTest_Performance()
        {
            TestCommon.TestTools.RunLocalTest("A1", Program.Process);
        }

        [TestMethod()]
        public void GradedTest_Stress()
        {
            int naiveResult;
            int fastResult;
            int M = 20;
            int N = 10;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (sw.ElapsedMilliseconds < 5000)
            {
                Random nRand = new Random();
                int n = nRand.Next(2, N);
                int[] numbers = new int[n];
                for (int i = 0; i < numbers.Length; i++)
                {
                    numbers[i] = nRand.Next(0, M);
                }
                for (int i = 0; i < numbers.Length; i++)
                {
                    Console.Write(numbers[i] + " ");
                }
                naiveResult = Program.NaiveMaxPairWiseProduct(numbers.ToList());
                fastResult = Program.FastMaxPairWiseProduct(numbers.ToList());
                if (naiveResult == fastResult)
                {
                    Console.WriteLine("\nOK");
                }
                else
                {
                    Console.WriteLine($"\nWrong, {naiveResult},{fastResult}");
                    Assert.Fail();
                }
            }
            Assert.IsTrue(true);
        }
    }
}