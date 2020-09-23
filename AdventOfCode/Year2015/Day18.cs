using System;

namespace AdventOfCode.Year2015
{
    public class Day18 : IAoC
    {
        // Challenge can be found on https://adventofcode.com/2015/day/18

        public string SolvePart1(string input)
        {
            bool[][] grid = ConvertToLightGrid(input);
            grid = AnimateGrid(grid, 100);

            return CountLights(grid).ToString(); ;
        }

        public bool[][] ConvertToLightGrid(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            var grid = new bool[lines.Length][];
            for (int i = 0; i < lines.Length; i++)
            {
                grid[i] = new bool[lines[i].Length];
                for (int j = 0; j < lines[i].Length; j++)
                {
                    grid[i][j] = lines[i][j] == '#';
                }
            }
            return grid;
        }

        public int CountLights(bool[][] grid)
        {
            // count lights
            int lights = 0;
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[0].Length; j++)
                {
                    lights += grid[i][j] ? 1 : 0;
                }
            }
            return lights;
        }

        public bool[][] AnimateGrid(bool[][] grid, int steps, bool lightsOnInCorner = false)
        {
            // iterate on steps
            for (int k = 0; k < steps; k++)
            {
                if (lightsOnInCorner)
                {
                    grid[0][0] = true;
                    grid[0][grid[0].Length - 1] = true;
                    grid[grid.Length - 1][0] = true;
                    grid[grid.Length - 1][grid[0].Length - 1] = true;
                }

                int lightstmp = CountLights(grid);

                var newGrid = new bool[grid.Length][];
                // lights
                for (int i = 0; i < grid.Length; i++)
                {
                    newGrid[i] = new bool[grid[i].Length];
                    for (int j = 0; j < grid[i].Length; j++)
                    {
                        newGrid[i][j] = grid[i][j];
                        int neighboursOn = 0;
                        int offsetLeft = i > 0 ? i - 1 : 0;
                        int offsetRight = i < grid.Length - 1 ? i + 1 : grid.Length - 1;
                        int offsetUpper = j > 0 ? j - 1 : 0;
                        int offsetLower = j < grid[i].Length - 1 ? j + 1 : grid[i].Length - 1;
                        for (int x = offsetLeft; x <= offsetRight; x++)
                        {
                            for (int y = offsetUpper; y <= offsetLower; y++)
                            {
                                if (x == i && y == j) { continue; }
                                if (grid[x][y]) { neighboursOn++; }
                            }
                        }

                        if (grid[i][j] && (neighboursOn != 2 && neighboursOn != 3)) { newGrid[i][j] = false; continue; }
                        if (!grid[i][j] && neighboursOn == 3) { newGrid[i][j] = true; }
                    }
                }

                grid = newGrid;
            }

            if (lightsOnInCorner)
            {
                grid[0][0] = true;
                grid[0][grid[0].Length - 1] = true;
                grid[grid.Length - 1][0] = true;
                grid[grid.Length - 1][grid[0].Length - 1] = true;
            }
            return grid;
        }

        public string SolvePart2(string input)
        {
            bool[][] grid = ConvertToLightGrid(input);
            grid = AnimateGrid(grid, steps: 100, lightsOnInCorner: true);
            return CountLights(grid).ToString(); ;
        }

        public string GetInput()
        {
            return new Inputs.Year2015.Day18().Input;
        }
    }
}