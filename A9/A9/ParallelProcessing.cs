using TestCommon;
using System;
using System.Collections.Generic;
using System.Linq;

namespace A9
{
    public class ParallelProcessing : Processor
    {
        public ParallelProcessing(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], Tuple<long, long>[]>)Solve);

        public Tuple<long, long>[] Solve(long threadCount, long[] jobDuration)
        {
            //Write your code here
            List<Tuple<long, long>> result = new List<Tuple<long, long>>();
            List<Thread> threads = new List<Thread>();

            for (int i = 0; i < threadCount; i++)
            {
                threads.Add(new Thread(i, 0));
            }

            for (int i = 0; i < jobDuration.Length; i++)
            {
                result.Add(new Tuple<long, long>(threads[0].Index, threads[0].Time));
                threads[0].Time += jobDuration[i];
                int j = 0;
                for (; j < threadCount; j++)
                {
                    if ((threads[j].Time > threads[0].Time) || 
                        (threads[j].Time == threads[0].Time && threads[j].Index > threads[0].Index))
                    {
                        break;
                    }
                }
                threads.Insert(j, threads[0]);
                threads.RemoveAt(0);
            }
            return result.ToArray();
        }
    }

    public class Thread
    {
        public long Index { get; set; }
        public long Time { get; set; }
        public Thread(int index, int time)
        {
            Time = time;
            Index = index;
        }
    }
}
