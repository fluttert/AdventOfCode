using System;

namespace AdventOfCode.Year2022
{
    public class Day02 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2022/day/1

        public string SolvePart1(string input)
        {
            int score = 0;
            string[] lines = input.Split(Environment.NewLine);
            foreach (string line in lines)
            {
                // A = Rock / B = Paper / C = Scissors
                // X = Rock (1) / Y = Paper (2) / Z = Scissors (3)
                // point per round 0 if lose, 3 if draw, 6 if win
                char opponent = line[0], player = line[2];
                // determine our points
                if (player == 'X')
                {
                    score += 1;
                    if (opponent == 'C') { score += 6; }
                    if (opponent == 'A') { score += 3; }
                }
                if (player == 'Y')
                {
                    score += 2;
                    if (opponent == 'A') { score += 6; }
                    if (opponent == 'B') { score += 3; }
                }
                if (player == 'Z')
                {
                    score += 3;
                    if (opponent == 'B') { score += 6; }
                    if (opponent == 'C') { score += 3; }
                }
            }
            return "" + score;
        }

        public string SolvePart2(string input)
        {
            int score = 0;
            string[] lines = input.Split(Environment.NewLine);
            foreach (string line in lines)
            {
                // A = Rock / B = Paper / C = Scissors
                // X = Lose / Y = Draw / Z = Win
                // point per round 0 if lose, 3 if draw, 6 if win
                char opponent = line[0], player = line[2];
                // determine our points
                if (player == 'X')
                {
                    // no score for loosing
                    if (opponent == 'A') { score += 3; }
                    if (opponent == 'B') { score += 1; }
                    if (opponent == 'C') { score += 2; }
                }
                if (player == 'Y')
                {
                    score += 3;
                    if (opponent == 'A') { score += 1; }
                    if (opponent == 'B') { score += 2; }
                    if (opponent == 'C') { score += 3; }
                }
                if (player == 'Z')
                {
                    score += 6;
                    if (opponent == 'A') { score += 2; }
                    if (opponent == 'B') { score += 3; }
                    if (opponent == 'C') { score += 1; }
                }
            }
            return "" + score;
        }

        public string GetInput()
        {
            return new Inputs.Year2022.Day02().Input;
        }
    }
}