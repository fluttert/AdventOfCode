using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2020
{
    public class Day07 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2020/day/7

        /// Generic idea for Day 6
        /// This is a graph/tree traversal problem
        /// Step 1: Create the graph
        /// Step 2: Search a specific node, and follow the links to other nodes
        /// 
        /// Further reading for Graphs, check out the intro from Stanford: https://web.stanford.edu/class/cs97si/06-basic-graph-algorithms.pdf

        public string SolvePart1(string input)
        {
            // read per line
            string[] lines = input.Split(Environment.NewLine);

            // Step 1: Create are graph, reversed from input
            // We are interested in the links to Parent-nodes
            Dictionary<string, List<string>> adjecencyList = CreateParentGraph(lines);

            // Step 2: Search for the shiny gold bag and follow the unique links
            var queue = new Queue<string>() { };        // Queue = First-in, First-Out principle
            queue.Enqueue("shiny gold bag");            // starting point
            var bagHolders = new HashSet<string>();     // result set
            while (queue.Count > 0)
            {
                var bagtype = queue.Dequeue();          // Get last node (bagtype in this case)
                bagHolders.Add(bagtype);                // add to the result
                foreach (var bagsThatCanHoldThisBag in adjecencyList[bagtype])
                {
                    // check if this bag had 1 or more parents, and add these to the queue
                    queue.Enqueue(bagsThatCanHoldThisBag);
                }
            }

            // don't forget to substract the shiny bag itself 
            return (bagHolders.Count - 1).ToString();
        }

        public string SolvePart2(string input)
        {
            // read per line
            string[] lines = input.Split(Environment.NewLine);
            // Step 1 create the graph (now focusing on the child-nodes)
            // have all nodes, and for each node all the child-nodes, and for each child-node the type and amount
            // so a Dictionary, with a list of tuples
            Dictionary<string, List<(string bagtype, int amount)>> graph = CreateGraph(lines);

            // Step 2: Search
            var queue = new Queue<(string bagtype, long currentbags)>() { };    
            queue.Enqueue(("shiny gold bag", 1));   // add the initial parameter
            long sum = 0;                           // total sum of bags
            while (queue.Count > 0)
            {
                var token = queue.Dequeue();        // get the last node

                // Exit, This BagType has no child-nodes. Nothing to process
                if (graph[token.bagtype].Count == 0) { continue; }

                // Otherwise process this node, and add the childnodes, while updating the sum (up-to-this-point)
                foreach (var node in graph[token.bagtype])
                {
                    sum += token.currentbags * node.amount;
                    queue.Enqueue((node.bagtype, token.currentbags * node.amount));
                }
            }

            return sum.ToString();
        }

        // This graph is only interested in the links from child-nodes to parent-nodes
        public Dictionary<string, List<string>> CreateParentGraph(string[] lines)
        {
            // create graph
            var adjecencyList = new Dictionary<string, List<string>>();

            // PASS 1: init the adjecency list with ALL bags
            foreach (string line in lines)
            {
                // cut up the pieces based on 'contain', and comma ','. Also get the bags/bag confusing out of the way
                string[] bagTypes = line.Replace(" contain", ",").Replace("bags", "bag").Split(new char[] { ',', '.' }, StringSplitOptions.RemoveEmptyEntries);
                adjecencyList.Add(bagTypes[0], new List<string>());
            }

            // Pass 2: reverse add, so A->[B,C] is now B->[A], C->[A], which is the link from child-note to parent-node
            foreach (string line in lines)
            {
                var bagTypes = line.Replace(" contain", ",").Replace("bags", "bag").Split(new char[] { ',', '.' }, StringSplitOptions.RemoveEmptyEntries);
                if (bagTypes[1].StartsWith(" no other")) { continue; }  // No children of this node, so nothing to add
                
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
                // cut up the pieces based on 'contain', and comma ','. Also get the bags/bag confusing out of the way
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