using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace si2
{
    class LatinSquare
    {
        protected int _n;
        protected int[,] _square;

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

        public void SetSquare(int[,] square)
        {
            _square = square;
        }

        protected virtual void init()
        {
            for (int i = 0; i < _n; i++)
            {
                for (int j = 0; j < _n; j++)
                {
                    _square[i, j] = 0;
                }
            }
        }

        protected int[] getRow(int[,] array, int index)
        {
            int[] _out = new int[array.GetLength(1)];

            for (int i = 0; i < array.GetLength(1); i++)
            {
                _out[i] = array[index, i];
            }
            return _out;
        }

        protected int[] getColumn(int[,] array, int index)
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
    }
}
