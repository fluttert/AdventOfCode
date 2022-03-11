using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2021
{
    public class Day12 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2021/day/12

        /**
         * Key Insights
         * 1: The cave system is bidirectional
         * 2: Need to build an vertices- and edge-list (aka a Graph)
         *
         */

        public string SolvePart1(string input)
        {
            // Convert input to something usable
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            // set up the graph
            Dictionary<string, List<string>> adjacent = new();
            HashSet<string> smallCaves = new();
            int totalRoutes = 0;

            // read the lines
            foreach (string line in lines)
            {
                string[] parts = line.Split('-', StringSplitOptions.RemoveEmptyEntries);
                // check if both vertices already exist
                if (!adjacent.ContainsKey(parts[0])) { adjacent.Add(parts[0], new()); }
                if (!adjacent.ContainsKey(parts[1])) { adjacent.Add(parts[1], new()); }

                // add bi-directional vertice
                adjacent[parts[0]].Add(parts[1]);
                adjacent[parts[1]].Add(parts[0]);

                // check if this is a big cave (only checks first letter)
                if (char.IsLower(parts[0][0])) { smallCaves.Add(parts[0]); }
                if (char.IsLower(parts[1][0])) { smallCaves.Add(parts[1]); }
            }

            // start the search
            // Search is only showing WHERE we are going
            Queue<(string vertex, HashSet<string> cavesVisited)> q = new();
            q.Enqueue(("start", new HashSet<string>() { }));
            while (q.Count > 0)
            {
                var p = q.Dequeue();
                string currentVertex = p.vertex;
                // Made it! terminate this branch
                if (currentVertex == "end")
                {
                    //p.cavesVisited.Add(currentVertex);
                    //Console.WriteLine(String.Join('-', p.cavesVisited));
                    totalRoutes++; continue;
                }

                // else looks further
                foreach (var vertex in adjacent[currentVertex])
                {
                    // only search further IF you havent been to this small cave
                    if (smallCaves.Contains(vertex) && p.cavesVisited.Contains(vertex)) { continue; }

                    // copy and move on
                    var duplicate = Utils.Utils.Duplicate(p.cavesVisited);
                    duplicate.Add(currentVertex);
                    q.Enqueue((vertex, duplicate));
                }
            }

            return "" + totalRoutes;
        }

        public string SolvePart2(string input)
        {
            // Convert input to something usable
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            // set up the graph
            Dictionary<string, List<string>> adjacent = new();
            HashSet<string> smallCaves = new();
            int totalRoutes = 0;

            // read the lines
            foreach (string line in lines)
            {
                string[] parts = line.Split('-', StringSplitOptions.RemoveEmptyEntries);
                // check if both vertices already exist
                if (!adjacent.ContainsKey(parts[0])) { adjacent.Add(parts[0], new()); }
                if (!adjacent.ContainsKey(parts[1])) { adjacent.Add(parts[1], new()); }

                // add bi-directional vertice
                adjacent[parts[0]].Add(parts[1]);
                adjacent[parts[1]].Add(parts[0]);

                // check if this is a big cave (only checks first letter)
                if (char.IsLower(parts[0][0])) { smallCaves.Add(parts[0]); }
                if (char.IsLower(parts[1][0])) { smallCaves.Add(parts[1]); }
            }

            // start the search
            // Search is only showing WHERE we are going
            Queue<(string vertex, HashSet<string> cavesVisited, bool smallCaveVisitedTwice)> q = new();
            q.Enqueue(("start", new HashSet<string>() { }, false));
            while (q.Count > 0)
            {
                var p = q.Dequeue();
                string currentVertex = p.vertex;
                bool smallCaveVisitedTwice = p.smallCaveVisitedTwice;
                // Made it! terminate this branch
                if (currentVertex == "end")
                {
                    //p.cavesVisited.Add(currentVertex);
                    //Console.WriteLine(String.Join('-', p.cavesVisited));
                    totalRoutes++; continue;
                }

                // else looks further
                foreach (var vertex in adjacent[currentVertex])
                {
                    // never go back to start
                    if (vertex == "start")
                    {
                        continue;
                    }

                    // copy and move on
                    var duplicate = Utils.Utils.Duplicate(p.cavesVisited);
                    duplicate.Add(currentVertex);

                    // always resume when it's a bigger cave
                    if (smallCaves.Contains(vertex) is false)
                    {
                        q.Enqueue((vertex, duplicate, p.smallCaveVisitedTwice));
                        continue;
                    }

                    // it's a smaller cave! -- Prune / don't move further if you already visited it twice and been here before
                    if (smallCaveVisitedTwice && p.cavesVisited.Contains(vertex)) { continue; }
                    
                    // just add as regular if we haven't been here.
                    if (smallCaveVisitedTwice && p.cavesVisited.Contains(vertex) is false) {
                        q.Enqueue((vertex, duplicate, p.smallCaveVisitedTwice));
                        continue;
                    }

                    // else we havent visited small Caves twice yet, and we can always add it
                    if(smallCaveVisitedTwice is false && p.cavesVisited.Contains(vertex)) {
                        q.Enqueue((vertex, duplicate, true));
                        continue;
                    }

                    if (smallCaveVisitedTwice is false && p.cavesVisited.Contains(vertex) is false)
                    {
                        q.Enqueue((vertex, duplicate, p.smallCaveVisitedTwice));
                        continue;
                    }
                }
            }

            return "" + totalRoutes;
        }

        public string GetInput()
        {
            return new Inputs.Year2021.Day12().Input;
        }
    }
}