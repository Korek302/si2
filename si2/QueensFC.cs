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

        private void relapse1(int n)
        {
            Tuple<int, int> temp = _queens[_queens.Count - 1];
            _square[_queens[_queens.Count - 1].Item1, _queens[_queens.Count - 1].Item2] = 0;
            _queens.RemoveAt(_queens.Count - 1);
            freeSpots(temp.Item1, temp.Item2);
            
            if (_freeSpots.Count > 1)
            {
                _queens.Add(_freeSpots[n]);
                _square[_freeSpots[n].Item1, _freeSpots[n].Item2] = 1;
            }
            else
            {
                relapse1(n + 1);
            }
        }

        private void relapse(out Tuple<int, int> currSpot, out int freeSpotId)
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
                relapse(out currSpot, out freeSpotId);
            }
        }

        public void Forwardchecking()
        {
            init();
            while(_queens.Count < _n)
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
                        relapse(out currSpot, out freeSpotId);
                    }
                }
                _queens.Add(currSpot);
                _square[currSpot.Item1, currSpot.Item2] = 1;
            }
        }
    }
}
