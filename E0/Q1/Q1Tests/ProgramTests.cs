using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod()]
        [DeploymentItem("TestData", "E0_TestData")]
        public void Graded_GetNamesTest()
        {
            string AssignmentName = "E0";
            string TestDataName = "Q1";

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
                    var hypFile = inFile.Replace("In_", "Hyp");
                    var numbers = File.ReadAllText(inFile).Trim()
                        .Select(c => int.Parse(c.ToString())).ToArray();

                    // صدا زدن متد شما
                    File.WriteAllLines(hypFile, Program.GetNames(numbers,0,numbers.Length-1));
                    Assert.IsTrue(FilesAreEqual(outFile, hypFile));
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

        private static bool FilesAreEqual(string refPath, string outPath)
        {
            try
            {
                var refSet = new HashSet<string>(File.ReadAllLines(refPath)
                    .Where(l => !string.IsNullOrWhiteSpace(l))
                    .Select(l => l.Trim()));

                var outSet = new HashSet<string>(File.ReadAllLines(outPath)
                    .Where(l => !string.IsNullOrWhiteSpace(l))
                    .Select(l => l.Trim()));

                return refSet.All(l => outSet.Contains(l)) &&
                       outSet.All(l => refSet.Contains(l));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

    }
}