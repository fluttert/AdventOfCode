using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2016
{
    public class Day12 : IAoC
    {
        // Challenge can be found on https://adventofcode.com/2016/day/12

        public string SolvePart1(string input)
        {
            var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            return Monorail(lines);
        }

        public string SolvePart2(string input)
        {
            var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            return Monorail(lines, new Dictionary<string, int>() { { "a", 0 }, { "b", 0 }, { "c", 1 }, { "d", 0 } });
        }

        public string Monorail(string[] input, Dictionary<string, int> initRegisters = null)
        {
            string registernames = "abcd";
            var registers = initRegisters;
            if (registers == null) { registers = new Dictionary<string, int>() { { "a", 0 }, { "b", 0 }, { "c", 0 }, { "d", 0 } }; }
            int lineNumber = 0;
            while (lineNumber < input.Length)
            {
                var parts = input[lineNumber].Trim().Split(' ');
                string instruction = parts[0];
                if (instruction == "inc") { registers[parts[1]]++; }
                if (instruction == "dec") { registers[parts[1]]--; }
                if (instruction == "cpy")
                {
                    int copyValue =
                        (registernames.IndexOf(parts[1]) != -1) ?
                        registers[parts[1]] : int.Parse(parts[1]);
                    registers[parts[2]] = copyValue;
                }
                if (instruction == "jnz")
                {
                    int jumpNonZeroValue =
                        (registernames.IndexOf(parts[1]) != -1) ?
                        registers[parts[1]] : int.Parse(parts[1]);
                    if (jumpNonZeroValue != 0)
                    {
                        lineNumber += int.Parse(parts[2]);
                        continue;
                    }
                }
                lineNumber++;
            }
            return registers["a"].ToString();
        }

        public string GetInput() => @"cpy 1 a
cpy 1 b
cpy 26 d
jnz c 2
jnz 1 5
cpy 7 c
inc d
dec c
jnz c -2
cpy a c
inc a
dec b
jnz b -2
cpy c b
dec d
jnz d -6
cpy 16 c
cpy 17 d
inc a
dec d
jnz d -2
dec c
jnz c -5";
    }
}