using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2022
{
    public class Day12 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2022/day/12

        public string SolvePart1(string input)
        {
            // parse
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            // grid set-up
            int[][] grid = new int[lines.Length][];
            int[][] minSteps = new int[lines.Length][];
            (int x, int y, int steps) start = (0, 0, 0); // dummy data
            (int x, int y) end = (0, 0); // dummy data
            for (int i = 0; i < lines.Length; i++)
            {
                grid[i] = new int[lines[i].Length];
                minSteps[i] = new int[lines[i].Length];
                for (int j = 0; j < lines[i].Length; j++)
                {
                    grid[i][j] = lines[i][j] - 'a';
                    minSteps[i][j] = int.MaxValue;
                    if (lines[i][j] == 'S') { grid[i][j] = 0; start = (i, j, 0); }
                    if (lines[i][j] == 'E') { grid[i][j] = 'z' - 'a'; end = (i, j); }
                }
            }

            //search
            Queue<(int x, int y, int steps)> q = new();
            int borderX = grid.Length, borderY = grid[0].Length;
            q.Enqueue(start);
            while (q.Count > 0)
            {
                var (x, y, steps) = q.Dequeue();
                // break conditions
                if (x == end.x && y == end.y) { minSteps[x][y] = steps; continue; }
                if (minSteps[x][y] <= steps) { continue; } // ignore, took to long to get here

                // set new minimum steps!
                minSteps[x][y] = steps;

                // search further: Check border of direction, then height-check, then prune if this branch should be explored (# steps)
                int maxHeight = grid[x][y] + 1, maxSteps = steps + 1; ;
                if (x - 1 >= 0 && grid[x - 1][y] <= maxHeight && minSteps[x - 1][y] > maxSteps) { q.Enqueue((x - 1, y, steps + 1)); }
                if (x + 1 < borderX && grid[x + 1][y] <= maxHeight && minSteps[x + 1][y] > maxSteps) { q.Enqueue((x + 1, y, steps + 1)); }
                if (y - 1 >= 0 && grid[x][y - 1] <= maxHeight && minSteps[x][y - 1] > maxSteps) { q.Enqueue((x, y - 1, steps + 1)); }
                if (y + 1 < borderY && grid[x][y + 1] <= maxHeight && minSteps[x][y + 1] > maxSteps) { q.Enqueue((x, y + 1, steps + 1)); }
            }

            return "" + minSteps[end.x][end.y];
        }

        public string SolvePart2(string input)
        {
            // switched startpoint to endpoint -> break when first 'a' (0) is reached
            // parse
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            // grid set-up
            int[][] grid = new int[lines.Length][];
            int[][] minSteps = new int[lines.Length][];
            (int x, int y, int steps) start = (0, 0, 0); // dummy data
            for (int i = 0; i < lines.Length; i++)
            {
                grid[i] = new int[lines[i].Length];
                minSteps[i] = new int[lines[i].Length];
                for (int j = 0; j < lines[i].Length; j++)
                {
                    grid[i][j] = lines[i][j] - 'a';
                    minSteps[i][j] = int.MaxValue;
                    if (lines[i][j] == 'S') { grid[i][j] = 0; } // start point is now a regular 'a'
                    if (lines[i][j] == 'E') { grid[i][j] = 'z' - 'a'; start = (i, j, 0); }
                }
            }

            //search
            Queue<(int x, int y, int steps)> q = new();
            int borderX = grid.Length, borderY = grid[0].Length, result = -1;
            q.Enqueue(start);
            while (q.Count > 0)
            {
                var (x, y, steps) = q.Dequeue();
                // break conditions
                if (grid[x][y] == 0) { result = steps; break; }
                if (minSteps[x][y] <= steps) { continue; } // ignore, took to long to get here

                // set new minimum steps!
                minSteps[x][y] = steps;

                // search further: Check border of direction, then height-check, then prune if this branch should be explored (# steps)
                int maxHeight = grid[x][y] - 1, maxSteps = steps + 1; ;
                if (x - 1 >= 0 && grid[x - 1][y] >= maxHeight && minSteps[x - 1][y] > maxSteps) { q.Enqueue((x - 1, y, steps + 1)); }
                if (x + 1 < borderX && grid[x + 1][y] >= maxHeight && minSteps[x + 1][y] > maxSteps) { q.Enqueue((x + 1, y, steps + 1)); }
                if (y - 1 >= 0 && grid[x][y - 1] >= maxHeight && minSteps[x][y - 1] > maxSteps) { q.Enqueue((x, y - 1, steps + 1)); }
                if (y + 1 < borderY && grid[x][y + 1] >= maxHeight && minSteps[x][y + 1] > maxSteps) { q.Enqueue((x, y + 1, steps + 1)); }
            }

            return "" + result;
        }

        public string GetInput()
        {
            return new Inputs.Year2022.Day12().Input;
        }
    }
}