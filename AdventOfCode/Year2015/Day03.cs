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
            var locations = new HashSet<Tuple<int, int>>() { new Tuple<int, int>(x, y) };

            for (int i = 0; i < input.Length; i++)
            {
                switch (input[i])
                {
                    case '^':
                        y++;
                        break;

                    case 'v':
                        y--;
                        break;

                    case '>':
                        x++;
                        break;

                    case '<':
                        x--;
                        break;

                    default:
                        throw new ArgumentOutOfRangeException("input can only contain <>^v");
                        //break;
                }
                locations.Add(new Tuple<int, int>(x, y));
            }
            return locations.Count.ToString();
        }

        public string SolvePart2(string input)
        {
            // position[0] = Santa X coordinate
            // position[1] = Santa Y coordinate
            // position[2] = RoboSanta X coordinate
            // position[3] = RoboSanta Y coordinate
            var positions = new int[] { 0, 0, 0, 0 };
            var locations = new HashSet<Tuple<int, int>>() { new Tuple<int, int>(positions[0], positions[1]) };

            for (int i = 0; i < input.Length; i++)
            {
                bool isSanta = i % 2 == 0;
                
                // update position
                switch (input[i])
                {
                    case '^':
                        positions[isSanta ? 1 : 3]++;
                        break;

                    case 'v':
                        positions[isSanta ? 1 : 3]--;
                        break;

                    case '>':
                        positions[isSanta ? 0 : 2]++;
                        break;

                    case '<':
                        positions[isSanta ? 0 : 2]--;
                        break;

                    default:
                        throw new ArgumentOutOfRangeException("input can only contain <>^v");
                        //break;
                }

                locations.Add(new Tuple<int, int>(positions[isSanta?0:2], positions[isSanta?1:3]));
            }

            return locations.Count.ToString();
        }
    }
}