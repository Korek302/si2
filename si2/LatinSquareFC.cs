using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace si2
{

    class LatinSquareFC : LatinSquare
    {
        private Dictionary<Tuple<int, int>, List<int>> _domains;

        public LatinSquareFC() : base()
        {
            _domains = new Dictionary<Tuple<int, int>, List<int>>();
        }

        public LatinSquareFC(int n) : base(n)
        {
            _domains = new Dictionary<Tuple<int, int>, List<int>>();
        }

        protected override void init()
        {
            base.init();
            for (int i = 0; i < _n; i++)
            {
                for (int j = 0; j < _n; j++)
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
            if (currJ < _n - 1)
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
                bool ok = true;
                for (int j = currJ + 1; j < _n; j++)
                {
                    for(int k = 0; k < _n; k++)
                    {
                        if(_square[k, j] == val)
                        {
                            ok = false;
                        }
                    }
                    if (ok)
                    {
                        _domains[new Tuple<int, int>(currI, j)].Add(val);
                        _domains[new Tuple<int, int>(currI, j)].Sort();
                    }
                }
            }

        }

        private bool isEmptyDomains()
        {
            for (int i = 0; i < _n; i++)
            {
                for (int j = 0; j < _n; j++)
                {
                    if (_domains[new Tuple<int, int>(i, j)].Count == 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void Forwardchecking()
        {
            init();
            int relapseCounter = 0;
            int currDomainId = 0;
            for (int i = 0; i < _n; i++)
            {
                for (int j = 0; j < _n; j++)
                {
                    _square[i, j] = _domains[new Tuple<int, int>(i, j)][currDomainId];
                    trimDomains(i, j, _square[i, j]);

                    if (isEmptyDomains())
                    {//nawrot
                        restoreDomains(i, j, _square[i, j]);
                        currDomainId++;
                        while(currDomainId >= _domains[new Tuple<int, int>(i, j)].Count)
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
                            relapseCounter++;
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
            Console.WriteLine("Relapses: " + relapseCounter);
        }
    }
}
