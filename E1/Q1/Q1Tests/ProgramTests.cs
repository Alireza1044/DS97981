using Microsoft.VisualStudio.TestTools.UnitTesting;
using Q1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1.Tests
{
    [TestClass()]
    [DeploymentItem("TestData", "E1_TestData")]
    public class ProgramTests
    {
        private static readonly WordCount[] WordCountArray;
        private static readonly FastLM FastLM;
        private static readonly SlowLM SlowLM;
        static ProgramTests()
        {
            WordCountArray = Program.Load(@"E1_TestData\unigram_count_1w.txt");
            SlowLM = new SlowLM(WordCountArray);
            FastLM = new FastLM(WordCountArray);
        }

        [TestMethod()]
        public void SlowLM_GetCountTest()
        {
            Validator("Q1", (inFile, outFile) =>
            {
                var words = File.ReadAllLines(inFile);
                var counts = File.ReadAllLines(outFile);
                Assert.AreEqual(words.Length, counts.Length);
                for (int i = 0; i < words.Length; i++)
                {
                    ulong count;
                    Assert.IsTrue(SlowLM.GetCount(words[i], out count));
                    Assert.AreEqual(counts[i], count.ToString());
                }
            });
        }

        [TestMethod(), Timeout(1000)]
        public void Graded_FastLM_GetCountTest()
        {
            Validator("Q1", (inFile, outFile) =>
            {
                var words = File.ReadAllLines(inFile);
                var counts = File.ReadAllLines(outFile);
                Assert.AreEqual(words.Length, counts.Length);
                for (int i = 0; i < words.Length; i++)
                {
                    ulong count;
                    Assert.IsTrue(FastLM.GetCount(words[i], out count));
                    Assert.AreEqual(counts[i], count.ToString());
                }
            });
        }

        [TestMethod]
        public void Graded_GetCandidatesTest()
        {
            Validator("Q2", (inFile, outFile) =>
            {
                var word = File.ReadAllText(inFile);
                HashSet<string> expectedCandidates = 
                    new HashSet<string>(File.ReadAllLines(outFile));

                var actualCandidates =
                    new HashSet<string>(
                        CandidateGenerator.GetCandidates(word));

                Assert.IsTrue(expectedCandidates
                    .All(c => actualCandidates.Contains(c)),
                    "Missing candidates");

                Assert.IsTrue(actualCandidates
                    .All(c => expectedCandidates.Contains(c)),
                    "Extra candidates");
            });
        }

        [TestMethod]
        public void Graded_SlowSpellCheckTest()
        {
            SpellChecker spellChecker = new SpellChecker(FastLM);

            Validator("Q4", (inFile, outFile) =>
            {
                var word = File.ReadAllText(inFile);
                var expectedCandidates = File.ReadAllLines(outFile);
                var actualCandidates = spellChecker.SlowCheck(word);

                CollectionAssert.AreEqual(
                    expectedCandidates,
                    actualCandidates);
            });
        }

        [TestMethod, Timeout(5000)]
        public void Graded_SpellCheckTest()
        {
            SpellChecker spellChecker = new SpellChecker(FastLM);

            Validator("Q4", (inFile, outFile) =>
            {
                var word = File.ReadAllText(inFile);
                var expectedCandidates = File.ReadAllLines(outFile);
                var actualCandidates = spellChecker.Check(word);

                CollectionAssert.AreEqual(
                    expectedCandidates, 
                    actualCandidates);
            });
        }

        private void Validator(string TestDataName, Action<string, string> ValidateFunction)
        {
            string AssignmentName = "E1";

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
                    ValidateFunction(inFile, outFile);
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
    }
}