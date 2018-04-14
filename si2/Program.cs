using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace si2
{
    class Program
    {
        static void Main(string[] args)
        {
            const int N = 5;

            LatinSquare instance = new LatinSquare(N);
            instance.Backtracking();
            instance.ShowSquare();

            Console.WriteLine();

            LatinSquare fcInstance = new LatinSquare(N);
            fcInstance.Forwardchecking();
            fcInstance.ShowSquare();
        }
    }
}
