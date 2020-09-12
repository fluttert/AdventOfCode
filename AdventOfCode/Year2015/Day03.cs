using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2015
{
    public class Day03 : IAoC
    {
        // Challenge can be found on https://adventofcode.com/2015/day/3

        public string SolvePart1(string input)
        {
            int x = 0, y = 0; // position
            var locations = new HashSet<Tuple<int, int>>() { new Tuple<int, int>(x,y) };

            for (int i = 0; i < input.Length; i++)
            {
                switch (input[i])
                {
                    case '^': y++;
                        break;
                    case 'v': y--;
                        break;
                    case '>': x++;
                        break;
                    case '<': x--;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("input can only contain <>^v");
                        break;
                }
                locations.Add(new Tuple<int, int>(x, y));
            }
            return locations.Count.ToString();
        }

        public string SolvePart2(string input)
        {
            return null;
        }
    }
}