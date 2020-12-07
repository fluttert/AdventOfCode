using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2020
{
    public class Day07 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2020/day/7

        /// Generic idea for Day 6
        /// Graphs

        public string SolvePart1(string input)
        {
            // read per line
            string[] lines = input.Split(Environment.NewLine);

            // Create are graph, reversed from input
            Dictionary<string, List<string>> adjecencyList = CreateReverseGraph(lines);

            // Pass 3: Look up the shiny gold bag (and how many steps it can take you)
            var queue = new Queue<string>() { };
            queue.Enqueue("shiny gold bag");
            var bagHolders = new HashSet<string>();
            while (queue.Count > 0)
            {
                var bagtype = queue.Dequeue();
                bagHolders.Add(bagtype);
                foreach (var bagsThatCanHoldThisBag in adjecencyList[bagtype])
                {
                    queue.Enqueue(bagsThatCanHoldThisBag);
                }
            }

            return (bagHolders.Count - 1).ToString();
        }

        public string SolvePart2(string input)
        {
            // read per line
            string[] lines = input.Split(Environment.NewLine);
            Dictionary<string, List<(string bagtype, int amount)>> graph = CreateGraph(lines);

            long sum = 0;
            // searchhh
            var queue = new Queue<(string bagtype, long currentbags)>() { };
            queue.Enqueue(("shiny gold bag", 1));
            while (queue.Count > 0)
            {
                var token = queue.Dequeue();
                // exit
                if (graph[token.bagtype].Count == 0) { continue; }

                // process
                foreach (var node in graph[token.bagtype])
                {
                    sum += token.currentbags * node.amount;
                    queue.Enqueue((node.bagtype, token.currentbags * node.amount));
                }
            }

            return sum.ToString();
        }

        public Dictionary<string, List<string>> CreateReverseGraph(string[] lines)
        {
            // create graph
            var adjecencyList = new Dictionary<string, List<string>>();

            // PASS 1: init the adjecency list with ALL bags
            foreach (string line in lines)
            {
                var bagTypes = line.Replace(" contain", ",").Split(new char[] { ',', '.' }, StringSplitOptions.RemoveEmptyEntries);
                adjecencyList.Add(bagTypes[0].Replace("bags", "bag"), new List<string>());
            }

            // Pass 2: reverse add, so A->[B,C] is now B->[A], C->[A]
            foreach (string line in lines)
            {
                var bagTypes = line.Replace(" contain", ",").Replace("bags", "bag").Split(new char[] { ',', '.' }, StringSplitOptions.RemoveEmptyEntries);
                // nothing to add
                if (bagTypes[1].StartsWith(" no other")) { continue; }
                for (int i = 1; i < bagTypes.Length; i++)
                {
                    adjecencyList[bagTypes[i][3..]].Add(bagTypes[0]);
                }
            }
            return adjecencyList;
        }

        public Dictionary<string, List<(string bagtype, int amount)>> CreateGraph(string[] lines)
        {
            // create graph
            var adjecencyList = new Dictionary<string, List<(string bagtype, int amount)>>();

            // In 1 pass add all lines
            foreach (string line in lines)
            {
                var bagTypes = line.Replace("bags", "bag").Replace(" contain", ",").Split(new char[] { ',', '.' }, StringSplitOptions.RemoveEmptyEntries);
                adjecencyList.Add(bagTypes[0], new List<(string bagtype, int amount)>());
                if (bagTypes[1].StartsWith(" no other")) { continue; }
                for (int i = 1; i < bagTypes.Length; i++)
                {
                    adjecencyList[bagTypes[0]].Add((bagtype: bagTypes[i][3..], amount: int.Parse(bagTypes[i][..2].Trim())));
                }
            }
            return adjecencyList;
        }

        public string GetInput()
        {
            return new Inputs.Year2020.Day07().Input;
        }
    }
}