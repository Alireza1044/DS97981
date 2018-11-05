using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1
{
    public class Program
    {

        static void Main(string[] args)
        {
        }

        public static WordCount[] Load(string filePath)
            => File.ReadAllLines(filePath)
                    .Select(l =>
                    {
                        var toks = l.Split('\t', ' ');
                        return new WordCount(toks[0], ulong.Parse(toks[1]));
                    }).ToArray();

    }
}
