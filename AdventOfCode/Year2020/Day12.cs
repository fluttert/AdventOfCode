using System;

namespace AdventOfCode.Year2020
{
    public class Day12 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2020/day/12

        /// Generic idea for Day 12
        /// Hardest part is to determine how to turn the ship

        public string SolvePart1(string input)
        {
            // read per lines
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            int x = 0, y = 0;
            char facing = 'E';

            foreach (var line in lines)
            {
                char action = line[0];
                int val = int.Parse(line[1..]);
                // determine if ship turns
                if ((action == 'R') || (action == 'L'))
                {
                    bool changed = false;
                    // REVERSE
                    if (line == "R180" || line == "L180")
                    {
                        if (facing == 'E') { facing = 'W'; changed = true; }
                        if (facing == 'W' && !changed) { facing = 'E'; }
                        if (facing == 'N') { facing = 'S'; changed = true; }
                        if (facing == 'S' && !changed) { facing = 'N'; }
                    }
                    // go LEFT
                    if (line == "R270" || line == "L90")
                    {
                        if (facing == 'E') { facing = 'N'; changed = true; }
                        if (facing == 'W' && !changed) { facing = 'S'; changed = true; }
                        if (facing == 'N' && !changed) { facing = 'W'; changed = true; }
                        if (facing == 'S' && !changed) { facing = 'E'; }
                    }
                    // go Right
                    if (line == "R90" || line == "L270")
                    {
                        if (facing == 'E') { facing = 'S'; changed = true; }
                        if (facing == 'W' && !changed) { facing = 'N'; changed = true; }
                        if (facing == 'N' && !changed) { facing = 'E'; changed = true; }
                        if (facing == 'S' && !changed) { facing = 'W'; }
                    }
                }

                // forward is just go in some facing direction
                if (action == 'F') { action = facing; }

                // actual movement
                if (action == 'N') { x += val; continue; }
                if (action == 'S') { x -= val; continue; }
                if (action == 'E') { y += val; continue; }
                if (action == 'W') { y -= val; continue; }
            }

            return (Math.Abs(x) + Math.Abs(y)).ToString();
        }

        // in progress
        public string SolvePart2(string input)
        {
            // read per lines
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            // ship (x,y), and waypoint (x,y)
            long sx = 0, sy = 0, wx = 10, wy = 1;

            foreach (var line in lines)
            {
                char action = line[0];
                long val = long.Parse(line[1..]);

                // Move the waypoint
                if (action == 'N') { wy += val; continue; }
                if (action == 'S') { wy -= val; continue; }
                if (action == 'E') { wx += val; continue; }
                if (action == 'W') { wx -= val; continue; }

                // Forward is the only thing moving the ship!
                if (action == 'F')
                {
                    sx += val * wx;
                    sy += val * wy;
                }

                // determine if waypoint turns
                if ((action == 'R') || (action == 'L'))
                {
                    // mirror position (10,4) -> (-10,-4)
                    if (line == "R180" || line == "L180")
                    {
                        wx *= -1;
                        wy *= -1;
                    }
                    // go Right (10,4) -> (4,-10)
                    if (line == "R90" || line == "L270")
                    {
                        long tmp = wx;
                        wx = wy;
                        wy = tmp * -1;
                    }
                    // go LEFT (10,4) -> (-4,10)
                    if (line == "R270" || line == "L90")
                    {
                        long tmp = wx;
                        wx = wy * -1;
                        wy = tmp;
                    }
                }
            }

            return (Math.Abs(sx) + Math.Abs(sy)).ToString();
        }

        public string GetInput()
        {
            return new Inputs.Year2020.Day12().Input;
        }
    }
}