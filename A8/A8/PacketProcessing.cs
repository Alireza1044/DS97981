using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A8
{
    public class PacketProcessing : Processor
    {
        public PacketProcessing(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[], long[], long[]>)Solve);

        public long[] Solve(long bufferSize, 
            long[] arrivalTimes, 
            long[] processingTimes)
        {
            return new long[] { };
        }
    }
}
