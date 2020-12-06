using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2017
{
    public class Day06 : IAoC
    {
        /// <summary>
        /// Determine the cycles before a recurring pattern emerges on distributing a memorybank
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string SolvePart1(string input)
        {
            var memoryBanks = Array.ConvertAll(input.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries), int.Parse);

            var states = new HashSet<string>();
            int cycles = 0, memoryLength = memoryBanks.Length;
            string state = String.Join("", memoryBanks);
            while (!states.Contains(state))
            {
                states.Add(state);
                cycles++;

                // determine max
                int highNumber = -1, highPos = -1;
                for (int i = 0; i < memoryBanks.Length; i++)
                {
                    if (memoryBanks[i] > highNumber)
                    {
                        highNumber = memoryBanks[i];
                        highPos = i;
                    }
                }

                // clear and redistribute
                memoryBanks[highPos] = 0;
                for (int i = (highPos + 1) % memoryLength; highNumber > 0; i = (i + 1) % memoryLength)
                {
                    memoryBanks[i]++;
                    highNumber--;
                }

                // create new state
                state = String.Join("", memoryBanks);
            }

            return cycles.ToString();
        }

        /// <summary>
        /// Determine the cycle-length before a recurring pattern emerges on distributing a memorybank
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string SolvePart2(string input)
        {
            var memoryBanks = Array.ConvertAll(input.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries), int.Parse);

            var states = new Dictionary<string, int>();
            int cycles = 0, memoryLength = memoryBanks.Length;
            string state = String.Join("", memoryBanks);
            while (!states.ContainsKey(state))
            {
                states.Add(state, cycles);
                cycles++;
                // determine max
                int highNumber = -1, highPos = -1;
                for (int i = 0; i < memoryBanks.Length; i++)
                {
                    if (memoryBanks[i] > highNumber)
                    {
                        highNumber = memoryBanks[i];
                        highPos = i;
                    }
                }

                // clear and redistribute
                memoryBanks[highPos] = 0;
                for (int i = (highPos + 1) % memoryLength; highNumber > 0; i = (i + 1) % memoryLength)
                {
                    memoryBanks[i]++;
                    highNumber--;
                }

                // create new state
                state = String.Join("", memoryBanks);
            }

            return (cycles - states[state]).ToString(); ;
        }

        public string GetInput() => @"10	3	15	10	5	15	5	15	9	2	5	8	5	2	3	6";
    }
}