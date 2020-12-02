using System;

namespace AdventOfCode.Year2020
{
    public class Day02 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2020/day/2

        public string SolvePart1(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            int validPasswords = 0;
            foreach (string line in lines)
            {
                string[] pieces = line.Split(new char[] { ' ', '-', ':' }, StringSplitOptions.RemoveEmptyEntries);
                int min = int.Parse(pieces[0]), max = int.Parse(pieces[1]);
                char letter = pieces[2][0];
                int count = 0;
                foreach (char c in pieces[3])
                {
                    if (letter == c) { count++; }
                }
                if (min <= count && count <= max) { validPasswords++; }
            }

            return validPasswords.ToString();
        }

        public string SolvePart2(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            int validPasswords = 0;
            foreach (string line in lines)
            {
                string[] pieces = line.Split(new char[] { ' ', '-', ':' }, StringSplitOptions.RemoveEmptyEntries);

                // convert non-zero index to zero index
                int position1 = int.Parse(pieces[0]) - 1;
                int position2 = int.Parse(pieces[1]) - 1;
                char letter = pieces[2][0];
                char letter1 = pieces[3][position1];
                char letter2 = pieces[3][position2];
                if ((letter1 == letter || letter2 == letter) && letter1 != letter2)
                {
                    validPasswords++;
                }
            }

            return validPasswords.ToString();
        }

        public string GetInput()
        {
            return new Inputs.Year2020.Day02().Input;
        }
    }
}
