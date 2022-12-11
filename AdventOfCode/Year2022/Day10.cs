using System;

namespace AdventOfCode.Year2022
{
    public class Day10 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2022/day/10

        public string SolvePart1(string input)
        {
            // parse
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            long cycle = 1;
            long register = 1;
            long result = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] == "noop")
                {
                    result += CycleCheck(cycle, register);
                    cycle++;
                }
                if (lines[i][..4] == "addx")
                {
                    result += CycleCheck(cycle, register);
                    cycle++;
                    result += CycleCheck(cycle, register);
                    cycle++;
                    register += long.Parse(lines[i][4..]);
                }
            }
            return "" + result;
        }

        public string SolvePart2(string input)
        {
            // parse
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            long cycle = 1;
            long register = 1;
            char[] crt = new char[240];

            for (int i = 0; i < lines.Length; i++)
            {
                if (cycle > 240) { break; }
                if (lines[i] == "noop")
                {
                    crt[cycle - 1] = CyclePrint(cycle - 1, register);
                    //Console.WriteLine(crt);
                    cycle++;
                }
                if (lines[i][..4] == "addx")
                {
                    crt[cycle - 1] = CyclePrint(cycle - 1, register);
                    //Console.WriteLine(crt);
                    cycle++;
                    crt[cycle - 1] = CyclePrint(cycle - 1, register);
                    //Console.WriteLine(crt);
                    cycle++;
                    register += long.Parse(lines[i][4..]);
                }
                
            }

            // print result
            for (int i = 0; i < crt.Length; i += 40)
            {
                Console.WriteLine(new string(crt[i..(i + 40)]));
            }

            return "";
        }

        public long CycleCheck(long cycle, long register)
        {
            if (cycle == 20 || cycle == 60 || cycle == 100 || cycle == 140 || cycle == 180 || cycle == 220)
            {
                return cycle * register;
            }
            return 0;
        }

        public char CyclePrint(long cycle, long register)
        {
            if ((cycle%40) >= (register - 1) && (cycle%40) <= (register + 1))
            {
                return '#';
            }
            return '.';
        }

        public string GetInput()
        {
            return new Inputs.Year2022.Day10().Input;
        }
    }
}