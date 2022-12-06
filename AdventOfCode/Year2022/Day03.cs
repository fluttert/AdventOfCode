using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2022
{
    public class Day03 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2022/day/1

        public string SolvePart1(string input)
        {
            int sum = 0;
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                int half = line.Length / 2;
                // add all letters from the first half
                HashSet<char> compartment1 = new();
                for (int i = 0; i < half; i++)
                {
                    compartment1.Add(line[i]);
                }
                for (int i = half; i < line.Length; i++)
                {
                    if (compartment1.Contains(line[i]))
                    {
                        sum += char.IsUpper(line[i]) ? (line[i] - 'A' + 27) : (line[i] - 'a' + 1);
                        break;
                    }
                }
            }
            return "" + sum;
        }

        public string SolvePart2(string input)
        {
            int sum = 0;
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < lines.Length; i += 3)
            {
                HashSet<char> rucksack1 = new();
                foreach (char rs1 in lines[i]) { rucksack1.Add(rs1); }
                HashSet<char> rucksack2 = new();
                foreach (char rs2 in lines[i + 1])
                {
                    if (rucksack1.Contains(rs2)) { rucksack2.Add(rs2); }
                }
                // no hashset needed for last, should only be 1
                foreach (char rs3 in lines[i + 2])
                {
                    if (rucksack2.Contains(rs3))
                    {
                        sum += char.IsUpper(rs3) ? (rs3 - 'A' + 27) : (rs3 - 'a' + 1);
                        break;
                    }
                }
            }
            return "" + sum;
        }

        public string GetInput()
        {
            return new Inputs.Year2022.Day03().Input;
        }
    }
}