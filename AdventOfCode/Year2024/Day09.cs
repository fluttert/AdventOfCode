using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;

namespace AdventOfCode.Year2024
{
    public class Day09 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2024/day/9

        public string SolvePart1(string input)
        {
            long result = 0;
            // Position - FileId
            List<int> diskmap = UnrollToDiskmap(input);

            // defrag this thing
            int head = 0, tail = diskmap.Count - 1;
            while (head < tail + 1)
            {
                // move head forward if not empty
                while (diskmap[head] != -1) { head++; }

                // move tail backwards till number is reach
                while (diskmap[tail] == -1) { tail--; }

                // swap numbers
                if (head > tail) { continue; }
                diskmap[head] = diskmap[tail];
                diskmap[tail] = -1;
            }
            //Console.WriteLine($"Head: {head}, Tail: {tail}");
            //Console.WriteLine(string.Join("", diskmap));
            // count the numbers
            for (int i = 0; i < diskmap.Count; i++)
            {
                if (diskmap[i] > 0)
                {
                    result += (i * diskmap[i]);
                }
            }

            return "" + result;
        }

        public string SolvePart2(string input)
        {
            long result = 0;
            // Position - FileId
            List<int> diskmap = UnrollToDiskmap(input);
            // blocklength - startIndex
            Dictionary<int, List<int>> freeBlocks = new();

            // work backwards
            for (int i = diskmap.Count - 1; i >= 0; i--)
            {
                if (diskmap[i] == -1) { continue; }
                int fLength = FindLengthFromEnd(diskmap, i);
                // check if we can swap it
                int swapStartIndex = FindEmptySpace(diskmap, fLength, i);
                if (swapStartIndex >= 0)
                {
                    // overwrite current empty space with a new one
                    for (int j = 0; j < fLength; j++)
                    {
                        diskmap[swapStartIndex + j] = diskmap[i];
                    }
                    // overwrite with empty space
                    for (int j = 0; j < fLength; j++)
                    {
                        diskmap[i - j] = -1;
                    }
                }
                // skip this section
                i -= fLength - 1;
            }

            // create result
            for (int i = 0; i < diskmap.Count; i++)
            {
                if (diskmap[i] != -1) { result += (i * diskmap[i]); }
            }

            return "" + result;
        }

        private int FindLengthFromEnd(List<int> diskmap, int index)
        {
            int result = 1;
            int curIndex = index - 1;
            while (curIndex > 0 && diskmap[index] == diskmap[curIndex])
            {
                result++;
                curIndex--;
            }
            return result;
        }

        private int FindEmptySpace(List<int> diskmap, int length, int maxIndex)
        {
            int result = -1;
            for (int i = 0; i < maxIndex; i++)
            {
                if (diskmap[i] >= 0) { continue; }
                int curIndex = i;
                int curLength = 1;
                while (curIndex < maxIndex)
                {
                    if (length == curLength) { return i; }
                    if (diskmap[curIndex + curLength] == -1)
                    {
                        curLength++;
                    }
                    else { break; }
                    // first possible break
                }
                // skip this length if not fitting
                i += curLength - 1;
            }
            return result;
        }

        private List<int> UnrollToDiskmap(string input)
        {
            List<int> diskmap = new();
            // unroll
            int fileId = 0;
            //int index = 0;
            for (int i = 0; i < input.Length; i++)
            {
                int cur = input[i] - '0';
                bool isFile = i % 2 == 0;
                for (int j = 0; j < cur; j++)
                {
                    diskmap.Add(isFile ? fileId : -1);
                }
                fileId += isFile ? 1 : 0;
            }
            // show me!
            //Console.WriteLine(string.Join("", diskmap));
            return diskmap;
        }

        public string GetInput()
        {
            var file_contents = File.ReadAllText("./input.txt");
            return file_contents;
        }
    }
}