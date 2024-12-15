using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Year2024
{
    public class Day14 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2024/day/14

        public string SolvePart1(string input)
        {
            long result = 0;
            int seconds = 100;
            int width = 101, height = 103;
            List<(int x, int y, int vx, int vy)> robots = new();

            // read input from file
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                var robot = Array.ConvertAll(line.Split(new char[] { 'p', '=', ',', ' ', 'v' }, StringSplitOptions.RemoveEmptyEntries), int.Parse);
                robots.Add((robot[0], robot[1], robot[2], robot[3]));
            }

            // simulate X second (including the last second)
            for (int i = 0; i < seconds; i++)
            {
                for (int j = 0; j < robots.Count; j++)
                {
                    var r = robots[j];
                    // C# module on negative numbers remain negative! => add width (for negative numbers) and module for positive
                    r.x = (r.x + r.vx + width) % width;
                    r.y = (r.y + r.vy + height) % height;

                    robots[j] = r;
                }
            }

            // compute per quadrant
            int q1 = 0, q2 = 0, q3 = 0, q4 = 0;
            int middleW = width / 2, middleH = height / 2;
            foreach (var robot in robots)
            {
                if (robot.x < middleW && robot.y < middleH) { q1++; }
                if (robot.x < middleW && robot.y > middleH) { q3++; }
                if (robot.x > middleW && robot.y < middleH) { q2++; }
                if (robot.x > middleW && robot.y > middleH) { q4++; }
            }

            result = q1 * q2 * q3 * q4;

            return "" + result;
        }

        private void PrintGrid(int width, int heigth, List<(int x, int y, int vx, int vy)> robots)
        {
            int[][] grid = new int[heigth][];
            for (int i = 0; i < grid.Length; i++) { grid[i] = new int[width]; }
            foreach (var robot in robots) { grid[robot.y][robot.x]++; }

            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    Console.Write(grid[i][j] == 0 ? " " : grid[i][j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        public string SolvePart2(string input)
        {
            long result = 0;
            int seconds = 10000;
            int width = 101, height = 103;
            long safetyFactor = long.MaxValue;

            List<(int x, int y, int vx, int vy)> robots = new();

            // read input from file
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                var robot = Array.ConvertAll(line.Split(new char[] { 'p', '=', ',', ' ', 'v' }, StringSplitOptions.RemoveEmptyEntries), int.Parse);
                robots.Add((robot[0], robot[1], robot[2], robot[3]));
            }

            // simulate X second (including the last second)
            for (int i = 0; i < seconds; i++)
            {
                HashSet<(int x, int y)> rp = new();
                for (int j = 0; j < robots.Count; j++)
                {
                    var r = robots[j];
                    // C# module on negative numbers remain negative! => add width (for negative numbers) and module for positive
                    r.x = (r.x + r.vx + width) % width;
                    r.y = (r.y + r.vy + height) % height;
                    rp.Add((r.x, r.y));
                    robots[j] = r;
                }

                
                // compute per quadrant
                int q1 = 0, q2 = 0, q3 = 0, q4 = 0;
                int middleW = width / 2, middleH = height / 2;
                foreach (var robot in robots)
                {
                    if (robot.x < middleW && robot.y < middleH) { q1++; }
                    if (robot.x < middleW && robot.y > middleH) { q3++; }
                    if (robot.x > middleW && robot.y < middleH) { q2++; }
                    if (robot.x > middleW && robot.y > middleH) { q4++; }
                }
                long curSafetyFactor = q1 * q2 * q3 * q4;
                if (curSafetyFactor < safetyFactor) { 
                    safetyFactor = curSafetyFactor;
                    result = i + 1;
                }
            }

            return "" + result;
        }

        // ~1200MS to complete
        public string SolvePart2FirstTry(string input)
        {
            long result = 0;
            int seconds = 10000;
            int width = 101, height = 103;
            List<(int x, int y, int vx, int vy)> robots = new();

            // read input from file
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                var robot = Array.ConvertAll(line.Split(new char[] { 'p', '=', ',', ' ', 'v' }, StringSplitOptions.RemoveEmptyEntries), int.Parse);
                robots.Add((robot[0], robot[1], robot[2], robot[3]));
            }

            // simulate X second (including the last second)
            for (int i = 0; i < seconds; i++)
            {
                HashSet<(int x, int y)> rp = new();
                for (int j = 0; j < robots.Count; j++)
                {
                    var r = robots[j];
                    // C# module on negative numbers remain negative! => add width (for negative numbers) and module for positive
                    r.x = (r.x + r.vx + width) % width;
                    r.y = (r.y + r.vy + height) % height;
                    rp.Add((r.x, r.y));
                    robots[j] = r;
                }

                // check for a clump of robots
                int maxAdjecent = 0;
                for (int j = 0; j < robots.Count; j++)
                {
                    Queue<(int x, int y)> q = new();
                    HashSet<(int x, int y)> visited = new();
                    q.Enqueue((robots[j].x, robots[j].y));
                    int adjecent = 0;
                    while (q.Count > 0)
                    {
                        var node = q.Dequeue();
                        if (visited.Contains(node)) { continue; }
                        visited.Add(node);
                        adjecent++;
                        if (!visited.Contains((node.x + 1, node.y)) && rp.Contains((node.x + 1, node.y))) { q.Enqueue((node.x + 1, node.y)); }
                        if (!visited.Contains((node.x - 1, node.y)) && rp.Contains((node.x - 1, node.y))) { q.Enqueue((node.x - 1, node.y)); }
                        if (!visited.Contains((node.x, node.y + 1)) && rp.Contains((node.x, node.y + 1))) { q.Enqueue((node.x, node.y + 1)); }
                        if (!visited.Contains((node.x, node.y - 1)) && rp.Contains((node.x, node.y - 1))) { q.Enqueue((node.x, node.y - 1)); }
                    }
                    if (adjecent > maxAdjecent) { maxAdjecent = adjecent; }
                }

                if (maxAdjecent > 25)
                {
                    Console.WriteLine("====================== SECOND" + (i + 1) + " ======================");
                    PrintGrid(width, height, robots);
                    result = i + 1;
                    break;
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