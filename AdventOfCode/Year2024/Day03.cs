using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace AdventOfCode.Year2024
{
    public class Day03 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2024/day/3

        public string SolvePart1(string input)
        {
            long result = 0;
            int pos = input.IndexOf("mul(", 0);

            while (pos > 0 && pos < input.Length)
            {
                int pos2 = input.IndexOf("mul(", pos + 1);
                int endBrace = input.IndexOf(')', pos + 1);            // determines end
                if (endBrace == -1 || endBrace - (pos + 4) > 7) { pos = pos2; continue; }     // early exit on not existing/fitting parts

                string middle = input.Substring(pos + 4, endBrace - pos - 4);   // middle part
                string[] tokens = middle.Split(',');
                if (tokens.Length != 2) { pos = pos2; continue; }      // early exit
                int left = 0, right = 0;
                int.TryParse(tokens[0], out left);
                int.TryParse(tokens[1], out right);

                result += (left * right);
                pos = pos2;
            }
            return "" + result;
        }

        public string SolvePart2(string input)
        {
            long result = 0;
            input += "don't()"; // pad the string
            int recentDo = 0;
            int recentDont = input.IndexOf("don't()");
            while (recentDo != -1 && recentDont != -1)
            {
                // chunk between do & dont
                result += long.Parse(SolvePart1(input.Substring(recentDo, recentDont - recentDo)));

                // next chunk please
                recentDo = input.IndexOf("do()", recentDont + 1);
                recentDont = input.IndexOf("don't()", recentDo + 1);
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