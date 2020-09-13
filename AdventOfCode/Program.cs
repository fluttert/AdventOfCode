using System;
using System.Diagnostics;

namespace AdventOfCode
{
    internal class AdventOfCode
    {
        private static void Main(string[] args)
        {
            var aoc = new AdventOfCode();
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // Change this to the day you want to run
            aoc.Solve(new Year2015.Day05(), Inputs.Year2015.Day05.Input);
            //aoc.Solve(new Year2015.Day04(), new Year2015.Day04().input); // slow: 18370ms
            //aoc.Solve(new Year2015.Day03(), Inputs.Year2015.Day03.Input);
            //aoc.Solve(new Year2015.Day02(), Inputs.Year2015.Day02.Input);
            //aoc.Solve(new Year2015.Day01(), Inputs.Year2015.Day01.Input);

            // timing
            Console.WriteLine("");
            Console.WriteLine($"Execution time was about {stopwatch.ElapsedMilliseconds}ms");
            Console.ReadLine();
        }

        public void Solve(IAoC aoc, string input)
        {
            Console.WriteLine($"{aoc.GetType().Name}, part 1: {aoc.SolvePart1(input)}");
            Console.WriteLine($"{aoc.GetType().Name}, part 2: {aoc.SolvePart2(input)}");
        }
    }
}