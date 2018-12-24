using Microsoft.VisualStudio.TestTools.UnitTesting;
using E2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Security.Cryptography;
using System.Diagnostics;

namespace E2.Tests
{
    [TestClass()]
    public class Q2BitCoinTests
    {
        public const int DifficultyLevel = 3;

        [TestMethod()]
        public void MineTest()
        {
            Assert.Inconclusive("Not Implemented");
            Q2BitCoin bc = new Q2BitCoin();
            string[] bitCoinData = {
                "00-00-00-20-E0-F5-AE-9C-D8-6A-1B-69-60-33-71-F8-FC-A4-DB-17-1E-63-99-CC-70-6A-20-00-00-00-00-00-00-00-00-00-06-B1-3B-CD-CB-57-D0-36-D2-0B-40-A4-4C-51-B9-2E-54-B4-84-AA-1B-F3-DC-21-C1-4B-ED-68-CD-6B-CE-D3-33-18-1F-5C-F4-1E-37-17-00-00-00-00",
                "00-00-00-20-3E-DC-C0-7A-4D-9B-3A-8F-5D-AA-2E-94-18-DE-96-51-39-FE-25-D6-FA-C1-28-00-00-00-00-00-00-00-00-00-63-51-D5-5D-B2-CB-1C-C2-6E-A3-7C-94-53-6D-DA-D4-EA-70-1F-E1-FB-0E-2E-3D-A4-20-D6-57-56-C9-D0-12-82-70-1F-5C-F4-1E-37-17-00-00-00-00",
                "00-00-00-20-3E-DC-C0-7A-4D-9B-3A-8F-5D-AA-2E-94-18-DE-96-51-39-FE-25-D6-FA-C1-28-00-00-00-00-00-00-00-00-00-B9-B8-D3-11-04-61-24-DE-7E-EB-23-B7-95-FB-0E-22-AE-39-32-64-3B-3B-C6-26-A3-B5-D4-A9-05-94-F5-52-82-70-1F-5C-F4-1E-37-17-A0-86-01-00",
                "00-00-00-20-3E-DC-C0-7A-4D-9B-3A-8F-5D-AA-2E-94-18-DE-96-51-39-FE-25-D6-FA-C1-28-00-00-00-00-00-00-00-00-00-11-51-7D-B8-72-BB-99-FE-AE-E2-26-C9-D3-2D-A0-C4-97-D0-26-C0-8B-A1-7B-BC-8D-E1-BD-D6-74-12-7B-9D-49-71-1F-5C-F4-1E-37-17-00-00-00-00"
                };

            int idx = 0;
            foreach (var dataStr in bitCoinData)
            {
                byte[] data = dataStr.Split('-')
                    .Select(s => byte.Parse(s, NumberStyles.HexNumber)).ToArray();

                Stopwatch sw = Stopwatch.StartNew();
                uint nonce;
                Assert.IsTrue(bc.Mine(data, DifficultyLevel, out nonce));
                Assert.IsTrue(VerifyHash(data, DifficultyLevel, nonce));
                sw.Stop();

                Console.WriteLine(
                    $"BitCoin {idx++} solved successfully: " +
                    $"0x{nonce.ToString("X")} in {sw.Elapsed.ToString()}");
            }
        }

        private bool VerifyHash(byte[] data, int difficulty, uint nonce)
        {
            SHA256Managed Hasher = new SHA256Managed();
            var hash = Hasher.ComputeHash(data);
            var doubleHash = Hasher.ComputeHash(hash);
            return Q2BitCoin.CountEndingZeroBytes(doubleHash) >= difficulty;
        }
    }
}