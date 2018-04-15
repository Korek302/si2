using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace si2
{
    class Queens
    {
        protected int _n;
        protected int[,] _square;
        protected List<Tuple<int, int>> _queens;

        public Queens()
        {
            _n = 5;
            _square = new int[_n, _n];
            _queens = new List<Tuple<int, int>>();
        }

        public Queens(int n)
        {
            _n = n;
            _square = new int[_n, _n];
            _queens = new List<Tuple<int, int>>();
        }

        public Queens(int n, int[,] square)
        {
            _n = n;
            _square = square;
            _queens = new List<Tuple<int, int>>();
        }

        public Queens(int[,] square)
        {
            _square = square;
            _n = _square.GetLength(0);
            _queens = new List<Tuple<int, int>>();
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

        protected int[] getRow(int index)
        {
            int[] _out = new int[_square.GetLength(1)];

            for (int i = 0; i < _square.GetLength(1); i++)
            {
                _out[i] = _square[index, i];
            }
            return _out;
        }

        protected int[] getColumn(int index)
        {
            int[] _out = new int[_square.GetLength(0)];

            for (int i = 0; i < _square.GetLength(0); i++)
            {
                _out[i] = _square[i, index];
            }
            return _out;
        }

        protected List<int> getDiag1(int rowId, int colId)
        {
            List<int> out1 = new List<int>();
            int tempI = rowId - 1;
            int tempJ = colId - 1;
            while (tempI >= 0 && tempJ >= 0)
            {
                out1.Add(_square[tempI, tempJ]);
                tempI--;
                tempJ--;
            }
            tempI = rowId + 1;
            tempJ = colId + 1;
            while (tempI < _n && tempJ < _n)
            {
                out1.Add(_square[tempI, tempJ]);
                tempI++;
                tempJ++;
            }
            return out1;
        }

        protected List<int> getDiag2(int rowId, int colId)
        {
            List<int> out1 = new List<int>();
            int tempI = rowId - 1;
            int tempJ = colId + 1;
            while (tempI >= 0 && tempJ < _n)
            {
                out1.Add(_square[tempI, tempJ]);
                tempI--;
                tempJ++;
            }
            tempI = rowId + 1;
            tempJ = colId - 1;
            while (tempI < _n && tempJ >= 0)
            {
                out1.Add(_square[tempI, tempJ]);
                tempI++;
                tempJ--;
            }
            return out1;
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
