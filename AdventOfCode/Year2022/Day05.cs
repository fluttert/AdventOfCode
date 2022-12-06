using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2022
{
    public class Day05 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2022/day/5

        public string SolvePart1(string input)
        {
            string topContainers = "";
            // split in 2 parts -> First containers config , then Movements
            string[] parts = input.Split(Environment.NewLine + Environment.NewLine);

            // part 1 = container configuration
            string[] containers = parts[0].Split(Environment.NewLine);
            // start with the numbers of the stacks
            Array.Reverse(containers);

            // initialize the containers
            List<Stack<char>> stacks = new();
            for (int i = 1; i < containers[0].Length; i += 4)
            {
                stacks.Add(new Stack<char>());
            }
            for (int i = 1; i < containers.Length; i++)
            {
                for (int j = 1; j < containers[i].Length; j += 4)
                {
                    if (containers[i][j] != ' ')
                    {
                        stacks[(j - 1) / 4].Push(containers[i][j]);
                    }
                }
            }

            // part 2 = movements
            string[] movements = parts[1].Split(Environment.NewLine);
            foreach (string movement in movements)
            {
                string[] procedure = movement.Split(' ');
                int repeat = int.Parse(procedure[1]);
                int from = int.Parse(procedure[3])-1;
                int to = int.Parse(procedure[5])-1;
                for (int i = 0; i < repeat; i++) { 
                    char c = stacks[from].Pop();
                    stacks[to].Push(c);
                }
            }

            // get the result
            foreach (var s in stacks) { topContainers += s.Peek(); }

            return "" + topContainers;
        }

        public string SolvePart2(string input)
        {
            string topContainers = "";
            // split in 2 parts -> First containers config , then Movements
            string[] parts = input.Split(Environment.NewLine + Environment.NewLine);

            // part 1 = container configuration
            string[] containers = parts[0].Split(Environment.NewLine);
            // start with the numbers of the stacks
            Array.Reverse(containers);

            // initialize the containers
            List<Stack<char>> stacks = new();
            for (int i = 1; i < containers[0].Length; i += 4)
            {
                stacks.Add(new Stack<char>());
            }
            for (int i = 1; i < containers.Length; i++)
            {
                for (int j = 1; j < containers[i].Length; j += 4)
                {
                    if (containers[i][j] != ' ')
                    {
                        stacks[(j - 1) / 4].Push(containers[i][j]);
                    }
                }
            }

            // part 2 = movements
            string[] movements = parts[1].Split(Environment.NewLine);
            foreach (string movement in movements)
            {
                string[] procedure = movement.Split(' ');
                int repeat = int.Parse(procedure[1]);
                int from = int.Parse(procedure[3]) - 1;
                int to = int.Parse(procedure[5]) - 1;
                Stack<char> tmp = new();
                for (int i = 0; i < repeat; i++)
                {
                    char c = stacks[from].Pop();
                    tmp.Push(c);
                }
                for (int i = 0; i < repeat; i++)
                {
                    char c = tmp.Pop();
                    stacks[to].Push(c);
                }
            }

            // get the result
            foreach (var s in stacks) { topContainers += s.Peek(); }

            return "" + topContainers;
        }

        public string GetInput()
        {
            return new Inputs.Year2022.Day05().Input;
        }
    }
}