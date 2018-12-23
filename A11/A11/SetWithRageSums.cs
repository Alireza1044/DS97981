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
        public int Key { get; set; }
        public bool IsLeftChild => Parent != null && Parent.LeftChild != null && Parent.LeftChild.Key == Key;
        //public bool IsLeftChild => Parent != null && ReferenceEquals(Parent.Left, this);

        public Nodes(int key = -1, Nodes left = null, Nodes right = null, Nodes parent = null)
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

        protected List<int> sumData;

        protected Nodes Tree = null;

        public string[] Solve(string[] lines)
        {
            X = 0;
            Data = new List<long>();
            Tree = null;
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
            if (Tree != null)
            {
                Tree = Summation(Tree, (int)l, (int)r);
                sumData.Sort();
                for (int i = 1; i < sumData.Count;)
                {
                    if (sumData[i] == sumData[i - 1])
                    {
                        sumData.RemoveAt(i);
                        continue;
                    }
                    i++;
                }
                foreach (var item in sumData)
                {
                    sum += item;
                }
            }
            else
            {
                sum = 0;
            }
            X = sum;
            return sum.ToString();
        }

        //public Nodes RotateRight(Nodes current)
        //{
        //    Nodes temp = current.LeftChild;
        //    current.LeftChild = temp.RightChild;
        //    temp.RightChild = current;
        //    current = temp;
        //    return current;
        //}

        //public Nodes RotateLeft(Nodes current)
        //{
        //    Nodes temp = current.RightChild;
        //    current.RightChild = temp.LeftChild;
        //    temp.LeftChild = current;
        //    current = temp;
        //    return current;
        //}

        public Nodes Splay(Nodes current, int key)
        {
            if (current == null || current.Key == key)//key is at the root Or root is null
                return current;

            if (current.Parent == null)
            {
                if (key > current.Key && current.RightChild != null)
                    current = ZigL(current.RightChild);
                else if (key < current.Key && current.LeftChild != null)
                    current = ZigR(current.LeftChild);
                return current;
            }

            //if (key < current.Key)
            //{
            //    if (current.LeftChild == null) return current;//return last visited node if key is not in the array
            //    if (key < current.LeftChild.Key)//zig-zig
            //    {
            //        current.LeftChild.LeftChild = Splay(current.LeftChild.LeftChild, key);
            //        current = RotateRight(current);
            //    }
            //    else if (key > current.LeftChild.Key)//zig-zag
            //    {
            //        current.LeftChild.RightChild = Splay(current.LeftChild.RightChild, key);
            //        if (current.LeftChild.RightChild != null)
            //            current.LeftChild = RotateLeft(current);
            //    }
            //    return current.LeftChild == null ? current : RotateRight(current);
            //}
            //else
            //{
            //    if (current.RightChild == null) return current;

            //    if (key < current.RightChild.Key)//zag-zig
            //    {
            //        current.RightChild.LeftChild = Splay(current.RightChild.LeftChild, key);
            //        if (current.RightChild.LeftChild != null)
            //            current.RightChild = RotateRight(current);
            //    }
            //    else if (key > current.RightChild.Key)//zag-zag
            //    {
            //        current.RightChild.RightChild = Splay(current, key);
            //        current = RotateLeft(current);
            //    }
            //    return current.RightChild == null ? current : RotateLeft(current);
            //}
            while (current != null && current.Parent != null)
            {
                if (current.IsLeftChild)
                {
                    if (current.Parent.Parent == null)
                        //current.Parent = ZigR(current);
                        current = ZigR(current);
                    else if (current.Parent.IsLeftChild)
                        //current.Parent.Parent = ZigZigRR(current);
                        current = ZigZigRR(current);
                    else if (!current.Parent.IsLeftChild)
                        //current.Parent.Parent = ZigZagRL(current);
                        current = ZigZagRL(current);
                }
                else
                {
                    if (current.Parent.Parent == null)
                        //current.Parent = ZigL(current.Parent);
                        current = ZigL(current.Parent);
                    else if (!current.Parent.IsLeftChild)
                        //current.Parent.Parent = ZigZigLL(current);
                        current = ZigZigLL(current);
                    else if (current.Parent.IsLeftChild)
                        //current.Parent.Parent = ZigZagLR(current);
                        current = ZigZagLR(current);

                }
            }
            return current;
        }

        public Nodes ZigZigRR(Nodes current)
        {
            Nodes temp = new Nodes(current.Key, current.LeftChild, current.Parent, current.Parent.Parent.Parent);
            temp.RightChild.Parent = temp;
            temp.RightChild.LeftChild = current.RightChild;
            temp.RightChild.RightChild = current.Parent.Parent;
            temp.RightChild.LeftChild = current.Parent.RightChild;
            temp.RightChild.RightChild.Parent = temp.RightChild;
            temp.RightChild.LeftChild.Parent = temp.RightChild;
            temp.RightChild.RightChild.LeftChild.Parent = temp.RightChild.RightChild;
            if (current.Parent.Parent.Parent.Key < current.Key)
            {
                current.Parent.Parent.Parent.RightChild = temp;
            }
            else
            {
                current.Parent.Parent.Parent.LeftChild = temp;
            }
            return temp;
        }

        public Nodes ZigZigLL(Nodes current)
        {
            Nodes temp = new Nodes(current.Key, current.Parent, current.RightChild, current.Parent.Parent.Parent);
            temp.LeftChild.Parent = temp;
            temp.LeftChild.RightChild = current.LeftChild;
            temp.LeftChild.LeftChild = current.Parent.Parent;
            temp.LeftChild.RightChild = current.Parent.LeftChild;
            temp.LeftChild.LeftChild.Parent = temp.LeftChild;
            temp.LeftChild.RightChild.Parent = temp.LeftChild;
            temp.LeftChild.LeftChild.RightChild.Parent = temp.LeftChild.LeftChild;
            if (current.Parent.Parent.Parent.Key < current.Key)
            {
                current.Parent.Parent.Parent.RightChild = temp;
            }
            else
            {
                current.Parent.Parent.Parent.LeftChild = temp;
            }
            return temp;
        }

        public Nodes ZigZagRL(Nodes current)
        {
            Nodes temp = new Nodes(current.Key, current.Parent.Parent, current.Parent, current.Parent.Parent.Parent);
            //paiini ro left o rightesho switch kon
            temp.RightChild.LeftChild = current.RightChild;
            temp.LeftChild.RightChild = current.LeftChild;
            current.RightChild = temp.RightChild.LeftChild;
            temp.RightChild.LeftChild.Parent = temp.RightChild;
            temp.LeftChild.RightChild.Parent = temp.LeftChild;
            temp.RightChild.Parent = temp;
            temp.LeftChild.Parent = temp;
            if (current.Parent.Parent.Parent.Key < current.Key)
            {
                current.Parent.Parent.Parent.RightChild = temp;
            }
            else
            {
                current.Parent.Parent.Parent.LeftChild = temp;
            }
            return temp;
        }

        public Nodes ZigZagLR(Nodes current)
        {
            Nodes temp = new Nodes(current.Key, current.Parent, current.Parent.Parent, current.Parent.Parent.Parent);
            temp.LeftChild.RightChild = current.LeftChild;
            temp.RightChild.LeftChild = current.RightChild;
            current.LeftChild = temp.LeftChild.RightChild;
            temp.LeftChild.RightChild.Parent = temp.LeftChild;
            temp.RightChild.LeftChild.Parent = temp.RightChild;
            temp.LeftChild.Parent = temp;
            temp.RightChild.Parent = temp;
            if (current.Parent.Parent.Parent.Key < current.Key)
            {
                current.Parent.Parent.Parent.RightChild = temp;
            }
            else
            {
                current.Parent.Parent.Parent.LeftChild = temp;
            }
            return temp;
        }

        public Nodes ZigR(Nodes current)
        {
            Nodes temp = new Nodes(current.Key, current.LeftChild, current.Parent, null);
            if (temp.RightChild != null)
                temp.RightChild.Parent = temp;
            if (temp.RightChild.LeftChild != null)
            {
                temp.RightChild.LeftChild.Parent = temp.RightChild;
                if (current.LeftChild != null)
                    temp.RightChild.LeftChild = current.LeftChild.RightChild;
                else
                    temp.RightChild.LeftChild = null;

            }
            return temp;
        }

        public Nodes ZigL(Nodes current)
        {
            Nodes temp = new Nodes(current.Key, current.Parent, current.RightChild, null);
            if (temp.LeftChild != null)
                temp.LeftChild.Parent = temp;
            if (temp.LeftChild.RightChild != null)
            {
                temp.LeftChild.RightChild.Parent = temp.LeftChild;
                if (current.RightChild != null)
                    temp.LeftChild.RightChild = current.RightChild.LeftChild;
                else
                    temp.LeftChild.RightChild = null;
            }
            return temp;
        }

        public Nodes Insert(Nodes current, int key)
        {
            if (current == null) return new Nodes(key);

            current = Splay(current, key);

            if (current.Key == key) return current;

            Nodes newNode = new Nodes(key);

            if (current.Key > key)
            {
                newNode.LeftChild = current.LeftChild;
                current.LeftChild = null;
                newNode.RightChild = current;
                if (newNode.LeftChild != null)
                    newNode.LeftChild.Parent = newNode;
                if (newNode.RightChild != null)
                    newNode.RightChild.Parent = newNode;
            }
            else
            {
                newNode.RightChild = current.RightChild;
                current.RightChild = null;
                newNode.LeftChild = current;
                if (newNode.RightChild != null)
                    newNode.RightChild.Parent = newNode;
                if (newNode.LeftChild != null)
                    newNode.LeftChild.Parent = newNode;
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
                current = current.RightChild;
                if(current!= null)
                    current.Parent = null;
            }
            else
            {
                temp = current.RightChild;
                current = current.LeftChild;
                current.Parent = null;
                current.RightChild = temp;
            }
            return current;
        }

        public Nodes Summation(Nodes current, int low, int high)
        {
            //if (current == null) return current;
            //if (low > high) return current;
            //Nodes temp;
            //temp = new Nodes(current);
            sumData = new List<int>();

            //while (temp != null)
            //{
            //    temp = Splay(temp, low);

            //    if (temp.Key < low)
            //        temp = temp.RightChild;
            //    else
            //        temp.LeftChild = null;

            //    temp = Splay(temp, high);

            //    if (temp.Key > high)
            //        temp = temp.LeftChild;
            //    else
            //        temp.RightChild = null;

            //    sumData.Add(temp.Key);
            //    if (temp.LeftChild != null)
            //    {
            //        temp.LeftChild.Parent = temp.Parent;
            //        temp = temp.LeftChild;
            //    }
            //    else
            //        break;
            //}

            //return current;
            Nodes temp = new Nodes(-1);
            for (int i = low; i <= high; i++)
            {
                current = Splay(current, i);
                if ((current.Key >= low && current.Key <= high))
                    sumData.Add(current.Key);
            }
            return current;
        }
    }
}
