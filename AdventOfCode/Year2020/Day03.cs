using System;

namespace AdventOfCode.Year2020
{
    public class Day03 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2020/day/3

        /// Generic idea for Day 3
        /// This assignment is to learn about Modulo A%B=C
        /// to wrap the map, without copying

        public string SolvePart1(string input)
        {
            // Convert the grid to lines
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            return TobogganSlopes(lines).ToString();
        }

        public string SolvePart2(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            int r1d1 = TobogganSlopes(lines, 1, 1);
            int r3d1 = TobogganSlopes(lines, 3, 1);
            int r5d1 = TobogganSlopes(lines, 5, 1);
            int r7d1 = TobogganSlopes(lines, 7, 1);
            int r1d2 = TobogganSlopes(lines, 1, 2);

            // cast to long, as the answer is bigger then int32.MaxValue
            long multiplied = (long)r1d1 * (long)r3d1 * (long)r5d1 * (long)r7d1 * (long)r1d2;

            return multiplied.ToString();
        }

        public int TobogganSlopes(string[] map, int right = 3, int down = 1)
        {
            // keep tabs on your position (row, X), and the amount of Trees
            // Length of map/forest is necessary for the repetition, and the bottom/last line is needed for when to stop
            int row = 0, x = 0, trees = 0, mapLength = map[0].Length, bottom = map.Length;

            while (row < bottom)
            {
                // due to the modulo (%) the pointer will wrap around the line
                if (map[row][x % mapLength] == '#') { trees++; }
                x += right;
                row += down;
            }
            return trees;
        }

        public string GetInput()
        {
            return new Inputs.Year2020.Day03().Input;
        }
    }
}