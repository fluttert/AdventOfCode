using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

namespace AdventOfCode.Year2024
{
    public class Day12 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2024/day/12

        private string[] grid;
        private HashSet<(int x, int y)> visited = new();

        public string SolvePart1(string input)
        {
            long result = 0;
            grid = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            visited = new();
            // collect all positions for each kind

            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    if (visited.Contains((i, j))) { continue; }
                    // NEW PLOT DETECTED
                    var plot = DetectPlot((i, j));
                    long plotcost = plot.fences * plot.area;
                    //Console.WriteLine($"Plot area: {plot.area}, {plot.fences}, makes {plotcost}");
                    result += plotcost;
                }
            }
            return "" + result;
        }

        private (int area, int fences) DetectPlot((int x, int y) startingPoint)
        {
            var result = (0, 0);
            int area = 0, fences = 0;
            char plant = grid[startingPoint.x][startingPoint.y];
            Queue<(int x, int y)> q = new Queue<(int x, int y)>();
            q.Enqueue(startingPoint);
            while (q.Count > 0)
            {
                (int x, int y) cur = q.Dequeue();
                if (visited.Contains(cur)) { continue; }    // already visited + indexed
                // NEW node, add to current plot
                visited.Add(cur);   // add to visited nodes
                area++;
                int curFences = 4;
                // check neighbours
                if (IsWithinBounds((cur.x - 1, cur.y)) && grid[cur.x - 1][cur.y] == plant) { curFences--; q.Enqueue((cur.x - 1, cur.y)); }
                if (IsWithinBounds((cur.x + 1, cur.y)) && grid[cur.x + 1][cur.y] == plant) { curFences--; q.Enqueue((cur.x + 1, cur.y)); }
                if (IsWithinBounds((cur.x, cur.y - 1)) && grid[cur.x][cur.y - 1] == plant) { curFences--; q.Enqueue((cur.x, cur.y - 1)); }
                if (IsWithinBounds((cur.x, cur.y + 1)) && grid[cur.x][cur.y + 1] == plant) { curFences--; q.Enqueue((cur.x, cur.y + 1)); }
                fences += curFences;
            }
            result = (area, fences);

            return result;
        }

        private bool IsWithinBounds((int x, int y) node)
        {
            return node.x >= 0 && node.x < grid.Length && node.y >= 0 && node.y < grid[0].Length;
        }

        public string SolvePart2(string input)
        {
            long result = 0;
            grid = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            visited = new();
            // collect all positions for each kind

            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    if (visited.Contains((i, j))) { continue; }
                    // NEW PLOT DETECTED
                    var plot = DetectBulkPlot((i, j));
                    long plotcost = plot.fences * plot.area;
                    //Console.WriteLine($"Plot area: {plot.area}, {plot.fences}, makes {plotcost}");
                    result += plotcost;
                }
            }
            return "" + result;
        }

        private (int area, int fences) DetectBulkPlot((int x, int y) startingPoint)
        {
            var result = (0, 0);
            int area = 0, fences = 0;
            char plant = grid[startingPoint.x][startingPoint.y];
            Queue<(int x, int y)> q = new Queue<(int x, int y)>();              // new Queue to detect all plants for this plot
            HashSet<(int x, int y)> localArea = new HashSet<(int x, int y)>();  // keep track of local area
            q.Enqueue(startingPoint);
            while (q.Count > 0)
            {
                (int x, int y) cur = q.Dequeue();
                if (visited.Contains(cur)) { continue; }    // already visited + indexed
                // NEW node, add to current plot
                visited.Add(cur);   // add to visited nodes
                localArea.Add(cur);
                area++;
                // check neighbours
                if (IsWithinBounds((cur.x - 1, cur.y)) && grid[cur.x - 1][cur.y] == plant) { q.Enqueue((cur.x - 1, cur.y)); }
                if (IsWithinBounds((cur.x + 1, cur.y)) && grid[cur.x + 1][cur.y] == plant) { q.Enqueue((cur.x + 1, cur.y)); }
                if (IsWithinBounds((cur.x, cur.y - 1)) && grid[cur.x][cur.y - 1] == plant) { q.Enqueue((cur.x, cur.y - 1)); }
                if (IsWithinBounds((cur.x, cur.y + 1)) && grid[cur.x][cur.y + 1] == plant) { q.Enqueue((cur.x, cur.y + 1)); }
                fences += AmountOfOuterCorners(cur);
            }
            fences += AmountOfInnerCorner(localArea);

            result = (area, fences);

            return result;
        }

        private int AmountOfOuterCorners((int x, int y) position)
        {
            int corners = 0;
            char plant = grid[position.x][position.y];
            (int x, int y) left = (position.x, position.y - 1);
            (int x, int y) right = (position.x, position.y + 1);
            (int x, int y) upper = (position.x - 1, position.y);
            (int x, int y) lower = (position.x + 1, position.y);
            bool checkLeft = IsWithinBounds(left) is false || (IsWithinBounds(left) && grid[left.x][left.y] != plant);
            bool checkRight = IsWithinBounds(right) is false || (IsWithinBounds(right) && grid[right.x][right.y] != plant);
            bool checkUpper = IsWithinBounds(upper) is false || (IsWithinBounds(upper) && grid[upper.x][upper.y] != plant);
            bool checklower = IsWithinBounds(lower) is false || (IsWithinBounds(lower) && grid[lower.x][lower.y] != plant);
            if (checkLeft && checkUpper) { corners++;  } // left uppercorner check
            if (checkRight && checkUpper) { corners++; } // right upper corner
            if (checkLeft && checklower) { corners++; } // lower left corner
            if (checkRight && checklower) { corners++; } // lower right corner

            return corners;
        }

        private int AmountOfInnerCorner(HashSet<(int x, int y)> localArea)
        {
            int corners = 0;
            foreach (var cur in localArea) {
                (int x, int y) left = (cur.x, cur.y - 1);
                (int x, int y) right = (cur.x, cur.y + 1);
                (int x, int y) upper = (cur.x - 1, cur.y);
                (int x, int y) lower = (cur.x + 1, cur.y);
                (int x, int y) upperleft = (cur.x-1, cur.y - 1);
                (int x, int y) lowerleft = (cur.x + 1, cur.y - 1);
                (int x, int y) upperRight = (cur.x-1, cur.y + 1);
                (int x, int y) lowerRight = (cur.x+1, cur.y + 1);

                if (localArea.Contains(upperleft) is false && localArea.Contains(left) && localArea.Contains(upper)) { corners++; }
                if (localArea.Contains(lowerleft) is false && localArea.Contains(left) && localArea.Contains(lower)) { corners++; }
                if (localArea.Contains(upperRight) is false && localArea.Contains(right) && localArea.Contains(upper)) { corners++; }
                if (localArea.Contains(lowerRight) is false && localArea.Contains(right) && localArea.Contains(lower)) { corners++; }
            }
            return corners;
        }

        public string GetInput()
        {
            var file_contents = File.ReadAllText("./input.txt");
            return file_contents;
        }
    }
}