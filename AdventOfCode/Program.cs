using System;
using System.Diagnostics;

namespace AdventOfCode
{
    internal class AdventOfCode
    {
        private static void Main()
        {
            var aoc = new AdventOfCode();
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // Change this to the day you want to run
            aoc.Solve(new Year2015.Day08()); 
            //aoc.Solve(new Year2015.Day07());
            //aoc.Solve(new Year2015.Day06());
            //aoc.Solve(new Year2015.Day05());
            //aoc.Solve(new Year2015.Day04()); // slow: 18370ms
            //aoc.Solve(new Year2015.Day03());
            //aoc.Solve(new Year2015.Day02());
            //aoc.Solve(new Year2015.Day01());

            // timing
            Console.WriteLine("");
            Console.WriteLine($"Execution time was about {stopwatch.ElapsedMilliseconds}ms");
            Console.ReadLine();
        }

        public void Solve(IAoC aoc)
        {
            Console.WriteLine($"{aoc.GetType().Name}, part 1: {aoc.SolvePart1(aoc.GetInput())}");
            Console.WriteLine($"{aoc.GetType().Name}, part 2: {aoc.SolvePart2(aoc.GetInput())}");
        }
    }
}