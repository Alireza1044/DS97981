using Microsoft.VisualStudio.TestTools.UnitTesting;
using A8;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A8.Tests
{
    [TestClass()]
    public class GradedTests
    {
        [TestMethod(), Timeout(1000)]
        [DeploymentItem("TestData", "A8_TestData")]
        public void SolveTest()
        {
            Processor[] problems = new Processor[] {
                new CheckBrackets("TD1"),
                new TreeHeight("TD2"),
                new PacketProcessing("TD3")
            };
            
            foreach (var p in problems)
            {
                TestTools.RunLocalTest("A8", p.Process, p.TestDataName);
            }
        }
    }
}