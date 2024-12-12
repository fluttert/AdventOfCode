using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace AdventOfCode.Year2024
{
    public class Day06 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2024/day/6

        public string SolvePart1(string input)
        {
            long result = 0;
            string[] grid = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            // collect all obstacles
            HashSet<(int x, int y)> obstacle = new();
            (int x, int y) position = (0, 0);
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    char c = grid[i][j];
                    if (c == '^') { position = (i, j); }
                    if (c == '#') { obstacle.Add((i, j)); }
                }
            }
            var gridDim = (grid.Length, grid[0].Length);
            // directions
            List<(int x, int y)> directions = new() { (-1, 0), (0, 1), (1, 0), (0, -1) };
            HashSet<(int x, int y)> path = new();
            int dirChange = 0;
            while (IsInBounds(gridDim, position))
            {
                path.Add(position);

                // update for next position
                var dir = directions[dirChange % 4];
                var candidate = (position.x + dir.x, position.y + dir.y);

                // OBSTACLE, move to the right
                if (obstacle.Contains(candidate))
                {
                    dirChange++;
                    dir = directions[dirChange % 4];
                    candidate = (position.x + dir.x, position.y + dir.y);
                }
                position = candidate;
            }
            result = path.Count;

            return "" + result;
        }

        private bool IsInBounds((int, int) grid, (int x, int y) node)
        {
            return node.x >= 0 && node.x < grid.Item1 && node.y >= 0 && node.y < grid.Item2;
        }

        public string SolvePart2(string input)
        {
            string[] grid = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            // collect all obstacles
            HashSet<(int x, int y)> obstacles = new();
            (int x, int y) startingPos = (0, 0);
            (int x, int y) position = (0, 0);
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    char c = grid[i][j];
                    if (c == '^') { position = (i, j); startingPos = position; }
                    if (c == '#') { obstacles.Add((i, j)); }
                }
            }
            HashSet<(int x, int y)> cycles = new();
            var gridDim = (grid.Length, grid[0].Length);

            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    char c = grid[i][j];
                    if (c != '#' && c != '^')
                    {
                        obstacles.Add((i, j));  // add obstacle
                        if (HasALoop(gridDim, startingPos, obstacles))
                        {
                            //Console.WriteLine($"Loop by adding obstacle here: ({i},{j})");
                            cycles.Add((i, j));
                        }
                        obstacles.Remove((i, j));// remove it again
                    }
                }
            }
            return "" + cycles.Count;
        }

        private bool HasALoop((int, int) gridDim, (int x, int y) position, HashSet<(int x, int y)> obstacle)
        {
            HashSet<(int x, int y, int dir)> path = new();
            List<(int x, int y)> directions = new() { (-1, 0), (0, 1), (1, 0), (0, -1) };
            int dirChange = 0;
            while (IsInBounds(gridDim, position))
            {
                dirChange %= 4;
                // cycle detection
                if (path.Contains((position.x, position.y, dirChange))) { return true; }

                // no cycle, add postion to the path
                path.Add((position.x, position.y, dirChange));

                // determine next position
                var dir = directions[dirChange];
                (int x, int y) candidate = (position.x + dir.x, position.y + dir.y);

                // OBSTACLE, move to the right
                if (obstacle.Contains(candidate))
                {
                    dirChange++;
                    dir = directions[dirChange % 4];
                    candidate = (position.x + dir.x, position.y + dir.y);
                }
                // could be another obstacle (due to obstacle change)
                if (obstacle.Contains(candidate))
                {
                    dirChange++;
                    dir = directions[dirChange % 4];
                    candidate = (position.x + dir.x, position.y + dir.y);
                }
                position = candidate;
            }
            return false;
        }

        public string GetInput()
        {
            var file_contents = File.ReadAllText("./input.txt");
            return file_contents;
        }
    }
}