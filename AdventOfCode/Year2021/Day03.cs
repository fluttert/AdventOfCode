using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Year2021
{
    public class Day03 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2021/day/3

        public string SolvePart1(string input)
        {
            // Convert input to usable instructions
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            StringBuilder mostCommon = new StringBuilder(); // gamma
            StringBuilder leastCommon = new StringBuilder(); // eps

            // go through the matrix :P
            for (int i = 0; i < lines[0].Length; i++)
            {   // lines
                int zeros = 0;
                for (int j = 0; j < lines.Length; j++)
                {   // same position on all lines
                    if (lines[j][i] == '0') { zeros++; }
                }

                // assign most/least to each one
                int ones = lines.Length - zeros;
                if (zeros > ones)
                {
                    mostCommon.Append('0');
                    leastCommon.Append('1');
                }
                else
                {
                    mostCommon.Append('1');
                    leastCommon.Append('0');
                }
            }

            // Binary to Integer
            int gammaRate = Convert.ToInt32(mostCommon.ToString(), 2); ;
            int epsilonRate = Convert.ToInt32(leastCommon.ToString(), 2);

            return "" + epsilonRate * gammaRate;
        }

        public string SolvePart2(string input)
        {
            // Convert input to usable instructions
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            var oxygen = new List<string>(lines);
            var co2 = new List<string>(lines);

            // Oxygen
            while (oxygen.Count > 1)
            {
                // go through the positions
                for (int i = 0; i < oxygen[0].Length; i++)
                {   // lines
                    var zeroList = new List<string>();
                    var onesList = new List<string>();
                    for (int j = 0; j < oxygen.Count; j++)
                    {   // same position on all lines
                        if (oxygen[j][i] == '0') { zeroList.Add(oxygen[j]); }
                        else { onesList.Add(oxygen[j]); }
                    }
                    // update list
                    oxygen = onesList.Count >= zeroList.Count ? onesList : zeroList;
                }
            }

            // co2
            while (co2.Count > 1)
            {
                // go through the positions
                for (int i = 0; i < co2[0].Length; i++)
                {   // lines
                    var zeroList = new List<string>();
                    var onesList = new List<string>();
                    for (int j = 0; j < co2.Count; j++)
                    {   // same position on all lines
                        if (co2[j][i] == '0') { zeroList.Add(co2[j]); }
                        else { onesList.Add(co2[j]); }
                    }
                    // update list
                    co2  = onesList.Count >= zeroList.Count ? zeroList : onesList;
                    if (co2.Count == 1) { break; }
                    
                }
            }

            // Binary to Integer
            int oxygenRating = Convert.ToInt32(oxygen[0], 2); ;
            int co2Rating = Convert.ToInt32(co2[0], 2);

            // 3205048 == to high
            return "" + oxygenRating * co2Rating;
        }

        public string GetInput()
        {
            return new Inputs.Year2021.Day03().Input;
        }
    }
}