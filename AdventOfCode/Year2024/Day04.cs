using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Year2024
{
    public class Day04 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2024/day/4

        public string SolvePart1(string input)
        {
            long result = 0;
            string searchWord = "XMAS";
            string[] grid = input.Split(System.Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            // all 8 directions (4 directions + reverses = 8)
            var direction = new List<(int row, int col)> { (0, 1), (1, 0), (1, 1), (1, -1) };
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    for (int k = 0; k < direction.Count; k++)
                    {
                        var word = new char[searchWord.Length];
                        bool outOfBound = false;
                        for (int l = 0; l < searchWord.Length; l++)
                        {
                            int posRow = i + (direction[k].row * l);
                            int posCol = j + (direction[k].col * l);
                            if (posRow >= 0 && posRow < grid.Length && posCol >= 0 && posCol < grid[0].Length)
                            {
                                word[l] = grid[posRow][posCol];
                            }
                            else { outOfBound = true; break; }
                        }

                        if (!outOfBound)
                        {
                            char[] revWord = Utils.Utils.Duplicate(word);
                            Array.Reverse(revWord);
                            if (new string(word) == searchWord || new string(revWord) == searchWord) { result++; }
                            //Console.WriteLine($"{new string(word)} on direction: ({direction[k].row},{direction[k].col})");
                        }
                    }
                }
            }
            return "" + result;
        }

        public string SolvePart2(string input)
        {
            long result = 0;
            string[] grid = input.Split(System.Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 1; i < grid.Length - 1; i++)
            {
                for (int j = 1; j < grid[i].Length - 1; j++)
                {
                    // if the middle is NOT an 'A' we can skip this
                    if (grid[i][j] is not 'A') { continue; }
                    bool diagonal1 = false, diagonal2 = false;
                    char upleft = grid[i - 1][j - 1];
                    char upright = grid[i - 1][j + 1];
                    char lowerleft = grid[i + 1][j - 1];
                    char lowerright = grid[i + 1][j + 1];
                    if ((upleft == 'M' && lowerright == 'S') || (upleft == 'S' && lowerright == 'M')) { diagonal1 = true; }
                    if ((upright == 'M' && lowerleft == 'S') || (upright == 'S' && lowerleft == 'M')) { diagonal2 = true; }
                    if (diagonal1 && diagonal2) { result++; }
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