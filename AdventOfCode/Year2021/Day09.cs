using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2021
{
    public class Day09 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2021/day/9

        /**
         * Key Insights
         * 1: check each number for up/down/left/right
         * 2: Part 01: if the current number is the same OR higher, skip it (this is NOT a lowest point)
         * 3: Part 02: having a low-point is just the starting, until it hits a 9 (use a breadth-search approach)
         *
         */

        public string SolvePart1(string input)
        {
            // put this in numbers please
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            // convert to grid, using a char conversion
            int[][] grid = new int[lines.Length][];
            for (int i = 0; i < lines.Length; i++)
            {
                grid[i] = new int[lines[i].Length];
                for (int j = 0; j < lines[i].Length; j++)
                {
                    // char has an underlying integer, simply 'substracting' zero gives the integer value we desire
                    grid[i][j] = lines[i][j] - '0';
                }
            }

            // check the grid
            int sumOfLowPoints = 0;
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    // check left/right
                    if (i > 0 && grid[i][j] >= grid[i - 1][j]) { continue; } // left
                    if (i < (grid.Length - 1) && grid[i][j] >= grid[i + 1][j]) { continue; } // right

                    // check up/down
                    if (j > 0 && grid[i][j] >= grid[i][j - 1]) { continue; } // up
                    if (j < (grid[i].Length - 1) && grid[i][j] >= grid[i][j + 1]) { continue; } // down

                    // this is a low point, in all other cases it would have been skipped
                    sumOfLowPoints += 1 + grid[i][j];
                    //Console.WriteLine($"grid ({i},{j}) with value {grid[i][j]}");
                }
            }

            // 532 was too high
            return "" + sumOfLowPoints;
        }

        public string SolvePart2(string input)
        {
            // put this in numbers please
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            // convert to grid, using a char conversion
            int[][] grid = new int[lines.Length][];
            for (int i = 0; i < lines.Length; i++)
            {
                grid[i] = new int[lines[i].Length];
                for (int j = 0; j < lines[i].Length; j++)
                {
                    // char has an underlying integer, simply 'substracting' zero gives the integer value we desire
                    grid[i][j] = lines[i][j] - '0';
                }
            }

            // check the grid
            var biggestBasins = new long[3];
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    // check left/right
                    if (i > 0 && grid[i][j] >= grid[i - 1][j]) { continue; } // left
                    if (i < (grid.Length - 1) && grid[i][j] >= grid[i + 1][j]) { continue; } // right

                    // check up/down
                    if (j > 0 && grid[i][j] >= grid[i][j - 1]) { continue; } // up
                    if (j < (grid[i].Length - 1) && grid[i][j] >= grid[i][j + 1]) { continue; } // down

                    // now lets see how big this basin is
                    long size = 0;
                    var q = new Queue<(int i, int j)>();
                    var c = new HashSet<(int i, int j)>();
                    q.Enqueue((i, j));
                    c.Add((i, j));
                    while (q.Count > 0)
                    {
                        var pos = q.Dequeue();

                        // if this is a 9, it's done
                        if (grid[pos.i][pos.j] == 9) { continue; }
                        
                        // add this position
                        size++;

                        if (pos.i > 0 && c.Contains((pos.i - 1, pos.j)) is false)
                        {
                            q.Enqueue((pos.i - 1, pos.j));
                            c.Add((pos.i - 1, pos.j));
                        }
                        if (pos.i < (grid.Length - 1) && c.Contains((pos.i + 1, pos.j)) is false)
                        {
                            q.Enqueue((pos.i + 1, pos.j));
                            c.Add((pos.i + 1, pos.j));
                        }
                        if (pos.j > 0 && c.Contains((pos.i, pos.j - 1)) is false)
                        {
                            q.Enqueue((pos.i, pos.j - 1));
                            c.Add((pos.i, pos.j - 1));
                        }
                        if (pos.j < (grid[pos.i].Length - 1) && c.Contains((pos.i, pos.j + 1)) is false)
                        {
                            q.Enqueue((pos.i, pos.j + 1));
                            c.Add((pos.i, pos.j + 1));
                        }
                    }

                    // this is a low point, in all other cases it would have been skipped
                    //Console.WriteLine($"grid ({i},{j}) with value {grid[i][j]} is a lowpoint, basinsize: {size}");

                    // check if this a biggest
                    if (size > biggestBasins[0] || size > biggestBasins[1] || size > biggestBasins[2])
                    {
                        if (biggestBasins[0] <= biggestBasins[1] && biggestBasins[0] <= biggestBasins[2])
                        {
                            biggestBasins[0] = size; continue;
                        }
                        if (biggestBasins[1] <= biggestBasins[0] && biggestBasins[1] <= biggestBasins[2])
                        {
                            biggestBasins[1] = size; continue;
                        }

                        biggestBasins[2] = size; continue;
                    }
                }
            }

            long multiplyBasins = biggestBasins[0] * biggestBasins[1] * biggestBasins[2];
            // 233160 is too low
            return "" + multiplyBasins;
        }

        public string GetInput()
        {
            return new Inputs.Year2021.Day09().Input;
        }
    }
}