using System;

namespace AdventOfCode.Year2015
{
    public class Day01
    {

        // Challenge can be found on https://adventofcode.com/2015/day/1

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
            int floor = 0;
            int position = 0;
            for (int i = 0; i < input.Length; i++)
            {
                // shorthand for incrementing or decrementing based on character
                floor += input[i] == '(' ? 1 : -1;

                if (floor < 0)
                {
                    position = i + 1;
                    break;
                }
            }
            return position.ToString();
        }
    }
}