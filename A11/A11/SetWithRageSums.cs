using System;
using System.Collections.Generic;
using TestCommon;

namespace A11
{
    public class Nodes
    {
        public Nodes LeftChild { get; set; }
        public Nodes RightChild { get; set; }
        public int Key { get; set; }
        public Nodes(int key = -1, Nodes left = null, Nodes right = null)
        {
            this.LeftChild = left;
            this.RightChild = right;
            this.Key = key;
        }
        public Nodes(Nodes Copy)
        {
            LeftChild = Copy.LeftChild;
            RightChild = Copy.RightChild;
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

        protected List<int> sumData;

        protected Nodes Tree = null;

        public string[] Solve(string[] lines)
        {
            X = 0;
            Data = new List<long>();
            sumData = new List<int>();
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
            Tree = Insert(Tree, (int)i);

            return null;
        }

        private string Del(string arg)
        {
            long i = Convert(long.Parse(arg));
            Tree = Delete(Tree, (int)i);

            return null;
        }

        private string Find(string arg)
        {
            long i = Convert(int.Parse(arg));
            var res = Splay(Tree, (int)i);
            if (res == null) return "Not found";
            return res.Key == i ? "Found" : "Not found";
        }

        private string Sum(string arg)
        {
            var toks = arg.Split();
            long l = Convert(long.Parse(toks[0]));
            long r = Convert(long.Parse(toks[1]));
            int sum = 0;
            Tree = Summation(Tree, (int)l, (int)r);
            foreach (var item in sumData)
            {
                sum += item;
            }
            X = sum;
            return sum.ToString();
        }

        public Nodes RotateRight(Nodes current)
        {
            Nodes temp = current.LeftChild;
            current.LeftChild = temp.RightChild;
            temp.RightChild = current;
            current = temp;
            return current;
        }

        public Nodes RotateLeft(Nodes current)
        {
            Nodes temp = current.RightChild;
            current.RightChild = temp.LeftChild;
            temp.LeftChild = current;
            current = temp;
            return current;
        }

        public Nodes Splay(Nodes current, int key)
        {
            if (current == null || current.Key == key)//key is at the root Or root is null
                return current;

            if (key < current.Key)
            {
                if (current.LeftChild == null) return current;//return last visited node if key is not in the array
                if (key < current.LeftChild.Key)//zig-zig
                {
                    current.LeftChild.LeftChild = Splay(current.LeftChild.LeftChild, key);
                    current = RotateRight(current);
                }
                else if (key > current.LeftChild.Key)//zig-zag
                {
                    current.LeftChild.RightChild = Splay(current.LeftChild.RightChild, key);
                    if (current.LeftChild.RightChild != null)
                        current.LeftChild = RotateLeft(current);
                }
                return current.LeftChild == null ? current : RotateRight(current);
            }
            else
            {
                if (current.RightChild == null) return current;

                if (key < current.RightChild.Key)//zag-zig
                {
                    current.RightChild.LeftChild = Splay(current.RightChild.LeftChild, key);
                    if (current.RightChild.LeftChild != null)
                        current.RightChild = RotateRight(current);
                }
                else if (key > current.RightChild.Key)//zag-zag
                {
                    current.RightChild.RightChild = Splay(current, key);
                    current = RotateLeft(current);
                }
                return current.RightChild == null ? current : RotateLeft(current);
            }
        }

        public Nodes Insert(Nodes current, int key)
        {
            if (current == null) return new Nodes(key);

            current = Splay(current, key);

            if (current.Key == key) return current;

            Nodes newNode = new Nodes(key);

            if (current.Key > key)
            {
                newNode.RightChild = current;
                newNode.LeftChild = current.LeftChild;
                current.LeftChild = null;
            }
            else
            {
                newNode.LeftChild = current;
                newNode.RightChild = current.RightChild;
                current.RightChild = null;
            }
            return newNode;
        }

        public Nodes Delete(Nodes current, int key)
        {
            Nodes temp;
            if (current == null) return null;

            current = Splay(current, key);

            if (current.Key != key)
                return current;

            if (current.LeftChild == null)
            {
                temp = current;
                current = current.RightChild;
            }
            else
            {
                temp = current;
                current = Splay(current.LeftChild, key);
                current.RightChild = temp.RightChild;
            }
            return current;
        }

        public Nodes Summation(Nodes current, int low, int high)
        {
            if (current == null) return current;
            if (low > high) return current;
            Nodes temp;
            temp = new Nodes(current);

            while (temp != null)
            {
                temp = Splay(temp, low);

                if (temp.Key < low)
                    temp = temp.RightChild;
                else
                    temp.LeftChild = null;

                temp = Splay(temp, high);

                if (temp.Key > high)
                    temp = temp.LeftChild;
                else
                    temp.RightChild = null;

                sumData.Add(temp.Key);
                temp = temp.LeftChild;
            }
            current = Splay(current, low);
            current = Splay(current, high);
            return current;
        }
    }
}
