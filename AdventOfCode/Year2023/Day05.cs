using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2023
{
    public class Day05 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2023/day/5
        public string SolvePart1(string input)
        {
            string[] parts = input.Split(Environment.NewLine + Environment.NewLine);
            long[] seeds = Array.ConvertAll(parts[0][7..].Split(' ', StringSplitOptions.RemoveEmptyEntries), long.Parse);
            var maps = ParseMaps(parts[1..]);

            long lowestLocation = long.MaxValue;
            for (int i = 0; i < seeds.Length; i++)
            //for (int i = 0; i < 1; i++)
            {
                long seed = seeds[i]; // fetch seed
                long curPosition = seed;

                for (int j = 0; j < maps.Keys.Count; j++)
                {   // cycle through the maps
                    long updatedPosition = curPosition;

                    for (int k = 0; k < maps[j].Count; k++)
                    {   // Cycle through all ranges, update if found
                        updatedPosition = UpdatePosition(curPosition, maps[j][k]);
                        // we found a match! that means we can move on to the next map
                        if (updatedPosition != curPosition) { break; }
                    }
                    curPosition = updatedPosition;
                    //Console.WriteLine($"Seed {seed} after map {j} on position {curPosition}"); // debug
                }
                lowestLocation = curPosition < lowestLocation ? curPosition : lowestLocation;
            }
            return "" + lowestLocation;
        }

        public string SolvePart2(string input)
        {
            string[] parts = input.Split(Environment.NewLine + Environment.NewLine);
            List<(long start, long length)> seeds = ParseSeeds(parts[0]);
            var maps = ParseMaps(parts[1..]);

            long lowestLocation = long.MaxValue;
            for (int i = 0; i < seeds.Count; i++)
            {
                for (long seed = seeds[i].start; seed < seeds[i].start + seeds[i].length; seed++)
                {
                    long curPosition = seed;

                    for (int j = 0; j < maps.Keys.Count; j++)
                    {   // cycle through the maps
                        long updatedPosition = curPosition;

                        for (int k = 0; k < maps[j].Count; k++)
                        {   // Cycle through all ranges, update if found
                            updatedPosition = UpdatePosition(curPosition, maps[j][k]);
                            // we found a match! that means we can move on to the next map
                            if (updatedPosition != curPosition) { break; }
                        }
                        curPosition = updatedPosition;
                        //Console.WriteLine($"Seed {seed} after map {j} on position {curPosition}"); // debug
                    }
                    lowestLocation = curPosition < lowestLocation ? curPosition : lowestLocation;
                }
            }
            return "" + lowestLocation;
        }

        private Dictionary<int, List<(long sourceStart, long sourceLength, long destStart)>> ParseMaps(string[] parts)
        {
            Dictionary<int, List<(long sourceStart, long sourceLength, long destStart)>> maps = new();
            int map = 0;
            for (int i = 0; i < parts.Length; i++)
            {
                maps.Add(map, new());
                string[] fromtoMap = parts[i].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

                // ignore the first line (eg: "seed-to-soil map:"  )
                for (int j = 1; j < fromtoMap.Length; j++)
                {
                    long[] numbers = Array.ConvertAll(fromtoMap[j].Split(' ', StringSplitOptions.RemoveEmptyEntries), long.Parse);
                    maps[map].Add((numbers[1], numbers[2], numbers[0]));
                }

                // go to next map
                map++;
            }
            return maps;
        }

        private long UpdatePosition(long position, (long sourceStart, long sourceLength, long destStart) range)
        {
            long sourceEnd = range.sourceStart + range.sourceLength;
            long newPosition = position;
            if (position >= range.sourceStart && position < sourceEnd)
            {
                newPosition = range.destStart + (position - range.sourceStart);
            }
            return newPosition;
        }

        private List<(long start, long length)> ParseSeeds(string input) {
            List<(long start, long length)> seeds = new();
            var raw = Array.ConvertAll(input[7..].Split(' ', StringSplitOptions.RemoveEmptyEntries), long.Parse);
            for(int i = 0; i<raw.Length;i+=2) {
                seeds.Add((raw[i], raw[i + 1]));
            }
            return seeds;
        }

        public string GetInput()
        {
            return new Inputs.Year2023.Day05Input().Input;
//            return """
//seeds: 79 14 55 13

//seed-to-soil map:
//50 98 2
//52 50 48

//soil-to-fertilizer map:
//0 15 37
//37 52 2
//39 0 15

//fertilizer-to-water map:
//49 53 8
//0 11 42
//42 0 7
//57 7 4

//water-to-light map:
//88 18 7
//18 25 70

//light-to-temperature map:
//45 77 23
//81 45 19
//68 64 13

//temperature-to-humidity map:
//0 69 1
//1 0 69

//humidity-to-location map:
//60 56 37
//56 93 4
//""";
        }
    }
}