using System;
using System.IO;
using System.Collections;

namespace AOC2021
{
    class Day03
    {
        public void Run()
        {
            StreamReader sr = new StreamReader("day03.txt");
            string[] diagnostics = sr.ReadToEnd().Replace("\n", ",").Split(',');
            int[] binary = new int[12] { 2048, 1024, 512, 256, 128, 64, 32, 16, 8, 4, 2, 1 };
            int[] zeroCount = new int[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int[] oneCount = new int[12] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int i;
            string gammaBinary = "";
            string epsilonBinary = "";
            int gamma = 0;
            int epsilon = 0;
            string oxygenBinary;
            string co2Binary;
            int oxygen = 0;
            int co2 = 0;
            ArrayList oxygenList = new ArrayList();
            ArrayList co2List = new ArrayList();
            ArrayList oxygenList2 = new ArrayList();
            ArrayList co2List2 = new ArrayList();
            string keep;
            int bit;
            int zero;
            int one;

            foreach (string value in diagnostics)
            {
                for (i = 0; i <= value.Length - 1; i++)
                {
                    if (value.Substring(i, 1) == "0")
                        zeroCount[i]++;
                    else
                        oneCount[i]++;
                }
            }

            for (i = 0; i <= zeroCount.GetUpperBound(0); i++)
            {
                if (zeroCount[i] == oneCount[i])
                    throw new Exception("Incorrect data");
                else
                {
                    if (zeroCount[i] > oneCount[i])
                    {
                        gammaBinary += "0";
                        epsilonBinary += "1";
                    }
                    else
                    {
                        gammaBinary += "1";
                        epsilonBinary += "0";
                    }
                }
            }

            if (oneCount[0] >= zeroCount[0])
                keep = "1";
            else
                keep = "0";
            foreach (string value in diagnostics)
            {
                if (value.Substring(0, 1) == keep)
                    oxygenList.Add(value);
                else
                    co2List.Add(value);
            }

            bit = 1;
            while (true)
            {
                if (oxygenList.Count > 1)
                {
                    oxygenList2.Clear();
                    zero = 0;
                    one = 0;
                    foreach (string value in oxygenList)
                    {
                        if (value.Substring(bit, 1) == "0")
                            zero++;
                        else
                            one++;
                    }
                    if (one == zero)
                        keep = "1";
                    else
                    {
                        if (one > zero)
                            keep = "1";
                        else
                            keep = "0";
                    }
                    for (i = 0; i <= oxygenList.Count - 1; i++)
                    {
                        if (oxygenList[i].ToString().Substring(bit, 1) == keep)
                            oxygenList2.Add(oxygenList[i]);
                    }
                    oxygenList.Clear();
                    foreach (string value in oxygenList2)
                        oxygenList.Add(value);
                }
                if (co2List.Count > 1)
                {
                    co2List2.Clear();
                    zero = 0;
                    one = 0;
                    foreach (string value in co2List)
                    {
                        if (value.Substring(bit, 1) == "0")
                            zero++;
                        else
                            one++;
                    }
                    if (one == zero)
                        keep = "0";
                    else
                    {
                        if (one < zero)
                            keep = "1";
                        else
                            keep = "0";
                    }
                    for (i = 0; i <= co2List.Count - 1; i++)
                    {
                        if (co2List[i].ToString().Substring(bit, 1) == keep)
                            co2List2.Add(co2List[i]);
                    }
                    co2List.Clear();
                    foreach (string value in co2List2)
                        co2List.Add(value);
                }

                bit++;
                if (oxygenList.Count == 1 && co2List.Count == 1)
                {
                    oxygenBinary = oxygenList[0].ToString();
                    co2Binary = co2List[0].ToString();
                    break;
                }
            }

            for (i = 0; i <= gammaBinary.Length - 1; i++)
            {
                if (gammaBinary.Substring(i, 1) == "1")
                    gamma += binary[i];
                if (epsilonBinary.Substring(i, 1) == "1")
                    epsilon += binary[i];
                if (oxygenBinary.Substring(i, 1) == "1")
                    oxygen += binary[i];
                if (co2Binary.Substring(i, 1) == "1")
                    co2 += binary[i];
            }

            Console.WriteLine("Day 3");
            Console.WriteLine("-----");
            Console.WriteLine("Part 1 Answer: " + (gamma * epsilon).ToString());
            Console.WriteLine("Part 2 Answer: " + (oxygen * co2).ToString());
            Console.WriteLine();
        }
    }
}
