using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace si2
{
    class TupleComparerLeastConstraining : IComparer<Tuple<int, int>>
    {
        private List<Tuple<int, int>> _freeSpots;

        public TupleComparerLeastConstraining(List<Tuple<int, int>> freeSpots)
        {
            _freeSpots = freeSpots;
        }

        public int Compare(Tuple<int, int> x, Tuple<int, int> y)
        {
            int temp1 = numOfConstraining(x);
            int temp2 = numOfConstraining(y);

            if (temp1 < temp2)
            {
                return -1;
            }
            else
            {
                return temp1 == temp2 ? 0 : 1;
            }
        }

        private int numOfConstraining(Tuple<int, int> tuple)
        {
            int counter = 0;
            foreach(Tuple<int, int> t in _freeSpots)
            {
                if(t.Item1 == tuple.Item1)
                {
                    counter++;
                }
                if(t.Item2 == tuple.Item2)
                {
                    counter++;
                }
                if(t.Item1 - tuple.Item1 == t.Item2 - tuple.Item2)
                {
                    counter++;
                }
                if(t.Item1 + t.Item2 == tuple.Item1 + tuple.Item2)
                {
                    counter++;
                }
            }
            
            return counter;
        }
    }

}
