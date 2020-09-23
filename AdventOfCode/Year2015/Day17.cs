using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

namespace AdventOfCode.Year2015
{
    public class Day17 : IAoC
    {
        // Challenge can be found on https://adventofcode.com/2015/day/17

        public string SolvePart1(string input)
        {
            return CombineContainers(input, 150).Item1;
        }

        // helper function to have an easy way to copy the array.
        private int[] ArrayDeepCopy(int[] source)
        {
            var copy = new int[source.Length];
            for (int i = 0; i < source.Length; i++)
            {
                copy[i] = source[i];
            }
            return copy;
        }

        // Return values are 1: Combinations possible, 2: Minimum amount of containers needed
        public Tuple<string,string> CombineContainers(string input, int amount)
        {
            int[] sizes = Array.ConvertAll(input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries), int.Parse);
            Array.Sort(sizes);
            Array.Reverse(sizes); // biggest sizes first -> first hit = minimum amount of containers
            
            int combinations = 0;
            int minimumAmountOfElems = int.MaxValue;
            int minimumCombinationsPossible = 0; 


            var knownstates = new HashSet<string>();
            int[] state = new int[sizes.Length];
            var queue = new Queue<int[]>();
            queue.Enqueue(state);
            while (queue.Count > 0)
            {
                // process
                var currentState = queue.Dequeue();

                // exit?
                int score = 0;
                int elems = 0;
                for (int i = 0; i < currentState.Length; i++)
                {
                    score += currentState[i] * sizes[i];
                    elems += currentState[i] == 1 ? 1 : 0;
                }
                if (score == amount)
                {
                    combinations++;

                    if (elems == minimumAmountOfElems) { minimumCombinationsPossible++; }
                    if (elems < minimumAmountOfElems) 
                    {
                        minimumAmountOfElems = elems;
                        minimumCombinationsPossible = 1;
                    }
                    continue;
                }
                if (score > amount)
                {
                    continue;
                }

                // increase everything 1 at the time from this point
                for (int i = 0; i < currentState.Length; i++)
                {
                    var copy = ArrayDeepCopy(currentState);
                    if (copy[i] == 1) { continue; }
                    copy[i]++;

                    string stringified = string.Join(',', copy);
                    if (knownstates.Contains(stringified)) { continue; }
                    knownstates.Add(stringified);
                    queue.Enqueue(copy);
                }
            }

            return new Tuple<string, string>(combinations.ToString(), minimumCombinationsPossible.ToString());
        }

        public string SolvePart2(string input)
        {
            return CombineContainers(input, 150).Item2;
        }

        public string GetInput()
        {
            return new Inputs.Year2015.Day17().Input;
        }
    }
}