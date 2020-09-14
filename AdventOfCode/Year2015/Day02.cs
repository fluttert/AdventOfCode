using System;

namespace AdventOfCode.Year2015
{
    public class Day02 : IAoC
    {
        // Challenge can be found on https://adventofcode.com/2015/day/2

        public string SolvePart1(string input)
        {
            long wrappingPaper = 0;
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                // length x width x h
                long[] lwh = Array.ConvertAll(line.Split('x', StringSplitOptions.RemoveEmptyEntries), long.Parse);
                long side1 = lwh[0] * lwh[1];
                long side2 = lwh[1] * lwh[2];
                long side3 = lwh[2] * lwh[0];
                // extra wrapping paper for the smallest side
                long extraWrap = Math.Min(side1, Math.Min(side2, side3));

                wrappingPaper += (2 * side1) + (2 * side2) + (2 * side3) + extraWrap;
            }

            return wrappingPaper.ToString();
        }

        public string SolvePart2(string input)
        {
            long ribbon = 0;
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                // length x width x h
                long[] lwh = Array.ConvertAll(line.Split('x', StringSplitOptions.RemoveEmptyEntries), long.Parse);
                Array.Sort(lwh); // sorting makes the array have the smallest value on the beginning

                long wrap = 2 * lwh[0] + 2 * lwh[1];
                long bow = lwh[0] * lwh[1] * lwh[2];

                ribbon += wrap + bow;
            }

            return ribbon.ToString();
        }

        public string GetInput()
        {
            return new Inputs.Year2015.Day02().Input;
        }
    }
}