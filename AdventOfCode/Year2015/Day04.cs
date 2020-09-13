using System;
using System.Text;

namespace AdventOfCode.Year2015
{
    public class Day04 : IAoC
    {
        // Challenge can be found on https://adventofcode.com/2015/day/4

        public string input = "yzbqklnj";

        public string SolvePart1(string input)
        {
            int number = 0;
            bool leadingZerosFound = false;
            while (!leadingZerosFound)
            {
                if (CalculateMD5(input + number).StartsWith("00000")){
                    break;
                }
                    number++;
            }
            return number.ToString();
        }

        public string SolvePart2(string input)
        {
            int number = 0;
            bool leadingZerosFound = false;
            while (!leadingZerosFound)
            {
                if (CalculateMD5(input + number).StartsWith("000000"))
                {
                    break;
                }
                number++;
            }
            return number.ToString();
        }

        // Calculate MD5 hashes, based on https://docs.microsoft.com/en-us/troubleshoot/dotnet/csharp/compute-hash-values
        public string CalculateMD5(string input)
        {
            using System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}