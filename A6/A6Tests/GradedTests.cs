using Microsoft.VisualStudio.TestTools.UnitTesting;
using A6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A6.Tests
{
    [TestClass()]
    public class GradedTests
    {
        [TestMethod()]
        [DeploymentItem("TestData","A6_TestData")]
        public void SolveTest()
        {
            Processor[] problems = new Processor[] {
                new MoneyChange("TD1"),
                new PrimitiveCalculator("TD2"),
                new EditDistance("TD3"),
                new LCSOfTwo("TD4"),
                new LCSOfThree("TD5")
            };

            foreach(var p in problems)
            {
                TestTools.RunLocalTest("A6", p.Process, p.TestDataName);
            }
        }
    }
}