using System;
using System.Text;

namespace AdventOfCode.Year2016
{
    public class Day09 : IAoC
    {
        // Challenge can be found on https://adventofcode.com/2016/day/9

        public string SolvePart1(string input)
        {
            var sb = new StringBuilder();
            int curIndex = 0;
            while (curIndex < input.Length)
            {
                if (input[curIndex] != '(')
                {
                    sb.Append(input[curIndex]);
                    curIndex++; continue;
                }
                int endParenthesis = input.IndexOf(')', curIndex);
                int[] marker = Array.ConvertAll(input.Substring(curIndex + 1, endParenthesis - curIndex - 1).Split('x'), int.Parse);
                string repeat = input.Substring(endParenthesis + 1, marker[0]);
                for (int i = 0; i < marker[1]; i++)
                {
                    sb.Append(repeat);
                }
                curIndex = endParenthesis + 1 + marker[0];
            }

            return sb.Length.ToString();
        }

        public string SolvePart2(string input)
        {
            // Parse(A(8x3)B(1x6)BCD)
            // == A + 3 * Parse(B(1x6)BC) + D
            // == A + 3 * ( B + 6* (Parse(B) + C) + D
            // == 1 + 3 * ( 1 + 6 * (1) + 1 ) + 1
            // == 1 + 3 * ( 8 ) + 1
            // == 1 + 24 + 1
            // == 26
            long decompressedLength = 0;
            int curIndex = 0;
            while (curIndex < input.Length)
            {
                if (input[curIndex] != '(')
                {
                    decompressedLength++;
                    curIndex++; continue;
                }
                int endParenthesis = input.IndexOf(')', curIndex);
                int[] marker = Array.ConvertAll(input.Substring(curIndex + 1, endParenthesis - curIndex - 1).Split('x'), int.Parse);
                string repeatSubstring = input.Substring(endParenthesis + 1, marker[0]);
                decompressedLength += (marker[1] * long.Parse(SolvePart2(repeatSubstring)));
                curIndex = endParenthesis + 1 + marker[0];
            }
            return decompressedLength.ToString();
        }

        public string GetInput()
        {
            return new Inputs.Year2016.Day09().Input;
        }
    }
}