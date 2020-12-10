using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2020
{
    public class Day10 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2020/day/10

        /// Generic idea for Day 10

        public string SolvePart1(string input)
        {
            // read per line and convert to integer array
            int[] adapters = Array.ConvertAll(input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries), int.Parse);
            Array.Sort(adapters); // sorting
            int diff = adapters[0];
            int diff1Jolt = diff == 1 ? 1 : 0;
            int diff3Jolt = diff == 3 ? 1 : 0;
            for (int i = 1; i < adapters.Length; i++)
            {
                diff = adapters[i] - adapters[i - 1];
                diff1Jolt += diff == 1 ? 1 : 0;
                diff3Jolt += diff == 3 ? 1 : 0;
            }
            diff3Jolt++; // there is ALWAYS a difference of 3 to the max Joltage
            long sum = diff1Jolt * diff3Jolt;
            return sum.ToString();
        }

        // in progress
        public string SolvePart2(string input)
        {
            // read per line
            int[] adapters = Array.ConvertAll(input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries), int.Parse);
            Array.Sort(adapters); // sorting
            var adaptersAvailable = new HashSet<int>(adapters);
            var queue = new Queue<int>();
            queue.Enqueue(0); // starting point
            int maxJoltage = adapters[^1] + 3;
            long arragements = 0;
            while (queue.Count > 0) {
                int curJoltage = queue.Dequeue();
                if (adaptersAvailable.Contains(curJoltage + 1)) { arragements++; }
            }
            return arragements.ToString();
        }

        public string GetInput()
        {
            return new Inputs.Year2020.Day10().Input;
        }
    }
}