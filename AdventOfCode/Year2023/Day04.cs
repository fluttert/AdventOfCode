using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2023
{
    public class Day04 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2023/day/4
        public string SolvePart1(string input)
        {
            // split into card
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            int totalPoints = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                int matches = 0;
                // parse a scratchcard
                string[] card = lines[i].Split(new char[] { ':', '|' });
                HashSet<string> winningNumbers = card[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToHashSet();
                string[] myNumbers = card[2].Split(' ', StringSplitOptions.RemoveEmptyEntries);

                // determine amount of matches
                for (int j = 0; j < myNumbers.Length; j++)
                {
                    if (winningNumbers.Contains(myNumbers[j])) { matches++; }
                }

                // determine score based on matches
                if (matches > 0)
                {
                    totalPoints += (int)Math.Pow(2, matches - 1);
                }
            }

            return "" + totalPoints;
        }

        public string SolvePart2(string input)
        {
            // split into card
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            int totalCards = 0;
            Dictionary<int, int> cards = new();
            Dictionary<int, int> matches = new();

            // init dictionaries, assuming cards ID are 1...N
            for (int i = 0; i < lines.Length; i++) { cards.Add(i + 1, 0); }

            // parse all cards
            for (int i = 0; i < lines.Length; i++)
            {
                // parse a scratchcard
                string[] card = lines[i].Split(new char[] { ':', '|' });

                // set the card ID / key
                int cardId = i + 1;
                cards[cardId]++;

                // settle amount of matches
                HashSet<string> winningNumbers = card[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToHashSet();
                string[] myNumbers = card[2].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                int cardMatches = 0;
                for (int j = 0; j < myNumbers.Length; j++)
                {
                    if (winningNumbers.Contains(myNumbers[j])) { cardMatches++; }
                }

                matches.Add(cardId, cardMatches);  // cache result

                // did this card have any duplicates?
                while (cards[cardId] > 0)
                {
                    for (int j = 0; j < matches[cardId]; j++)
                    {
                        cards[cardId + j + 1]++;
                    }
                    totalCards++;
                    cards[cardId]--;
                }
            }

            return "" + totalCards;
        }

        public string GetInput()
        {
            return new Inputs.Year2023.Day04Input().Input;
//            return """
//Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
//Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
//Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
//Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
//Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
//Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11
//""";
        }
    }
}