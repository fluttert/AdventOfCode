using System;

namespace AdventOfCode.Year2020
{
    public class Day05 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2020/day/5

        /// Generic idea for Day 5
        /// If you have spotted that the numbers being asked are encoded in binary
        /// then this day will be much easier
        /// BFFFBBF = 1000110 = 70

        public string SolvePart1(string input)
        {
            // read per line
            string[] lines = input.Split(Environment.NewLine);
            int highestSeatId = int.MinValue;
            foreach (string line in lines)
            {
                int row = BinarySeats(line[0..7]);      // takes the first 7 characters
                int column = BinarySeats(line[^3..]);   // takes the last 3 characters
                int seatId = (row * 8) + column;        // calculate seatID
                highestSeatId = Math.Max(seatId, highestSeatId);
            }
            return highestSeatId.ToString();
        }

        public string SolvePart2(string input)
        {
            // read per line
            string[] lines = input.Split(Environment.NewLine);
            int missingSeatId = 0;
            var seats = new int[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                int row = BinarySeats(lines[i][0..7]);      // takes the first 7 characters
                int column = BinarySeats(lines[i][^3..]);   // takes the last 3 characters
                int seatId = (row * 8) + column;            // calculate seatID
                seats[i] = seatId;                          // save the seatID
            }

            // sort and see which seat is missing
            Array.Sort(seats);
            for (int i = 1; i < seats.Length; i++)
            {
                if ((seats[i] - seats[i - 1]) == 2)
                {
                    missingSeatId = seats[i] - 1;
                }
            }
            return missingSeatId.ToString();
        }

        // small pun on BinarySearch
        // Directly calculate the number without converters
        public int BinarySeats(string code)
        {
            // the max increment = 2^6 (64), which is then halved every round
            int seatNumber = 0;
            int increment = (int)Math.Pow(2, (code.Length - 1));

            foreach (char isUpperHalf in code)
            {
                // actually test if this is the upper half (binary 1)
                if (isUpperHalf == 'B' || isUpperHalf == 'R')
                {
                    seatNumber += increment;
                }
                increment /= 2; // next  will be half of the current ( 64/32/16/8/4/2/1 )
            }
            return seatNumber;
        }

        // Did not end up using this code -> BinarySeats = better then this
        // Idea: Convert the seat-code to a binary string (like 010111)
        // Then convert binary string to int using built-in converter
        public int ConvertSeatingToNumber(string code)
        {
            var binaryChars = new char[code.Length];
            for (int i = 0; i < code.Length; i++)
            {
                char binchar = '0';
                if (code[i] == 'B' || code[i] == 'R') { binchar = '1'; }
                binaryChars[i] = binchar;
            }
            return Convert.ToInt32(new string(binaryChars), 2);
        }

        public string GetInput()
        {
            return new Inputs.Year2020.Day05().Input;
        }
    }
}