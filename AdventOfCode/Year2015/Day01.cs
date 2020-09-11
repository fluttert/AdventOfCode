using System;
using System.Diagnostics;

namespace AdventOfCode.Year2015
{

    public class Day01
    {
        public string Part1(string input)
        {
            int floor = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '(') { floor++; continue; }
                if (input[i] == ')') { floor--; continue; }
                throw new ArgumentException($"Invalid input, it contains {input[i]}");
            }
            return floor.ToString();
        }

        public string Part2(string input)
        {
            return null;
        }
    }
}