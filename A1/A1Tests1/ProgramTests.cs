﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using A1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A1.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod()]
        public void AddTest()
        {
            Assert.AreEqual(3, Program.Add(1, 2));
        }

        [TestMethod(), Timeout(1000)]
        [DeploymentItem("TestData", "A0_TestData")]
        public void GradedTest() //Grade:A1:100 // A0 ???
        {
            TestTools.RunLocalTest("A0", Program.Process);
        }
    }
}