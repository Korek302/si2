using System;
using System.Diagnostics;

namespace si2
{
    class Program
    {
        static void Main(string[] args)
        {
            const int N = 6;

            Stopwatch sw = new Stopwatch();

            LatinSquareBT btInstance = new LatinSquareBT(N);
            Console.WriteLine("Latin Square BT");
            sw.Start();
            btInstance.Backtracking();
            sw.Stop();
            Console.WriteLine("Time: " + sw.ElapsedMilliseconds);
            btInstance.ShowSquare();

            Console.WriteLine();

            sw.Reset();
            Console.WriteLine("Latin Square BT2");
            sw.Start();
            btInstance.Backtracking2();
            sw.Stop();
            Console.WriteLine("Time: " + sw.ElapsedMilliseconds);
            btInstance.ShowSquare();

            Console.WriteLine();

            sw.Reset();
            Console.WriteLine("Latin Square BT SmallestDomain");
            sw.Start();
            btInstance.BacktrackingSmallestDomain();
            sw.Stop();
            Console.WriteLine("Time: " + sw.ElapsedMilliseconds);
            btInstance.ShowSquare();

            Console.WriteLine();

            sw.Reset();
            Console.WriteLine("Latin Square BT MostConstrained");
            sw.Start();
            btInstance.BacktrackingMostConstrained();
            sw.Stop();
            Console.WriteLine("Time: " + sw.ElapsedMilliseconds);
            btInstance.ShowSquare();

            Console.WriteLine();

            LatinSquareFC fcInstance = new LatinSquareFC(N);
            sw.Reset();
            Console.WriteLine("Latin Square FC");
            sw.Start();
            fcInstance.Forwardchecking();
            sw.Stop();
            Console.WriteLine("Time: " + sw.ElapsedMilliseconds);
            fcInstance.ShowSquare();

            Console.WriteLine();

            QueensBT btQueens = new QueensBT(N);
            sw.Reset();
            Console.WriteLine("NQueens BT");
            sw.Start();
            btQueens.Backtracking();
            sw.Stop();
            Console.WriteLine("Time: " + sw.ElapsedMilliseconds);
            btQueens.ShowSquare();

            Console.WriteLine();

            QueensFC fcQueens = new QueensFC(N);

            sw.Reset();
            Console.WriteLine("NQueens FC");
            sw.Start();
            fcQueens.Forwardchecking();
            sw.Stop();
            Console.WriteLine("Time: " + sw.ElapsedMilliseconds);
            fcQueens.ShowSquare();

            Console.WriteLine();

            QueensFC fcQueens2 = new QueensFC(N);
            sw.Reset();
            Console.WriteLine("NQueens FC LeastConstraining");
            sw.Start();
            fcQueens2.ForwardcheckingLeastConstraining();
            sw.Stop();
            Console.WriteLine("Time: " + sw.ElapsedMilliseconds);
            fcQueens2.ShowSquare();
        }
    }
}
