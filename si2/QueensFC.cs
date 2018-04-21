using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace si2
{
    class QueensFC : Queens
    {
        private List<Tuple<int, int>> _freeSpots;

        public QueensFC() : base()
        {
            _freeSpots = new List<Tuple<int, int>>();
        }

        public QueensFC(int n) : base(n)
        {
            _freeSpots = new List<Tuple<int, int>>();
        }

        protected override void init()
        {
            base.init();
            for(int i = 0; i < _n; i++)
            {
                for(int j = 0; j < _n; j++)
                {
                    _freeSpots.Add(new Tuple<int, int>(i, j));
                }
            }
        }

        private List<Tuple<int, int>> getDiagIds1(int rowId, int colId)
        {
            List<Tuple<int, int>> out1 = new List<Tuple<int, int>>();
            int tempI = rowId - 1;
            int tempJ = colId - 1;
            while (tempI >= 0 && tempJ >= 0)
            {
                out1.Add(new Tuple<int, int>(tempI, tempJ));
                tempI--;
                tempJ--;
            }
            tempI = rowId + 1;
            tempJ = colId + 1;
            while (tempI < _n && tempJ < _n)
            {
                out1.Add(new Tuple<int, int>(tempI, tempJ));
                tempI++;
                tempJ++;
            }
            return out1;
        }

        private List<Tuple<int, int>> getDiagIds2(int rowId, int colId)
        {
            List<Tuple<int, int>> out1 = new List<Tuple<int, int>>();
            int tempI = rowId - 1;
            int tempJ = colId + 1;
            while (tempI >= 0 && tempJ < _n)
            {
                out1.Add(new Tuple<int, int>(tempI, tempJ));
                tempI--;
                tempJ++;
            }
            tempI = rowId + 1;
            tempJ = colId - 1;
            while (tempI < _n && tempJ >= 0)
            {
                out1.Add(new Tuple<int, int>(tempI, tempJ));
                tempI++;
                tempJ--;
            }
            return out1;
        }

        private void seizeSpots(int rowId, int colId)
        {
            for(int i = 0; i < _n; i++)
            {
                _freeSpots.Remove(new Tuple<int, int>(i, colId));
                _freeSpots.Remove(new Tuple<int, int>(rowId, i));
            }
            foreach(Tuple<int, int> t in getDiagIds1(rowId, colId))
            {
                _freeSpots.Remove(t);
            }
            foreach (Tuple<int, int> t in getDiagIds2(rowId, colId))
            {
                _freeSpots.Remove(t);
            }
        }

        private void seizeSpotsLeastConstraining(int rowId, int colId)
        {
            for (int i = 0; i < _n; i++)
            {
                _freeSpots.Remove(new Tuple<int, int>(i, colId));
                _freeSpots.Remove(new Tuple<int, int>(rowId, i));
            }
            foreach (Tuple<int, int> t in getDiagIds1(rowId, colId))
            {
                _freeSpots.Remove(t);
            }
            foreach (Tuple<int, int> t in getDiagIds2(rowId, colId))
            {
                _freeSpots.Remove(t);
            }
            _freeSpots.Sort(new TupleComparerLeastConstraining(_freeSpots));
        }

        private void freeSpots(int rowId, int colId)
        {
            for (int i = 0; i < _n; i++)
            {
                _freeSpots.Add(new Tuple<int, int>(rowId, i));
                if (i != rowId)
                {
                    _freeSpots.Add(new Tuple<int, int>(i, colId));
                }
            }
            foreach (Tuple<int, int> t in getDiagIds1(rowId, colId))
            {
                _freeSpots.Add(t);
            }
            foreach (Tuple<int, int> t in getDiagIds2(rowId, colId))
            {
                _freeSpots.Add(t);
            }
            foreach(Tuple<int, int> t in _queens)
            {
                seizeSpots(t.Item1, t.Item2);
            }
            _freeSpots.Sort(new TupleComparer());
        }

        private void freeSpotsLeastConstraining(int rowId, int colId)
        {
            for (int i = 0; i < _n; i++)
            {
                _freeSpots.Add(new Tuple<int, int>(rowId, i));
                if (i != rowId)
                {
                    _freeSpots.Add(new Tuple<int, int>(i, colId));
                }
            }
            foreach (Tuple<int, int> t in getDiagIds1(rowId, colId))
            {
                _freeSpots.Add(t);
            }
            foreach (Tuple<int, int> t in getDiagIds2(rowId, colId))
            {
                _freeSpots.Add(t);
            }
            foreach (Tuple<int, int> t in _queens)
            {
                seizeSpots(t.Item1, t.Item2);
            }
            _freeSpots.Sort(new TupleComparerLeastConstraining(_freeSpots));
        }

        private void relapse(out Tuple<int, int> currSpot, out int freeSpotId, ref int relapseCounter)
        {
            currSpot = _queens[_queens.Count - 1];
            _queens.RemoveAt(_queens.Count - 1);
            _square[currSpot.Item1, currSpot.Item2] = 0;
            freeSpots(currSpot.Item1, currSpot.Item2);

            freeSpotId = _freeSpots.IndexOf(currSpot) + 1;

            if (freeSpotId < _freeSpots.Count)
            {
                currSpot = _freeSpots[freeSpotId];
                seizeSpots(currSpot.Item1, currSpot.Item2);
            }
            else
            {
                relapse(out currSpot, out freeSpotId, ref relapseCounter);
            }
            relapseCounter++;
        }

        private void relapseLeastConstraining(out Tuple<int, int> currSpot, out int freeSpotId)
        {
            currSpot = _queens[_queens.Count - 1];
            _queens.RemoveAt(_queens.Count - 1);
            _square[currSpot.Item1, currSpot.Item2] = 0;
            freeSpotsLeastConstraining(currSpot.Item1, currSpot.Item2);

            freeSpotId = _freeSpots.IndexOf(currSpot) + 1;

            if (freeSpotId < _freeSpots.Count)
            {
                currSpot = _freeSpots[freeSpotId];
                seizeSpotsLeastConstraining(currSpot.Item1, currSpot.Item2);
            }
            else
            {
                relapseLeastConstraining(out currSpot, out freeSpotId);
            }
        }

        public void Forwardchecking()
        {
            init();
            int relapseCounter = 0;
            while (_queens.Count < _n)
            {
                Tuple<int, int> currSpot = _freeSpots[0];

                seizeSpots(currSpot.Item1, currSpot.Item2);

                int freeSpotId = 1;
                while(!_freeSpots.Any() && _queens.Count != _n - 1)
                {//nawrot
                    freeSpots(currSpot.Item1, currSpot.Item2);
                    if(freeSpotId < _freeSpots.Count)
                    {
                        currSpot = _freeSpots[freeSpotId];
                        seizeSpots(currSpot.Item1, currSpot.Item2);
                        freeSpotId++;
                    }
                    else
                    {
                        relapse(out currSpot, out freeSpotId, ref relapseCounter);
                    }
                    relapseCounter++;
                }
                _queens.Add(currSpot);
                _square[currSpot.Item1, currSpot.Item2] = 1;
            }
            Console.WriteLine("Relapses: " + relapseCounter);
        }

        public void ForwardcheckingLeastConstraining()
        {
            init();
            int relapseCounter = 0;
            while (_queens.Count < _n)
            {
                Tuple<int, int> currSpot = _freeSpots[0];

                seizeSpotsLeastConstraining(currSpot.Item1, currSpot.Item2);

                int freeSpotId = 1;
                while (!_freeSpots.Any() && _queens.Count != _n - 1)
                {//nawrot
                    freeSpots(currSpot.Item1, currSpot.Item2);
                    if (freeSpotId < _freeSpots.Count)
                    {
                        currSpot = _freeSpots[freeSpotId];
                        seizeSpots(currSpot.Item1, currSpot.Item2);
                        freeSpotId++;
                    }
                    else
                    {
                        relapse(out currSpot, out freeSpotId, ref relapseCounter);
                    }
                    relapseCounter++;
                }
                _queens.Add(currSpot);
                _square[currSpot.Item1, currSpot.Item2] = 1;
                _freeSpots.Sort(new TupleComparerLeastConstraining(_freeSpots));
            }
            Console.WriteLine("Relapses: " + relapseCounter);
        }
    }
}
