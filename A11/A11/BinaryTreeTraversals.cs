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
            this.Key = key;
            this.LeftChild = left;
            this.RightChild = right;
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
                Tree[i].Parent = -1;
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
            }

            result.Add(InOrder(Tree));
            result.Add(PreOrder(Tree));
            result.Add(PostOrder(Tree));
            return result.ToArray();
        }

        public static long[] InOrder(Node[] Tree)
        {
            Stack<Node> nodePile = new Stack<Node>();
            Node current = Tree[0];
            List<long> result = new List<long>();

            while (true)
            {
                nodePile.Push(current);
                if (current.LeftChild == -1)
                    break;
                current = Tree[current.LeftChild];
            }

            while (true)
            {
                if (current.LeftChild == -1 && nodePile.Count != 0)
                {
                    result.Add(nodePile.Peek().Key);
                    if (nodePile.Peek().RightChild != -1)
                        current = Tree[nodePile.Pop().RightChild];
                    else
                    {
                        nodePile.Pop();
                        continue;
                    }
                    while (true)
                    {
                        nodePile.Push(current);
                        if (current.LeftChild == -1)
                            break;
                        current = Tree[current.LeftChild];
                    }
                }
                if (current.LeftChild == -1 && nodePile.Count == 0)
                {
                    return result.ToArray();
                }
            }
        }
        public static long[] PreOrder(Node[] Tree)
        {
            Node current = Tree[0];
            Stack<Node> nodePile = new Stack<Node>();
            List<long> result = new List<long>();
            nodePile.Push(current);

            while (nodePile.Count != 0)
            {
                result.Add(nodePile.Peek().Key);
                current = nodePile.Pop();
                if (current.RightChild != -1)
                    nodePile.Push(Tree[current.RightChild]);
                if (current.LeftChild != -1)
                    nodePile.Push(Tree[current.LeftChild]);
            }

            return result.ToArray();
        }
        private long[] PostOrder(Node[] Tree)
        {
            Node current = Tree[0];
            Node temp;
            Stack<Node> nodePile = new Stack<Node>();
            List<long> result = new List<long>();

            while (true)
            {
                while (current != null)
                {
                    if (current.RightChild != -1)
                        nodePile.Push(Tree[current.RightChild]);
                    nodePile.Push(current);
                    if (current.LeftChild == -1)
                        break;
                    current = Tree[current.LeftChild];
                }
                current = nodePile.Pop();
                if (nodePile.Count > 0)
                {
                    if (current.RightChild != -1 && Tree[current.RightChild].Key == nodePile.Peek().Key)
                    {
                        temp = current;
                        current = nodePile.Pop();
                        nodePile.Push(temp);
                    }
                    else
                    {
                        result.Add(current.Key);
                        current = null;
                    }
                }
                else
                {
                    result.Add(current.Key);
                    break;
                }
            }
            return result.ToArray();
        }
    }
}
