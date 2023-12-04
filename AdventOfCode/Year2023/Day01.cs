using AdventOfCode.Inputs.Year2023;
using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2023
{
    public class Day01 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2023/day/1

        public string SolvePart1(string input)
        {
            string[] lines = input.Split('\n');
            int total = 0;
            foreach (string line in lines)
            {
                char[] number = new char[2];
                for (int i = 0; i < line.Length; i++)
                {
                    if (char.IsDigit(line[i]))
                    {
                        number[0] = line[i];
                        break;
                    }
                }
                for (int i = line.Length - 1; i >= 0; i--)
                {
                    if (char.IsDigit(line[i]))
                    {
                        number[1] = line[i];
                        break;
                    }
                }
                total += int.Parse(number);
            }
            return "" + total;
        }

        public string SolvePart2(string input)
        {
            string[] lines = input.Split(Environment.NewLine);
            int total = 0;
            Dictionary<string, char> writtenNumbers = new(){
                    { "one",'1'},
                    { "two",'2'},
                    { "three",'3'},
                    { "four",'4'},
                    { "five",'5'},
                    { "six",'6'},
                    { "seven",'7'},
                    { "eight",'8'},
                    { "nine",'9'}
                };
            foreach (string line in lines)
            {
                //// shorter answer, but needs to allocate substrings
                //List<char> digits = new List<char>();
                //for (int i = 0; i < line.Length; i++) {
                //    if (char.IsDigit(line[i])) { digits.Add(line[i]); continue; }
                //    string tmp = line[i..]; // expensive!
                //    foreach(string key in writtenNumbers.Keys) {
                //        if (tmp.StartsWith(key)){ digits.Add(writtenNumbers[key]); break; }
                //    }
                //}
                //int concatenatedNumber = int.Parse("" + digits[0] + digits[digits.Count - 1]);
                //total += concatenatedNumber;

                // IDEA to do everything with indexof, so we dont need any temporary strings
                int firstDigit = line.Length, lastDigit = -1;
                char[] number = new char[2];
                foreach (string key in writtenNumbers.Keys)
                {
                    int first = line.IndexOf(key);
                    int last = line.LastIndexOf(key);
                    if (first is not -1 && first < firstDigit) { number[0] = writtenNumbers[key]; firstDigit = first; }
                    if (last is not -1 && last > lastDigit) { number[1] = writtenNumbers[key]; lastDigit = last; }
                }
                for (int i = 0; i < line.Length; i++)
                {
                    if (char.IsDigit(line[i]))
                    {
                        if (i < firstDigit) { number[0] = line[i]; }
                        break;
                    }
                }
                for (int i = line.Length - 1; i >= 0; i--)
                {
                    if (char.IsDigit(line[i]))
                    {
                        if (i > lastDigit) { number[1] = line[i]; }
                        break;
                    }
                }
                total += int.Parse(number);
            }
            return "" + total;
        }

        public string GetInput()
        {
            return new Day01Input().Input;
        }
    }
}