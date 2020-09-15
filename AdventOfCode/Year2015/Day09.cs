using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2015
{
    public class Day09 : IAoC
    {
        // Challenge can be found on https://adventofcode.com/2015/day/9

        public string SolvePart1(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            int shortestDistance = int.MaxValue;
            
            // create pairs 
            var pairs = new Dictionary<string, Dictionary<string, int>>();
            var uniqueplaces = new HashSet<string>();
            foreach (string line in lines) {
                string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (!pairs.ContainsKey(parts[0])) { pairs.Add(parts[0], new Dictionary<string, int>()); }
                if (!pairs.ContainsKey(parts[2])) { pairs.Add(parts[2], new Dictionary<string, int>()); }
                pairs[parts[0]].Add(parts[2], int.Parse(parts[4]));
                pairs[parts[2]].Add(parts[0], int.Parse(parts[4]));
                uniqueplaces.Add(parts[0]);
                uniqueplaces.Add(parts[2]);
            }

            var locations = uniqueplaces.ToArray();
            // permutate!
            var permutations = GetPermutations<string>(locations, locations.Length);
            foreach (var permutation in permutations) {
                List<string> cur = permutation.ToList<string>();
                int distance = 0;
                for (int i = 1; i < cur.Count; i++) {
                    distance += pairs[cur[i - 1]][cur[i]];
                }
                if (distance < shortestDistance) { shortestDistance = distance; }
            }

            return shortestDistance.ToString() ;
        }

        public string SolvePart2(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            int longestDistance = int.MinValue;

            // create pairs 
            var pairs = new Dictionary<string, Dictionary<string, int>>();
            var uniqueplaces = new HashSet<string>();
            foreach (string line in lines)
            {
                string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (!pairs.ContainsKey(parts[0])) { pairs.Add(parts[0], new Dictionary<string, int>()); }
                if (!pairs.ContainsKey(parts[2])) { pairs.Add(parts[2], new Dictionary<string, int>()); }
                pairs[parts[0]].Add(parts[2], int.Parse(parts[4]));
                pairs[parts[2]].Add(parts[0], int.Parse(parts[4]));
                uniqueplaces.Add(parts[0]);
                uniqueplaces.Add(parts[2]);
            }

            var locations = uniqueplaces.ToArray();
            // permutate!
            var permutations = GetPermutations<string>(locations, locations.Length);
            foreach (var permutation in permutations)
            {
                List<string> cur = permutation.ToList<string>();
                int distance = 0;
                for (int i = 1; i < cur.Count; i++)
                {
                    distance += pairs[cur[i - 1]][cur[i]];
                }
                if (distance > longestDistance) { longestDistance = distance; }
            }

            return longestDistance.ToString();
        }

        public string GetInput()
        {
            return new Inputs.Year2015.Day09().Input;
        }


        // thank you https://stackoverflow.com/questions/756055/listing-all-permutations-of-a-string-integer
        internal static IEnumerable<IEnumerable<T>>
    GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }
    }
}