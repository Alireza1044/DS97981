using System;
using System.Collections.Generic;
using TestCommon;

namespace A11
{
    public class IsItBSTHard : Processor
    {
        public IsItBSTHard(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long[][], bool>)Solve);


        public bool Solve(long[][] nodes)
        {
            Node[] Tree = new Node[nodes.GetLength(0)];
            if (nodes.GetLength(0) == 0)
                return true;
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
            return IsBST(Tree,Tree[0],long.MinValue,long.MaxValue);
        }

        public static bool IsBST(Node[] Tree,Node current,long min,long max)
        {
            if (current.Key < min || current.Key >= max)
                return false;
            if (current.LeftChild != -1 && current.RightChild != -1)
                return IsBST(Tree, Tree[current.LeftChild], min, current.Key) &&
                    IsBST(Tree, Tree[current.RightChild], current.Key, max);

            else if (current.LeftChild != -1 && current.RightChild == -1)
                return IsBST(Tree, Tree[current.LeftChild], min, current.Key) && true;

            else if (current.LeftChild == -1 && current.RightChild != -1)
                return true && IsBST(Tree, Tree[current.RightChild], current.Key, max);

            else if (current.LeftChild == -1 && current.RightChild == -1)
                return true && true;

            return true;

        }
    }
}
