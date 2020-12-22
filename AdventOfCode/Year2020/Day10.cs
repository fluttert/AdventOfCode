using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2020
{
    public class Day10 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2020/day/10

        /// Generic idea for Day 10
        /// Sort the list first, so we have ascending numbers for the Joltage Adapters
        /// Part 1: Sort & go through the list
        /// Part 2: Make a graph :)

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

        public string SolvePart2(string input)
        {
            // To make this work BETTER, add the zero :)
            input = "0" + Environment.NewLine + input;
            int[] adapters = Array.ConvertAll(input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries), int.Parse);

            // prep all the inputs
            Array.Sort(adapters);                           // sorting
            var groups = new List<List<int>>();
            var group = new List<int>();
            // phase 1: determine the parts (a hard disconnect on every 3 jump)
            for (int i = 0; i < adapters.Length; i++)
            {
                group.Add(adapters[i]);
                if (i == adapters.Length - 1 || adapters[i+1] - adapters[i] == 3)
                {
                    groups.Add(group);
                    group = new List<int>();
                }
            }

            // phase 2: count the possibilities in the sub-groups
            long sum = 1;
            foreach (var subgroup in groups) {
                // can only sort groups of 1 and 2's in 1 way
                if (subgroup.Count <= 2) { continue; }
                if (subgroup.Count == 3) { sum *= 2; }
                if (subgroup.Count == 4) { sum *= 4; }
                if (subgroup.Count == 5) { sum *= 7; }
                if (subgroup.Count == 6) { Console.WriteLine("There is a group of 6 what now?"); }
            }
            return sum.ToString();
        }

        //My very mathematical approach
        //  Group of 5
        //    0,1,2,3,4
        //    0,1,2,4
        //    0,1,3,4
        //    0,2,3,4
        //    0,1,4
        //    0,2,4
        //    0,3,4

        //  Group of 4
        //    4 5 6 7
        //    4 5 7
        //    4 6 7
        //    4 7

        //  Group of 3
        //    7 8 9
        //    7 9 

        public string GetInput()
        {
            return new Inputs.Year2020.Day10().Input;
        }
    }
}