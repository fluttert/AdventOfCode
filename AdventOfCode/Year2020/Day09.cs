using System;

namespace AdventOfCode.Year2020
{
    public class Day09 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2020/day/9

        /// Generic idea for Day 9
        /// Today is about sliding-window / rolling-window: Search for subranges within ranges
        /// Both can be bruteforced, part 1 can be pre-calculated
        /// Some theory reading on https://www.baeldung.com/cs/sliding-window-algorithm

        public string SolvePart1(string input)
        {
            // read per line
            string[] lines = input.Split(Environment.NewLine);
            return XMASBreaker(lines, 25).ToString();
        }

        public string SolvePart2(string input)
        {
            // read per line
            string[] lines = input.Split(Environment.NewLine);
            long[] numbers = Array.ConvertAll(lines, long.Parse);
            long invalidNumber = XMASBreaker(lines, 25);
            long sumSmallestAndLargest = -1;

            // Sliding windows brute-force with break as soon as possible
            // i = starting point, index = last added number
            for (int i = 0; i < numbers.Length; i++)
            {
                long sum = 0;
                int index = i;
                while (sum < invalidNumber)
                {
                    sum += numbers[index];
                    if (sum == invalidNumber)
                    {
                        //sumSmallestAndLargest = numbers[i] + numbers[index];
                        //Console.WriteLine($"Range found from: {i} ({numbers[i]}) up to {index} ({numbers[index]})");
                        // The numbers are NOT in sorted order, slice the array (sub-array) and only sort that
                        var sortedRange = numbers[i..(index+1)];
                        Array.Sort(sortedRange);
                        sumSmallestAndLargest = sortedRange[0] + sortedRange[^1];
                        break;      // found it, so break the while-loop
                    }
                    index++;
                }
                if (sumSmallestAndLargest > 0) { break; }   // found it, break the for-loop
            }
            return sumSmallestAndLargest.ToString();
        }

        public long XMASBreaker(string[] input, int preamble = 25)
        {
            // convert everything to int64 (there are some huge numbers in the input)
            long[] numbers = Array.ConvertAll(input, long.Parse);
            long invalidNumber = -1;
            // create a sliding window
            for (int i = preamble; i < numbers.Length; i++)
            {
                long numberToCheck = numbers[i];
                bool numberFound = false;
                // bruteforce all combinations, but early break where possible
                // this is all the combination within the preamble windows (sliding window)
                for (int j = 0; j < preamble; j++)
                {
                    for (int k = j + 1; k < preamble; k++)
                    {
                        // if the sum matches we can continue to the next numbers
                        long sum = numbers[i - j - 1] + numbers[i - k - 1];
                        if (sum == numberToCheck) { numberFound = true; break; }
                    }
                    if (numberFound) { break; } // break for-loop with j, up to the next number
                }
                // if we have found an invalid number, stop searching, break for-loop i
                if (numberFound is false) { invalidNumber = numberToCheck; break; }
            }
            return invalidNumber;
        }

        public string GetInput()
        {
            return new Inputs.Year2020.Day09().Input;
        }
    }
}