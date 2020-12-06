using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2020
{
    public class Day06 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2020/day/6

        /// Generic idea for Day 6
        /// When we are comparing and checking lists; use the datastructure HashSet

        public string SolvePart1(string input)
        {
            // read per line
            string[] lines = input.Split(Environment.NewLine);
            int sumOfQuestions = 0;

            // a hashset only contains unique elements
            // add an element that already exists, does not alter the set
            // a hashset also had O(1) for looking up elements
            var letters = new HashSet<char>();
            for (int i = 0; i < lines.Length; i++)
            {
                // add count to sum + reset
                if (lines[i] == "")
                {
                    sumOfQuestions += letters.Count;
                    letters = new HashSet<char>();
                }
                // just count the letters
                foreach (char c in lines[i])
                {
                    if (char.IsLetterOrDigit(c)) { letters.Add(c); }
                }
            }
            return sumOfQuestions.ToString();
        }

        public string SolvePart2(string input)
        {
            // read per line
            string[] lines = input.Split(Environment.NewLine);
            int sumOfQuestions = 0;
            var letters = new HashSet<char>();


            for (int i = 0; i < lines.Length; i++)
            {
                // add count to sum + reset
                if (lines[i] == "")
                {
                    sumOfQuestions += letters.Count;
                    letters = new HashSet<char>();
                    continue;
                }

                
                bool firstLine = i == 0 || lines[i - 1] == "";
                var lettersUpdated = new HashSet<char>();
                foreach (char c in lines[i])
                {
                    // accept everything on the firstline
                    if (firstLine && char.IsLetterOrDigit(c))
                    {
                        lettersUpdated.Add(c);
                    }

                    // check otherwise if the answer was given before
                    else
                    {
                        if (char.IsLetterOrDigit(c) && letters.Contains(c))
                        {
                            lettersUpdated.Add(c);
                        }
                    }
                }
                // update the list
                letters = lettersUpdated;
            }

            return sumOfQuestions.ToString();
        }

        public string GetInput()
        {
            return new Inputs.Year2020.Day06().Input;
        }
    }
}