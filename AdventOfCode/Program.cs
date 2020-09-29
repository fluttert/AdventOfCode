using System;
using System.Diagnostics;

namespace AdventOfCode
{
    internal class AdventOfCode
    {
        private static void Main()
        {
            var aoc = new AdventOfCode();

            // Change this to the day you want to run
            aoc.Solve(new Year2015.Day23());
            //aoc.Solve(new Year2015.Day22());
            //aoc.Solve(new Year2015.Day21());
            //aoc.Solve(new Year2015.Day20());
            //aoc.Solve(new Year2015.Day19());
            //aoc.Solve(new Year2015.Day18());
            //aoc.Solve(new Year2015.Day17());
            //aoc.Solve(new Year2015.Day16());
            //aoc.Solve(new Year2015.Day15());
            //aoc.Solve(new Year2015.Day14());
            //aoc.Solve(new Year2015.Day13());
            //aoc.Solve(new Year2015.Day12());
            //aoc.Solve(new Year2015.Day11());
            //aoc.Solve(new Year2015.Day10());
            //aoc.Solve(new Year2015.Day09());
            //aoc.Solve(new Year2015.Day08());
            //aoc.Solve(new Year2015.Day07());
            //aoc.Solve(new Year2015.Day06());
            //aoc.Solve(new Year2015.Day05());
            //aoc.Solve(new Year2015.Day04()); // slow: 18370ms
            //aoc.Solve(new Year2015.Day03());
            //aoc.Solve(new Year2015.Day02());
            //aoc.Solve(new Year2015.Day01());

            // timing

            Console.ReadLine();
        }

        public void Solve(IAoC aoc)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine($"{aoc.GetType().Name}, part 1: {aoc.SolvePart1(aoc.GetInput())}");
            Console.WriteLine($"{aoc.GetType().Name}, part 2: {aoc.SolvePart2(aoc.GetInput())}");
            Console.WriteLine("");
            Console.WriteLine($"Execution time was about {stopwatch.ElapsedMilliseconds}ms");
        }
    }
}