using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace si2
{
    class Program
    {
        private int _n;
        private int[,] _square;

        public Program()
        {
            _n = 5;
            _square = new int[_n,_n];
        }

        public Program(int n)
        {
            _n = n;
            _square = new int[_n,_n];
        }

        public Program(int n, int[,] square)
        {
            _n = n;
            _square = square;
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

        public void Init()
        {
            for(int i = 0; i < _n; i++)
            {
                for(int j = 0; j < _n; j++)
                {
                    _square[i, j] = 0;
                }
            }
        }

        public bool Check()
        {
            for(int i = 0; i < _n; i++)
            {
                if(!checkRow(getRow(_square, i)))
                {
                    return false;
                }
            }
            for (int i = 0; i < _n; i++)
            {
                if (!checkRow(getColumn(_square, i)))
                {
                    return false;
                }
            }
            return true;
        }

        private bool checkRow(int[] row)
        {
            for(int i = 0; i < row.Length; i++)
            {
                for(int j = i; j < row.Length; j++)
                {
                    if(row[i] != 0 && row[j] != 0 && row[i] == row[j])
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
            for(int i = 0; i < _n; i++)
            {
                for(int j = 0; j < _n; j++)
                {
                    Console.Write(_square[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            int n = 5;
            int[,] square = new int[n, n];

            Program instance = new Program(n, square);

            bool flag1 = true;
            bool flag2 = true;
            bool flag3 = true;

            instance.Init();
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    for(int k = 0; k < n; k++)
                    {
                        instance.SetCell(k, i, j);
                        if(instance.Check())
                        {
                            k = n;
                        }
                        else
                        {
                            if(k == n - 1)
                            {
                                if(j == 0)
                                {
                                    if(i == 0)
                                    {
                                        Console.WriteLine("Brak rozwiazan");
                                    }
                                    else
                                    {
                                        instance.SetCell(0, i, j);
                                        i--;
                                        j = n - 1;
                                    }
                                }
                                else
                                {
                                    instance.GetSquare()[i, j] = 0;
                                    j--;
                                }
                                k = 0;
                            }
                        }
                    }
                }
            }
            instance.ShowSquare();

        }
    }
}
