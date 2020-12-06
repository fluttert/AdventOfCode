using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2017
{
    public class Day10 : IAoC
    {
        private int listLength = 256;
        public int ListLength { get => listLength; set => listLength = value; }

        public string SolvePart1(string input)
        {
            // populate list
            var list = new int[ListLength];
            for (int i = 0; i < ListLength; i++) { list[i] = i; }

            int currentPosition = 0;
            int skipSize = 0;
            var sequences = Array.ConvertAll(input.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries), int.Parse);
            foreach (int seq in sequences)
            {
                // select elements in list to be reversed
                int halfSeq = seq / 2; // 6/2=3, 5/2=2
                int seqenceEndIdex = currentPosition + seq - 1;
                for (int i = 0; i < halfSeq; i++)
                {
                    int startIndex = (currentPosition + i) % ListLength;
                    int endIndex = (seqenceEndIdex - i) % ListLength;

                    // swap / reverse
                    int swap = list[startIndex];
                    list[startIndex] = list[endIndex];
                    list[endIndex] = swap;
                }

                // move to next position
                currentPosition = (currentPosition + seq + skipSize) % ListLength;

                // increase skipsize
                skipSize++;
            }

            return (list[0] * list[1]).ToString();
        }

        public string SolvePart2(string input)
        {
            // populate list
            var list = new int[ListLength];
            for (int i = 0; i < ListLength; i++) { list[i] = i; }

            int currentPosition = 0;
            int skipSize = 0;

            List<int> sequences = input.Select(e => (int)e).ToList();
            sequences.Add(17); sequences.Add(31); sequences.Add(73); sequences.Add(47); sequences.Add(23);

            for (int rounds = 0; rounds < 64; rounds++)
            {
                foreach (int seq in sequences)
                {
                    int halfSeq = seq / 2; // 6/2=3, 5/2=2
                    int seqenceEndIdex = currentPosition + seq - 1;
                    for (int i = 0; i < halfSeq; i++)
                    {
                        int startIndex = (currentPosition + i) % ListLength;
                        int endIndex = (seqenceEndIdex - i) % ListLength;

                        // swap / reverse
                        int swap = list[startIndex];
                        list[startIndex] = list[endIndex];
                        list[endIndex] = swap;
                    }
                    // move to next position
                    currentPosition = (currentPosition + seq + skipSize) % ListLength;

                    // increase skipsize
                    skipSize++;
                }
            }

            string[] output = new string[16];
            for (int i = 0; i < 16; i++)
            {
                int offset = i * 16;
                int xorValue = list[offset];
                for (int j = 1; j < 16; j++)
                {
                    xorValue ^= list[(offset + j)];
                }
                output[i] = xorValue.ToString("X2");
            }

            return string.Join("", output).ToLower();
        }

        public string GetInput() => @"165,1,255,31,87,52,24,113,0,91,148,254,158,2,73,153";
    }
}