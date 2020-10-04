using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Utils;

namespace AdventOfCode.Year2015
{
    // Challenge can be found on: https://adventofcode.com/2015/day/24
    public class Day24 : IAoC
    {
        public string SolvePart1(string input)
        {
            // get the numbers and sort them descending (high -> low)
            int[] numbers = Array.ConvertAll(input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries), int.Parse);
            Array.Sort(numbers);
            Array.Reverse(numbers);

            int sum = numbers.Sum();
            int targetSize = sum / 3;
            var sets = MakeSmallestSets(numbers, targetSize);
            int smallestGroup = int.MaxValue;
            long smallestQuantumEntanglement = long.MaxValue;
            foreach (var smallSet in sets)
            {
                long qe = 1;
                for (int i = 0; i < smallSet.Length; i++)
                {
                    qe *= (long)smallSet[i];
                }

                // Only continue & validate other sets if we have a potential smallest set
                if (qe < smallestQuantumEntanglement) {
                    bool validGroup = ValidOther2Sets(numbers, smallSet, targetSize);
                    if (validGroup) {
                        smallestQuantumEntanglement = qe;
                    }
                }
            }
            return smallestQuantumEntanglement.ToString();
        }


        public bool ValidOther2Sets(int[] numbers, int[] smallestSet, int targetSum) {
            var remainingNumbers = ArrayDifference(numbers, smallestSet);

            // with the smallest set determined, can we have 2 other valid sets
            // if you can make 1 valid extra set, we guarantee validity
            // as TotalSum - targetSize - TargetSize = TargetSize

            var anotherSetCreated = MakeSmallestSets(remainingNumbers, targetSum, true);

            return anotherSetCreated.Count>0;
        }

        public int[] ArrayDifference(int[] numbers, int[] subset) {
            int[] diff = new int[numbers.Length - subset.Length];
            int i = 0, j = 0, k=0;
            while (i < numbers.Length && j< subset.Length) {
                // skip if the same
                if (numbers[i] == subset[j]) { i++; j++; }
                else { diff[k] = numbers[i]; k++; i++; }
            }
            return diff;
        }

        public List<int[]> MakeSmallestSets(int[] numbers, int targetSum, bool returnAtFirstResult=false)
        {
            var output = new List<int[]>();
            var queue = new Queue<(int index, int sum, int[] numbers)>();
            int smallestTargetGroupSize = int.MaxValue;

            // add all numbers
            for (int i = 0; i < numbers.Length; i++)
            {
                // check if the desired number is already the targetsize
                if (numbers[i] == targetSum)
                {
                    output.Add(new int[] { numbers[i] });
                    smallestTargetGroupSize = 1;
                    continue;
                }
                // add new entries to the queue
                queue.Enqueue((i + 1, numbers[i], new int[] { numbers[i] }));
            }

            while (queue.Count > 0)
            {
                if (returnAtFirstResult && output.Count>0) { break; }

                var tuple = queue.Dequeue();

                // don't bother with groups that have already exceeded the smallest length
                if (tuple.numbers.Length > smallestTargetGroupSize) { continue; }

                int sum = tuple.sum;

                for (int i = tuple.index; i < numbers.Length; i++)
                {
                    // exceeds targetsize
                    int sumUpdate = sum + numbers[i];
                    var updatedNumbers = Utils.Utils.DuplicateAndAddOneElement(tuple.numbers, numbers[i]);
                    if (sumUpdate > targetSum || updatedNumbers.Length > smallestTargetGroupSize) { continue; }
                    
                    // we have a target in sight
                    if (sumUpdate == targetSum) {
                        if (smallestTargetGroupSize > updatedNumbers.Length) {
                            output.Clear();
                            smallestTargetGroupSize = updatedNumbers.Length;
                        }
                        output.Add(updatedNumbers);
                        continue; 
                    }

                    // we have not reached targetsize & there is room for additional numbers
                    queue.Enqueue((i + 1, sumUpdate, Utils.Utils.DuplicateAndAddOneElement(tuple.numbers, numbers[i])));
                }
            }
            return output;
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