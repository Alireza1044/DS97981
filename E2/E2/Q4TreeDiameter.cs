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
        List<Node> Tree;
        public Q4TreeDiameter(int nodeCount, int seed = 0)
        {
            Nodes = GenerateRandomTree(size: nodeCount, seed: seed);
            BuildTree(Nodes, nodeCount);
        }
        public static int height = 0;

        public void BuildTree(List<int>[] nodes,int nodeCount)
        {
            Tree = new List<Node>();
            for (int i = 0; i < nodeCount; i++)
            {
                Tree.Add(new Node(i));
                for (int j = 0; j < nodes[i].Count; j++)
                {
                    Tree[i].Connected.Add(Nodes[i][j]);
                }
                for (int j = 0; j < Nodes[i].Count; j++)
                {
                    Tree[Nodes[i][j]].Connected.Add(i);
                }
            }
        }

        public class Node
        {
            public List<int> Connected;
            public int Key;
            public int Height;
            public bool IsChecked;
            public Node(int key)
            {
                Key = key;
                Connected = new List<int>();
                IsChecked = false;
                Height = -1;
            }
        }

        public int TreeHeight()
        {
            return TreeHeightFromNode(0,true);
        }

        public int TreeHeightFromNode(int node,bool isRoot = false)
        {
            int maxHeight = 0;
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(Tree[node]);
            Tree[node].Height = 0;
            Tree[node].IsChecked = true;
            Node temp = new Node(-1);
            while(queue.Count != 0)
            {
                temp = queue.Dequeue();
                for (int i = 0; i < temp.Connected.Count; i++)
                {
                    if (!Tree[temp.Connected[i]].IsChecked)
                    {
                        Tree[temp.Connected[i]].Height = temp.Height + 1;
                        Tree[temp.Connected[i]].IsChecked = true;
                        maxHeight = Math.Max(maxHeight, Tree[temp.Connected[i]].Height);
                        queue.Enqueue(Tree[temp.Connected[i]]);
                    }
                }
            }
            if (isRoot)
                return maxHeight;
            else
                return temp.Key;
        }

        public int TreeDiameterN2()
        {
            return 0;
        }

        public int TreeDiameterN()
        {
            int root = TreeHeightFromNode(0);
            for (int i = 0; i < Tree.Count; i++)
            {
                Tree[i].IsChecked = false;
            }
            return TreeHeightFromNode(root,true);
        }

        private static List<int>[] GenerateRandomTree(int size, int seed)
        {
            Random rnd = new Random(seed);
            List<int>[] nodes = Enumerable.Range(0, size)
                .Select(n => new List<int>())
                .ToArray();

            List<int> orphans =
                new List<int>(Enumerable.Range(1, size - 1)); // 0 is root it will remain orphan
            Queue<int> parentsQ = new Queue<int>();
            parentsQ.Enqueue(0);
            while (orphans.Count > 0)
            {
                int parent = parentsQ.Dequeue();
                int childCount = rnd.Next(1, 4);
                for (int i = 0; i < Math.Min(childCount, orphans.Count); i++)
                {
                    int orphanIdx = rnd.Next(0, orphans.Count - 1);
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