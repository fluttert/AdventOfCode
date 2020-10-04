using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2015
{
    // Challenge can be found on: https://adventofcode.com/2015/day/24
    public class Day24 : IAoC
    {
        public string SolvePart1(string input)
        {
            int[] numbers = Array.ConvertAll(input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries), int.Parse);
            int sum = numbers.Sum();
            int targetSize = sum / 3;
            var sets = MakeSets(numbers, targetSize);
            int smallestGroup = int.MaxValue;
            int smallestQuantumEntanglement = int.MaxValue;
            var alreadyChecked = new HashSet<int>();

            // check of the groups, in a non-repeating order
            for (int i = 0; i < sets.Count; i++)
            {
                int ilength = sets[i].Count;
                for (int j = i + 1; j < sets.Count; j++)
                {
                    int jlength = sets[j].Count;
                    for (int k = j + 1; k < sets.Count; k++)
                    {
                        if (ilength + jlength + sets[k].Count != numbers.Length) { continue; }
                        // check if all numbers only occur once
                        var check = new HashSet<int>(sets[i]);
                        bool valid = true;
                        foreach (int n in sets[j])
                        {
                            if (check.Contains(n)) { valid = false;  break; }
                            check.Add(n);
                        }
                        foreach (int n in sets[k])
                        {
                            if (check.Contains(n)) { valid = false; break; }
                            check.Add(n);
                        }

                        // great succes
                        //validGroups.Add(new int[] { i, j, k });
                        if (valid is true && sets[i].Count == smallestGroup && alreadyChecked.Contains(i) is false) {
                            smallestGroup = sets[i].Count;
                            int qe = 1;
                            foreach (int n in sets[i]) { qe *= n; }
                            smallestQuantumEntanglement = Math.Min(smallestQuantumEntanglement, qe);
                            alreadyChecked.Add(i);
                        }

                        if (valid is true && sets[i].Count < smallestGroup)
                        {
                            smallestGroup = sets[i].Count; alreadyChecked = new HashSet<int>();
                            int qe = 1;
                            foreach (int n in sets[i]) { qe *= n; }
                            smallestQuantumEntanglement = Math.Min(smallestQuantumEntanglement, qe);
                            alreadyChecked.Add(i);
                        }
                    }
                }
            }

            return smallestQuantumEntanglement.ToString();
        }


        /// <summary>
        /// Naive implmentation
        /// </summary>
        /// <param name="numbers"></param>
        /// <param name="targetSize"></param>
        /// <returns></returns>
        public List<List<int>> MakeSets(int[] numbers, int targetSize)
        {
            var output = new List<List<int>>();
            var queue = new Queue<(int index, List<int> list)>();
            for (int i = 0; i < numbers.Length; i++)
            {
                // check if the desired number is already the targetsize
                if (numbers[i] == targetSize)
                {
                    output.Add(new List<int>() { numbers[i] });
                    continue;
                }

                // add new entries to the queue
                queue.Enqueue((i + 1, new List<int>() { numbers[i] }));
            }

            // create all possible sets (non duplicate) for the targetsize
            // go through all numbers, and determine if it exceeds, equals, or is less then the targetsize
            // repeat untill no more numbers are available in the numbers-array
            while (queue.Count > 0)
            {
                var pair = queue.Dequeue();
                int sum = pair.list.Sum();
                for (int i = pair.index; i < numbers.Length; i++)
                {
                    if (sum + numbers[i] > targetSize) { continue; }
                    var l = Utils.Utils.Duplicate(pair.list);
                    l.Add(numbers[i]);
                    if (sum + numbers[i] == targetSize)
                    {
                        output.Add(l);
                    }
                    else
                    {
                        queue.Enqueue((i + 1, l));
                    }
                }
            }

            return output;
        }

        public string SolvePart2(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            return "";
        }

        public string GetInput()
        {
            return new Inputs.Year2015.Day24().Input;
        }
    }
}