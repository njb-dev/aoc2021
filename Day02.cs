using System;
using System.IO;

namespace AOC2021
{
    class Day02
    {
        public void Run()
        {
            int position = 0;
            int part1Depth = 0;
            int part2Depth = 0;
            int part2Aim = 0;
            int arg;
            StreamReader sr = new StreamReader("day02.txt");
            string[] commands = sr.ReadToEnd().Replace("\n", ",").Split(',');

            foreach (string cmd in commands)
            {
                arg = Convert.ToInt32(cmd.Split(' ')[1]);
                switch (cmd.Split(' ')[0].ToLower())
                {
                    case "forward":
                        {
                            position += arg;
                            part2Depth += (part2Aim * arg);
                            break;
                        }
                    case "down":
                        {
                            part1Depth += Convert.ToInt32(cmd.Split(' ')[1]);
                            part2Aim += arg;
                            break;
                        }
                    case "up":
                        {
                            part1Depth -= Convert.ToInt32(cmd.Split(' ')[1]);
                            part2Aim -= arg;
                            break;
                        }
                }
            }

            Console.WriteLine("Day 2");
            Console.WriteLine("-----");
            Console.WriteLine("Part 1 Answer: " + (position * part1Depth).ToString());
            Console.WriteLine("Part 2 Answer: " + (position * part2Depth).ToString());
            Console.WriteLine();
        }
    }
}
