using Microsoft.VisualStudio.TestTools.UnitTesting;
using E2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E2.Tests
{
    [TestClass()]
    public class Q1LinkedListTests
    {
        [TestMethod()]
        public void ReverseTest()
        {
            Assert.Inconclusive("Not Implemented");
            Random rnd = new Random(0);

            for (int testIteration = 0; testIteration < 10; testIteration++)
            {
                int[] random = Enumerable.Range(1, 20).Select(n => rnd.Next(1, 100)).ToArray();
                Q1LinkedList list = new Q1LinkedList();
                for (int i = 0; i < random.Length; i++)
                    list.Insert(random[i]);

                list.Reverse();

                int idx = 0;
                foreach (var k in list.GetReverseEnumerator())
                    Assert.AreEqual(random[idx++], k);

                foreach (var k in list.GetForwardEnumerator())
                    Assert.AreEqual(random[--idx], k);
            }
        }


        [TestMethod(), Timeout(2000)]
        public void ReverseDeepTest()
        {
            Assert.Inconclusive("Not Implemented");
            Random rnd = new Random(0);

            int[] random = Enumerable.Range(1, 1_000_000)
                .Select(n => rnd.Next(1, 100)).ToArray();

            Q1LinkedList list = new Q1LinkedList();
            for (int i = 0; i < random.Length; i++)
                list.Insert(random[i]);

            list.DeepReverse();

            int idx = 0;
            foreach (var k in list.GetReverseEnumerator())
                Assert.AreEqual(random[idx++], k);

            foreach (var k in list.GetForwardEnumerator())
                Assert.AreEqual(random[--idx], k);
        }

        [TestMethod]
        public void DeepReverseSimpleTest()
        {
            Assert.Inconclusive("Not Implemented");
            Q1LinkedList list = new Q1LinkedList();
            for (int i = 0; i < 5; i++)
                list.Insert(i);

            list.DeepReverse();

            int idx = 0;
            foreach (var k in list.GetReverseEnumerator())
                Assert.AreEqual(idx++, k);

            foreach (var k in list.GetForwardEnumerator())
                Assert.AreEqual(--idx, k);
        }

    }
}