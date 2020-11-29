using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2016
{
    public class Day13 : IAoC
    {
        private const char openSpace = '.';
        private const char wall = '#';
        private const char beenHere = 'O';

        public string SolvePart1(string input)
        {
            var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            return Part1(lines).ToString();
        }

        public string SolvePart2(string input)
        {
            var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            return Part2(lines).ToString();
        }

        public string GetInput() => @""; // none

        public int Part1(string[] input, int seed = 1362, int destX = 31, int destY = 39)
        {
            // create a maze that has x+20 and y+20
            var maze = CreateMaze(seed, destY + 20, destX + 20);

            // tuple = X - Y - #steps
            var queue = new Queue<Tuple<int, int, int>>();
            queue.Enqueue(new Tuple<int, int, int>(1, 1, 0));
            int minimumSteps = -1;
            while (queue.Count > 0)
            {
                var curStep = queue.Dequeue();
                int x = curStep.Item1, y = curStep.Item2, steps = curStep.Item3;
                if (destX == x && destY == y)
                {
                    minimumSteps = steps;
                    break;
                }
                maze[y][x] = beenHere;
                steps++;
                if (x > 0 && maze[y][x - 1] == openSpace) { queue.Enqueue(new Tuple<int, int, int>(x - 1, y, steps)); }
                if (x < (maze[0].Length - 2) && maze[y][x + 1] == openSpace) { queue.Enqueue(new Tuple<int, int, int>(x + 1, y, steps)); }
                if (y > 0 && maze[y - 1][x] == openSpace) { queue.Enqueue(new Tuple<int, int, int>(x, y - 1, steps)); }
                if (y < (maze.Length - 2) && maze[y + 1][x] == openSpace) { queue.Enqueue(new Tuple<int, int, int>(x, y + 1, steps)); }
            }

            return minimumSteps;
        }

        public int Part2(string[] input, int seed = 1362)
        {
            // create a maze that is 60 * 60
            var maze = CreateMaze(seed, 60, 60);

            // tuple = X - Y - #steps
            var queue = new Queue<Tuple<int, int, int>>();
            queue.Enqueue(new Tuple<int, int, int>(1, 1, 0));
            int maximumSteps = 50;
            int uniqueCoordinates = 0;
            while (queue.Count > 0)
            {
                var curStep = queue.Dequeue();
                int x = curStep.Item1, y = curStep.Item2, steps = curStep.Item3;
                if (steps > maximumSteps || maze[y][x] == beenHere) { continue; }
                uniqueCoordinates++;
                maze[y][x] = beenHere;
                steps++;
                if (x > 0 && maze[y][x - 1] == openSpace) { queue.Enqueue(new Tuple<int, int, int>(x - 1, y, steps)); }
                if (x < (maze[0].Length - 2) && maze[y][x + 1] == openSpace) { queue.Enqueue(new Tuple<int, int, int>(x + 1, y, steps)); }
                if (y > 0 && maze[y - 1][x] == openSpace) { queue.Enqueue(new Tuple<int, int, int>(x, y - 1, steps)); }
                if (y < (maze.Length - 2) && maze[y + 1][x] == openSpace) { queue.Enqueue(new Tuple<int, int, int>(x, y + 1, steps)); }
            }

            return uniqueCoordinates;
        }

        public char[][] CreateMaze(int seed, int height, int width)
        {
            char[][] maze = new char[height][];
            for (int i = 0; i < height; i++)
            {
                maze[i] = new char[width];
                for (int j = 0; j < width; j++)
                {
                    // formula: x*x + 3*x + 2*x*y + y + y*y
                    int sum = seed + (j * j) + (3 * j) + (2 * j * i) + i + (i * i);
                    maze[i][j] = IsBinarySumEven(sum) ? openSpace : wall;
                }
            }
            return maze;
        }

        // based on https://davidzych.com/converting-an-int-to-a-binary-string-in-c/
        public bool IsBinarySumEven(int sum)
        {
            // using a bit-operation-voodoo to count the 1's
            int ones = 0;
            while (sum > 0)
            {
                if ((sum & 1) == 1)
                {
                    ones++;
                }
                sum >>= 1;
            }
            return ones % 2 == 0;
        }
    }
}