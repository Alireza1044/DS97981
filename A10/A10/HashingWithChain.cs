using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon;

namespace A10
{
    public class HashingWithChain : Processor
    {
        public HashingWithChain(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, string[], string[]>)Solve);

        LinkedList<string>[] HashTable;
        long m;
        public string[] Solve(long bucketCount, string[] commands)
        {
            HashTable = new LinkedList<string>[(int)bucketCount];
            m = bucketCount;
            List<string> result = new List<string>();
            foreach (var cmd in commands)
            {
                var toks = cmd.Split();
                var cmdType = toks[0];
                var arg = toks[1];

                switch (cmdType)
                {
                    case "add":
                        Add(arg);
                        break;
                    case "del":
                        Delete(arg);
                        break;
                    case "find":
                        result.Add(Find(arg));
                        break;
                    case "check":
                        result.Add(Check(int.Parse(arg)));
                        break;
                }
            }
            return result.ToArray();
        }

        public const long BigPrimeNumber = 1000000007;
        public const long ChosenX = 263;

        public static long PolyHash(
            string str, long count,
            long p = BigPrimeNumber, long x = ChosenX, int start = 0)
        {
            long hash = 0;

            for (int i = 0; i < str.Length; i++)
            {
                hash += (long)(((long)str[i] * Power(x,i)) % p);
                hash = hash % p;
            }
            hash = hash % count;
            return hash;
        }

        public void Add(string str)
        {
            long hash = PolyHash(str, m);
            if (HashTable[(int)hash] == null)
            {
                HashTable[(int)hash] = new LinkedList<string>();
                HashTable[(int)hash].AddFirst(str);
                return;
            }
            for (int i = 0; i < HashTable[(int)hash].Count; i++)
            {
                if (HashTable[(int)hash].ElementAt(i) == str)
                    return;
            }
            HashTable[(int)hash].AddFirst(str);
            return;
        }

        public string Find(string str)
        {
            long hash = PolyHash(str, m);
            if (HashTable[(int)hash] == null)
                return "no";
            for (int i = 0; i < HashTable[(int)hash].Count; i++)
            {
                if (HashTable[(int)hash].ElementAt(i) == str)
                    return "yes";
            }
            return "no";
        }

        public void Delete(string str)
        {
            long hash = PolyHash(str, m);
            if (HashTable[(int)hash] == null)
                return;
            for (int i = 0; i < HashTable[(int)hash].Count; i++)
            {
                if (HashTable[(int)hash].ElementAt(i) == str)
                {
                    HashTable[(int)hash].Remove(HashTable[(int)hash].ElementAt(i));
                    return;
                }
            }
            return;
        }

        public string Check(int i)
        {
            string list = null;
            if (HashTable[i] == null)
                return "-";
            if (HashTable[i].Count == 0)
                return "-";
            for (int j = 0; j < HashTable[i].Count; j++)
            {
                list += HashTable[i].ElementAt(j) + " ";
            }
            list = list.TrimEnd();
            return list;
        }
        public static long Power(long x,int i)
        {
            long res = 1;
            for (int j = 0; j < i; j++)
            {
                res *= x;
                res = res % BigPrimeNumber;
            }
            return res;
        }
    }
}
