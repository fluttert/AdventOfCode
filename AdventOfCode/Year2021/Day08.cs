using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Year2021
{
    public class Day08 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2021/day/8

        /** digits on a 7 segment display
         *   0:      1:      2:      3:      4:
         *  aaaa    ....    aaaa    aaaa    ....
         * b    c  .    c  .    c  .    c  b    c
         * b    c  .    c  .    c  .    c  b    c
         *  ....    ....    dddd    dddd    dddd
         * e    f  .    f  e    .  .    f  .    f
         * e    f  .    f  e    .  .    f  .    f
         *  gggg    ....    gggg    gggg    ....
         *
         *   5:      6:      7:      8:      9:
         *  aaaa    aaaa    aaaa    aaaa    aaaa
         * b    .  b    .  .    c  b    c  b    c
         * b    .  b    .  .    c  b    c  b    c
         *  dddd    dddd    ....    dddd    dddd
         * .    f  e    f  .    f  e    f  .    f
         * .    f  e    f  .    f  e    f  .    f
         *  gggg    gggg    ....    gggg    gggg
         */

        /**
         * Key Insights
         * 1: Write down the 7-segment digits
         * 2: Read the question for part 1
         *
         *
         *
         * 0 = abcefg (6 segments)
         * 1 = cf (2 segments)
         * 2 = acdeg (5 segments)
         * 3 = acdfg (5 segments)
         * 4 = bcdf (4 segments)
         * 5 = abdfg (5 segments)
         * 6 = abdefg (6 segments)
         * 7 = acf (3 segments)
         * 8 = abcdefg (7 segments)
         * 9 = abcdfg (6 segments)
         */

        public string SolvePart1(string input)
        {
            // get the part we are interested in
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            int uniqueSegmentDigits = 0;
            foreach (string line in lines)
            {
                string fourDigits = line.Substring(line.IndexOf('|') + 1);
                string[] digits = fourDigits.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                foreach (string d in digits)
                {
                    if (d.Length == 2 || d.Length == 4 || d.Length == 3 || d.Length == 7)
                    { uniqueSegmentDigits++; }
                }
            }

            return "" + uniqueSegmentDigits;
        }

        public string SolvePart2(string input)
        {
            /** Note which unique number go to other numbers
              * 1 (CF) => 0,3,9
              * 4 (BCDF) => 9
              * 7 (ACF) => 9
              * 8 (ABCDEFG) => none
              *
              * segments lenghts
              * 2: 1
              * 3: 7
              * 4: 4
              * 5: 2, 3, 5
              * 6: 0, 6, 9
              * 7: 8
              *
              * Decoded
              * 0: length = 6 && contains 7
              * 1: length = 2
              * 2: length = 5 && is not 3 and not 5
              * 3: lenght = 5 && contains (code of 1 or code of 7)
              * 4: length = 4
              * 5: lenght = 5 && overlaps (code of 6 or code of 9)
              * 6: length = 6 && is not 0 and not 9
              * 7: length = 3
              * 8: length = 7
              * 9: lenght = 6 && contains code of 4
              *
              * */
            // get the part we are interested in
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            int sumOfOutputs = 0;
            foreach (string line in lines)
            {
                string[] digits = line.Split(new char[] { ' ', '|' }, StringSplitOptions.RemoveEmptyEntries);
                HashSet<char>[] numbers = new HashSet<char>[10]; // 0 - 9 = 10 numbers

                // first pass -> fetch the unique ones
                foreach (string d in digits)
                {
                    if (d.Length == 2) { numbers[1] = new HashSet<char>(d.ToCharArray()); }
                    if (d.Length == 3) { numbers[7] = new HashSet<char>(d.ToCharArray()); }
                    if (d.Length == 4) { numbers[4] = new HashSet<char>(d.ToCharArray()); }
                    if (d.Length == 7) { numbers[8] = new HashSet<char>(d.ToCharArray()); }
                }

                // second pass -> Fetch the length = 6 (number 0, 6, an 9)
                foreach (string d in digits)
                {
                    if (d.Length == 6)
                    {
                        var chars = new HashSet<char>(d.ToCharArray());

                        // check for 7
                        bool contains7 = true;
                        bool contains4 = true;
                        foreach (char c in numbers[7])
                        {
                            if (chars.Contains(c) == false) { contains7 = false; }
                        }
                        foreach (char c in numbers[4])
                        {
                            if (chars.Contains(c) == false) { contains4 = false; }
                        }
                        if (contains4 && contains7) { numbers[9] = chars; continue; }
                        if (contains4 == false && contains7 == false) { numbers[6] = chars; continue; }
                        if (contains4 == false && contains7 == true) { numbers[0] = chars; continue; }
                        Console.WriteLine("I cannot be processed?? Length 6");
                    }
                }

                // third pass (numbers 2, 3, and 5)
                foreach (string d in digits)
                {
                    if (d.Length == 5)
                    {
                        var chars = new HashSet<char>(d.ToCharArray());

                        // check for 7
                        bool contains7 = true;
                        bool isFive = true;
                        foreach (char c in numbers[7])
                        {
                            if (chars.Contains(c) == false) { contains7 = false; }
                        }
                        foreach (char c in chars)
                        {
                            if (numbers[6].Contains(c) == false) { isFive = false; }
                        }
                        if (isFive) { numbers[5] = chars; continue; }
                        if (contains7) { numbers[3] = chars; continue; }
                        if (isFive == false && contains7 == false) { numbers[2] = chars; continue; }
                        Console.WriteLine("I cannot be processed?? Length 5");
                    }
                }

                // all numbers decoded! yeah :D lets calculate the sumers
                StringBuilder sb = new();
                for (int i = 4; i > 0; i--)
                {
                    int pos = digits.Length - i;
                    if (digits[pos].Length == 2) { sb.Append('1'); continue; }
                    if (digits[pos].Length == 3) { sb.Append('7'); continue; }
                    if (digits[pos].Length == 4) { sb.Append('4'); continue; }
                    if (digits[pos].Length == 7) { sb.Append('8'); continue; }

                    bool nFound = false;
                    for (int j = 0; j < 10; j++)
                    {
                        // check on length
                        if (numbers[j].Count == digits[pos].Length)
                        {
                            // same length of segments, let's check if this is the same
                            bool isSame = true;
                            foreach (char n in numbers[j])
                            {
                                if (digits[pos].Contains(n) == false) { isSame = false; }
                            }
                            if (isSame)
                            {
                                sb.Append(j); nFound = true;
                            }
                        }
                    }
                    if (nFound == false) { Console.WriteLine("Damn this digit is unknown?"); }
                }
                string presumedDigits = sb.ToString();
                sumOfOutputs += int.Parse(presumedDigits);
                //Console.WriteLine("Parse to: " + int.Parse(presumedDigits));
            }

            return "" + sumOfOutputs;
        }

        public string GetInput()
        {
            return new Inputs.Year2021.Day08().Input;
        }
    }
}