using System;
using System.Collections.Generic;
using TestCommon;

namespace A11
{
    public class Nodes
    {
        public Nodes LeftChild { get; set; }
        public Nodes RightChild { get; set; }
        public Nodes Parent { get; set; }
        public long Key { get; set; }
        public bool IsLeftChild => Parent != null && Parent.LeftChild != null && Parent.LeftChild.Key == Key;

        public Nodes(long key = -1, Nodes left = null, Nodes right = null, Nodes parent = null)
        {
            this.LeftChild = left;
            this.RightChild = right;
            this.Parent = parent;
            this.Key = key;
        }
        public Nodes(Nodes Copy)
        {
            LeftChild = Copy.LeftChild;
            RightChild = Copy.RightChild;
            Parent = Copy.Parent;
            Key = Copy.Key;
        }
    }

    public class SetWithRageSums : Processor
    {
        public SetWithRageSums(string testDataName) : base(testDataName)
        {
            CommandDict =
                        new Dictionary<char, Func<string, string>>()
                        {
                            ['+'] = Add,
                            ['-'] = Del,
                            ['?'] = Find,
                            ['s'] = Sum
                        };
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<string[], string[]>)Solve);

        public readonly Dictionary<char, Func<string, string>> CommandDict;

        protected const long M = 1_000_000_001;

        protected long X = 0;

        protected List<long> Data;

        protected List<long> sumData;

        protected Nodes root = null;

        public string[] Solve(string[] lines)
        {
            X = 0;
            Data = new List<long>();
            root = null;
            List<string> result = new List<string>();
            foreach (var line in lines)
            {
                char cmd = line[0];
                string args = line.Substring(1).Trim();
                var output = CommandDict[cmd](args);
                if (null != output)
                    result.Add(output);
            }
            return result.ToArray();
        }

        private long Convert(long i)
            => i = (i + X) % M;

        private string Add(string arg)
        {
            long i = Convert(long.Parse(arg));
            var x = Find(arg);
            if (x == "Found")
                return null;
            Insert(root, i);
            return null;
        }

        private string Del(string arg)
        {
            long i = Convert(long.Parse(arg));
            Nodes node = TravelDown(i);
            Delete(node);
            return null;
        }

        private string Find(string arg)
        {
            long i = Convert(int.Parse(arg));
            TravelDown(i);
            if (root == null) return "Not found";
            return root.Key == i ? "Found" : "Not found";
        }

        private string Sum(string arg)
        {
            var toks = arg.Split();
            long l = Convert(long.Parse(toks[0]));
            long r = Convert(long.Parse(toks[1]));
            long sum = 0;
            sum =  inorder(root,  l,  r);
            X = sum;
            return sum.ToString();
        }

        public Nodes TravelDown(long key)
        {
            Nodes PrevNode = null;
            Nodes z = root;
            while (z != null)
            {
                PrevNode = z;
                if (key > z.Key)
                    z = z.RightChild;
                else if (key < z.Key)
                    z = z.LeftChild;
                else if (key == z.Key)
                {
                    Splay(z);
                    return z;
                }

            }
            if (PrevNode != null)
            {
                Splay(PrevNode);
                return null;
            }
            return null;
        }

        private void Splay(Nodes x)
        {
            if (x == null)
                return;

            while (x.Parent != null)
            {
                Nodes Parent = x.Parent;
                Nodes GrandParent = Parent.Parent;
                if (GrandParent == null)
                {
                    if (x == Parent.LeftChild)
                        ZigR(x, Parent);
                    else
                        ZigL(x, Parent);
                }
                else
                {
                    if (x == Parent.LeftChild)
                    {
                        if (Parent == GrandParent.LeftChild)
                        {
                            ZigR(Parent, GrandParent);
                            ZigR(x, Parent);
                        }
                        else
                        {
                            ZigR(x, x.Parent);
                            ZigL(x, x.Parent);
                        }
                    }
                    else
                    {
                        if (Parent == GrandParent.LeftChild)
                        {
                            ZigL(x, x.Parent);
                            ZigR(x, x.Parent);
                        }
                        else
                        {
                            ZigL(Parent, GrandParent);
                            ZigL(x, Parent);
                        }
                    }
                }
            }
            root = x;
        }

