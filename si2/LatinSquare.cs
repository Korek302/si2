using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace si2
{
    class LatinSquare
    {
        private int _n;
        private int[,] _square;

        public LatinSquare()
        {
            _n = 5;
            _square = new int[_n, _n];
        }

        public LatinSquare(int n)
        {
            _n = n;
            _square = new int[_n, _n];
        }

        public LatinSquare(int n, int[,] square)
        {
            _n = n;
            _square = square;
        }

        public LatinSquare(int[,] square)
        {
            _square = square;
            _n = _square.GetLength(0);
        }

        public int GetN()
        {
            return _n;
        }

        public int[,] GetSquare()
        {
            return _square;
        }
        public void SetN(int n)
        {
            _n = n;
        }

        public void SetCell(int value, int i, int j)
        {
            _square[i, j] = value;
        }

        public void SetSquare(int[,] square)
        {
            _square = square;
        }

        private void init()
        {
            for (int i = 0; i < _n; i++)
            {
                for (int j = 0; j < _n; j++)
                {
                    _square[i, j] = 0;
                }
            }
        }

        private bool check()
        {
            for (int i = 0; i < _n; i++)
            {
                if (!checkRow(getRow(_square, i)))
                {
                    return false;
                }
                if (!checkRow(getColumn(_square, i)))
                {
                    return false;
                }
            }
            return true;
        }

        private bool checkRow(int[] row)
        {
            for (int i = 0; i < row.Length; i++)
            {
                for (int j = i + 1; j < row.Length; j++)
                {
                    if (row[i] != 0 && row[j] != 0 && row[i] == row[j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private int[] getRow(int[,] array, int index)
        {
            int[] _out = new int[array.GetLength(1)];

            for (int i = 0; i < array.GetLength(1); i++)
            {
                _out[i] = array[index, i];
            }
            return _out;
        }

        private int[] getColumn(int[,] array, int index)
        {
            int[] _out = new int[array.GetLength(0)];

            for (int i = 0; i < array.GetLength(0); i++)
            {
                _out[i] = array[i, index];
            }
            return _out;
        }

        public void ShowSquare()
        {
            for (int i = 0; i < _n; i++)
            {
                for (int j = 0; j < _n; j++)
                {
                    Console.Write(_square[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        public void Backtracking()
        {
            init();
            for (int i = 0; i < _n; i++)
            {
                for (int j = 0; j < _n; j++)
                {
                    for (int k = 1; k < _n + 1; k++)
                    {
                        _square[i, j] = k;
                        if (check())
                        {
                            k = _n + 1;
                        }
                        else
                        {
                            if (k == _n)
                            {
                                _square[i, j] = 0;
                                if (j == 0)
                                {
                                    if (i == 0)
                                    {
                                        Console.WriteLine("Brak rozwiazan");
                                        i = _n;
                                        j = _n;
                                        k = _n + 1;
                                    }
                                    else
                                    {
                                        i--;
                                        j = _n - 1;
                                    }
                                }
                                else
                                {
                                    j--;
                                }
                                k = _square[i, j];
                            }
                        }
                    }
                }
            }
        }

        private Dictionary<Tuple<int, int>, List<int>> _domains; 

        private void initFC()
        {
            init();
            _domains = new Dictionary<Tuple<int, int>, List<int>>();
            for(int i = 0; i < _n; i++)
            {
                for(int j = 0; j < _n; j++)
                {
                    _domains.Add(new Tuple<int, int>(i, j), 
                        new List<int>(Enumerable.Range(1, _n).ToArray()));
                }
            }
        }

        private void trimDomains(int currI, int currJ, int val)
        {
            if (currI < _n - 1)
            {
                for (int i = currI + 1; i < _n; i++)
                {
                    _domains[new Tuple<int, int>(i, currJ)].Remove(val);
                }
            }
            if(currJ < _n - 1)
            {
                for (int j = currJ + 1; j < _n; j++)
                {

                    _domains[new Tuple<int, int>(currI, j)].Remove(val);
                }
            }
        }

        private void restoreDomains(int currI, int currJ, int val)
        {
            if (currI < _n - 1)
            {
                for (int i = currI + 1; i < _n; i++)
                {
                    _domains[new Tuple<int, int>(i, currJ)].Add(val);
                    _domains[new Tuple<int, int>(i, currJ)].Sort();
                }
            }
            if (currJ < _n - 1)
            {
                for (int j = currJ + 1; j < _n; j++)
                {

                    _domains[new Tuple<int, int>(currI, j)].Add(val);
                    _domains[new Tuple<int, int>(currI, j)].Sort();
                }
            }

        }

        private bool isEmptyDomains()
        {
            for(int i = 0; i < _n; i++)
            {
                for (int j = 0; j < _n; j++)
                {
                    if(_domains[new Tuple<int, int>(i, j)].Count == 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void Forwardchecking()
        {
            initFC();
            int currDomainId = 0;
            for(int i = 0; i < _n; i++)
            {
                for(int j = 0; j < _n; j++)
                {
                    _square[i, j] = _domains[new Tuple<int, int>(i,j)][currDomainId];
                    trimDomains(i, j, _square[i, j]);

                    if(isEmptyDomains())
                    {
                        restoreDomains(i, j, _square[i, j]);
                        currDomainId++;
                        if(currDomainId >= _domains[new Tuple<int, int>(i, j)].Count)
                        {
                            do
                            {
                                _square[i, j] = 0;
                                if (j == 0)
                                {
                                    if (i == 0)
                                    {
                                        Console.WriteLine("Brak rozwiazan");
                                        i = _n;
                                        j = _n;
                                        break;
                                    }
                                    else
                                    {
                                        i--;
                                        j = _n - 1;
                                    }
                                }
                                else
                                {
                                    j--;
                                }
                                currDomainId = _domains[new Tuple<int, int>(i, j)].IndexOf(_square[i, j]) + 1;
                                restoreDomains(i, j, _square[i, j]);
                            } while (currDomainId >= _domains[new Tuple<int, int>(i, j)].Count);
                        }

                        if (j == 0)
                        {
                            if (i == 0)
                            {
                                Console.WriteLine("Brak rozwiazan");
                                i = _n;
                                j = _n;
                                break;
                            }
                            else
                            {
                                i--;
                                j = _n - 1;
                            }
                        }
                        else
                        {
                            j--;
                        }
                    }
                    else
                    {
                        currDomainId = 0;
                    }
                }
            }
        }
    }
}
