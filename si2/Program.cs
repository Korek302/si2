using System;

namespace si2
{
    class Program
    {
        static void Main(string[] args)
        {
            const int N = 5;

            LatinSquareBT btInstance = new LatinSquareBT(N);
            btInstance.Backtracking();
            btInstance.ShowSquare();

            Console.WriteLine();

            LatinSquareFC fcInstance = new LatinSquareFC(N);
            fcInstance.Forwardchecking();
            fcInstance.ShowSquare();
        
        }
    }
}
