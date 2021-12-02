using System.Collections.Generic;

namespace AdventOfCode.Year2020
{
    public class Day23 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2020/day/23

        /// Generic idea for Day 23
        /// part 1: Do a simple int-array and shuffle the cups around
        /// part 2: OMG, need to implement some kind of linked list -> only list the cup-values and their neighbour value (simpler then part 1)
        public string SolvePart1(string input)
        {
            var state = ParseStatePart01(input);
            int rounds = 0;
            int index = 0;
            while (rounds < 100)
            {
                var roundresult = PlayRound(index % state.Length, state);
                index = roundresult.startCup + 1;
                state = roundresult.cups;
                System.Diagnostics.Debug.WriteLine($"Round: {rounds}, result: {string.Join(' ', state)}");
                rounds++;
            }

            // where is the 1
            int indexof1 = -1;
            for (int i = 0; i < state.Length; i++)
            {
                if (state[i] == 1) { indexof1 = i; break; }
            }

            string result = "";
            for (int i = 1; i < state.Length; i++)
            {
                result += state[(indexof1 + i) % state.Length];
            }

            return result;
        }

        public string SolvePart2(string input)
        {
            int[] result = PlayGame(input, 1_000_000, 10_000_000);
            long next = (long)result[1];
            long nextnext = (long)result[next];
            return (next * nextnext).ToString();
        }

        public int[] PlayGame(string input, int maxNumber, int rounds)
        {
            // state CUPVALUE -> CupValue to the right
            int[] state = ParseStatePart02(input, maxNumber);

            // now do a round :)
            int currentCup = input[0] - '0';
            int round = 0;
            while (round < rounds)
            {
                // determine all nodes that are now fringed
                int neighbour1 = state[currentCup];
                int neighbour2 = state[neighbour1];
                int neighbour3 = state[neighbour2];
                int neighbour4 = state[neighbour3]; // will be the new currentcup neighbour, and also the new current cup

                // determine new insert
                int destinationCup = currentCup - 1;
                while (destinationCup == 0 || destinationCup == neighbour1 || destinationCup == neighbour2 || destinationCup == neighbour3)
                {
                    if (destinationCup == 0) { destinationCup = maxNumber; continue; }
                    destinationCup--;
                }

                int destNeighbour = state[destinationCup];

                // alter links
                state[currentCup] = neighbour4;
                state[destinationCup] = neighbour1;
                state[neighbour3] = destNeighbour;

                // update round info
                currentCup = neighbour4;
                round++;
            }
            return state;
        }

        public int[] ParseStatePart01(string input)
        {
            var result = new int[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                result[i] = input[i] - '0';
            }
            return result;
        }

        public int[] ParseStatePart02(string input, int maxNumber)
        {
            // the cups are 1-based
            var state = new int[maxNumber + 1];

            // init perfect circle
            for (int i = 1; i <= maxNumber; i++) { state[i - 1] = i; }

            // overwrite beginning
            for (int i = 1; i < input.Length; i++)
            {
                int number1 = input[i - 1] - '0';
                int number2 = input[i] - '0';
                state[number1] = number2;
            }

            // finish it off with the loop back to the first element
            state[input[^1] - '0'] = (input[0] - '0');

            // and fix the transition between regular input to perfect cirlce
            if (input.Length < maxNumber)
            {
                // transition to normal part
                int n1 = input[^1] - '0'; // get the last value
                state[n1] = input.Length + 1;

                // fix ending
                state[maxNumber] = (input[0] - '0');
            }
            state[0] = 0;       // Zero is never an option
            return state;
        }

        /// <summary>
        /// In this attempt, the array = static, meaning the postion of the cups is fixed, while the value shuffle arround
        /// </summary>
        /// <param name="startCup"></param>
        /// <param name="cups"></param>
        /// <returns></returns>
        public (int startCup, int[] cups) PlayRound(int startCup, int[] cups)
        {
            int cupAmount = cups.Length;
            var result = new int[cupAmount];
            int startCupValue = cups[startCup];
            int resultStartCup = startCup;

            // get 3 neighbours (fringe)
            var fringe = new HashSet<int>(3);
            for (int i = 0; i < 3; i++)
            {
                fringe.Add(cups[(startCup + 1 + i) % cupAmount]);
            }

            // select destination (current cup - 1/2/3/4/5)
            int destCupValue = startCupValue == 1 ? 9 : startCupValue - 1;
            while (fringe.Contains(destCupValue))
            {
                // zero is not allowed
                //if (destCupValue == 1) { destCupValue = 9; }
                //destCupValue--;
                destCupValue = destCupValue == 1 ? 9 : destCupValue - 1;
            }

            // insert back
            int resultIndex = 0, originalIndex = 0;
            while (resultIndex < cupAmount && originalIndex < cupAmount)
            {
                // case 1: ignore fringe numbers
                if (fringe.Contains(cups[originalIndex])) { originalIndex++; continue; }

                // case 2: destination cup found
                if (cups[originalIndex] == destCupValue)
                {
                    // add this + the 3 fringe
                    result[resultIndex] = cups[originalIndex];
                    resultIndex++; originalIndex++;
                    foreach (int f in fringe)
                    {
                        result[resultIndex] = f;
                        resultIndex++;
                    }
                    continue;
                }

                // case 3 insert startcup
                if (originalIndex == startCup) { resultStartCup = resultIndex; }

                // case 3 (default) copy it over
                result[resultIndex] = cups[originalIndex];
                resultIndex++; originalIndex++;
            }

            return (resultStartCup, result);
        }

        public string GetInput() => @"318946572";
    }
}