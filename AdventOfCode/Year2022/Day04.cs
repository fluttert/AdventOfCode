using System;

namespace AdventOfCode.Year2022
{
    public class Day04 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2022/day/4

        public string SolvePart1(string input)
        {
            int overlappingPairs = 0;
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                string[] pairs = line.Split(',');
                // parse
                int[] pair1Range = Array.ConvertAll(pairs[0].Split('-'), int.Parse);
                int[] pair2Range = Array.ConvertAll(pairs[1].Split('-'), int.Parse);

                bool overlap1 = pair1Range[0] >= pair2Range[0] && pair1Range[1] <= pair2Range[1];
                bool overlap2 = pair1Range[0] <= pair2Range[0] && pair1Range[1] >= pair2Range[1];
                if (overlap1 || overlap2)
                {
                    overlappingPairs++;
                }
            }
            return "" + overlappingPairs;
        }

        public string SolvePart2(string input)
        {
            int overlappingPairs = 0;
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                string[] pairs = line.Split(',');
                // parse
                int[] pair1Range = Array.ConvertAll(pairs[0].Split('-'), int.Parse);
                int[] pair2Range = Array.ConvertAll(pairs[1].Split('-'), int.Parse);

                bool overLap1 = pair1Range[1] >= pair2Range[0];
                // sort make sure smallest is first
                if (pair1Range[0] > pair2Range[0])
                {
                    overLap1 = pair2Range[1] >= pair1Range[0];
                }
               
                if (overLap1)
                {
                    overlappingPairs++;
                }
            }
            return "" + overlappingPairs;
        }

        public string GetInput()
        {
            return new Inputs.Year2022.Day04().Input;
        }
    }
}