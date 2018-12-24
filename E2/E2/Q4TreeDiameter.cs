using System;
using System.Collections.Generic;
using System.Linq;

namespace E2
{
    public class Q4TreeDiameter
    {
        /// <summary>
        /// ریشه همیشه نود صفر است.
        ///توی این آرایه در مکان صفر لیستی از بچه های ریشه موجودند.
        ///و در مکانه آی از این آرایه لیست بچه های نود آیم هستند
        ///اگر لیست خالی بود، بچه ندارد
        /// </summary>
        public List<int>[] Nodes;

        public Q4TreeDiameter(int nodeCount, int seed = 0)
        {
            Nodes = GenerateRandomTree(size: nodeCount, seed: seed);
            
        }

        public int TreeHeight()
        {
            return 0;
        }

        public int TreeHeightFromNode(int node)
        {
            return 0;
        }

        public int TreeDiameterN2()
        {
            return 0;
        }

        public int TreeDiameterN()
        {
            return 0;
        }

        private static List<int>[] GenerateRandomTree(int size, int seed)
        {
            Random rnd = new Random(seed);
            List<int>[] nodes = Enumerable.Range(0, size)
                .Select(n => new List<int>())
                .ToArray();
            
            List<int> orphans = 
                new List<int>(Enumerable.Range(1, size-1)); // 0 is root it will remain orphan
            Queue<int> parentsQ = new Queue<int>();
            parentsQ.Enqueue(0);
            while (orphans.Count > 0)
            {
                int parent = parentsQ.Dequeue();
                int childCount = rnd.Next(1, 4);
                for (int i=0; i< Math.Min(childCount, orphans.Count); i++)
                {
                    int orphanIdx = rnd.Next(0, orphans.Count-1);
                    int orphan = orphans[orphanIdx];
                    orphans.RemoveAt(orphanIdx);
                    nodes[parent].Add(orphan);
                    parentsQ.Enqueue(orphan);
                }
            }
            return nodes;
        }
    }
}