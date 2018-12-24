using System;
using System.Collections;
using System.Collections.Generic;

namespace E2
{

    public class Q1LinkedList
    {
        public class Node
        {
            public Node(int key) { this.Key = key; }
            public int Key;
            public Node Next = null;
            public Node Prev = null;
            public override string ToString() => ToString(4);

            public string ToString(int maxDepth)
            {
                return maxDepth == 1 || Next == null ?
                    $"{Key.ToString()}" + (Next != null ? "..." : string.Empty) :
                    $"{Key.ToString()} {Next.ToString(maxDepth - 1)}";
            }
        }

        private Node Head = null;
        private Node Tail = null;

        public void Insert(int key)
        {
            if (Head == null)
            {
                Head = Tail = new Node(key);
            }
            else
            {
                var newNode = new Node(key);
                Tail.Next = newNode;
                newNode.Prev = Tail;
                Tail = newNode;
            }
        }

        public override string ToString() => Head.ToString();

        public void Reverse()
        {
            // زحمت بکشید پیاده سازی کنید
            // اگر نیاز بود میتوانید متد اضافه کنید

            rvs(Tail, Head);
        }
        public void rvs(Node tail, Node head)
        {
            if (tail.Prev.Key == head.Key && head.Next.Key == tail.Key)
            {
                var n = tail.Key;
                tail.Key = head.Key;
                head.Key = n;
                return;
            }

            var x = tail.Key;
            tail.Key = head.Key;
            head.Key = x;
            rvs(tail.Prev, head.Next);

        }

        public void DeepReverse()
        {
            // زحمت بکشید پیاده سازی کنید
            // اگر نیاز بود میتوانید متد اضافه کنید
            while (true)
            {
                var x = Head.Key;
                Head.Key = Tail.Key;
                Tail.Key = x;
                Head = Head.Next;
                Tail = Tail.Prev;
                if(Head.Prev.Key == Tail.Prev.Key && Head.Next.Key == Tail.Next.Key && Head.Key == Tail.Key)
                {
                    var y = Head.Key;
                    Head.Key = Tail.Key;
                    Tail.Key = y;
                    break;
                }
                else if ((Head.Next.Key == Tail.Key && Tail.Prev.Key == Head.Key)
                    &&(Head.Prev.Key == Tail.Prev.Prev.Key && Tail.Next.Key == Head.Next.Next.Key))
                {
                    var y = Head.Key;
                    Head.Key = Tail.Key;
                    Tail.Key = y;
                    Head = Head.Next;
                    Tail = Tail.Prev;
                    break;
                }
            }
            while (Head.Prev != null)
            {
                Head = Head.Prev;
            }
            while(Tail.Next != null)
            {
                Tail = Tail.Next;
            }
        }

        public IEnumerable<int> GetForwardEnumerator()
        {
            var it = this.Head;
            while (it != null)
            {
                yield return it.Key;
                it = it.Next;
            }
        }

        public IEnumerable<int> GetReverseEnumerator()
        {
            var it = this.Tail;
            while (it != null)
            {
                yield return it.Key;
                it = it.Prev;
            }
        }
    }
}