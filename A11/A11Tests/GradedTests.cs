using Microsoft.VisualStudio.TestTools.UnitTesting;
using A11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A11.Tests
{
    [TestClass()]
    public class GradedTests
    {
        [TestMethod(), Timeout(2000)]
        [DeploymentItem("TestData", "A11_TestData")]
        public void SolveTest()
        {
            Processor[] problems = new Processor[] {
                new BinaryTreeTraversals("TD1"),
                new IsItBST("TD2"),
                new IsItBSTHard("TD3"),
                new SetWithRageSums("TD4"),
                new Rope("TD5")
            };

            foreach (var p in problems)
            {
                TestTools.RunLocalTest("A11", p.Process, p.TestDataName);
            }
        }
    }
}