using System;

namespace AdventOfCode.Year2015
{
    public class Day08 : IAoC
    {
        // Challenge can be found on https://adventofcode.com/2015/day/8

        public string SolvePart1(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            int totalLiteralChars = 0;
            int totalMemoryChars = 0;

            foreach (string line in lines)
            {
                totalLiteralChars += line.Length;
                for (int i = 1; i < (line.Length - 1); i++)
                {
                    // first and last letter doesnt count, as they are ""
                    if (line[i] == '\\')
                    {
                        if (line[i + 1] == '\\' || line[i + 1] == '"') { i++; totalMemoryChars++; continue; }
                        if (line[i + 1] == 'x')
                        {
                            i += 3;
                            totalMemoryChars++;
                            continue;
                        }
                    }
                    totalMemoryChars++;
                }
            }

            return (totalLiteralChars - totalMemoryChars).ToString();
        }

        public string SolvePart2(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            int totalLiteralChars = 0;
            int totalEncodedChars = 0;

            foreach (string line in lines)
            {
                totalLiteralChars += line.Length;
                totalEncodedChars += 2; // add qoutes 
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == '\\' || line[i] == '"') { totalEncodedChars++; }
                    totalEncodedChars++;
                }
            }

            return (totalEncodedChars -  totalLiteralChars).ToString();
        }

        public string GetInput()
        {
            return new Inputs.Year2015.Day08().Input;
        }
    }
}