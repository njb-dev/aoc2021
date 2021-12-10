using System;
using System.IO;

namespace AOC2021
{
    class Day07
    {
        public void Run()
        {
            int min = 999999;
            int max = 0;
            int i;
            int j;
            int k;
            int moves;
            int totalMoves;
            long fuel;
            int minMoves = 999999;
            long minFuel = 9999999999;

            StreamReader sr = new StreamReader("day07.txt");
            int[] position = Array.ConvertAll(sr.ReadLine().Split(','), int.Parse);

            for (i = 0; i <= position.GetUpperBound(0); i++)
            {
                if (position[i] < min)
                    min = position[i];
                if (position[i] > max)
                    max = position[i];
            }

            for (i = min; i <= max; i++)
            {
                totalMoves = 0;
                fuel = 0;
                for (j = 0; j <= position.GetUpperBound(0); j++)
                {
                    moves = Math.Abs(i - position[j]);
                    totalMoves += moves;
                    for (k = 1; k <= moves; k++)
                        fuel += k;
                }
                if (totalMoves < minMoves)
                    minMoves = totalMoves;
                if (fuel < minFuel)
                    minFuel = fuel;
            }

            Console.WriteLine("Day 7");
            Console.WriteLine("-----");
            Console.WriteLine("Part 1 Answer: " + minMoves.ToString());
            Console.WriteLine("Part 2 Answer: " + minFuel.ToString());
            Console.WriteLine();
        }
    }
}
