using System;
using System.Diagnostics;

namespace AdventOfCode
{
    class AdventOfCode
    {
        static void Main(string[] args)
        {
            var aoc = new AdventOfCode();
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            
            // Change this to the day you want to run
            aoc.Year2015Day02();
            
            // timing
            Console.WriteLine("");
            Console.WriteLine($"Execution time was about {stopwatch.ElapsedMilliseconds}ms");
            Console.ReadLine();
        }

        public void Year2015Day02()
        {
            var day = new Year2015.Day02();
            Console.WriteLine($"Day 02, part 1: {day.Part1(Inputs.Year2015.Day02.Input)}");
            Console.WriteLine($"Day 02, part 2: {day.Part2(Inputs.Year2015.Day02.Input)}");
        }

        public void Year2015Day01() {
            var day = new Year2015.Day01();
            Console.WriteLine($"Day 01, part 1: {day.Part1(Inputs.Year2015.Day01.Input)}");
            Console.WriteLine($"Day 01, part 2: {day.Part2(Inputs.Year2015.Day01.Input)}");
        }
    }
}