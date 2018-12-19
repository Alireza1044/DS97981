using System;
using System.Collections.Generic;
using TestCommon;

namespace A11
{
    public class Node
    {
        public int LeftChild { get; set; }
        public int RightChild { get; set; }
        public int Parent { get; set; }
        public int Key { get; set; }
        public Node(int key, int left, int right)
        {
            Key = key;
            LeftChild = left;
            RightChild = right;
        }
    }
    public class BinaryTreeTraversals : Processor
    {
        public BinaryTreeTraversals(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long[][], long[][]>)Solve);


        public long[][] Solve(long[][] nodes)
        {
            Node[] Tree = new Node[nodes.GetLength(0)];
            List<long[]> result = new List<long[]>();

            for (int i = 0; i < Tree.Length; i++)
            {
                Tree[i] = new Node(-1, -1, -1);
            }

            for (int i = 0; i < nodes.GetLength(0); i++)
            {
                Tree[i].Key = (int)nodes[i][0];
                Tree[i].LeftChild = (int)nodes[i][1];
                Tree[i].RightChild = (int)nodes[i][2];
                if (Tree[i].LeftChild != -1)
                    Tree[(int)nodes[i][1]].Parent = i;
                if (Tree[i].RightChild != -1)
                    Tree[(int)nodes[i][2]].Parent = i;
            weresult.Add(PreOrder(Tree));
            return result.ToArray();
        }

        public static long[] PreOrder(Node[] Tree)
        {
            Stack<Node> nodePile = new Stack<Node>();
            Node current = Tree[0];
            List<long> result = new List<long>();

            while (current.Key != -1)
            {
                nodePile.Push(current);
                current = Tree[current.LeftChild];
            }

            while (true)
            {
                if(current.LeftChild == -1 && nodePile.Count != 0)
                {
                    result.Add(nodePile.Peek().Key);
                    current = Tree[nodePile.Pop().RightChild];
                    while (current.Key != -1)
                    {
                        nodePile.Push(current);
                        current = Tree[current.LeftChild];
                    }
                }
                if (current.LeftChild == -1 && nodePile.Count == 0)
                {
                    return result.ToArray();
                }
            }
        }
    }
}
