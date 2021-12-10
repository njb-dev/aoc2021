using System;
using System.IO;

namespace AOC2021
{
    class Day06
    {
        public void Run()
        {
            long[] school = new long[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int day;
            long totalFish;
            long saveZero;
            long after80Days = 0;
            long after256Days = 0;
            StreamReader sr;
            int i;

            sr = new StreamReader("day06.txt");
            string data = sr.ReadToEnd();
            foreach (string s in data.Split(','))
                school[Convert.ToInt64(s)]++;

            for (day = 1; day <= 256; day++)
            {
                saveZero = school[0];
                for (i = 0; i <= school.GetUpperBound(0) - 1; i++)
                    school[i] = school[i + 1];
                school[8] = saveZero;
                school[6] += saveZero;

                if (day == 80 || day == 256)
                {
                    totalFish = 0;
                    for (i = 0; i <= school.GetUpperBound(0); i++)
                        totalFish += school[i];
                    if (day == 80)
                        after80Days = totalFish;
                    else
                        after256Days = totalFish;
                }
            }
            Console.WriteLine("Day 6");
            Console.WriteLine("-----");
            Console.WriteLine("Part 1 Answer: " + after80Days.ToString());
            Console.WriteLine("Part 2 Answer: " + after256Days.ToString());
            Console.WriteLine();
        }

    }
}
