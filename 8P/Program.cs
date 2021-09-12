using System;

namespace _8P
{
    class Program
    {
        static void Main(string[] args)
        {
            var board = new Board(new int[3, 3] { { 0, 1, 3 }, { 4, 2, 5 }, { 7, 8, 6 } });

            var solver = new ASolver(board);

            if (!solver.IsSolvable())
            {
                Console.WriteLine(("No solution possible"));
            }
            else
            {
                foreach (var board1 in solver.Solution())
                {
                    Console.WriteLine(board1.TooString());
                    Console.WriteLine();
                }
            }
        }
    }
}
