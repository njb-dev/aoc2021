using System;
using System.IO;
using System.Collections;

namespace AOC2021
{
    class Day04
    {
        public void Run()
        {
            ArrayList boards = new ArrayList();
            string boardNumbers = "";
            int ball;
            int board;
            int finalScore1 = 0;
            int finalScore2 = 0;

            StreamReader sr = new StreamReader("day04.txt");
            int[] bingoBall = Array.ConvertAll(sr.ReadLine().Split(','), int.Parse);

            while (!sr.EndOfStream)
            {
                string s = sr.ReadLine();
                if (s.Trim().Length > 0)
                {
                    boardNumbers += s.Trim().Replace("  ", " ").Replace(" ", ",") + ",";
                    if (boardNumbers.Split(',').Length >= 25)
                    {
                        if (boardNumbers.EndsWith(","))
                            boardNumbers = boardNumbers.Substring(0, boardNumbers.Length - 1);
                        BingoBoard b = new BingoBoard(boardNumbers);
                        boards.Add(b);
                        boardNumbers = "";
                    }
                }
            }

            for (ball = 0; ball <= bingoBall.GetUpperBound(0); ball++)
            {
                for (board = 0; board <= boards.Count - 1; board++)
                {
                    if (!((BingoBoard)boards[board]).Winner)
                    {
                        ((BingoBoard)boards[board]).MatchNumber(bingoBall[ball]);
                        if (((BingoBoard)boards[board]).IsAWinner())
                        {
                            ((BingoBoard)boards[board]).Winner = true;
                            if (finalScore1 == 0)
                            {
                                finalScore1 = bingoBall[ball] * ((BingoBoard)boards[board]).TotalUnmatched();
                                finalScore2 = finalScore1;
                            }
                            else
                                finalScore2 = bingoBall[ball] * ((BingoBoard)boards[board]).TotalUnmatched();
                        }
                    }
                }
            }

            Console.WriteLine("Day 4");
            Console.WriteLine("-----");
            Console.WriteLine("Part 1 Answer: " + finalScore1.ToString());
            Console.WriteLine("Part 2 Answer: " + finalScore2.ToString());
            Console.WriteLine();
        }
    }

    class BingoBoard
    {
        public struct TColumn
        {
            public int Number;
            public bool Matched;
        }

        public struct TRow
        {
            public TColumn[] Column;
        }

        public struct TBoard
        {
            public TRow[] Row;
        }

        public TBoard Board;
        public bool Winner { get; set; }

        public BingoBoard(string numbers)
        {
            int number = 0;
            int row;
            int col;
            Board = new TBoard()
            {
                Row = new TRow[5]
            };
            for (row = 0; row <= Board.Row.GetUpperBound(0); row++)
            {
                Board.Row[row].Column = new TColumn[5];
                for (col = 0; col <= Board.Row[row].Column.GetUpperBound(0); col++)
                {
                    Board.Row[row].Column[col] = new TColumn
                    {
                        Number = Convert.ToInt32(numbers.Split(',')[number]),
                        Matched = false
                    };
                    number++;
                }
            }
        }
        public bool MatchNumber(int number)
        {
            bool result = false;
            int row;
            int col;

            for (row = 0; row <= Board.Row.GetUpperBound(0); row++)
            {
                for (col = 0; col <= Board.Row[row].Column.GetUpperBound(0); col++)
                {
                    if (Board.Row[row].Column[col].Number == number)
                    {
                        Board.Row[row].Column[col].Matched = true;
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

        public bool IsAWinner()
        {
            bool result = false;
            int matches;
            int row;
            int col;

            for (row = 0; row <= Board.Row.GetUpperBound(0); row++)
            {
                matches = 0;
                for (col = 0; col <= Board.Row[row].Column.GetUpperBound(0); col++)
                {
                    if (Board.Row[row].Column[col].Matched)
                        matches++;
                }
                if (matches == Board.Row[row].Column.Length)
                {
                    result = true;
                    break;
                }
            }

            for (col = 0; col <= Board.Row[0].Column.GetUpperBound(0); col++)
            {
                matches = 0;
                for (row = 0; row <= Board.Row.GetUpperBound(0); row++)
                {
                    if (Board.Row[row].Column[col].Matched)
                        matches++;
                }
                if (matches == Board.Row.Length)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public int TotalUnmatched()
        {
            int result = 0;
            int row;
            int col;

            for (row = 0; row <= Board.Row.GetUpperBound(0); row++)
            {
                for (col = 0; col <= Board.Row[row].Column.GetUpperBound(0); col++)
                {
                    if (!Board.Row[row].Column[col].Matched)
                        result += Board.Row[row].Column[col].Number;
                }
            }
            return result;
        }
    }
}
