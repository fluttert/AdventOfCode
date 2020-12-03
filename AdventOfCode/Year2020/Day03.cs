using System;

namespace AdventOfCode.Year2020
{
    public class Day03 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2020/day/3

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
            // keep tabs on your position (X,Y), and the amount of Trees
            // Length of map/forest is necessary for the repetition, and the bottom/last line is needed for when to stop
            int x = 0, y = 0, trees = 0, mapLength = map[0].Length, bottom = map.Length;

            while (x < bottom)
            {
                // due to the modulo (%) the pointer will wrap around the line
                if (map[x][y % mapLength] == '#') { trees++; }
                y += right;
                x += down;
            }

            return trees;
        }

        public string GetInput()
        {
            return new Inputs.Year2020.Day03().Input;
        }
    }
}