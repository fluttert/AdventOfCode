using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Year2016
{
    public class Day05 : IAoC
    {
        // Challenge can be found on https://adventofcode.com/2016/day/5

        internal static System.Security.Cryptography.MD5 Md5 = System.Security.Cryptography.MD5.Create();

        public string SolvePart1(string input)
        {
            var password = new List<char>();
            int currentIndex = 0;
            while (password.Count < 8)
            {
                byte[] digest = Md5.ComputeHash(Encoding.UTF8.GetBytes(input + currentIndex));
                if (digest[0] == 0 && digest[1] == 0 && (digest[2] & 0xF0) == 0)
                {
                    password.Add(BitConverter.ToString(digest, 2, 1)[1]);
                }
                currentIndex++;
            }
            return new string(password.ToArray());
        }

        public string SolvePart2(string input)
        {
            var password = new char[8];
            int currentIndex = 0, charsFound = 0;
            while (charsFound < 8)
            {
                Byte[] digest = Md5.ComputeHash(Encoding.UTF8.GetBytes(input + currentIndex));
                if (digest[0] == 0 && digest[1] == 0 && (digest[2] & 0xF0) == 0)
                {
                    char pos = BitConverter.ToString(digest, 2, 1)[1];
                    if (pos >= '0' && pos <= '7' && password[(pos - '0')] == '\0')
                    {
                        password[(pos - '0')] = BitConverter.ToString(digest, 3, 1)[0];
                        charsFound++;
                    }
                }
                currentIndex++;
            }
            return new string(password);
        }

        public string GetInput()
        {
            return "cxdnnyjw";
        }
    }
}
