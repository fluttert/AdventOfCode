using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2021
{
    public class Day01 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2021/day/1

        public string SolvePart1(string input)
        {
            int[] lines = Array.ConvertAll(input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries), int.Parse);
            int increased = 0;
            for (int i = 1; i < lines.Length; i++)
            {
                if (lines[i - 1] < lines[i]) {
                    increased++;
                }
            }

            return ""+increased;
        }

        public string SolvePart2(string input)
        {
            int[] lines = Array.ConvertAll(input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries), int.Parse);
            int increased = 0;

            // Insight A + B + c < B + C + D == A <
            // D
            for (int i = 3; i < lines.Length; i++)
            {
                if (lines[i - 3] < lines[i])
                {
                    increased++;
                }
            }

            return "" + increased;
        }

        public string GetInput()
        {
            return new Inputs.Year2021.Day01().Input;
        }
    }
}