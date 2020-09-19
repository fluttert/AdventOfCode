using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2015
{
    public class Day14 : IAoC
    {
        // Challenge can be found on https://adventofcode.com/2015/day/14

        public string SolvePart1(string input)
        {
            return SolvePart1(input, 2503);
        }

        public string SolvePart1(string input, int seconds)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            int maxDistance = int.MinValue;
            foreach (string line in lines)
            {
                string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                int speed = int.Parse(parts[3]);
                int travelTime = int.Parse(parts[6]);
                int resttime = int.Parse(parts[^2]);
                int period = travelTime + resttime;

                int distance = 0;
                for (int i = 0; i <= seconds; i++)
                {
                    int secondInPeriod = ((period + i) % period);
                    distance += secondInPeriod < travelTime ? speed : 0;
                }

                if (distance > maxDistance) { maxDistance = distance; }
            }

            return maxDistance.ToString(); ;
        }

        public string SolvePart2(string input)
        {
            return SolvePart2(input, 2503);
        }

        public string SolvePart2(string input, int seconds)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            // index of reindeer -> speed, traveltime, period
            //var reindeers = new Dictionary<string, Tuple<int,int, int>>();
            var reindeers = new Dictionary<string, (int speed, int traveltime, int period)>();

            var reindeerDistance = new Dictionary<string, int>();
            var reindeerPoints = new Dictionary<string, int>();

            // set-up the race
            foreach (string line in lines)
            {
                string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string reindeer = parts[0];
                int speed = int.Parse(parts[3]);
                int travelTime = int.Parse(parts[6]);
                int resttime = int.Parse(parts[^2]);
                int period = travelTime + resttime;

                reindeers.Add(reindeer, (speed, travelTime, period));
                reindeerDistance.Add(reindeer, 0);
                reindeerPoints.Add(reindeer, 0);
            }

            // RACE!
            for (int i = 0; i <= seconds; i++)
            {
                int farthest = 0;

                foreach (var r in reindeers)
                {
                    int distance = ((r.Value.period + i) % r.Value.period) < r.Value.traveltime ? r.Value.speed : 0;
                    reindeerDistance[r.Key] += distance;
                    if (farthest < reindeerDistance[r.Key]) { farthest = reindeerDistance[r.Key]; }
                }

                foreach (var r in reindeerDistance)
                {
                    if (r.Value == farthest)
                    {
                        reindeerPoints[r.Key]++;
                    }
                }
            }

            // determine winner!
            int maxPoints = 0;
            foreach (var r in reindeerPoints)
            {
                if (r.Value > maxPoints) { maxPoints = r.Value; }
            }

            return maxPoints.ToString(); ;
        }

        public string GetInput()
        {
            return new Inputs.Year2015.Day14().Input;
        }
    }
}