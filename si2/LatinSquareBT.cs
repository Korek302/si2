using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace si2
{
    class LatinSquareBT : LatinSquare
    {
        public LatinSquareBT() : base()
        { }

        public LatinSquareBT(int n) : base(n)
        { }

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

        private Tuple<int, int> mostConstrainedVar()
        {
            int counterMax = 0;
            Tuple<int, int> out1 = new Tuple<int, int>(0, 0);
            for(int i = 0; i < _n; i++)
            {
                for(int j = 0; j < _n; j++)
                {
                    if(_square[i, j] == 0)
                    {
                        int counter = 0;
                        for(int k = 0; k < _n; k++)
                        {
                            if(_square[k, j] == 0)
                            {
                                counter++;
                            }
                            if(_square[i, k] == 0)
                            {
                                counter++;
                            }
                        }
                        if(counter > counterMax)
                        {
                            out1 = new Tuple<int, int>(i, j);
                            counterMax = counter;
                        }
                    }
                }
            }
            return out1;
        }

        private Tuple<int, int> smallestDomain()
        {
            Tuple<int, int> out1 = new Tuple<int, int>(0, 0);
            List<int> outOfDomain = new List<int>();
            int maxShortage = 0; ;
            for (int i = 0; i < _n; i++)
            {
                for (int j = 0; j < _n; j++)
                {
                    if(_square[i, j] == 0)
                    {
                        outOfDomain.Clear();
                        for (int k = 0; k < _n; k++)
                        {
                            if (_square[k, j] != 0 && !outOfDomain.Contains(_square[k, j]))
                            {
                                outOfDomain.Add(_square[k, j]);
                            }
                            if (_square[i, k] != 0 && !outOfDomain.Contains(_square[k, j]))
                            {
                                outOfDomain.Add(_square[i, k]);
                            }
                        }
                        if(outOfDomain.Count > maxShortage)
                        {
                            out1 = new Tuple<int, int>(i, j);
                            maxShortage = outOfDomain.Count;
                        }
                    }
                }
            }


            return out1;
        }

        public void Backtracking()
        {
            int relapseCounter = 0;
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
                            while (k >= _n)
                            {//nawrot
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
                                relapseCounter++;
                            }
                            
                        }
                    }
                    //ShowSquare();
                }
            }
            Console.WriteLine("Relapses: " + relapseCounter);
        }

        public void BacktrackingSmallestDomain()
        {
            init();
            int relapseCounter = 0;
            List<Tuple<int, int>> varList = new List<Tuple<int, int>>();
            Stack<Tuple<int, int>> usedVars = new Stack<Tuple<int, int>>();
            for (int i = 0; i < _n; i++)
            {
                for (int j = 0; j < _n; j++)
                {
                    varList.Add(new Tuple<int, int>(i, j));
                }
            }
            while (varList.Any())
            {
                Tuple<int, int> currVarCoords = smallestDomain();
                int i = currVarCoords.Item1;
                int j = currVarCoords.Item2;
                for (int k = 1; k < _n + 1; k++)
                {
                    //Console.WriteLine(i + " " + j);
                    _square[i, j] = k;
                    if (check())
                    {
                        k = _n + 1;
                        varList.Remove(currVarCoords);
                        usedVars.Push(currVarCoords);
                    }
                    else
                    {
                        while (k == _n)
                        {//nawrot
                            _square[i, j] = 0;
                            currVarCoords = usedVars.Pop();
                            varList.Add(currVarCoords);
                            varList.Sort(new TupleComparer());
                            i = currVarCoords.Item1;
                            j = currVarCoords.Item2;
                            k = _square[i, j];
                            relapseCounter++;
                        }
                    }
                }
                //ShowSquare();
                //Console.WriteLine();
            }
            Console.WriteLine("Relapses: " + relapseCounter);
        }

        public void BacktrackingMostConstrained()
        {
            init();
            int relapseCounter = 0;
            List<Tuple<int, int>> varList = new List<Tuple<int, int>>();
            Stack<Tuple<int, int>> usedVars = new Stack<Tuple<int, int>>();
            for (int i = 0; i < _n; i++)
            {
                for (int j = 0; j < _n; j++)
                {
                    varList.Add(new Tuple<int, int>(i, j));
                }
            }
            while (varList.Any())
            {
                Tuple<int, int> currVarCoords = mostConstrainedVar();
                int i = currVarCoords.Item1;
                int j = currVarCoords.Item2;
                for (int k = 1; k < _n + 1; k++)
                {
                    //Console.WriteLine(i + " " + j);
                    _square[i, j] = k;
                    if (check())
                    {
                        k = _n + 1;
                        varList.Remove(currVarCoords);
                        usedVars.Push(currVarCoords);
                    }
                    else
                    {
                        while (k == _n)
                        {//nawrot
                            _square[i, j] = 0;
                            currVarCoords = usedVars.Pop();
                            varList.Add(currVarCoords);
                            varList.Sort(new TupleComparer());
                            i = currVarCoords.Item1;
                            j = currVarCoords.Item2;
                            k = _square[i, j];
                            relapseCounter++;
                        }
                    }
                }
                //ShowSquare();
                //Console.WriteLine();
            }
            Console.WriteLine("Relapses: " + relapseCounter);
        }

        public void Backtracking2()
        {
            init();
            int relapseCounter = 0;
            List<Tuple<int, int>> varList = new List<Tuple<int, int>>();
            Stack<Tuple<int, int>> usedVars = new Stack<Tuple<int, int>>();
            for (int i = 0; i < _n; i++)
            {
                for (int j = 0; j < _n; j++)
                {
                    varList.Add(new Tuple<int, int>(i, j));
                }
            }
            while (varList.Any())
            {
                Tuple<int, int> currVarCoords = varList[0];
                int i = currVarCoords.Item1;
                int j = currVarCoords.Item2;
                for (int k = 1; k < _n + 1; k++)
                {
                    //Console.WriteLine(i + " " + j);
                    _square[i, j] = k;
                    if (check())
                    {
                        k = _n + 1;
                        varList.Remove(currVarCoords);
                        usedVars.Push(currVarCoords);
                    }
                    else
                    {
                        while (k == _n)
                        {//nawrot
                            _square[i, j] = 0;
                            currVarCoords = usedVars.Pop();
                            varList.Add(currVarCoords);
                            varList.Sort(new TupleComparer());
                            i = currVarCoords.Item1;
                            j = currVarCoords.Item2;
                            k = _square[i, j];
                            relapseCounter++;
                        }
                    }
                }
                //ShowSquare();
                //Console.WriteLine();
            }
            Console.WriteLine("Relapses: " + relapseCounter);
        }
    }
}
