using System;

namespace AdventOfCode.Year2015
{
    public class Day06 : IAoC
    {
        // Challenge can be found on https://adventofcode.com/2015/day/6

        public string SolvePart1(string input)
        {
            // init light grid
            bool[][] grid = new bool[1000][];
            for (int i = 0; i < grid.Length; i++) { grid[i] = new bool[1000]; }

            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                int indexForGrid = parts[0] == "turn" ? 2 : 1;
                var fromCoordinate = Array.ConvertAll(parts[indexForGrid].Split(','), int.Parse);
                var toCoordinate = Array.ConvertAll(parts[indexForGrid + 2].Split(','), int.Parse);
                for (int i = fromCoordinate[0]; i <= toCoordinate[0]; i++)
                {
                    for (int j = fromCoordinate[1]; j <= toCoordinate[1]; j++)
                    {
                        if (parts[0] == "toggle") { grid[i][j] = !grid[i][j]; continue; }
                        if (parts[1] == "off") { grid[i][j] = false; continue; }
                        if (parts[1] == "on") { grid[i][j] = true; continue; }
                    }
                }
            }
            int totalLightsOn = 0;
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[0].Length; j++)
                {
                    if (grid[i][j]) { totalLightsOn++; }
                }
            }
            return totalLightsOn.ToString(); ;
        }

        public string SolvePart2(string input)
        {
            // init light grid
            int[][] grid = new int[1000][];
            for (int i = 0; i < grid.Length; i++) { grid[i] = new int[1000]; }

            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                int indexForGrid = parts[0] == "turn" ? 2 : 1;
                var fromCoordinate = Array.ConvertAll(parts[indexForGrid].Split(','), int.Parse);
                var toCoordinate = Array.ConvertAll(parts[indexForGrid + 2].Split(','), int.Parse);
                for (int i = fromCoordinate[0]; i <= toCoordinate[0]; i++)
                {
                    for (int j = fromCoordinate[1]; j <= toCoordinate[1]; j++)
                    {
                        if (parts[0] == "toggle") { grid[i][j] += 2; continue; }
                        if (parts[1] == "on") { grid[i][j] += 1; continue; }
                        if (parts[1] == "off")
                        {
                            grid[i][j] -= grid[i][j] == 0 ? 0 : 1; continue;
                        }
                    }
                }
            }
            int totalBrightness = 0;
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[0].Length; j++)
                {
                    totalBrightness += grid[i][j];
                }
            }
            return totalBrightness.ToString(); ;
        }
    }
}