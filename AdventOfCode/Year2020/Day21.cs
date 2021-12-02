using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2020
{
    public class Day21 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2020/day/21

        /// Generic idea for Day 21
        /// create a histogram, and reduce
        /// 
        public string SolvePart1(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            // a b c d = x y
            // b e f = y
            // g h i 

            // line 1: y = a b c d
            // line 1: x = a b c d
            // line 2: y = b e f -> union with line 1-y 
            // y = b , remove from other 'allergenes'

            // map ingredients to allergens
            Dictionary<string, string> ingredients = new ();
            // map allergenes to ingredients
            Dictionary<string, HashSet<string>> allergenes = new();




            return "";
        }

        // in progress
        public string SolvePart2(string input)
        {
            // read per lines
            return "";
        }

        public string GetInput()
        {
            return new Inputs.Year2020.Day21().Input;
        }
    }
}