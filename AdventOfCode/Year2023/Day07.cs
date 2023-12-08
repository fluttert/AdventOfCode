using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2023
{
    public class Day07 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2023/day/7
        public string SolvePart1(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            List<Hand> hands = new();
            foreach (string line in lines) { hands.Add(new Hand(line, false)); }
            hands.Sort();   // sort on comparer
            long result = 0;
            for (int i = 0; i < hands.Count; i++)
            {
                result += hands[i].Bid * (i + 1);
            }
            return "" + result;
        }

        public string SolvePart2(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            List<Hand> hands = new();
            foreach (string line in lines) { hands.Add(new Hand(line, true)); }
            hands.Sort();   // sort on comparer
            long result = 0;
            for (int i = 0; i < hands.Count; i++)
            {
                result += hands[i].Bid * (i + 1);
            }
            return "" + result;
        }

        public string GetInput()
        {
            return new Inputs.Year2023.Day07Input().Input;
//            return """
//32T3K 765
//T55J5 684
//KK677 28
//KTJJT 220
//QQQJA 483
//""";
        }
    }

    public class Hand : IComparable<Hand>
    {
        public int Bid { get; }
        public string Cards { get; }
        public int Strength { get; }
        public bool IncludeJoker { get; }

        public static readonly char Joker = 'J';

        public static readonly Dictionary<char, int> CardsOrder = new() {
            { '2', 2 }, { '3', 3 }, { '4', 4 } , { '5', 5 }, { '6', 6 }, { '7', 7 }, { '8', 8 }, { '9', 9 },
            { 'T', 10 }, { 'J', 11 }, { 'Q', 12 }, { 'K', 13 }, { 'A', 14 }
        };

        public static readonly Dictionary<char, int> CardsOrderWithJoker = new() {
            { '2', 2 }, { '3', 3 }, { '4', 4 } , { '5', 5 }, { '6', 6 }, { '7', 7 }, { '8', 8 }, { '9', 9 },
            { 'T', 10 }, { 'J', 1 }, { 'Q', 12 }, { 'K', 13 }, { 'A', 14 }
        };

        public Hand(string input, bool includeJoker = false)
        {
            Bid = int.Parse(input[6..].Trim());
            Cards = input[..5];
            Strength = SetStrength(Cards.ToCharArray(), includeJoker);
            IncludeJoker = includeJoker;
        }

        /// <summary>
        /// CompareTo function to make sure sorting can be done efficiently on collection level
        /// </summary>
        /// <param name="other">Hand</param>
        /// <returns>int, positive if stronger, negative if weaker, else 0 on equal strength</returns>
        public int CompareTo(Hand other)
        {
            if (Strength > other.Strength) { return 1; }
            if (Strength < other.Strength) { return -1; }
            if (Strength == other.Strength)
            {
                for (int i = 0; i < Cards.Length; i++)
                {
                    if (Cards[i] == other.Cards[i]) { continue; }
                    return IncludeJoker
                        ? CardsOrderWithJoker[Cards[i]] - CardsOrderWithJoker[other.Cards[i]] // with Joker
                        : CardsOrder[Cards[i]] - CardsOrder[other.Cards[i]];                  // without Joker
                }
            }
            return 0; // 100% equal cards
        }

        private int SetStrength(char[] cards, bool joker)
        {
            // no joker OR no joker-card present = regular strength
            int strength = DetermineStrength(cards);                // always determine initial strength
            if (joker is false || cards.Contains(Joker) == false) { return strength; }

            List<int> jokers = new();                       // get indexes of Jokers
            for (int i = 0; i < cards.Length; i++) { if (cards[i] == Joker) { jokers.Add(i); } }

            // Short circuit obvious choices
            if (strength == 7 || jokers.Count >= 4) { return 7; }   // JJJJJJ || JJJJA = 5 of a kind
            if (jokers.Count == 3 && strength == 5) { return 7; }   // Full house JJJAA => makes 5 of kind
            if (jokers.Count == 3 && strength == 4) { return 6; }   // 3 of kind upgraded to 4 of a kind
            if (jokers.Count == 1 && strength == 1) { return 2; }   // High Card will be converted to 1 pair

            // TODO
            // use queue + while loop to clean up
            // queue<(char[], list<int>)> queue => list contains the joker indexes, and only determine strength when the joker list is empty

            // get all 1 or 2 place permutations (max 13^2 possibilities)
            foreach (var kvp in CardsOrderWithJoker)
            {
                if (kvp.Key == 'J') { continue; }
                char[] curCard = cards;
                int curStrength = 0;
                curCard[jokers[0]] = kvp.Key;
                if (jokers.Count > 1)
                {
                    foreach (var kvp2 in CardsOrderWithJoker)
                    {
                        if (kvp.Key == 'J') { continue; }
                        curCard[jokers[1]] = kvp2.Key;
                        curStrength = DetermineStrength(curCard);
                        strength = curStrength > strength ? curStrength : strength;
                    }
                }
                else
                {
                    curStrength = DetermineStrength(curCard);
                    strength = curStrength > strength ? curStrength : strength;
                }
            }
            return strength;
        }

        private int DetermineStrength(char[] cards)
        {
            Dictionary<char, int> hand = new();    // make histogram of characters
            for (int i = 0; i < cards.Length; i++)
            {
                char c = cards[i];                                  // examine the current character
                if (hand.ContainsKey(c)) { continue; }              // skip if already determines
                int amount = cards.Count(p => p == c);
                hand.Add(c, amount);                                // add amount
            }

            // 5 of a kind = 7  , AAAAA
            if (hand.Keys.Count == 1) { return 7; }
            // High card = 1    , 34567
            if (hand.Keys.Count == 5) { return 1; }
            // 4 of a kind = 6  , AAAA2
            if (hand.Any(p => p.Value == 4)) { return 6; }
            // full house = 5   , AAA22
            if (hand.Any(p => p.Value == 3) && hand.Any(p => p.Value == 2)) { return 5; }
            // 3 of a kind = 4  , AAA23
            if (hand.Any(p => p.Value == 3)) { return 4; }
            // 2 pair = 3       , AA223
            if (hand.Count(p => p.Value == 2) == 2) { return 3; }
            // 1 pair = 2       , AA234
            if (hand.Any(p => p.Value == 2)) { return 2; }

            // else throw exception, as we should not be here
            throw new InvalidOperationException($"Invalid hand: {cards} ");
        }
    }
}