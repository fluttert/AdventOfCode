using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Year2016
{
    public class Day06 : IAoC
    {
        // Challenge can be found on https://adventofcode.com/2016/day/6

        public string SolvePart1(string input)
        {
            var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            var message = new List<char>();
            for (int column = 0; column < lines[0].Length; column++)
            {
                int[] chars = new int[26];
                for (int row = 0; row < lines.Length; row++)
                {
                    chars[(lines[row][column] - 'a')]++;
                }
                int max = -1, index = -1; ;
                for (int i = 0; i < chars.Length; i++)
                {
                    if (chars[i] > max) { max = chars[i]; index = i; }
                }
                message.Add((char)('a' + index));
            }
            return new string(message.ToArray());
        }

        public string SolvePart2(string input)
        {
            var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            var message = new List<char>();
            for (int column = 0; column < lines[0].Length; column++)
            {
                int[] chars = new int[26];
                for (int row = 0; row < lines.Length; row++)
                {
                    chars[(lines[row][column] - 'a')]++;
                }
                int min = Int32.MaxValue, index = -1; ;
                for (int i = 0; i < chars.Length; i++)
                {
                    if (chars[i] != 0 && chars[i] < min) { min = chars[i]; index = i; }
                }
                message.Add((char)('a' + index));
            }
            return new string(message.ToArray());
        }

        public string GetInput()
        {
            return new Inputs.Year2016.Day06().Input;
        }
    }
}
