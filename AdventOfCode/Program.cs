using System;
using System.Diagnostics;

namespace AdventOfCode
{
    internal class AdventOfCode
    {
        private static void Main()
        {
            var aoc = new AdventOfCode();
            aoc.Solved2022();

            Console.ReadLine();
        }

        private void Solved2022()
        {
            var aoc = new AdventOfCode();
            //aoc.Solve(new Year2022.Day01());
            //aoc.Solve(new Year2022.Day02());
            //aoc.Solve(new Year2022.Day03());
            //aoc.Solve(new Year2022.Day04());
            //aoc.Solve(new Year2022.Day05());
            //aoc.Solve(new Year2022.Day06());
            //aoc.Solve(new Year2022.Day07());
            //aoc.Solve(new Year2022.Day08());
            aoc.Solve(new Year2022.Day10());
            //aoc.Solve(new Year2022.Day11());
        }

        private void Solved2021()
        {
            var aoc = new AdventOfCode();
            aoc.Solve(new Year2021.Day11());
            aoc.Solve(new Year2021.Day10());
            aoc.Solve(new Year2021.Day09());
            aoc.Solve(new Year2021.Day08());
            aoc.Solve(new Year2021.Day07());
            aoc.Solve(new Year2021.Day06());
            aoc.Solve(new Year2021.Day05());
            aoc.Solve(new Year2021.Day04());
            aoc.Solve(new Year2021.Day03());
            aoc.Solve(new Year2021.Day02());
            aoc.Solve(new Year2021.Day01());
        }

        private void Solved2020()
        {
            var aoc = new AdventOfCode();
            aoc.Solve(new Year2020.Day07());
            aoc.Solve(new Year2020.Day06());
            aoc.Solve(new Year2020.Day05());
            aoc.Solve(new Year2020.Day04());
            aoc.Solve(new Year2020.Day03());
            aoc.Solve(new Year2020.Day02());
            aoc.Solve(new Year2020.Day01());
        }

        private void Solved2019()
        {
            var aoc = new AdventOfCode();
            //aoc.Solve(new Year2019.Day07()); // 2 slow on part 2
            aoc.Solve(new Year2019.Day06());
            aoc.Solve(new Year2019.Day05());
            aoc.Solve(new Year2019.Day04());
            aoc.Solve(new Year2019.Day03());
            aoc.Solve(new Year2019.Day02());
            aoc.Solve(new Year2019.Day01());
        }

        private void Solved2017()
        {
            var aoc = new AdventOfCode();
            aoc.Solve(new Year2017.Day20());
            aoc.Solve(new Year2017.Day19());
            aoc.Solve(new Year2017.Day18());
            aoc.Solve(new Year2017.Day17());
            aoc.Solve(new Year2017.Day16());
            aoc.Solve(new Year2017.Day15()); // slow ~20 sec
            aoc.Solve(new Year2017.Day14());
            aoc.Solve(new Year2017.Day13());
            aoc.Solve(new Year2017.Day12());
            aoc.Solve(new Year2017.Day11());
            aoc.Solve(new Year2017.Day10());
            aoc.Solve(new Year2017.Day09());
            aoc.Solve(new Year2017.Day08());
            aoc.Solve(new Year2017.Day07());
            aoc.Solve(new Year2017.Day06());
            aoc.Solve(new Year2017.Day05());
            aoc.Solve(new Year2017.Day04());
            aoc.Solve(new Year2017.Day03());
            aoc.Solve(new Year2017.Day02());
            aoc.Solve(new Year2017.Day01());
        }

        private void Solved2016()
        {
            var aoc = new AdventOfCode();
            aoc.Solve(new Year2016.Day21());
            aoc.Solve(new Year2016.Day20());
            aoc.Solve(new Year2016.Day19());
            aoc.Solve(new Year2016.Day18());
            aoc.Solve(new Year2016.Day17());
            aoc.Solve(new Year2016.Day16());
            aoc.Solve(new Year2016.Day15());
            aoc.Solve(new Year2016.Day14()); // slow, MD5 cracking in 37 seconds
            aoc.Solve(new Year2016.Day13());
            aoc.Solve(new Year2016.Day12()); // slow (4 sec)
                                             // aoc.Solve(new Year2016.Day11()); // TODO TODO TODO
            aoc.Solve(new Year2016.Day10());
            aoc.Solve(new Year2016.Day09());
            aoc.Solve(new Year2016.Day08());
            aoc.Solve(new Year2016.Day07());
            aoc.Solve(new Year2016.Day06());
            aoc.Solve(new Year2016.Day05()); // slow around 14 seconds (MD5 challenge)
            aoc.Solve(new Year2016.Day04());
            aoc.Solve(new Year2016.Day03());
            aoc.Solve(new Year2016.Day02());
            aoc.Solve(new Year2016.Day01());
        }

        private void Solved2015()
        {
            var aoc = new AdventOfCode();
            aoc.Solve(new Year2015.Day25());
            aoc.Solve(new Year2015.Day24());
            aoc.Solve(new Year2015.Day23());
            aoc.Solve(new Year2015.Day22());
            aoc.Solve(new Year2015.Day21());
            aoc.Solve(new Year2015.Day20());
            aoc.Solve(new Year2015.Day19());
            aoc.Solve(new Year2015.Day18());
            aoc.Solve(new Year2015.Day17());
            aoc.Solve(new Year2015.Day16());
            aoc.Solve(new Year2015.Day15());
            aoc.Solve(new Year2015.Day14());
            aoc.Solve(new Year2015.Day13());
            aoc.Solve(new Year2015.Day12());
            aoc.Solve(new Year2015.Day11());
            aoc.Solve(new Year2015.Day10());
            aoc.Solve(new Year2015.Day09());
            aoc.Solve(new Year2015.Day08());
            aoc.Solve(new Year2015.Day07());
            aoc.Solve(new Year2015.Day06());
            aoc.Solve(new Year2015.Day05());
            aoc.Solve(new Year2015.Day04()); // slow: 18370ms
            aoc.Solve(new Year2015.Day03());
            aoc.Solve(new Year2015.Day02());
            aoc.Solve(new Year2015.Day01());
        }

        public void Solve(IAoC aoc)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine($"{aoc.GetType().Namespace}");
            Console.WriteLine($"{aoc.GetType().Name}, part 1: {aoc.SolvePart1(aoc.GetInput())}");
            Console.WriteLine($"{aoc.GetType().Name}, part 2: {aoc.SolvePart2(aoc.GetInput())}");
            Console.WriteLine(string.Empty);
            Console.WriteLine($"Execution time was about {stopwatch.ElapsedMilliseconds}ms");
            Console.WriteLine(string.Empty);
            Console.WriteLine(string.Empty);
        }
    }
}