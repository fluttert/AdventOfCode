using System;

namespace AdventOfCode.Year2022
{
    public class Day08 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2022/day/8

        public string SolvePart1(string input)
        {
            // parse
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            int[][] grid = new int[lines.Length][];
            for (int i = 0; i < lines.Length; i++)
            {
                grid[i] = new int[lines[i].Length];
                for (int j = 0; j < lines[i].Length; j++)
                {
                    grid[i][j] = lines[i][j] - '0';
                }
            }
            // dimensions
            int row = grid.Length, col = grid[0].Length;
            int visibleTrees = row + row + col + col - 4;

            // calculate
            for (int i = 1; i < row - 1; i++)
            {
                for (int j = 1; j < col - 1; j++)
                {
                    // back track?
                    bool visible = true;
                    // look left (row--)
                    for (int k = i - 1; k >= 0 && visible; k--) { if (grid[k][j] >= grid[i][j]) { visible = false; } }
                    if (visible) { visibleTrees++; continue; }
                    visible = true;
                    // look right
                    for (int k = i + 1; k < row && visible; k++) { if (grid[k][j] >= grid[i][j]) { visible = false; } }
                    if (visible) { visibleTrees++; continue; }
                    visible = true;
                    // look up
                    for (int k = j - 1; k >= 0 && visible; k--) { if (grid[i][k] >= grid[i][j]) { visible = false; } }
                    if (visible) { visibleTrees++; continue; }
                    visible = true;
                    // look down
                    for (int k = j + 1; k < col && visible; k++) { if (grid[i][k] >= grid[i][j]) { visible = false; } }
                    if (visible) { visibleTrees++; }
                }
            }

            return "" + visibleTrees;
        }

        public string SolvePart2(string input)
        {
            // parse
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            int[][] grid = new int[lines.Length][];
            for (int i = 0; i < lines.Length; i++)
            {
                grid[i] = new int[lines[i].Length];
                for (int j = 0; j < lines[i].Length; j++)
                {
                    grid[i][j] = lines[i][j] - '0';
                }
            }
            // dimensions
            int row = grid.Length, col = grid[0].Length;
            int scenicScore = 0;

            // Ignore the edges, are they have a view of 0, and multiplying would give 0
            for (int i = 1; i < row - 1; i++)
            {
                for (int j = 1; j < col - 1; j++)
                {
                    // back track?
                    int trees = 0, score = 1, k = i - 1;

                    // left // right
                    while (k >= 0) { if (grid[k][j] >= grid[i][j]) { trees++; break; } trees++; k--; }
                    score *= trees; trees = 0; k = i + 1;
                    while (k < row) { if (grid[k][j] >= grid[i][j]) { trees++; break; } trees++; k++; }
                    score *= trees; trees = 0; k = j - 1;
                    
                    // up // down
                    while (k >= 0) { if (grid[i][k] >= grid[i][j]) { trees++; break; } trees++; k--; }
                    score *= trees; trees = 0; k = j + 1;
                    while (k < col) { if (grid[i][k] >= grid[i][j]) { trees++; break; } trees++; k++; }
                    score *= trees;

                    if (score > scenicScore)
                    {
                        scenicScore = score;
                    }
                }
            }

            return "" + scenicScore;
        }

        public string GetInput()
        {
            return new Inputs.Year2022.Day08().Input;
        }
    }
}