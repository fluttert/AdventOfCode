using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Year2024
{
    public class Day10 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2024/day/10

        private string[] grid;  // you can traverse like grid[][]
        private HashSet<(int x, int y)> visited = new();

        public string SolvePart1(string input)
        {
            long result = 0;
            grid = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            // collect all positions for each kind

            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    if (grid[i][j] != '0') { continue; }
                    // NEW TRAILHEAD!
                    result += TrailheadScore((i, j));
                }
            }
            return "" + result;
        }

        private int TrailheadScore((int x, int y) startingPosition, bool uniqueRoutes = false)
        {
            int score = 0;
            Queue<(int x, int y)> q = new();
            HashSet<(int x, int y)> visited = new();
            q.Enqueue(startingPosition);

            while (q.Count > 0)
            {
                var pos = q.Dequeue();
                char val = GridChar(pos);
                // exit clause
                if (val == '9')
                {
                    if (uniqueRoutes == false && visited.Contains(pos)) { continue; }
                    score++; visited.Add(pos);
                    continue;
                }

                // search for and increment around you
                (int x, int y) left = (pos.x, pos.y - 1);
                (int x, int y) right = (pos.x, pos.y + 1);
                (int x, int y) upper = (pos.x - 1, pos.y);
                (int x, int y) lower = (pos.x + 1, pos.y);
                if (IsWithinBounds(left) && GridChar(left) - val == 1) { q.Enqueue(left); }
                if (IsWithinBounds(right) && GridChar(right) - val == 1) { q.Enqueue(right); }
                if (IsWithinBounds(upper) && GridChar(upper) - val == 1) { q.Enqueue(upper); }
                if (IsWithinBounds(lower) && GridChar(lower) - val == 1) { q.Enqueue(lower); }
            }

            // Console.WriteLine($"Trailhead ({startingPosition.x},{startingPosition.y}) has {score} score");

            return score;
        }

        private bool IsWithinBounds((int x, int y) node)
        {
            return node.x >= 0 && node.x < grid.Length && node.y >= 0 && node.y < grid[0].Length;
        }

        private char GridChar((int x, int y) position)
        { return grid[position.x][position.y]; }

        public string SolvePart2(string input)
        {
            long result = 0;
            grid = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            // collect all positions for each kind

            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    if (grid[i][j] != '0') { continue; }
                    // NEW TRAILHEAD!
                    result += TrailheadScore((i, j), true);
                }
            }
            return "" + result;
        }

        public string GetInput()
        {
            var file_contents = File.ReadAllText("./input.txt");
            return file_contents;
        }
    }
}