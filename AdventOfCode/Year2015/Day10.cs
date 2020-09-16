using System.Text;

namespace AdventOfCode.Year2015
{
    public class Day10 : IAoC
    {
        // Challenge can be found on https://adventofcode.com/2015/day/10

        public string SolvePart1(string input)
        {
            return LookAndSay(input, 40).Length.ToString();
        }

        public string LookAndSay(string input, int iterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                StringBuilder sb = new StringBuilder();
                for (int j = 0; j < input.Length; j++)
                {
                    char current = input[j];
                    int count = 1;
                    int offset = 1;
                    while (j + offset < input.Length && input[j + offset] == current)
                    {
                        count++;
                        offset++;
                    }
                    sb.Append(count);
                    sb.Append(current);
                    j += (offset - 1);
                }
                input = sb.ToString();
            }
            return input;
        }

        public string SolvePart2(string input)
        {
            return LookAndSay(input, 50).Length.ToString();
        }

        public string GetInput()
        {
            return "1113122113";
        }
    }
}