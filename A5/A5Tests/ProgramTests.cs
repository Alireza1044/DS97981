using Microsoft.VisualStudio.TestTools.UnitTesting;
using A5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A5.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod(),Timeout(1000)]
        [DeploymentItem(@"TestData", "A5_TestData")]
        public void Graded_BinarySearch1Test()
        {
            TestTools.RunLocalTest("A5", Program.ProcessBinarySearch1, "TD1");
        }

        [TestMethod(), Timeout(1000)]
        [DeploymentItem(@"TestData", "A5_TestData")]
        public void Graded_MajorityElement2Test()
        {
            TestTools.RunLocalTest("A5", Program.ProcessMajorityElement2, "TD2");
        }


        [TestMethod(), Timeout(1000)]
        [DeploymentItem(@"TestData", "A5_TestData")]
        public void Graded_ImprovingQuickSort3Test()
        {
            TestTools.RunLocalTest("A5", Program.ProcessImprovingQuickSort3, "TD3");
        }

        [TestMethod(), Timeout(1000)]
        [DeploymentItem(@"TestData", "A5_TestData")]
        public void Graded_NumberofInversions4Test()
        {
            TestTools.RunLocalTest("A5", Program.ProcessNumberofInversions4, "TD4");
        }

        [TestMethod(), Timeout(1000)]
        [DeploymentItem(@"TestData", "A5_TestData")]
        public void Graded_OrganizingLottery5Test()
        {
            TestTools.RunLocalTest("A5", Program.ProcessOrganizingLottery5, "TD5");
        }

        [TestMethod(), Timeout(1000)]
        [DeploymentItem(@"TestData", "A5_TestData")]
        public void Graded_ClosestPoints6()
        {
            TestTools.RunLocalTest("A5", Program.ProcessClosestPoints6, "TD6");
        }

    }
}