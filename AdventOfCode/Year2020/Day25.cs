using System;

namespace AdventOfCode.Year2020
{
    public class Day25 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2020/day/25

        /// Generic idea for Day 25
        ///
        public string SolvePart1(string input)
        {
            long[] numbers = Array.ConvertAll(input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries), long.Parse);
            long cardPublicKey = numbers[0];
            long doorPublicKey = numbers[1];
            long loopSizeCard = ReverseSecretLoop(cardPublicKey);
            long loopSizeDoor = ReverseSecretLoop(doorPublicKey);
            return SecretLoop(cardPublicKey,loopSizeDoor).ToString();
        }

        public string SolvePart2(string input)
        {
            return "This is the end!";
        }

        

        public long ReverseSecretLoop(long publicKey) {
            long result = 1;
            int loopValue = 0;
            while (result != publicKey) {
                result *= 7;
                result %= 20201227;
                loopValue++;
            }
            return loopValue;
        }

        public long SecretLoop(long subjectNumber, long loopValue)
        {
            long result = 1;
            for (long i = 0; i < loopValue; i++)
            {
                result *= subjectNumber;
                result %= 20201227;
            }
            return result;
        }

        public string GetInput() => @"10705932
12301431";
    }
}