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
                                while(k >= _n);
                            }
                        }
                    }
                    //ShowSquare();
                }
            }
        }
    }
}
