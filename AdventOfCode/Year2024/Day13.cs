using System;
using System.IO;

namespace AdventOfCode.Year2024
{
    public class Day13 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2024/day/13

        public string SolvePart1(string input)
        {
            long result = 0;
            string[] clawmachines = input.Split(Environment.NewLine + Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            foreach (string clawmachine in clawmachines)
            {
                string[] lines = clawmachine.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
                int[] buttonA = Array.ConvertAll(lines[0][11..].Split(new char[] { ' ', ',', 'Y', 'X' }, StringSplitOptions.RemoveEmptyEntries), int.Parse);
                int[] buttonB = Array.ConvertAll(lines[1][11..].Split(new char[] { ' ', ',', 'Y', 'X' }, StringSplitOptions.RemoveEmptyEntries), int.Parse);
                int[] prize = Array.ConvertAll(lines[2][7..].Split(new char[] { ' ', ',', 'Y', 'X', '=' }, StringSplitOptions.RemoveEmptyEntries), int.Parse);

                // CRAMERS LAW
                // A = (p_x*b_y - prize_y*b_x) / (a_x * b_y - a_y * b_x)
                // B = (a_x * p_y - a_y * p_x) / (a_x * b_y - a_y * b_x)

                // A = (8400*67 - 5400*22) / (94*67 - 34*22) = 80
                // B = (8400*34 - 5400*94) / (94*67 - 34*22) = 40
                // src https://www.reddit.com/r/adventofcode/comments/1hd7irq/2024_day_13_an_explanation_of_the_mathematics/
                int determinant = (buttonA[0] * buttonB[1]) - (buttonA[1] * buttonB[0]);
                int pushedA = Math.Abs((prize[0] * buttonB[1] - prize[1] * buttonB[0]) / determinant);
                int pushedB = Math.Abs((prize[0] * buttonA[1] - prize[1] * buttonA[0]) / determinant);
                if ((buttonA[0] * pushedA + buttonB[0] * pushedB == prize[0]) && (buttonA[1] * pushedA + buttonB[1] * pushedB == prize[1]))
                {
                    result += (pushedA * 3) + pushedB;
                }
            }

            return "" + result;
        }

        public string SolvePart2(string input)
        {
            long result = 0;
            string[] clawmachines = input.Split(Environment.NewLine + Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            foreach (string clawmachine in clawmachines)
            {
                string[] lines = clawmachine.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
                long[] buttonA = Array.ConvertAll(lines[0][11..].Split(new char[] { ' ', ',', 'Y', 'X' }, StringSplitOptions.RemoveEmptyEntries), long.Parse);
                long[] buttonB = Array.ConvertAll(lines[1][11..].Split(new char[] { ' ', ',', 'Y', 'X' }, StringSplitOptions.RemoveEmptyEntries), long.Parse);
                long[] prize = Array.ConvertAll(lines[2][7..].Split(new char[] { ' ', ',', 'Y', 'X', '=' }, StringSplitOptions.RemoveEmptyEntries), long.Parse);

                // add some extra zero up front
                prize[0] += 10_000_000_000_000;
                prize[1] += 10_000_000_000_000;

                long determinant = (buttonA[0] * buttonB[1]) - (buttonA[1] * buttonB[0]);
                long pushedA = Math.Abs((prize[0] * buttonB[1] - prize[1] * buttonB[0]) / determinant);
                long pushedB = Math.Abs((prize[0] * buttonA[1] - prize[1] * buttonA[0]) / determinant);
                if ((buttonA[0] * pushedA + buttonB[0] * pushedB == prize[0]) && (buttonA[1] * pushedA + buttonB[1] * pushedB == prize[1]))
                {
                    result += (pushedA * 3) + pushedB;
                }
            }

            return "" + result;
        }

        public string GetInput()
        {
            var file_contents = File.ReadAllText("./input.txt");
            return file_contents;
        }
    }
}