using System;

namespace si2
{
    class Program
    {
        static void Main(string[] args)
        {
            const int N = 6;

            LatinSquareBT btInstance = new LatinSquareBT(N);
            btInstance.Backtracking();
            btInstance.ShowSquare();

            Console.WriteLine();

            LatinSquareFC fcInstance = new LatinSquareFC(N);
            fcInstance.Forwardchecking();
            fcInstance.ShowSquare();

            Console.WriteLine();

            QueensBT btQueens = new QueensBT(N);
            btQueens.Backtracking();
            btQueens.ShowSquare();

            Console.WriteLine();

            QueensFC fcQueens = new QueensFC(N);
            fcQueens.Forwardchecking();
            fcQueens.ShowSquare(); 
        }
    }
}
