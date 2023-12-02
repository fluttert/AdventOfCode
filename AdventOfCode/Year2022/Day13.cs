using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2022
{
    public class Day13 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2022/day/13

        public string SolvePart1(string input)
        {
            // parse, not that gaps are removed
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            int inOrder = 0;
            for (int i = 0; i < lines.Length; i += 2)
            {
                string pair1 = lines[i];
                string pair2 = lines[i + 1];
                Stack<string> stack1 = new();
                Stack<string> stack2 = new();


                Console.WriteLine($"Pair1: {pair1}");
                Console.WriteLine($"Pair1: {pair2}");

                string[] piecesp1 = pair1.Split('[');

            }
           
            return ""+inOrder;
        }

        private string ListProcessor(string s) {
            // string should start with a list, if not => no list, just return
            if (s[0] != '[') { return s; }

            return null;
        }

        public string SolvePart2(string input)
        {
            // switched startpoint to endpoint -> break when first 'a' (0) is reached
            // parse
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            
            return "";
        }

        public string GetInput()
        {
            return new Inputs.Year2022.Day13().Input;
        }
    }
}