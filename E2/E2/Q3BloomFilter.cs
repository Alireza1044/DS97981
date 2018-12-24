using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace E2
{
    public class Q3BloomFilter
    {
        BitArray Filter;
        Func<string, int>[] HashFunctions;

        public Q3BloomFilter(int filterSize, int hashFnCount)
        {
            // زحمت بکشید پیاده سازی کنید
            Random rnd = new Random();
            Filter = new BitArray(filterSize);
            HashFunctions = new Func<string, int>[hashFnCount];
            for (int i = 0; i < HashFunctions.Length; i++)
            {
                HashFunctions[i] = str => MyHashFunction(str, i);
            }
        }

        public int MyHashFunction(string str, int num)
        {
            int res = str.GetHashCode();
            if (num == 1)
                return Math.Abs(res)%Filter.Count;
            for (int i = 1; i < num; i++)
            {
                res = res.ToString().GetHashCode() + res;
            }
            return Math.Abs(res)%Filter.Count;
            //return str.GetHashCode() + num;
        }

        public void Add(string str)
        {
            // زحمت بکشید پیاده سازی کنید
            for (int i = 1; i <= HashFunctions.Count(); i++)
            {
                Filter[MyHashFunction(str, i)] = true;
            }
            
        }

        public bool Test(string str)
        {
            // زحمت بکشید پیاده سازی کنید
            for (int i = 1; i <= HashFunctions.Count(); i++)
            {
                if (Filter[MyHashFunction(str, i)] != true)
                    return false;
            }
            return true;
        }
    }
}