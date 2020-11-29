using System;

namespace AdventOfCode.Year2016
{
    public class Day19 : IAoC
    {
        // classis Josephus problem
        // see also https://www.youtube.com/watch?v=uCsD3ZGzMgE

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

        public string GetInput() => @"3005290";

        public int Part1(string[] input)
        {
            var binary = Convert.ToString(3005290, 2);
            // take the first 1 and put it last
            return Convert.ToInt32((binary.Substring(2) + "1"), 2);
        }

        public int Part2(string[] input)
        {
            // did not feel like implementing a linked-list
            // instead found this gem on https://www.reddit.com/r/adventofcode/comments/5j4lp1/2016_day_19_solutions/

            int newInput = 3005290;
            int pow = (int)Math.Floor(Math.Log(newInput) / Math.Log(3));
            int b = (int)Math.Pow(3, pow);
            if (newInput == b)
                return newInput;
            if (newInput - b <= b)
                return newInput - b;
            return 2 * newInput - 3 * b;
        }
    }
}