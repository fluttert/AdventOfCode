using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2020
{
    public class Day01 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2020/day/1

        public string SolvePart1(string input)
        {
            int[] lines = Array.ConvertAll(input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries), int.Parse);

            return ReportRepair(lines, 2020).ToString();
        }

        public string SolvePart2(string input)
        {
            int[] lines = Array.ConvertAll(input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries), int.Parse);

            return ReportRepairTriple(lines, 2020).ToString();
        }

        public int ReportRepair(int[] input, int expectedSum)
        {
            Array.Sort(input);
            bool found = false;
            int result = -1;
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = input.Length - 1; j >= 0; j--)
                {
                    int sum = input[i] + input[j];
                    if (sum == expectedSum)
                    {
                        result = input[i] * input[j];
                        found = true;
                    }
                    if (sum < expectedSum) { break; }
                }
                if (found) { break; }
            }

            return result;
        }

        public long ReportRepairTriple(int[] input, int excpectedSum)
        {
            var checkMap = new HashSet<int>(input);
            long result = -1;
            bool found = false;
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = i + 1; j < input.Length; j++)
                {
                    int third = excpectedSum - input[i] - input[j];
                    if (third > 0 && checkMap.Contains(third))
                    {
                        result = (long)third * (long)input[i] * (long)input[j];
                        found = true;
                        break;
                    }
                }
                if (found) { break; }
            }

            return result;
        }

        public string GetInput()
        {
            return new Inputs.Year2020.Day01().Input;
        }
    }
}