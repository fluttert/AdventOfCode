using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2021
{
    public class Day05 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2021/day/5

        /**
         * Key Insights
         * 1: Determine gridsize by grabbing the maximum dimensions, or take a fair estimation
         * 2: Use a direction vector, determine steps
         * 3: Use tuples for parsing
         */


        public string SolvePart1(string input)
        {
            // Convert input to usable instructions
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            int minX = 0, minY = 0, maxX = 0, maxY = 0;
            // parse all coordinates
            List<(int x1, int y1, int x2, int y2)> coordinates = new();
            foreach (string line in lines)
            {
                int[] numbers = Array.ConvertAll(line.Split(new char[] { ' ', '-', '>', ',' }, StringSplitOptions.RemoveEmptyEntries), int.Parse);
                coordinates.Add((numbers[0], numbers[1], numbers[2], numbers[3]));

                // determine minimum & maximum bounds of the grid
                //if (numbers[0] < minX || numbers[2] < minX) { minX = numbers[0] < numbers[2] ? numbers[0] : numbers[2]; }
                if (numbers[0] > maxX || numbers[2] > maxX) { maxX = numbers[0] > numbers[2] ? numbers[0] : numbers[2]; }
                //if (numbers[1] < minY || numbers[3] < minY) { minY = numbers[1] < numbers[3] ? numbers[1] : numbers[3]; }
                if (numbers[1] > maxY || numbers[3] > maxY) { maxY = numbers[1] > numbers[3] ? numbers[1] : numbers[3]; }
            }

            // create grid
            Console.WriteLine($"Grid size is: X-axis {minX} - {maxY}, by Y-axis {minY} - {maxY}");
            int[][] grid = new int[maxX + 1][];
            for (int i = 0; i < grid.Length; i++) { grid[i] = new int[maxY + 1]; }
            int OverlapOfAtLeast2 = 0;

            // fill in the lines (yeah)
            foreach ((int x1, int y1, int x2, int y2) in coordinates)
            {
                // early break, only process when there is a line
                if (x1 != x2 && y1 != y2) { continue; }

                // line detected
                if (x1 == x2)
                {
                    int start = y1 > y2 ? y2 : y1;
                    int end = y1 > y2 ? y1 : y2;
                    for (int i = start; i <= end; i++)
                    {
                        grid[x1][i]++;
                        if (grid[x1][i] == 2) { OverlapOfAtLeast2++; }
                    }
                }
                // line detected
                if (y1 == y2)
                {
                    int start = x1 > x2 ? x2 : x1;
                    int end = x1 > x2 ? x1 : x2;
                    for (int i = start; i <= end; i++)
                    {
                        grid[i][y1]++;
                        if (grid[i][y1] == 2) { OverlapOfAtLeast2++; }
                    }
                }
            }

            return "" + OverlapOfAtLeast2;
        }

        public string SolvePart2(string input)
        {
            // Convert input to usable instructions
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            int minX = 0, minY = 0, maxX = 0, maxY = 0;
            // parse all coordinates
            List<(int x1, int y1, int x2, int y2)> coordinates = new();
            foreach (string line in lines)
            {
                int[] numbers = Array.ConvertAll(line.Split(new char[] { ' ', '-', '>', ',' }, StringSplitOptions.RemoveEmptyEntries), int.Parse);
                coordinates.Add((numbers[0], numbers[1], numbers[2], numbers[3]));

                // determine minimum & maximum bounds of the grid
                if (numbers[0] > maxX || numbers[2] > maxX) { maxX = numbers[0] > numbers[2] ? numbers[0] : numbers[2]; }
                if (numbers[1] > maxY || numbers[3] > maxY) { maxY = numbers[1] > numbers[3] ? numbers[1] : numbers[3]; }
            }

            // create grid
            Console.WriteLine($"Grid size is: X-axis {minX} - {maxY}, by Y-axis {minY} - {maxY}");
            int[][] grid = new int[maxX + 1][];
            for (int i = 0; i < grid.Length; i++) { grid[i] = new int[maxY + 1]; }
            int OverlapOfAtLeast2 = 0;

            // fill in the lines (yeah)
            foreach (var t in coordinates)
            {
                // create a vector
                // determine steps
                int incX = 0;
                int incY = 0;
                int steps = 0;
                if (t.x1 != t.x2)
                {
                    incX = t.x1 > t.x2 ? -1 : 1;
                    steps = Math.Abs(t.x1 - t.x2);
                }
                if (t.y1 != t.y2)
                {
                    incY = t.y1 > t.y2 ? -1 : 1;
                    steps = Math.Abs(t.y1 - t.y2);
                }

                int posX = t.x1, posY = t.y1;
                while (steps>=0)
                {
                    grid[posX][posY]++;
                    if (grid[posX][posY] == 2) { OverlapOfAtLeast2++; }
                    posX += incX;
                    posY += incY;
                    steps--;
                    
                }

                
            }
            //Console.WriteLine(" ");
            //foreach (var g in grid) { Console.WriteLine(String.Join("", g)); }

            return "" + OverlapOfAtLeast2;
        }

        public string GetInput()
        {
            return new Inputs.Year2021.Day05().Input;
        }
    }
}