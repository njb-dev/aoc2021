using System;
using System.IO;
using System.Collections.Generic;

namespace AOC2021
{
    class Day11
    {
        public void Run()
        {
            int row;
            int col;
            Octopus[,] octopus;
            string[] data;
            int step = 0;
            int flashesAtStep100 = 0;

            StreamReader sr = new StreamReader("day11.txt");
            data = sr.ReadToEnd().Replace("\r", "").Split('\n');
            octopus = new Octopus[data.Length, data[0].Length];
            for (row = 0; row <= data.GetUpperBound(0); row++)
                for (col = 0; col <= data[row].Length - 1; col++)
                    octopus[row, col] = new Octopus(row, col, data.GetUpperBound(0), data[row].Length - 1, Convert.ToInt32(data[row].Substring(col, 1)));

            while (!AllHaveFlashed(octopus))
            {
                step++;

                for (row = 0; row <= octopus.GetUpperBound(0); row++)
                    for (col = 0; col <= octopus.GetUpperBound(1); col++)
                        octopus[row, col].Energy++;

                while (FlashesToDo(octopus))
                {
                    for (row = 0; row <= octopus.GetUpperBound(0); row++)
                        for (col = 0; col <= octopus.GetUpperBound(1); col++)
                        {
                            if (octopus[row, col].Energy > 9 && !octopus[row, col].HasFlashed)
                            {
                                Flash(octopus, row, col);
                                if (step <= 100)
                                    flashesAtStep100++;
                            }
                        }
                }

                for (row = 0; row <= octopus.GetUpperBound(0); row++)
                    for (col = 0; col <= octopus.GetUpperBound(1); col++)
                        octopus[row, col].Reset();
            }

            Console.WriteLine("Day 11");
            Console.WriteLine("-----");
            Console.WriteLine("Part 1 Answer: " + flashesAtStep100.ToString());
            Console.WriteLine("Part 2 Answer: " + step.ToString());
            Console.WriteLine();
        }

        private void Flash(Octopus[,] octopus, int row, int col)
        {
            int i;

            for (i = 0; i <= octopus[row, col].AdjacentRow.Count - 1; i++)
                octopus[octopus[row, col].AdjacentRow[i], octopus[row, col].AdjacentCol[i]].Energy++;

            octopus[row, col].HasFlashed = true;
        }

        private bool FlashesToDo(Octopus[,] octopus)
        {
            bool result = false;
            int row;
            int col;

            for (row = 0; row <= octopus.GetUpperBound(0); row++)
                for (col = 0; col <= octopus.GetUpperBound(1); col++)
                {
                    if (octopus[row, col].Energy > 9 && !octopus[row, col].HasFlashed)
                    {
                        result = true;
                        break;
                    }
                }
            return result;
        }

        private bool AllHaveFlashed(Octopus[,] octopus)
        {
            bool result = true;
            int row;
            int col;

            for (row = 0; row <= octopus.GetUpperBound(0); row++)
                for (col = 0; col <= octopus.GetUpperBound(1); col++)
                {
                    if (octopus[row, col].Energy != 0)
                    {
                        result = false;
                        break;
                    }
                }
            return result;
        }
    }

    class Octopus
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public int Energy { get; set; }
        public bool HasFlashed { get; set; }
        public List<int> AdjacentRow { get; set; }
        public List<int> AdjacentCol { get; set; }

        public Octopus(int row, int col, int maxRow, int maxCol, int energy)
        {
            Row = row;
            Col = col;
            Energy = energy;
            HasFlashed = false;

            AdjacentRow = new List<int>();
            AdjacentCol = new List<int>();

            if (row - 1 >= 0 && col - 1 >= 0)
            {
                AdjacentRow.Add(row - 1);
                AdjacentCol.Add(col - 1);
            }
            if (row - 1 >= 0)
            {
                AdjacentRow.Add(row - 1);
                AdjacentCol.Add(col);
            }
            if (row - 1 >= 0 && col + 1 <= maxCol)
            {
                AdjacentRow.Add(row - 1);
                AdjacentCol.Add(col + 1);
            }
            if (col - 1 >= 0)
            {
                AdjacentRow.Add(row);
                AdjacentCol.Add(col - 1);
            }
            if (col + 1 <= maxCol)
            {
                AdjacentRow.Add(row);
                AdjacentCol.Add(col + 1);
            }
            if (row + 1 <= maxRow && col - 1 >= 0)
            {
                AdjacentRow.Add(row + 1);
                AdjacentCol.Add(col - 1);
            }
            if (row + 1 <= maxRow)
            {
                AdjacentRow.Add(row + 1);
                AdjacentCol.Add(col);
            }
            if (row + 1 <= maxRow && col + 1 <= maxCol)
            {
                AdjacentRow.Add(row + 1);
                AdjacentCol.Add(col + 1);
            }
        }

        public void Reset()
        {
            HasFlashed = false;
            if (Energy > 9)
                Energy = 0;
        }
    }
}
