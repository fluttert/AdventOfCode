using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2015
{
    public class Day05 : IAoC
    {
        // Challenge can be found on https://adventofcode.com/2015/day/5

        public string SolvePart1(string input)
        {
            int niceStrings = 0;
            var vowels = new HashSet<char>() { 'a', 'e', 'i', 'o', 'u' };
            var forbiddenSubstringStart = new HashSet<char>() { 'a', 'c', 'p', 'x' };
            var forbiddenSubstrings = new HashSet<string>() { "ab", "cd", "pq", "xy" };
            //var forbiddenSubstrings = new HashSet<string>
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                // at least 3 vowels, so anything shorter can be skipped
                if (line.Length < 3) { continue; }
                int vowelCount = 0;
                int duplicateCount = 0;
                bool forbiddenSubstring = false;
                for (int i = 0; i < line.Length - 1; i++)
                {
                    if (vowels.Contains(line[i])) { vowelCount++; }
                    if (line[i] == line[i + 1]) { duplicateCount++; }
                    if (forbiddenSubstringStart.Contains(line[i])
                        && forbiddenSubstrings.Contains(line.Substring(startIndex: i, length: 2)))
                    {
                        forbiddenSubstring = true;
                        break;
                    }
                }
                if (vowels.Contains(line[^1])) { vowelCount++; } // do not skip the last letter

                // final verdict
                if (vowelCount >= 3 && duplicateCount >= 1 && !forbiddenSubstring) { niceStrings++; }
            }

            return niceStrings.ToString();
        }

        public string SolvePart2(string input)
        {
            int niceStrings = 0;
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                if (line.Length < 3) { continue; } // at least 3 letters for a nice string

                var letterPairs = new HashSet<string>();
                bool doubleLetterPairFound = false;
                for (int i = 1; i < line.Length; i++)
                {
                    string pair = line.Substring(startIndex: i - 1, length: 2);
                    if (letterPairs.Contains(pair))
                    {
                        doubleLetterPairFound = true;
                        break; // early break no further searching
                    }
                    else { letterPairs.Add(pair); };

                    if (i + 1 < line.Length && line[i - 1] == line[i] && line[i] == line[i + 1])
                    {
                        i++; // increase the counter on the case 'aaa'
                    }
                }

                bool repeatedLetterFound = false;
                for (int i = 2; i < line.Length; i++)
                {
                    if (line[i - 2] == line[i])
                    {
                        repeatedLetterFound = true;
                        break;
                    }
                }

                if (doubleLetterPairFound && repeatedLetterFound)
                {
                    niceStrings++;
                }
            }

            return niceStrings.ToString(); ;
        }

        public string GetInput()
        {
            return new Inputs.Year2015.Day05().Input;
        }
    }
}