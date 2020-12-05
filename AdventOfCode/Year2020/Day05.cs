using System;

namespace AdventOfCode.Year2020
{
    public class Day05 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2020/day/5

        public string SolvePart1(string input)
        {
            // read per line, and do NOT remove empty entries (e.g. empty lines)
            string[] lines = input.Split(Environment.NewLine);
            return string.Empty;
        }

        public string SolvePart2(string input)
        {
            // read per line
            string[] lines = input.Split(Environment.NewLine);

            return string.Empty;
        }

        // small pun on BinarySearch
        public int BinarySeats(string code)
        {
            int min = 1;
            int max = (int)Math.Pow(2, code.Length); // 2^3 = 8 || 2^7 = 128

            foreach (char half in code)
            {
                int difference = (max - min) / 2;
                if (half == 'F' || half == 'L')
                {
                    // keep lower half
                    max -= difference;
                }
                else
                {
                    min += difference;
                }
            }

            return min-1;
        }

        public string GetInput()
        {
            return new Inputs.Year2020.Day05().Input;
        }
    }
}