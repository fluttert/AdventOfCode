using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Year2024
{
    public class Day01 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2024/day/1

        public string SolvePart1(string input)
        {
            string[] lines = input.Split('\n');
            int[] left = new int[lines.Length];
            int[] right = new int[lines.Length];
            for (int i = 0; i<lines.Length;i++)
            {
                int[] numbers = Array.ConvertAll(lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries), int.Parse);
                left[i] = numbers[0];
                right[i] = numbers[1];
            }
            Array.Sort(left);
            Array.Sort(right);

            long totalDistance = 0;
            for (int i = 0; i < left.Length; i++) { 
                totalDistance += Math.Abs(left[i] - right[i]);
            }
            return ""+totalDistance;
        }

        public string SolvePart2(string input)
        {
            string[] lines = input.Split(Environment.NewLine);
            int[] left = new int[lines.Length];
            Dictionary<int,int> right = new Dictionary<int,int>();
            for (int i = 0; i < lines.Length; i++)
            {
                int[] numbers = Array.ConvertAll(lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries), int.Parse);
                left[i] = numbers[0];
                if (right.ContainsKey(numbers[1])) {
                    right[numbers[1]]++;
                }
                else { right.Add(numbers[1], 1);  }
            }


            long totalDistance = 0;
            for (int i = 0; i < left.Length; i++)
            {
                totalDistance += right.ContainsKey(left[i]) ? (left[i] * right[left[i]]) : 0; 
            }
            return "" + totalDistance;
        }

        public string GetInput()
        {
            var file_contents = File.ReadAllText("./input.txt");
            return file_contents;
        }
    }
}