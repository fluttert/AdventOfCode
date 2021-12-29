using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2021
{
    public class Day04 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2021/day/4

        public string SolvePart1(string input)
        {
            // Convert input to usable instructions
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            int[] bingoNumbers = Array.ConvertAll(lines[0].Split(','), int.Parse);
            List<BingoCard> cards = new();
            for (int i = 1; i < lines.Length; i += 5)
            {
                BingoCard bc = new();
                for (int j = 0; j < 5; j++)
                {
                    bc.SetRow(j, lines[i + j]);
                }
                cards.Add(bc);
            }

            // play a game!
            int finalScore = -1;
            for (int i = 0; i < bingoNumbers.Length; i++)
            {
                for (int j = 0; j < cards.Count; j++)
                {
                    if (cards[j].ScratchNumber(bingoNumbers[i]))
                    {
                        finalScore = cards[j].FinalScore();
                        break;
                    }
                }
                if (finalScore > 0) { break; }
            }
            return "" + finalScore;
        }

        public string SolvePart2(string input)
        {
            // Convert input to usable instructions
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            int[] bingoNumbers = Array.ConvertAll(lines[0].Split(','), int.Parse);
            List<BingoCard> cards = new();
            for (int i = 1; i < lines.Length; i += 5)
            {
                BingoCard bc = new();
                for (int j = 0; j < 5; j++)
                {
                    bc.SetRow(j, lines[i + j]);
                }
                cards.Add(bc);
            }

            // play ALL the bingo cards simply keep playing
            int finalScore = -1;
            for (int i = 0; i < bingoNumbers.Length; i++)
            {
                for (int j = 0; j < cards.Count; j++)
                {
                    if (cards[j].hasBingo is false && cards[j].ScratchNumber(bingoNumbers[i]))
                    {
                        finalScore = cards[j].FinalScore();
                    }
                }
            }
            return "" + finalScore;
        }

        public string GetInput()
        {
            return new Inputs.Year2021.Day04().Input;
        }
    }

    public class BingoCard
    {
        private int[][] card = new int[5][];
        private HashSet<int> cardNumbers = new HashSet<int>();
        public bool hasBingo = false;
        private int lastNumber = 0;

        /// <param name="row">Int between 0 and 4 (5 rows, zero based)</param>
        /// <param name="numbers">String of integers seperated by spaces</param>
        /// <returns>bool if insert was successful</returns>
        public bool SetRow(int row, string numbers)
        {
            int[] rowNumbers = Array.ConvertAll(numbers.Split(' ', StringSplitOptions.RemoveEmptyEntries), int.Parse); ;
            // invalid row and/or number fed to the card
            if (row > 4 || rowNumbers.Length != 5) { return false; }
            card[row] = rowNumbers;

            foreach (int n in rowNumbers) { cardNumbers.Add(n); }
            return true; ;
        }

        public bool ScratchNumber(int number)
        {
            lastNumber = number;
            if (!cardNumbers.Contains(number)) { return false; }
            // locate and scratch
            bool found = false;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (card[i][j] == number)
                    {
                        card[i][j] = -1;
                        found = true;
                        break;
                    }
                }
                if (found) { break; }
            }

            // check bingo
            return CheckBingo();
        }

        public bool CheckBingo()
        {
            // when a complete row OR column is scratched (set to -1), it is considerd a bingo
            for (int i = 0; i < 5; i++)
            {
                bool bingoRow = true;
                bool bingoColumn = true;
                for (int j = 0; j < 5; j++)
                {
                    // check rows && columns at the same time
                    if (card[i][j] != -1) { bingoRow = false; }
                    if (card[j][i] != -1) { bingoColumn = false; }
                }
                if (bingoColumn || bingoRow)
                {
                    hasBingo = true;
                    return true;
                }
            }
            return false;
        }

        // Score is calculated by multiplying the last played number with the sum of the unscratched numbers
        public int FinalScore()
        {
            int sum = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (card[i][j] > 0) { sum += card[i][j]; }
                }
            }
            return lastNumber * sum;
        }
    }
}