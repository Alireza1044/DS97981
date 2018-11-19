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
            if (arrivalTimes.Length == 0)
                return new long[] { };
            List<(long, long, int)> packetsList = new List<(long, long, int)>();
            for (int i = 0; i < arrivalTimes.Length; i++)
            {
                packetsList.Add((arrivalTimes[i], processingTimes[i], i));
            }

            Queue<(long, long, int)> processingPackets = new Queue<(long, long, int)>();
            long[] result = new long[packetsList.Count];

            processingPackets.Enqueue(packetsList[0]);
            long time = processingPackets.Peek().Item1;

            for (int i = 1; i < packetsList.Count; i++)
            {
                if (packetsList[i].Item1 >= time + processingPackets.Peek().Item2)
                {
                    var temp = processingPackets.Dequeue();
                    result[temp.Item3] = time;
                    if (processingPackets.Count > 0)
                        time = Math.Max(temp.Item2 + time, processingPackets.Peek().Item1);
                    else
                        time = Math.Max(temp.Item2 + time, packetsList[i].Item1);
                }

                if (processingPackets.Count < bufferSize)
                    processingPackets.Enqueue(packetsList[i]);
                else
                    result[i] = -1;
            }
            while (processingPackets.Count > 0)
            {
                var temp = processingPackets.Dequeue();
                result[temp.Item3] = time;
                if (processingPackets.Count > 0)
                    time = Math.Max(temp.Item2 + time, processingPackets.Peek().Item1);
            }
            return result;
        }
    }
}
