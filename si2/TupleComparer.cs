using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace si2
{
    class TupleComparer : IComparer<Tuple<int, int>>
    {
        public int Compare(Tuple<int, int> x, Tuple<int, int> y)
        {
            if(x.Item1 < y.Item1)
            {
                return -1;
            }
            else if(x.Item1 == y.Item1)
            {
                if(x.Item2 < y.Item2)
                {
                    return -1;
                }
                else if(x.Item2 == y.Item2)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                return 1;
            }
        }
    }
}
