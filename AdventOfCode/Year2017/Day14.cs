using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Year2017
{
    public class Day14 : IAoC
    {
        public string SolvePart1(string input)
        {
            List<string> grid = GenerateGrid(input);
            int used = 0;
            foreach (string row in grid)
            {
                used += row.Count(a => a == '1');
            }
            return used.ToString();
        }

        public string SolvePart2(string input)
        {
            // convert that grid to something sensible
            List<string> tmpGrid = GenerateGrid(input);
            char[][] grid = new char[tmpGrid.Count][];
            for (int i = 0; i < tmpGrid.Count; i++) { grid[i] = tmpGrid[i].ToCharArray(); }
            int regions = 0;
            // search regions and mark em
            for (int i = 0; i < 128; i++)
            {
                for (int j = 0; j < 128; j++)
                {
                    if (grid[i][j] == '1')
                    {
                        // region found!
                        regions++;

                        // mark em done
                        var queue = new Queue<Tuple<int, int>>();
                        queue.Enqueue(new Tuple<int, int>(i, j));
                        while (queue.Count > 0)
                        {
                            var t = queue.Dequeue();
                            int x = t.Item1, y = t.Item2;

                            if (grid[x][y] == '0') { continue; }

                            // mark done
                            grid[x][y] = '0';

                            // add left, right, upper, lower neighbour
                            if (x > 0 && grid[x - 1][y] == '1') { queue.Enqueue(new Tuple<int, int>(x - 1, y)); }
                            if (x < 127 && grid[x + 1][y] == '1') { queue.Enqueue(new Tuple<int, int>(x + 1, y)); }
                            if (y > 0 && grid[x][y - 1] == '1') { queue.Enqueue(new Tuple<int, int>(x, y - 1)); }
                            if (y < 127 && grid[x][y + 1] == '1') { queue.Enqueue(new Tuple<int, int>(x, y + 1)); }
                        }
                    }
                }
            }

            return regions.ToString();
        }

        public string GetInput() => "jxqlasbh";

        public List<string> GenerateGrid(string input, int rows = 128)
        {
            var grid = new List<string>();
            for (int i = 0; i < rows; i++)
            {
                string token = $"{input}-{i}";
                string hash = KnotHash(token);
                StringBuilder sb = new StringBuilder();
                foreach (char c in hash)
                {
                    int val = 0;
                    if (c >= '0' && c <= '9')
                    {
                        val = c - '0';
                    }
                    if (c >= 'a' && c <= 'f')
                    {
                        val = 10 + (c - 'a');
                    }

                    sb.Append(Convert.ToString(val, 2).PadLeft(4, '0'));
                }
                grid.Add(sb.ToString());
            }
            return grid;
        }

        public string KnotHash(string input)
        {
            // populate list
            var ListLength = 256;
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
    }
}