using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Year2016
{
    public class Day14 : IAoC
    {
        internal static System.Security.Cryptography.MD5 Md5 = System.Security.Cryptography.MD5.Create();

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

        public string GetInput() => @"qzyelonm";

        public int Part1(string[] input, int keystretching = 0)
        {
            string salt = input[0].Trim();
            int currentIndex = 0;
            int keysFound = 0;
            string[] triplets = new string[1000];
            string[] quintets = new string[1000];
            string[] hashes = new string[1000];
            while (keysFound < 64 && currentIndex < 1e+6)
            {
                // search the current space (so n-1000)
                int indexModThousand = currentIndex % 1000;
                if (!string.IsNullOrEmpty(triplets[indexModThousand]))
                {
                    //bool keyFound = false;
                    char curChar = triplets[indexModThousand][0];
                    for (int j = 0; j < 1000; j++)
                    {
                        // skip yourself
                        if (j == indexModThousand) { continue; }

                        if (quintets[j].Contains(curChar))
                        {
                            //keyFound = true;
                            keysFound++;
                            //Debug.WriteLine($"Found key {keysFound} for index: {currentIndex - 1000}");
                            break;
                        }
                    }

                    if (keysFound == 64)
                    {
                        return currentIndex - 1000;
                    }
                }

                // overwrite with new entry (= n)
                string hash = BitConverter.ToString(
                    Md5.ComputeHash(
                        Encoding.UTF8.GetBytes(salt + currentIndex)
                        )
                     ).Replace("-", "").ToLowerInvariant();

                // part 2
                for (int keystretch = 0; keystretch < keystretching; keystretch++)
                {
                    hash = BitConverter.ToString(
                    Md5.ComputeHash(
                        Encoding.UTF8.GetBytes(hash)
                        )
                     ).Replace("-", "").ToLowerInvariant();
                }

                triplets[indexModThousand] = ConsecutiveChars(hash, 3);
                quintets[indexModThousand] = ConsecutiveChars(hash, 5);
                currentIndex++;
            }
            return currentIndex;
        }

        public int Part2(string[] input)
        {
            return Part1(input, 2016);
        }

        public string ConsecutiveChars(string input, int length)
        {
            HashSet<char> consecutivechars = new HashSet<char>();
            for (int i = length - 1; i < input.Length; i++)
            {
                char curChar = input[i];
                bool consecutive = true;
                for (int j = 1; j < length; j++)
                {
                    if (input[i - j] != curChar)
                    {
                        consecutive = false;
                        break;
                    }
                }
                if (consecutive) { consecutivechars.Add(curChar); }
            }
            return new string(consecutivechars.ToArray());
        }
    }
}