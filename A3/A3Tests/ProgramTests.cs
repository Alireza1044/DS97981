using Microsoft.VisualStudio.TestTools.UnitTesting;
using A3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A3.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod(), Timeout(1000)]
        [DeploymentItem("TestData", "A3_TestData")]
        public void Graded_FibonacciTest()
        {
            TestTools.RunLocalTest("A3", Program.ProcessFibonacci, "TD1");
        }

        [TestMethod(), Timeout(1000)]
        [DeploymentItem("TestData", "A3_TestData")]
        public void Graded_FibonacciLastDigitTest()
        {
            TestTools.RunLocalTest("A3", Program.ProcessFibonacci_LastDigit, "TD2");
        }

        [TestMethod(), Timeout(1000)]
        [DeploymentItem("TestData", "A3_TestData")]
        public void Graded_GCDTest()
        {
            TestTools.RunLocalTest("A3", Program.ProcessGCD, "TD3");
        }

        [TestMethod(), Timeout(1000)]
        [DeploymentItem("TestData", "A3_TestData")]
        public void Graded_LCMTest()
        {
            TestTools.RunLocalTest("A3", Program.ProcessLCM, "TD4");
        }

        [TestMethod(), Timeout(1000)]
        [DeploymentItem("TestData", "A3_TestData")]
        public void Graded_FibonacciModTest()
        {
            TestTools.RunLocalTest("A3", Program.ProcessFibonacci_Mod, "TD5");
        }

        [TestMethod(), Timeout(1000)]
        [DeploymentItem("TestData", "A3_TestData")]
        public void Graded_FibonacciSumTest()
        {
            TestTools.RunLocalTest("A3", Program.ProcessFibonacci_Sum, "TD6");
        }

        [TestMethod(), Timeout(1000)]
        [DeploymentItem("TestData", "A3_TestData")]
        public void Fibonacci_Partial_SumTest()
        {
            TestTools.RunLocalTest("A3", Program.ProcessFibonacci_Partial_Sum, "TD7");
        }

        [TestMethod(), Timeout(1000)]
        [DeploymentItem("TestData", "A3_TestData")]
        public void Graded_FibonacciSumSquaresTest()
        {
            TestTools.RunLocalTest("A3", Program.ProcessFibonacci_Sum_Squares, "TD8");
        }
    }
}