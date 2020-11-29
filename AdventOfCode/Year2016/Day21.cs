using System;

namespace AdventOfCode.Year2016
{
    internal class Day21
    {
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

        public string GetInput() => new Inputs.Year2016.Day21().Input;

        public string Part1(string[] input, string toScramble = "abcdefgh")
        {
            char[] output = toScramble.ToCharArray();
            for (int i = 0; i < input.Length; i++)
            {
                string line = input[i];
                if (line.IndexOf("swap position") == 0)
                {
                    output = SwapPosition(line, output);
                }
                if (line.IndexOf("swap letter") == 0)
                {
                    output = SwapLetter(line, output);
                }
                if (line.IndexOf("rotate left") == 0 || line.IndexOf("rotate left") == 0)
                {
                    output = Rotate(line, output);
                }
                if (line.IndexOf("rotate based on position of letter") == 0)
                {
                    output = RotateBasedOnLetter(line, output);
                }
                if (line.IndexOf("reverse positions") == 0)
                {
                    output = ReversePosition(line, output);
                }
            }
            return new string(output);
        }

        private char[] ReversePosition(string line, char[] output)
        {
            throw new NotImplementedException();
        }

        private char[] RotateBasedOnLetter(string line, char[] output)
        {
            throw new NotImplementedException();
        }

        private char[] Rotate(string line, char[] output)
        {
            throw new NotImplementedException();
        }

        private char[] SwapLetter(string line, char[] output)
        {
            throw new NotImplementedException();
        }

        private char[] SwapPosition(string line, char[] output)
        {
            throw new NotImplementedException();
        }

        public string Part2(string[] input)
        {
            return string.Empty;
        }
    }
}