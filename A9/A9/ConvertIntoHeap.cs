using TestCommon;
using System;

namespace A9
{
    public class ConvertIntoHeap : Processor
    {
        public ConvertIntoHeap(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long[], Tuple<long, long>[]>)Solve);

        public Tuple<long, long>[] Solve(
            long[] array)
        {
            //Write your code here
            return new Tuple<long, long>[] 
            {
                Tuple.Create<long, long>(1, 1)
            };
        }
    }

}