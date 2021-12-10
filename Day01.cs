using System;
using System.IO;

namespace AOC2021
{
    class Day01
    {
        public void Run()
        {
            int i;
            int singleIncreases = 0;
            int groupedIncreases = 0;
            StreamReader sr = new StreamReader("day01.txt");
            int[] measurement = Array.ConvertAll(sr.ReadToEnd().Replace("\n", ",").Split(','), int.Parse);

            for (i = 0; i <= measurement.GetUpperBound(0); i++)
            {
                if (i > 0 && measurement[i] > measurement[i - 1])
                    singleIncreases++;
                if (i > 2)
                {
                    int a = measurement[i - 1] + measurement[i - 2] + measurement[i - 3];
                    int b = measurement[i] + measurement[i - 1] + measurement[i - 2];
                    if (b > a)
                        groupedIncreases++;
                }
            }

            Console.WriteLine("Day 1");
            Console.WriteLine("-----");
            Console.WriteLine("Part 1 Answer: " + singleIncreases.ToString());
            Console.WriteLine("Part 2 Answer: " + groupedIncreases.ToString());
            Console.WriteLine();
        }
    }
}
