using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Year2024
{
    public class Day08 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2024/day/8

        public string SolvePart1(string input)
        {
            long result = 0;
            string[] grid = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            // collect all positions for each kind
            var dict = new Dictionary<char, List<(int x, int y)>>();
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    char c = grid[i][j];
                    if (c == '.') { continue; }
                    if (dict.ContainsKey(c) is false) { dict.Add(c, new List<(int x, int y)>()); }
                    dict[c].Add((i, j));
                }
            }
            // create antinodes
            var antinodes = new HashSet<(int x, int y)>();  // prevent antinodes in the same position
            foreach (var kvp in dict)
            {
                char c = kvp.Key;
                var nodes = kvp.Value;
                for (int i = 0; i < nodes.Count; i++)
                {
                    for (int j = i + 1; j < nodes.Count; j++)
                    {
                        //Console.WriteLine($"Char: {c} | node 1 = ({nodes[i].x},{nodes[i].y}) , node 2 = ({nodes[j].x},{nodes[j].y})");
                        int diffX = nodes[i].x - nodes[j].x;
                        int diffY = nodes[i].y - nodes[j].y;

                        var antinode1 = (nodes[i].x + diffX, nodes[i].y + diffY);
                        var antinode2 = (nodes[j].x - diffX, nodes[j].y - diffY);

                        if (IsWithinBounds(grid, antinode1)) { antinodes.Add(antinode1); }
                        if (IsWithinBounds(grid, antinode2)) { antinodes.Add(antinode2); }
                    }
                }
            }

            return "" + antinodes.Count;
        }

        private bool IsWithinBounds(string[] grid, (int x, int y) node)
        {
            return node.x >= 0 && node.x < grid.Length && node.y >= 0 && node.y < grid[0].Length;
        }

        public string SolvePart2(string input)
        {
            string[] grid = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            // collect all positions for each kind
            var dict = new Dictionary<char, List<(int x, int y)>>();
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    char c = grid[i][j];
                    if (c == '.') { continue; }
                    if (dict.ContainsKey(c) is false) { dict.Add(c, new List<(int x, int y)>()); }
                    dict[c].Add((i, j));
                }
            }
            // create antinodes
            var antinodes = new HashSet<(int x, int y)>();  // prevent antinodes in the same position
            foreach (var kvp in dict)
            {
                char c = kvp.Key;
                var nodes = kvp.Value;
                for (int i = 0; i < nodes.Count; i++)
                {
                    for (int j = i + 1; j < nodes.Count; j++)
                    {
                        //Console.WriteLine($"Char: {c} | node 1 = ({nodes[i].x},{nodes[i].y}) , node 2 = ({nodes[j].x},{nodes[j].y})");
                        int diffX = nodes[i].x - nodes[j].x;
                        int diffY = nodes[i].y - nodes[j].y;
                        (int x, int y) antinode1 = (nodes[i].x + diffX, nodes[i].y + diffY);
                        while (IsWithinBounds(grid, antinode1))
                        {
                            antinodes.Add(antinode1);
                            antinode1 = (antinode1.x + diffX, antinode1.y + diffY);
                        }

                        (int x, int y) antinode2 = (nodes[j].x - diffX, nodes[j].y - diffY);
                        while (IsWithinBounds(grid, antinode2))
                        {
                            antinodes.Add(antinode2);
                            antinode2 = (antinode2.x - diffX, antinode2.y - diffY);
                        }
                    }
                }
            }

            return "" + antinodes.Count;
        }

        public string GetInput()
        {
            var file_contents = File.ReadAllText("./input.txt");
            return file_contents;
        }
    }
}