using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2020
{
    public class Day20 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2020/day/20

        /// Generic idea for Day 20
        /// rotate and flip the shiznits out of it
        /// Each puzzle piece has 6 states
        
        /// initial
        /// 1 2
        /// 3 4
        /// 5 6
        
        /// flipped
        /// 5 6  2 1
        /// 3 4  4 3
        /// 1 2  6 5
        
        /// rotated 180 degrees
        /// 6 5
        /// 4 3
        /// 2 1
        
        /// rotated flipped
        /// 2 1  5 6    
        /// 4 3  3 4  
        /// 6 5  1 2  
        /// 
        public string SolvePart1(string input)
        {
            // read per line and convert to integer array
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            var puzzlePieces = new List<PuzzlePiece>();
            int index = -1;
            foreach (var line in lines)
            {
                if (line.StartsWith("Tile"))
                {
                    var p = new PuzzlePiece(line);
                    puzzlePieces.Add(p);
                    index++;
                    continue;
                }
                puzzlePieces[index].AddLine(line);
            }

            // now mix and match

            return "";
        }

        public string SolvePart2(string input)
        {
            // read per line and convert to integer array
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            return "";
        }

        public string GetInput()
        {
            return new Inputs.Year2020.Day20().Input;
        }
    }



    public class PuzzlePiece
    {
        public long Id = -1;
        public List<string> Lines;

        public PuzzlePiece(string name)
        {
            Id = long.Parse(name.Split(new char[] { ' ', ':' })[1]);
            Lines = new List<string>();
        }

        public void AddLine(string line)
        {
            Lines.Add(line);
        }

        public List<string> ShortEdges()
        {
            var l = new List<string>();
            l.Add(Lines[0]);
            l.Add(Reverse(Lines[0]));
            return l;
        }

        private string Reverse(string line)
        {
            char[] rev = new char[line.Length];
            int index = 0;
            for (int i = line.Length - 1; i >= 0; i--)
            {
                rev[index] = line[i];
                index++;
            }
            return new string(rev);
        }
    }
}