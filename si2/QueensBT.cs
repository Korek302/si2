using System;
using System.Collections.Generic;
using System.Linq;

namespace si2
{
    class QueensBT : Queens
    {
        public QueensBT() : base()
        { }

        public QueensBT(int n) : base(n)
        { }

        private bool check(int rowId, int colId)
        {
            if (!checkRow(getRow(rowId)))
            {
                return false;
            }
            if (!checkRow(getColumn(colId)))
            {
                return false;
            }
            if(!checkDiag(getDiag1(rowId, colId)))
            {
                return false;
            }
            if (!checkDiag(getDiag2(rowId, colId)))
            {
                return false;
            }
            return true;
        }

        private bool checkRow(int[] row)
        {
            for (int i = 0; i < row.Length; i++)
            {
                if(row[i] == 1)
                {
                    return false;
                }
            }
            return true;
        }

        private bool checkDiag(List<int> diag)
        {
            foreach(int i in diag)
            {
                if(i == 1)
                {
                    return false;
                }
            }
            return true;
        }

        public void Backtracking()
        {
            init();
            int relapseCounter = 0;
            int backCounterJ = 0;
            int backCounterI = 0;
            for (int i = 0; i < _n; i++)
            {
                for (int j = 0; j < _n; j++)
                {
                    if (check(i, j))
                    {
                        _square[i, j] = 1;
                        _queens.Add(new Tuple<int, int>(i, j));
                        if(_queens.Count == _n)
                        {
                            i = _n;
                            j = _n;
                            break;
                        }
                        backCounterJ = 1;
                        backCounterI = 1;
                    }
                    else
                    {
                        while(i == _n - 1 && j == _n - backCounterJ)
                        {//nawrot
                            i = _queens.Last().Item1;
                            j = _queens.Last().Item2;
                            _queens.RemoveAt(_queens.Count - 1);
                            _square[i, j] = 0;

                            if(backCounterJ == _n - 1)
                            {
                                backCounterI++;
                                backCounterJ = 1;
                            }
                            else
                            {
                                backCounterJ++;
                            }
                            relapseCounter++;
                        }
                    }
                }
            }
            Console.WriteLine("Relapses: " + relapseCounter);
        }
    }
}
