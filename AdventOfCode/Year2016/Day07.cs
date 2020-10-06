using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Year2016
{
    public class Day07 : IAoC
    {
        // Challenge can be found on https://adventofcode.com/2016/day/7

        internal static System.Security.Cryptography.MD5 Md5 = System.Security.Cryptography.MD5.Create();

        public string SolvePart1(string input)
        {
            var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            int tlsSupported = 0;
            foreach (string line in lines)
            {
                int brackets = 0;
                bool foundAbba = false, foundInBrackets = false;
                for (int i = 0; i < (line.Length - 3); i++)
                {
                    if (line[i] == '[' || line[i] == ']')
                    {
                        brackets += line[i] == '[' ? 1 : -1;
                        continue;
                    }
                    if (line[i] != line[i + 1] && line[i] == line[i + 3] && line[i + 1] == line[i + 2])
                    {
                        if (brackets > 0) { foundInBrackets = true; }
                        foundAbba = true;
                    }
                }
                tlsSupported += foundAbba && !foundInBrackets ? 1 : 0;
            }
            return tlsSupported.ToString() ;
        }

        public string SolvePart2(string input)
        {
            var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            int sslSupported = 0;
            foreach (string line in lines)
            {
                int brackets = 0;
                var aba = new HashSet<string>();
                var bab = new HashSet<string>();
                for (int i = 0; i < (line.Length - 2); i++)
                {
                    if (line[i] == '[' || line[i] == ']')
                    {
                        brackets += line[i] == '[' ? 1 : -1;
                        continue;
                    }
                    if (line[i] != line[i + 1] && line[i] == line[i + 2])
                    {
                        string curSubstring = "" + line[i] + line[i + 1] + line[i + 2];
                        string inverse = "" + line[i + 1] + line[i] + line[i + 1];
                        if (brackets == 0)
                        {
                            if (bab.Contains(inverse)) { sslSupported++; break; }
                            aba.Add(curSubstring);
                        }
                        else
                        {
                            if (aba.Contains(inverse)) { sslSupported++; break; }
                            bab.Add(curSubstring);
                        }
                    }
                }
            }
            return sslSupported.ToString();
        }

        public string GetInput()
        {
            return new Inputs.Year2016.Day07().Input;
        }
    }
}
