using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2020
{
    public class Day15 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2020/day/15

        /// Generic idea for Day 15
        ///
        public string SolvePart1(string input)
        {

            return MemoryGame(input, 2020).ToString();
        }

        // in progress
        public string SolvePart2(string input)
        {
            // read per lines
            return MemoryGame(input, 30000000).ToString();
        }

        public int MemoryGame(string input, int turnsBeforeExit)
        {

            int[] startNumbers = Array.ConvertAll(input.Split(','), int.Parse);
            var dict = new Dictionary<int, int>(); // spokennumber - lastRound 
            int turn = 1;
            int lastNumber = -1;

            // add the initials
            foreach (var number in startNumbers)
            {
                dict.Add(number, turn);
                turn++;
                lastNumber = number;
            }

            while (turn <= turnsBeforeExit)
            {
                // consider last number
                int nextNumber = -1;


                // If it was spoken for the first time, tell zero!
                if ((turn - 1 == dict[lastNumber]))
                {
                    nextNumber = 0;
                }

                // otherwise is was spoken before
                else
                {
                    nextNumber = turn - 1 - dict[lastNumber];
                    dict[lastNumber] = turn - 1;
                }



                // add to dictionary if necessary
                if (dict.ContainsKey(nextNumber) is false) { dict.Add(nextNumber, turn); }
                lastNumber = nextNumber;
                turn++;
            }


            return lastNumber;
        } 
       

        public string GetInput() => @"6,4,12,1,20,0,16";
    }
}