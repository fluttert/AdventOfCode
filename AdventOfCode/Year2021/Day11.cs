using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2021
{
    public class Day11 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2021/day/11

        /**
         * Key Insights
         * 1: Remember Day 9!
         * 2: The size of the grid is fixed (10x10)
         * 3: There is no grid, just Octopuses with a position (x,y)
         *
         */

        public string SolvePart1(string input)
        {
            // Convert input to something usable
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            // dictionary with Tuple X,Y as key, and the value = value
            var octupuses = new Dictionary<(int row, int column), int>();
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    // Trick 1: a string is an array of characters
                    // Trick 2: char has an integer mapped to it, simply subtracting '0' will get the integer value we seek
                    octupuses.Add((i, j), lines[i][j] - '0');
                }
            }

            // grid is filled, now step through
            int totalFlashes = 0;                   // end-result
            for (int step = 0; step < 100; step++)  // iterate through the field X-times
            {
                // queue is all that is flashing
                Queue<(int x, int y)> q = new Queue<(int, int)>();
                // hashset is needed to make sure we only flash once
                HashSet<(int x, int y)> flashed = new HashSet<(int, int)>();

                // increase all the values by 1
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        octupuses[(i, j)]++;
                        if (octupuses[(i, j)] > 9)
                        {
                            q.Enqueue((i, j));
                        }
                    }
                }

                while (q.Count > 0)
                {
                    var oct = q.Dequeue();
                    int x = oct.x, y = oct.y;

                    // ignore if already flashes, outside the grid, or simply not needed to flash
                    if (flashed.Contains(oct) || octupuses[oct] <= 9) { continue; }
                    //Console.WriteLine($"FLASHING oct[{x},{y}], with value: {octupuses[oct]}");

                    // flash and increase surrounding octopuses
                    flashed.Add(oct);

                    // add the neighbours
                    for (int i = -1; i < 2; i++)
                    {
                        for (int j = -1; j < 2; j++)
                        {
                            // updated X and Y coordinate
                            int ux = x + i, uy = y + j;
                            // ignore out-of-bounds
                            if (ux < 0 || ux > 9 || uy < 0 || uy > 9) { continue; }
                            octupuses[(ux, uy)]++;
                            q.Enqueue((ux, uy));
                        }
                    }
                }

                //set all flashed octopuses to 0
                foreach (var oct in flashed) { octupuses[(oct.x, oct.y)] = 0; }

                // update the flash totals
                totalFlashes += flashed.Count;
            }

            return "" + totalFlashes;
        }

        public string SolvePart2(string input)
        {
            // Convert input to something usable
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            // dictionary with Tuple X,Y as key, and the value = value
            var octupuses = new Dictionary<(int row, int column), int>();
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    // Trick 1: a string is an array of characters
                    // Trick 2: char has an integer mapped to it, simply subtracting '0' will get the integer value we seek
                    octupuses.Add((i, j), lines[i][j] - '0');
                }
            }

            // grid is filled, now step through
            int finalStep = 0;                   // end-result
            for (int step = 1; step < 1000; step++)  // iterate through the field X-times
            {
                // queue is all that is flashing
                Queue<(int x, int y)> q = new Queue<(int, int)>();
                // hashset is needed to make sure we only flash once
                HashSet<(int x, int y)> flashed = new HashSet<(int, int)>();

                // increase all the values by 1
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        octupuses[(i, j)]++;
                        if (octupuses[(i, j)] > 9)
                        {
                            q.Enqueue((i, j));
                        }
                    }
                }

                while (q.Count > 0)
                {
                    var oct = q.Dequeue();
                    int x = oct.x, y = oct.y;

                    // ignore if already flashes, outside the grid, or simply not needed to flash
                    if (flashed.Contains(oct) || octupuses[oct] <= 9) { continue; }
                    //Console.WriteLine($"FLASHING oct[{x},{y}], with value: {octupuses[oct]}");

                    // flash and increase surrounding octopuses
                    flashed.Add(oct);

                    // add the neighbours
                    for (int i = -1; i < 2; i++)
                    {
                        for (int j = -1; j < 2; j++)
                        {
                            // updated X and Y coordinate
                            int ux = x + i, uy = y + j;
                            // ignore out-of-bounds
                            if (ux < 0 || ux > 9 || uy < 0 || uy > 9) { continue; }
                            octupuses[(ux, uy)]++;
                            q.Enqueue((ux, uy));
                        }
                    }
                }

                //set all flashed octopuses to 0
                foreach (var oct in flashed) { octupuses[(oct.x, oct.y)] = 0; }

                if (flashed.Count == 100) { finalStep = step; break; }
            }

            return "" + finalStep;
        }

        public string GetInput()
        {
            return new Inputs.Year2021.Day11().Input;
        }
    }
}