        public void ZigR(Nodes c,Nodes p)
        {
            if ((c == null) || (p == null) || (p.LeftChild != c) || (c.Parent != p))
                return;

            if (p.Parent != null)
            {
                if (p == p.Parent.LeftChild)
                    p.Parent.LeftChild = c;
                else
                    p.Parent.RightChild = c;
            }
            if (c.RightChild != null)
                c.RightChild.Parent = p;

            c.Parent = p.Parent;
            p.Parent = c;
            p.LeftChild = c.RightChild;
            c.RightChild = p;
        }

        public void ZigL(Nodes c,Nodes p)
        {
            if ((c == null) || (p == null) || (p.RightChild != c) || (c.Parent != p))
                return;
            if (p.Parent != null)
            {
                if (p == p.Parent.LeftChild)
                    p.Parent.LeftChild = c;
                else
                    p.Parent.RightChild = c;
            }
            if (c.LeftChild != null)
                c.LeftChild.Parent = p;
            c.Parent = p.Parent;
            p.Parent = c;
            p.RightChild = c.LeftChild;
            c.LeftChild = p;
        }

        public void Insert(Nodes current, long key)
        {
            Nodes z = root;
            Nodes p = null;
            while (z != null)
            {
                p = z;
                if (key > p.Key)
                    z = z.RightChild;
                else
                    z = z.LeftChild;
            }
            z = new Nodes();
            z.Key = key;
            z.Parent = p;
            if (p == null)
                root = z;
            else if (key > p.Key)
                p.RightChild = z;
            else
                p.LeftChild = z;
            Splay(z);
        }

        public void Delete(Nodes node)
        {
            if (node == null)
                return;

            Splay(node);
            if ((node.LeftChild != null) && (node.RightChild != null))
            {
                Nodes min = node.LeftChild;
                while (min.RightChild != null)
                    min = min.RightChild;

                min.RightChild = node.RightChild;
                node.RightChild.Parent = min;
                node.LeftChild.Parent = null;
                root = node.LeftChild;
            }
            else if (node.RightChild != null)
            {
                node.RightChild.Parent = null;
                root = node.RightChild;
            }
            else if (node.LeftChild != null)
            {
                node.LeftChild.Parent = null;
                root = node.LeftChild;
            }
            else
            {
                root = null;
            }
            node.Parent = null;
            node.LeftChild = null;
            node.RightChild = null;
            node = null;
        }

        public long inorder(Nodes current,long low,long high)
        {
            Stack<Nodes> nodePile = new Stack<Nodes>();
            List<long> result = new List<long>();
            if (current == null)
                return 0;

            while (true)
            {
                nodePile.Push(current);
                if (current.LeftChild == null)
                    break;
                current = current.LeftChild;
            }

            while (true)
            {
                if (current.LeftChild == null && nodePile.Count != 0)
                {
                    result.Add(nodePile.Peek().Key);
                    if (nodePile.Peek().RightChild != null)
                        current = nodePile.Pop().RightChild;
                    else
                    {
                        nodePile.Pop();
                        continue;
                    }
                    while (true)
                    {
                        nodePile.Push(current);
                        if (current.LeftChild == null)
                            break;
                        current = current.LeftChild;
                    }
                }
                if (current.LeftChild == null && nodePile.Count == 0)
                {
                    long sum = 0;
                    for (int i = 0; i < result.Count; i++)
                    {
                        if (result[i] <= high && result[i] >= low)
                            sum += result[i];
                    }
                    return sum;
                }
            }
        }
    }
}
