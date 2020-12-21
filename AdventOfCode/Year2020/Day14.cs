using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2020
{
    public class Day14 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2020/day/14

        /// Generic idea for Day 14
        /// Learn about bitshifts !
        /// well just make efficient use of character-arrays (aka strings)

        public string SolvePart1(string input)
        {
            // read per line and convert to integer array
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            var mask = "";
            var mem = new Dictionary<int, long>();
            foreach (var line in lines)
            {
                // split further
                string[] ops = line.Split(new char[] { ' ', '=', '[', ']' }, StringSplitOptions.RemoveEmptyEntries);

                // if mask, update mask
                if (ops[0] == "mask") { mask = ops[1]; continue; }

                // else write to mem
                int memLocation = int.Parse(ops[1]);
                if (mem.ContainsKey(memLocation) is false) { mem.Add(memLocation, 0); }

                // convert all string (sub-optimal)
                // 11 = 000000000000000000000000000000001011
                string bits = (Convert.ToString(long.Parse(ops[2]), 2)).PadLeft(36, '0');
                char[] result = new char[36];
                // apply mask
                for (int i = 0; i < bits.Length; i++)
                {
                    if (mask[i] == 'X') { result[i] = bits[i]; }
                    if (mask[i] == '1') { result[i] = '1'; }
                    if (mask[i] == '0') { result[i] = '0'; }
                }
                // story result on the desired location
                mem[memLocation] = Convert.ToInt64(new string(result), 2);
            }
            long sum = 0;
            foreach (var memresult in mem) { sum += memresult.Value; }
            return sum.ToString();
        }

        public string SolvePart2(string input)
        {
            // read per line and convert to integer array
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            // spinning it around, just see which numbers we have
            var mem = new Dictionary<long, long>();
            long sum = 0;
            var mask = "";
            foreach (var line in lines)
            {
                // split further
                string[] ops = line.Split(new char[] { ' ', '=', '[', ']' }, StringSplitOptions.RemoveEmptyEntries);

                // if mask, update mask
                if (ops[0] == "mask") { mask = ops[1]; continue; }

                // else write to mem && convert the start location bits
                long memStartLocation = long.Parse(ops[1]);
                long memValue = long.Parse(ops[2]);
                string startLocationBits = (Convert.ToString(memStartLocation, 2)).PadLeft(36, '0'); 

                var queue = new Queue<char[]>();// queue for different memlocations
                queue.Enqueue(new char[36]);    // start with empty strings
                int index = 0;                  // all string work on the same index, due to LIFO of Queue
                var memLocations = new List<long>();    // the different memory locations that we will fille
                while (queue.Count > 0)
                {
                    char[] cur = queue.Dequeue();

                    // completed the sequence, convert back to an memory index
                    if (index == 35 && (cur[index] == '0' || cur[index] == '1'))
                    {
                        memLocations.Add(Convert.ToInt64(new string(cur), 2));
                        continue;
                    }

                    // index check, increase when necessary
                    if (cur[index] == '0' || cur[index] == '1') { index++; }

                    // do the Mask magic on the bits
                    if (mask[index] == '1') { cur[index] = '1'; queue.Enqueue(cur); }
                    if (mask[index] == '0') { cur[index] = startLocationBits[index]; queue.Enqueue(cur); }
                    if (mask[index] == 'X')
                    { // X means add both possibilities
                        var copy = Utils.Utils.Duplicate(cur);
                        cur[index] = '1';   // option 1 add a '1'
                        copy[index] = '0';  // option 2 add a '0'
                        queue.Enqueue(cur);
                        queue.Enqueue(copy);
                    }
                }

                // now actually store the memory value on all these locations
                foreach (long memLocation in memLocations)
                {
                    if (mem.ContainsKey(memLocation)) { mem[memLocation] = memValue; }
                    else {mem.Add(memLocation, memValue);}
                }
                // go to the next line (end of while loop)
            }

            // sum all memory values
            foreach (var memresult in mem) { sum += memresult.Value; }
            return sum.ToString();
        }

        public string GetInput()
        {
            return new Inputs.Year2020.Day14().Input;
        }
    }
}