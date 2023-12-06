using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2023
{
    public class Day06 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2023/day/6
        public string SolvePart1(string input)
        {
            var races = ParseRaces(input);
            long result = 1;
            foreach (var race in races)
            {
                int recordDistance = race.distance;
                int beatingRangeStart = -1, beatingRangeEnd = -1;
                // due to parabolic nature (quadratic equation)
                // beating range end = Time - beatingRangeStart
                for (int i = 0; i < race.time; i++)
                {
                    // distance = speed * remaining time
                    if ((i * (race.time - i)) > recordDistance)
                    {
                        beatingRangeStart = i;
                        beatingRangeEnd = race.time - beatingRangeStart;
                        break;
                    }
                }
                int amountOfWaysToBeatRecord = beatingRangeEnd - beatingRangeStart + 1;
                result *= amountOfWaysToBeatRecord;
            }
            return "" + result;
        }

        public string SolvePart2(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            long racetime = long.Parse(lines[0][10..].Replace(" ", "").Trim());
            long recordDistance = long.Parse(lines[1][10..].Replace(" ", "").Trim());
            long beatingRangeStart = -1, beatingRangeEnd = -1;
            for (long i = 0; i < racetime; i++)
            {
                // distance = speed * remaining time
                if ((i * (racetime - i)) > recordDistance)
                {
                    beatingRangeStart = i;
                    beatingRangeEnd = racetime - beatingRangeStart;
                    break;
                }
            }

            return "" + (beatingRangeEnd - beatingRangeStart + 1);
        }

        private List<(int time, int distance)> ParseRaces(string input)
        {
            List<(int, int)> values = new();
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            var times = Array.ConvertAll(lines[0][10..].Split(' ', StringSplitOptions.RemoveEmptyEntries), int.Parse);
            var distances = Array.ConvertAll(lines[1][10..].Split(' ', StringSplitOptions.RemoveEmptyEntries), int.Parse);
            for (int i = 0; i < times.Length; i++)
            {
                values.Add((times[i], distances[i]));
            }
            return values;
        }

        public string GetInput()
        {
            return new Inputs.Year2023.Day06Input().Input;
//            return """
//Time:      7  15   30
//Distance:  9  40  200
//""";
        }
    }
}