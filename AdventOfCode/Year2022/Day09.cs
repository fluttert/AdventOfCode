using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2022
{
    public class Day09 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2022/day/09

        public string SolvePart1(string input)
        {
            // parse
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            (int x, int y) head = (0, 0);
            (int x, int y) tail = (0, 0);
            HashSet<(int x, int y)> path = new();

            for (int i = 0; i < lines.Length; i++)
            {
                // process instruction
                char direction = lines[i][0];
                int steps = int.Parse(lines[i][2..]);
                int stepsDone = 0;
                while (stepsDone < steps)
                {
                    (int x, int y) updatedTail = (tail.x, tail.y);
                    // update head
                    if (direction == 'R') { head.x++; }
                    if (direction == 'L') { head.x--; }
                    if (direction == 'U') { head.y++; }
                    if (direction == 'D') { head.y--; }

                    // oh oh! snap the tail closer!
                    if (Math.Abs(head.x - tail.x) > 1)
                    {
                        updatedTail.x = head.x - tail.x > 0 ? head.x - 1 : head.x + 1;
                        updatedTail.y = head.y;
                    }
                    if (Math.Abs(head.y - tail.y) > 1)
                    {
                        updatedTail.x = head.x;
                        updatedTail.y = head.y - tail.y > 0 ? head.y - 1 : head.y + 1;
                    }

                    tail = updatedTail;

                    // tags path
                    //Console.WriteLine($"Tail: ({tail.x},{tail.y})");
                    path.Add(tail);

                    // next step
                    stepsDone++;
                }
            }

            return "" + path.Count;
        }

        public string SolvePart2(string input)
        {
            // parse
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            (int x, int y) head = (0, 0);
            HashSet<(int x, int y)> path = new();
            (int x, int y)[] knots = new (int x, int y)[10];
            for (int i = 0; i < 10; i++) { knots[i] = (0, 0); }

            for (int i = 0; i < lines.Length; i++)
            {
                // process instruction
                char direction = lines[i][0];
                int steps = int.Parse(lines[i][2..]);
                int stepsDone = 0;
                while (stepsDone < steps)
                {
                    if (direction == 'R') { knots[0].x++; }
                    if (direction == 'L') { knots[0].x--; }
                    if (direction == 'U') { knots[0].y++; }
                    if (direction == 'D') { knots[0].y--; }

                    // update all other knots
                    for (int j = 1; j < knots.Length; j++)
                    {
                        // special case when moving diagonally
                        if (Math.Abs(knots[j - 1].x - knots[j].x) == 2
                            && Math.Abs(knots[j - 1].y - knots[j].y) == 2)
                        {
                            knots[j].x = knots[j - 1].x - knots[j].x > 0 ? knots[j - 1].x - 1 : knots[j - 1].x + 1;
                            knots[j].y = knots[j - 1].y - knots[j].y > 0 ? knots[j - 1].y - 1 : knots[j - 1].y + 1;
                        }

                        // regular behaviour
                        if (Math.Abs(knots[j - 1].x - knots[j].x) > 1)
                        {
                            knots[j].x = knots[j - 1].x - knots[j].x > 0 ? knots[j - 1].x - 1 : knots[j - 1].x + 1;
                            knots[j].y = knots[j - 1].y;
                        }
                        if (Math.Abs(knots[j - 1].y - knots[j].y) > 1)
                        {
                            knots[j].x = knots[j - 1].x;
                            knots[j].y = knots[j - 1].y - knots[j].y > 0 ? knots[j - 1].y - 1 : knots[j - 1].y + 1;
                        }
                    }
                    path.Add(knots[9]);

                    // next step
                    stepsDone++;
                }
            }

            // 2464 = too low
            return "" + path.Count;
        }

        public string GetInput()
        {
            return new Inputs.Year2022.Day09().Input;
        }
    }
}