using System;
using System.IO;
using System.Collections;

namespace AOC2021
{
    class Day10
    {
        public void Run()
        {
            string[] data;
            int i;
            string symbol;
            string openers;
            int errors = 0;
            bool error;
            long fixes;
            ArrayList fixScores = new ArrayList();

            StreamReader sr = new StreamReader("day10.txt");
            data = sr.ReadToEnd().Replace("\r", "").Split('\n');

            foreach (string line in data)
            {
                openers = "";
                error = false;
                for (i = 0; i <= line.Length - 1; i++)
                {
                    symbol = line.Substring(i, 1);
                    if (IsAnOpen(symbol))
                        openers += symbol;
                    else
                    {
                        if (IsAClose(symbol))
                        {
                            if (IsValid(openers.Substring(openers.Length - 1, 1), symbol))
                                openers = openers.Substring(0, openers.Length - 1);
                            else
                            {
                                errors += ErrorValue(symbol);
                                error = true;
                                break;
                            }
                        }
                    }
                }
                if (!error)
                {
                    fixes = 0;
                    for (i = openers.Length - 1; i >= 0; i--)
                    {
                        symbol = openers.Substring(i, 1);
                        fixes *= 5;
                        fixes += FixValue(symbol);
                    }
                    fixScores.Add(fixes);
                }
            }

            fixScores.Sort();
            fixes = Convert.ToInt32(fixScores[(fixScores.Count / 2)]);

            Console.WriteLine("Day 10");
            Console.WriteLine("-----");
            Console.WriteLine("Part 1 Answer: " + errors.ToString());
            Console.WriteLine("Part 2 Answer: " + fixes.ToString());
            Console.WriteLine();
        }

        public bool IsAnOpen(string s)
        {
            return "([{<".IndexOf(s) != -1;
        }

        public bool IsAClose(string s)
        {
            return ")]}>".IndexOf(s) != -1;
        }

        public bool IsValid(string open, string close)
        {
            return ((open == "(" && close == ")") || (open == "[" && close == "]") || (open == "{" && close == "}") || (open == "<" && close == ">"));
        }

        public int ErrorValue(string close)
        {
            int result = 0;
            switch (close)
            {
                case ")": result = 3; break;
                case "]": result = 57; break;
                case "}": result = 1197; break;
                case ">": result = 25137; break;
            }
            return result;
        }

        public int FixValue(string open)
        {
            int result = 0;
            switch (open)
            {
                case "(": result = 1; break;
                case "[": result = 2; break;
                case "{": result = 3; break;
                case "<": result = 4; break;
            }
            return result;
        }
    }
}
