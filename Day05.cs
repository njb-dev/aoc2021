using System;
using System.IO;
using System.Collections;

namespace AOC2021
{
    class Day05
    {
        public void Run()
        {
            ArrayList Lines = new ArrayList();
            int maxX = 0;
            int maxY = 0;
            int minX = 999999;
            int minY = 999999;
            int[,] map1;
            int[,] map2;
            int x;
            int y;
            int start;
            int end;
            int mostDangerous1 = 0;
            int mostDangerous2 = 0;
            string data;

            StreamReader sr = new StreamReader("day05.txt");
            while (!sr.EndOfStream)
            {
                data = sr.ReadLine();
                if (data.Length > 0)
                {
                    Line l = new Line(data);
                    Lines.Add(l);
                    if (l.StartX > maxX)
                        maxX = l.StartX;
                    if (l.EndX > maxX)
                        maxX = l.EndX;
                    if (l.StartY > maxY)
                        maxY = l.StartY;
                    if (l.EndY > maxY)
                        maxY = l.EndY;
                    if (l.StartX < minX)
                        minX = l.StartX;
                    if (l.EndX < minX)
                        minX = l.EndX;
                    if (l.StartY < minY)
                        minY = l.StartY;
                    if (l.EndY < minY)
                        minY = l.EndY;
                }
            }
            map1 = new int[maxX + 1, maxY + 1];
            map2 = new int[maxX + 1, maxY + 1];

            foreach (Line line in Lines)
            {
                switch (line.Type)
                {
                    case "vertical":
                        {
                            x = line.StartX;
                            if (line.StartY > line.EndY)
                            {
                                start = line.EndY;
                                end = line.StartY;
                            }
                            else
                            {
                                start = line.StartY;
                                end = line.EndY;
                            }
                            for (y = start; y <= end; y++)
                            {
                                map1[x, y]++;
                                map2[x, y]++;
                            }
                            break;
                        }
                    case "horizontal":
                        {
                            y = line.StartY;
                            if (line.StartX > line.EndX)
                            {
                                start = line.EndX;
                                end = line.StartX;
                            }
                            else
                            {
                                start = line.StartX;
                                end = line.EndX;
                            }
                            for (x = start; x <= end; x++)
                            {
                                map1[x, y]++;
                                map2[x, y]++;
                            }
                            break;
                        }
                    case "diagonal":
                        {
                            x = line.StartX;
                            y = line.StartY;
                            while (true)
                            {
                                map2[x, y]++;
                                if (x != line.EndX && y != line.EndY)
                                {
                                    if (x < line.EndX)
                                        x++;
                                    else
                                        x--;
                                    if (y < line.EndY)
                                        y++;
                                    else
                                        y--;
                                }
                                else
                                    break;
                            }
                            break;
                        }
                }
            }

            for (y = 0; y <= map1.GetUpperBound(1); y++)
            {
                for (x = 0; x <= map1.GetUpperBound(0); x++)
                {
                    if (map1[x, y] > 1)
                        mostDangerous1++;
                }
            }

            for (y = 0; y <= map2.GetUpperBound(1); y++)
            {
                for (x = 0; x <= map2.GetUpperBound(0); x++)
                {
                    if (map2[x, y] > 1)
                        mostDangerous2++;
                }
            }

            Console.WriteLine("Day 5");
            Console.WriteLine("-----");
            Console.WriteLine("Part 1 Answer: " + mostDangerous1.ToString());
            Console.WriteLine("Part 2 Answer: " + mostDangerous2.ToString());
            Console.WriteLine();
        }
    }

    class Line
    {
        public int StartX { get; set; }
        public int StartY { get; set; }
        public int EndX { get; set; }
        public int EndY { get; set; }
        public string Type { get; set; }
        public Line(string specification)
        {
            StartX = Convert.ToInt32(specification.Replace(" -> ", "*").Split('*')[0].Split(',')[0]);
            StartY = Convert.ToInt32(specification.Replace(" -> ", "*").Split('*')[0].Split(',')[1]);
            EndX = Convert.ToInt32(specification.Replace(" -> ", "*").Split('*')[1].Split(',')[0]);
            EndY = Convert.ToInt32(specification.Replace(" -> ", "*").Split('*')[1].Split(',')[1]);
            if (StartX == EndX && StartY != EndY)
                Type = "vertical";
            else
            {
                if (StartY == EndY && StartX != EndX)
                    Type = "horizontal";
                else
                    Type = "diagonal";
            }
        }
    }
}
