using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2023
{
    public class Day03 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2023/day/3

        public string SolvePart1(string input)
        {
            int sum = 0;
            string[] gridLines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < gridLines.Length; i++)
            {
                for (int j = 0; j < gridLines[i].Length; j++)
                {
                    // nothing to see, please carry on
                    if (char.IsDigit(gridLines[i][j]) is false) { continue; }

                    // check for number and each number if they have any surrounding symbol
                    // numbers are only on the line
                    bool symbolfound = false;
                    List<char> digits = new();
                    for (int k = j; k < gridLines[i].Length; k++)
                    {
                        // check if char is digit, otherwise update iterator of lines (preventing double checks)
                        if (char.IsDigit(gridLines[i][k])) { digits.Add(gridLines[i][k]); }
                        else { j = k; break; }
                        if (hasSurroundingSymbol(i, k, gridLines)) { symbolfound = true; }
                    }

                    // do something with the numbers?
                    if (symbolfound) { sum += int.Parse(digits.ToArray()); }
                }
            }

            return "" + sum;
        }

        public string SolvePart2(string input)
        {
            string[] gridLines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            long sum = 0; // sum of gear ratio might get very large

            // Step 1: scan for all gears, note them down and create empty lists (to add numbers in step 2)
            Dictionary<(int x, int y), List<int>> gearNumbers = new();
            for (int i = 0; i < gridLines.Length; i++)
            {
                for (int j = 0; j < gridLines[i].Length; j++)
                {
                    if (gridLines[i][j] is '*') { gearNumbers.Add((i, j), new List<int>()); }
                }
            }

            // step 2: scan for all numbers & add them if adjecent to a gear
            for (int i = 0; i < gridLines.Length; i++)
            {
                for (int j = 0; j < gridLines[i].Length; j++)
                {
                    if (char.IsDigit(gridLines[i][j]) is false) { continue; }
                    // first pass to determine number and see which gear to tag along
                    List<char> digits = new List<char>();
                    HashSet<(int x, int y)> gears = new();
                    
                    for (int k = j; k < gridLines[i].Length; k++)
                    {
                        if (char.IsDigit(gridLines[i][k])) { digits.Add(gridLines[i][k]); }
                        else { j = k; break; }

                        // process digit + gears
                        for (int l = -1; l < 2; l++)
                        {
                            for (int m = -1; m < 2; m++)
                            {
                                if (gearNumbers.ContainsKey((i + l, k + m)))
                                {
                                    gears.Add((i + l, k + m));
                                }
                            }
                        }
                    }

                    // add numbers to dictionary
                    int number = int.Parse(digits.ToArray());
                    foreach (var gear in gears) { gearNumbers[(gear.x, gear.y)].Add(number); }
                }
            }

            // check for all gears with exactly 2 numbers
            foreach (var gear in gearNumbers)
            {
                if (gear.Value.Count == 2)
                {
                    sum += (gear.Value[0] * gear.Value[1]);
                }
            }

            return "" + sum;
        }

        private bool hasSurroundingSymbol(int x, int y, string[] grid)
        {
            bool surroundingSymbol = false;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (isSymbol(x + i, y + j, grid))
                    {
                        surroundingSymbol = true;
                        break;
                    }
                }
            }
            return surroundingSymbol;
        }

        private bool isSymbol(int x, int y, string[] grid)
        {
            return (x >= 0 && y >= 0 && x < grid.Length && y < grid[x].Length && grid[x][y] is not '.' && char.IsDigit(grid[x][y]) is false);
        }

        public string GetInput()
        {
            return new Inputs.Year2023.Day03Input().Input;
            //            return """
            //467..114..
            //...*......
            //..35..633.
            //......#...
            //617*......
            //.....+.58.
            //..592.....
            //......755.
            //...$.*....
            //.664.598..
            //""";
        }
    }
}