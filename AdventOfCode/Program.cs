using System;

namespace AdventOfCode
{
    class AdventOfCode
    {
        static void Main(string[] args)
        {
            var aoc = new AdventOfCode();
            aoc.Year2015Day01();
        }

        public void Year2015Day01() {
            var Day01 = new Year2015.Day01();
            Console.WriteLine($"Day 01, part 1: {Day01.Part1(Inputs.Year2015.Day01.Input)}"); 
        }
    }
}