using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2016
{
    public class Day01 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2016/day/1

        public string SolvePart1(string input)
        {
            int vertical = 0, horizontal = 0;   // coordinate
            char direction = 'N';               // N E S W
            var instructions = input.Replace(" ", "").Split(',');
            for (int i = 0; i < instructions.Length; i++)
            {
                char leftOrRight = instructions[i][0];
                int distance = int.Parse(instructions[i].Substring(1));
                int multiplier = 1; // negative distance or positive?

                if ((direction == 'N' && leftOrRight == 'L') ||
                    (direction == 'S' && leftOrRight == 'R') ||
                    (direction == 'E' && leftOrRight == 'R') ||
                    (direction == 'W' && leftOrRight == 'L'))
                {
                    multiplier = -1;
                }
                if (i % 2 == 0)  // on even turn we go east or west
                {
                    direction = multiplier == 1 ? 'E' : 'W';
                    horizontal += (multiplier * distance);
                }
                else  // on odd turn we go north south
                {
                    direction = multiplier == 1 ? 'N' : 'S';
                    vertical += (multiplier * distance);
                }
            }
            return (Math.Abs(horizontal) + Math.Abs(vertical)).ToString();
        }

        public string SolvePart2(string input)
        {
            var coordinates = new HashSet<Tuple<int, int>>();
            int vertical = 0, horizontal = 0;
            char direction = 'N'; // N E S W
            var instructions = input.Replace(" ", "").Split(',');
            for (int i = 0; i < instructions.Length; i++)
            {
                char leftOrRight = instructions[i][0];
                int distance = int.Parse(instructions[i].Substring(1));
                int multiplier = 1;

                if ((direction == 'N' && leftOrRight == 'L') ||
                    (direction == 'S' && leftOrRight == 'R') ||
                    (direction == 'E' && leftOrRight == 'R') ||
                    (direction == 'W' && leftOrRight == 'L'))
                {
                    multiplier = -1;
                }
                if (i % 2 == 0)
                {
                    direction = multiplier == 1 ? 'E' : 'W';
                    int newHorizontal = horizontal + (multiplier * distance);
                    while (horizontal != newHorizontal)
                    {
                        var coordinate = new Tuple<int, int>(horizontal, vertical);
                        if (coordinates.Contains(coordinate))
                        {
                            return (Math.Abs(horizontal) + Math.Abs(vertical)).ToString();
                        }
                        coordinates.Add(coordinate);
                        horizontal += multiplier;
                    }
                }
                else
                {
                    direction = multiplier == 1 ? 'N' : 'S';
                    int newVertical = vertical + (multiplier * distance);
                    while (vertical != newVertical)
                    {
                        var coordinate = new Tuple<int, int>(horizontal, vertical);
                        if (coordinates.Contains(coordinate))
                        {
                            return (Math.Abs(horizontal) + Math.Abs(vertical)).ToString();
                        }
                        coordinates.Add(coordinate);
                        vertical += multiplier;
                    }
                }
            }
            return (Math.Abs(horizontal) + Math.Abs(vertical)).ToString();
        }

        public string GetInput()
        {
            return new Inputs.Year2016.Day01().Input;
        }
    }
}