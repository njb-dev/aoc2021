using System;
using System.IO;
using System.Collections.Generic;

namespace AOC2021
{
    class Day09
    {
        public void Run()
        {
            int row;
            int col;
            int[,] map;
            string[] data;
            bool low;
            long risk = 0;
            List<Basin> basins = new List<Basin>();
            bool changes;
            int b;
            int p;
            int i;
            int[] biggest = new int[3];
            int max = 0;
            int maxSize;
            int threeLargest;

            StreamReader sr = new StreamReader("day09.txt");
            data = sr.ReadToEnd().Replace("\r", "").Split('\n');
            map = new int[data.Length, data[0].Length];
            for (row = 0; row <= data.GetUpperBound(0); row++)
                for (col = 0; col <= data[row].Length - 1; col++)
                    map[row, col] = Convert.ToInt32(data[row].Substring(col, 1));

            for (row = 0; row <= data.GetUpperBound(0); row++)
            {
                for (col = 0; col <= map.GetUpperBound(1); col++)
                {
                    low = true;
                    if (low && row > 0)
                        if (map[row - 1, col] <= map[row, col])
                            low = false;
                    if (low && row < map.GetUpperBound(0))
                        if (map[row + 1, col] <= map[row, col])
                            low = false;
                    if (low && col > 0)
                        if (map[row, col - 1] <= map[row, col])
                            low = false;
                    if (low && col < map.GetUpperBound(1))
                        if (map[row, col + 1] <= map[row, col])
                            low = false;
                    if (low)
                    {
                        risk += 1 + map[row, col];
                        Basin basin = new Basin(row, col);
                        basins.Add(basin);
                    }
                }
            }

            changes = true;
            while (changes)
            {
                changes = false;
                for (b = 0; b <= basins.Count - 1; b++)
                {
                    if (!basins[b].FullyChecked())
                    {
                        for (p = 0; p <= basins[b].Points.Count - 1; p++)
                        {
                            if (!basins[b].Points[p].Checked)
                            {
                                if (basins[b].Points[p].Row > 0)
                                    if (map[basins[b].Points[p].Row - 1, basins[b].Points[p].Col] != 9)
                                        changes = basins[b].AddPoint(basins[b].Points[p].Row - 1, basins[b].Points[p].Col);
                                if (basins[b].Points[p].Row < map.GetUpperBound(0))
                                    if (map[basins[b].Points[p].Row + 1, basins[b].Points[p].Col] != 9)
                                        changes = basins[b].AddPoint(basins[b].Points[p].Row + 1, basins[b].Points[p].Col);
                                if (basins[b].Points[p].Col > 0)
                                    if (map[basins[b].Points[p].Row, basins[b].Points[p].Col - 1] != 9)
                                        changes = basins[b].AddPoint(basins[b].Points[p].Row, basins[b].Points[p].Col - 1);
                                if (basins[b].Points[p].Col < map.GetUpperBound(1))
                                    if (map[basins[b].Points[p].Row, basins[b].Points[p].Col + 1] != 9)
                                        changes = basins[b].AddPoint(basins[b].Points[p].Row, basins[b].Points[p].Col + 1);
                                basins[b].Points[p].Checked = true;
                            }
                        }
                    }
                }
            }

            for (i = 0; i <= 2; i++)
            {
                maxSize = -1;
                for (b = 0; b <= basins.Count - 1; b++)
                {
                    if (basins[b].Size > maxSize && !basins[b].Selected)
                    {
                        maxSize = basins[b].Size;
                        max = b;
                    }
                }
                basins[max].Selected = true;
                biggest[i] = maxSize;
            }
            threeLargest = biggest[0] * biggest[1] * biggest[2];

            Console.WriteLine("Day 9");
            Console.WriteLine("-----");
            Console.WriteLine("Part 1 Answer: " + risk.ToString());
            Console.WriteLine("Part 2 Answer: " + threeLargest.ToString());
            Console.WriteLine();
        }
    }

    class Point
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public bool Checked { get; set; }
        public Point(int row, int col)
        {
            Row = row;
            Col = col;
            Checked = false;
        }
    }

    class Basin
    {
        public List<Point> Points { get; set; }
        public int Size
        {
            get { return Points.Count; }
        }
        public bool Selected { get; set; }
        public Basin(int row, int col)
        {
            Points = new List<Point>();
            Point p = new Point(row, col);
            Points.Add(p);
            Selected = false;
        }
        public bool AddPoint(int row, int col)
        {
            bool result = true;

            foreach (Point p in Points)
            {
                if (p.Row == row && p.Col == col)
                    result = false;
            }
            if (result)
                Points.Add(new Point(row, col));
            return result;
        }
        public bool FullyChecked()
        {
            bool result = true;

            foreach (Point p in Points)
            {
                if (!p.Checked)
                {
                    result = false;
                    break;
                }
            }
            return result;
        }
    }

}
