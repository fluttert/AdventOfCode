using System.Collections.Generic;

namespace AdventOfCode.Year2022
{
    public class Day06 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2022/day/6

        public string SolvePart1(string input)
        {
            int startOfPacket = -1;
            for (int i = 3; i < input.Length; i++)
            {
                if (input[i] != input[i - 1] && input[i] != input[i - 2]
                    && input[i] != input[i - 3] && input[i - 1] != input[i - 2]
                    && input[i - 1] != input[i - 3] && input[i - 2] != input[i - 3])
                {
                    startOfPacket = i;
                    break;
                }
            }

            return "" + (startOfPacket + 1);
        }

        public string SolvePart2(string input)
        {
            int startOfPacket = -1;
            for (int i = 13; i < input.Length; i++)
            {
                HashSet<char> unique = new();
                for (int j = 0; j < 14; j++)
                {
                    unique.Add(input[i - j]);
                    // early break when there non-unique characters added
                    // a hash-set will ignore duplicates, and thus have less characters then the max added
                    if (unique.Count < (j + 1)) { break; }
                }
                if (unique.Count == 14) { startOfPacket = i; break; }
            }
            return "" + (startOfPacket + 1);
        }

        public string GetInput()
        {
            return new Inputs.Year2022.Day06().Input;
        }
    }
}