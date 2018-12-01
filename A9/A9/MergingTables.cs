using TestCommon;
using System;
using System.Collections.Generic;
using System.Linq;

namespace A9
{
    public class MergingTables : Processor
    {
        public MergingTables(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long[], long[], long[], long[]>)Solve);

        public class Table
        {
            public int Size { get; set; }
            public int Link { get; set; }

            public Table(int size, int link)
            {
                Size = size;
                Link = link;
            }
        }

        public long[] Solve(long[] tableSizes, long[] sourceTables, long[] targetTables)
        {
            //Write your code here
            List<Table> tables = new List<Table>();
            tables.Add(new Table(0, 0));
            for (int i = 0; i < tableSizes.Length; i++)
            {
                tables.Add(new Table((int)tableSizes[i], i+1));
            }

            List<long> result = new List<long>();
            int temp =tables.OrderBy(x => x.Size).Last().Size;

            for (int i = 0; i < sourceTables.Length; i++)
            {
                while (tables[(int)sourceTables[i]].Link != sourceTables[i])
                {
                    sourceTables[i] = tables[(int)sourceTables[i]].Link;
                }
                
                while(tables[(int)targetTables[i]].Link != targetTables[i])
                {
                    targetTables[i] = tables[(int)targetTables[i]].Link;
                }

                if (targetTables[i] == sourceTables[i])
                {
                    result.Add(temp);
                    continue;
                }

                tables[(int)targetTables[i]].Size += tables[(int)sourceTables[i]].Size;

                tables[(int)sourceTables[i]].Size = 0;

                tables[(int)sourceTables[i]].Link = tables[(int)targetTables[i]].Link;
                if (tables[(int)targetTables[i]].Size > temp)
                    temp = tables[(int)targetTables[i]].Size;
                result.Add(temp);
            }
            return result.ToArray();
        }


    }
}