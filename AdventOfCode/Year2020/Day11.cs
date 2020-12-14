using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2020
{
    public class Day11 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2020/day/11

        /// Generic idea for Day 11

        public string SolvePart1(string input)
        {
            // read per line and convert to integer array
            int[] adapters = Array.ConvertAll(input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries), int.Parse);
            return "";
        }

        // in progress
        public string SolvePart2(string input)
        {
            // read per line
            int[] adapters = Array.ConvertAll(input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries), int.Parse);
            return "";
        }

        public string GetInput()
        {
            return new Inputs.Year2020.Day10().Input;
        }
    }
}