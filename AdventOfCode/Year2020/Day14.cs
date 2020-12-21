using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2020
{
    public class Day14 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2020/day/14

        /// Generic idea for Day 14
        /// Learn about bitshifts !

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
                if (mem.ContainsKey(memLocation) is false) { mem.Add(memLocation,0); }

                // convert all string (sub-optimal)
                // 11 = 000000000000000000000000000000001011
                string bits = (Convert.ToString(long.Parse(ops[2]),2)).PadLeft(36,'0');
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

       
        public string SolvePart2(string input) {
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

                // else write to mem
                long memStartLocation = long.Parse(ops[1]);
                long memValue = long.Parse(ops[2]);

                // queue for different memlocations
                var queue = new Queue<char[]>();
                string bits = (Convert.ToString(memStartLocation, 2)).PadLeft(36, '0');
                queue.Enqueue(new char[36]); // start with empty strings
                int index = 0;
                var memLocations = new List<long>();
                while (queue.Count > 0) {
                    char[] cur = queue.Dequeue();

                    // completed
                    if (index == 35 && (cur[index] == '0' || cur[index] == '1'))
                    {
                        memLocations.Add(Convert.ToInt64(new string(cur), 2));
                        continue;
                    }

                    // index check
                    if (cur[index] == '0' || cur[index] == '1') { index++; }
                    
                    

                    // go to next position
                    if (mask[index] == '1') { cur[index] = '1'; queue.Enqueue(cur); }
                    if (mask[index] == '0') { cur[index] = bits[index]; queue.Enqueue(cur); }
                    if (mask[index] == 'X')
                    { // branch
                        var copy = Utils.Utils.Duplicate(cur);
                        cur[index] = '1';
                        copy[index] = '0';
                        queue.Enqueue(cur);
                        queue.Enqueue(copy);
                    }

                }

                // now actually store the value on all these locations
                foreach (long memLocation in memLocations) {
                    if (mem.ContainsKey(memLocation) is false) { mem.Add(memLocation, memValue); }
                    else { mem[memLocation] = memValue; }
                }
            }

            foreach (var memresult in mem) { sum += memresult.Value; }
            return sum.ToString();
        }



        public string GetInput()
        {
            return new Inputs.Year2020.Day14().Input;
        }
    }
}