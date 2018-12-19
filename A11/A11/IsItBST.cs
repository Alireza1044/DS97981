using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon;

namespace A11
{

    public class IsItBST : Processor
    {
        public IsItBST(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long[][], bool>)Solve);


        public bool Solve(long[][] nodes)
        {
            Node[] Tree = new Node[nodes.GetLength(0)];

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
            return InOrder(Tree);

        }
        public static bool InOrder(Node[] Tree)
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
                    if (result.Count > 1)
                        if (result[result.Count - 1] < result[result.Count - 2])
                            return false;
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
                    return true;
                }
            }
        }
    }    
}
