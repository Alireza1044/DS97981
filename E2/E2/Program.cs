using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Q4TreeDiameter q = new Q4TreeDiameter(10, 0);
            q.TreeHeight(q.Nodes);
        }
    }
}
