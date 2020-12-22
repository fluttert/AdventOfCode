using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2020
{
    public class Day22 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2020/day/22

        /// Generic idea for Day 22
        ///
        public string SolvePart1(string input)
        {
            var player1 = new Queue<int>();
            var player2 = new Queue<int>();
            // parse input
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            bool isPlayer2 = false;
            for (int i = 1; i < lines.Length; i++)
            {
                if (lines[i][0] == 'P') { isPlayer2 = true; continue; }
                if (isPlayer2) { player2.Enqueue(int.Parse(lines[i])); }
                else { player1.Enqueue(int.Parse(lines[i])); }
            }

            // play the game! untill 1 player has no cards left
            while (player1.Count > 0 && player2.Count > 0)
            {
                var p1 = player1.Dequeue();
                var p2 = player2.Dequeue();

                // determine winner or current cards
                if (p1 > p2)
                {   // player 1 wins
                    player1.Enqueue(p1); player1.Enqueue(p2);
                }
                else
                {   // player 2 wins
                    player2.Enqueue(p2); player2.Enqueue(p1);
                }
            }

            // scoring mechanism
            int cardMultiplyer = lines.Length - 2;
            bool player1Won = player1.Count > 0;
            long sum = 0;
            for (int i = 0; i < lines.Length-2; i++)
            {
                sum += player1Won ? (player1.Dequeue() * cardMultiplyer) : (player2.Dequeue() * cardMultiplyer);
                cardMultiplyer--;
            }
            return sum.ToString();
        }

        // in progress
        public string SolvePart2(string input)
        {
            // read per lines
            return "";
        }

        public string GetInput()
        {
            return new Inputs.Year2020.Day22().Input;
        }
    }
}