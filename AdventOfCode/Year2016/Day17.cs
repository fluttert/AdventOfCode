using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Year2016
{
    public class Day17 : IAoC
    {
        internal static System.Security.Cryptography.MD5 Md5 =
            System.Security.Cryptography.MD5.Create();

        public string SolvePart1(string input)
        {
            var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            return Part1(lines).ToString();
        }

        public string SolvePart2(string input)
        {
            var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            return Part2(lines).ToString();
        }

        public string GetInput() => @"udskfozm";

        public string Part1(string[] input, bool longestPath = false)
        {
            // tuple = X, Y, HASH
            var q = new Queue<Tuple<int, int, string>>();
            q.Enqueue(new Tuple<int, int, string>(0, 0, input[0]));
            string result = string.Empty;
            while (q.Count > 0)
            {
                var curItem = q.Dequeue();
                int x = curItem.Item1;
                int y = curItem.Item2;
                string path = curItem.Item3;

                // exit
                if (x == 3 && y == 3)
                {
                    result = curItem.Item3;

                    // Part 1 is shortest, part 2 is longest
                    if (longestPath) { continue; }
                    else { break; }
                }

                // hash is in capitals and contains dashes
                string hash = BitConverter.ToString(
                    Md5.ComputeHash(Encoding.UTF8.GetBytes(curItem.Item3))
                     );
                bool upIsOpen = hash[0] > 'A';
                bool downIsOpen = hash[1] > 'A';
                bool leftIsOpen = hash[3] > 'A';
                bool rightIsOpen = hash[4] > 'A';

                // process
                if (x > 0 && leftIsOpen) { q.Enqueue(new Tuple<int, int, string>(x - 1, y, path + "L")); }
                if (x < 3 && rightIsOpen) { q.Enqueue(new Tuple<int, int, string>(x + 1, y, path + "R")); }
                if (y > 0 && upIsOpen) { q.Enqueue(new Tuple<int, int, string>(x, y - 1, path + "U")); }
                if (y < 3 && downIsOpen) { q.Enqueue(new Tuple<int, int, string>(x, y + 1, path + "D")); }
            }

            result = result.Substring(input[0].Length);

            return longestPath ? "" + result.Length : result;
        }

        public string Part2(string[] input)
        {
            return Part1(input, true);
        }
    }
}