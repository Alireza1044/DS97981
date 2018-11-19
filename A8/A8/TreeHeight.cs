using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A8
{
    public class Node
    {
        public List<Node> childs = new List<Node>();
        public int id { get; set; }
        public int depth { get; set; }
        public Node()
        {

        }
        public Node(int Id)
        {
            id = Id;
        }
        public Node(int Id,int Depth)
        {
            id = Id;
            depth = Depth;
        }
    }

    public class Tree
    {
        List<Node> nodes = new List<Node>();
        public int TreeHeight { get; set; }
        public long[] treeList { get; set; }
        public Node root = new Node();
        public Tree(long[] TreeList)
        {
            treeList = TreeList;
            for (int i = 0; i < treeList.Length; i++)
            {
                nodes.Add(new Node(i));
            }
            for (int i = 0; i < treeList.Length; i++)
            {
                if(treeList[i] == -1)
                {
                    root = nodes[i];
                    root.depth = 1;
                    TreeHeight = 1;
                }
                else
                {
                    nodes[(int)treeList[i]].childs.Add(nodes[i]);
                }
            }
        }

        public void CalculateHeight()
        {
            Queue<Node> waitingList = new Queue<Node>();
            waitingList.Enqueue(root);
            while(waitingList.Count > 0)
            {
                Node temp = waitingList.Dequeue();
                for (int i = 0; i < temp.childs.Count; i++)
                {
                    waitingList.Enqueue(temp.childs[i]);
                    temp.childs[i].depth = temp.depth + 1;
                    if (temp.childs[i].depth > TreeHeight)
                        TreeHeight = temp.childs[i].depth;
                }
            }
        }
    }

    public class TreeHeight : Processor
    {
        public TreeHeight(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long>)Solve);

        public long Solve(long nodeCount, long[] treeList)
        {
            Tree tree = new Tree(treeList);
            tree.CalculateHeight();
            return tree.TreeHeight;
        }
    }
}
