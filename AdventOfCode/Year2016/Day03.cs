using System;

namespace AdventOfCode.Year2016
{
    public class Day03 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2016/day/3

        public string SolvePart1(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            int possibleTriangles = 0;
            for (int index = 0; index < lines.Length; index++)
            {
                string[] tmp = lines[index].Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                int[] sides = Array.ConvertAll(tmp, int.Parse);
                Array.Sort(sides);
                if ((sides[0] + sides[1]) > sides[2])
                {
                    possibleTriangles++;
                }
            }
            return possibleTriangles.ToString();
        }

        public string SolvePart2(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            int possibleTriangles = 0;
            for (int index = 0; index < lines.Length; index += 3)
            {
                if (lines.Length < (index + 2)) { continue; }
                int[] line1 = Array.ConvertAll(lines[index].Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries), int.Parse);
                int[] line2 = Array.ConvertAll(lines[index + 1].Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries), int.Parse);
                int[] line3 = Array.ConvertAll(lines[index + 2].Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries), int.Parse);
                for (int i = 0; i < 3; i++)
                {
                    int[] sides = new[] { line1[i], line2[i], line3[i] };
                    Array.Sort(sides);
                    if ((sides[0] + sides[1]) > sides[2])
                    {
                        possibleTriangles++;
                    }
                }
            }
            return possibleTriangles.ToString();
        }

        public string GetInput()
        {
            return new Inputs.Year2016.Day03().Input;
        }
    }
}