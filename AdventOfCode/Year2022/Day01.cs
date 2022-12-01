using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2022
{
    public class Day01 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2022/day/1

        public string SolvePart1(string input)
        {
            int highestCalory = 0, currentElf=0;
            string[] lines = input.Split(Environment.NewLine);
            for (int i = 0; i < lines.Length; i++) {
                if (string.IsNullOrEmpty(lines[i])) {
                    if (currentElf > highestCalory) { highestCalory = currentElf; }
                    currentElf = 0;
                    continue;
                }
                currentElf += int.Parse(lines[i]);
            }
            return ""+ highestCalory;
        }

        public string SolvePart2(string input)
        {
            List<int> elves = new();
            int currentElf = 0; 
            string[] lines = input.Split(Environment.NewLine);
            for (int i = 0; i < lines.Length; i++)
            {
                if (string.IsNullOrEmpty(lines[i]))
                {
                    elves.Add(currentElf);
                    currentElf = 0;
                    continue;
                }
                currentElf += int.Parse(lines[i]);
            }

            // First default sort, sorts the list in ASCENDING order; then simply reverse the list
            elves.Sort();
            elves.Reverse();

            return "" + (elves.Take(3)).Sum();
        }

        public string GetInput()
        {
            return new Inputs.Year2022.Day01().Input;
        }
    }
}