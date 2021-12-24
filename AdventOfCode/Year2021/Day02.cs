using System;

namespace AdventOfCode.Year2021
{
    public class Day02 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2021/day/2

        public string SolvePart1(string input)
        {
            // Convert input to usable instructions
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            (string Command, int Unit)[] instructions = new (string Command, int Unit)[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                instructions[i] = (line[0], int.Parse(line[1]));
            }
            long horizontal = 0;
            long depth = 0;
            for (int i = 0; i < instructions.Length; i++)
            {
                switch (instructions[i].Command)
                {
                    case "forward":
                            horizontal += instructions[i].Unit;
                        break;
                    case "down":
                        depth += instructions[i].Unit;
                        break;
                    case "up":
                        depth -= instructions[i].Unit;
                        break;
                    default:
                        break;
                }
            }
            

            return "" + horizontal*depth;
        }

        public string SolvePart2(string input)
        {
            // Convert input to usable instructions
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            (string Command, int Unit)[] instructions = new (string Command, int Unit)[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                instructions[i] = (line[0], int.Parse(line[1]));
            }
            long horizontal = 0;
            long depth = 0;
            long aim = 0; 

            for (int i = 0; i < instructions.Length; i++)
            {
                switch (instructions[i].Command)
                {
                    case "forward":
                        horizontal += instructions[i].Unit;
                        depth += aim* instructions[i].Unit;
                        break;
                    case "down":
                        aim += instructions[i].Unit;
                        break;
                    case "up":
                        aim -= instructions[i].Unit;
                        break;
                    default:
                        break;
                }
            }


            return "" + horizontal * depth;
        }

        public string GetInput()
        {
            return new Inputs.Year2021.Day02().Input;
        }
    }
}