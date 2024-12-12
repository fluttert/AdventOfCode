using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AdventOfCode.Year2024
{
    public class Day11 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2024/day/11

        public string SolvePart1(string input)
        {
            string cur = input;
            for (int i = 0; i < 25; i++)
            {
                StringBuilder sb = new StringBuilder();
                string[] stones = cur.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < stones.Length; j++)
                {
                    // zero stone becomes 1
                    if (stones[j] == "0") { sb.Append("1 "); continue; }
                    // even digit become 2 stones
                    if (stones[j].Length % 2 == 0)
                    {
                        string splitStone = stones[j];
                        int half = stones[j].Length / 2;
                        string stone1 = stones[j][0..half];
                        string stone2 = stones[j][(half)..];

                        sb.Append(long.Parse(stone1) + " ");
                        sb.Append(long.Parse(stone2) + " ");

                        continue;
                    }
                    // else multiply
                    sb.Append(long.Parse(stones[j]) * 2024 + " ");
                }
                cur = sb.ToString();
                //Console.WriteLine(cur);
            }
            // count the sape
            return "" + (cur.Count(e => e == ' '));
        }

        public string SolvePart2(string input)
        {
            Dictionary<string, long> stones = new();
            var inputStones = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            long amountOfStones = 0;
            foreach (var s in inputStones) { stones.Add(s, 1); amountOfStones++; }
            // apply memoization
            for (int i = 0; i < 75; i++)
            {
                amountOfStones = 0;
                Dictionary<string, long> update = new();
                foreach (var kvp in stones)
                {
                    string curStone = kvp.Key;
                    long amount = kvp.Value;
                    if (curStone == "0")
                    {
                        if (update.ContainsKey("1") is false) { update.Add("1", 0); }
                        update["1"]+=amount;
                        amountOfStones += amount;
                        continue;
                    }
                    if (curStone.Length % 2 == 0)
                    {
                        int half = curStone.Length / 2;
                        string stone1 = ""+long.Parse(curStone[0..half]);
                        string stone2 = ""+long.Parse(curStone[(half)..]);
                        if (update.ContainsKey(stone1) is false) { update.Add(stone1, 0); }
                        if (update.ContainsKey(stone2) is false) { update.Add(stone2, 0); }
                        update[stone1] += amount;
                        update[stone2] += amount;
                        amountOfStones += amount+amount;

                        continue;
                    }
                    // else multiply
                    curStone = "" + long.Parse(curStone) * 2024;
                    if (update.ContainsKey(curStone) is false) { update.Add(curStone, 0); }
                    update[curStone] += amount;
                    amountOfStones += amount;
                }

                stones = update;

            }
            // count the sape
            return "" + amountOfStones;
        }

        public string GetInput()
        {
            var file_contents = File.ReadAllText("./input.txt");
            return file_contents;
        }
    }
}