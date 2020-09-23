using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2015
{
    public class Day16 : IAoC
    {
        // Challenge can be found on https://adventofcode.com/2015/day/16

        public string SolvePart1(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            var theSue = Part1Sue();
            int answer = -1;

            foreach (string line in lines)
            {
                var parts = line.Split(new char[] { ':', ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                int SueNumber = int.Parse(parts[1]);

                string prop1 = parts[2];
                int val1 = int.Parse(parts[3]);
                string prop2 = parts[4];
                int val2 = int.Parse(parts[5]);
                string prop3 = parts[6];
                int val3 = int.Parse(parts[7]);

                if (theSue[prop1] == val1)
                {
                    if (theSue[prop2] == val2)
                    {
                        if (theSue[prop3] == val3)
                        {
                            answer = SueNumber;
                            break;
                        }
                    }
                }
            }

            return answer.ToString();
        }

        public string SolvePart2(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            var theSue = Part1Sue();
            int answer = -1;

            foreach (string line in lines)
            {
                var parts = line.Split(new char[] { ':', ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                int sueNumber = int.Parse(parts[1]);
                var sueProps = new Dictionary<string, int>();
                sueProps.Add(parts[2], int.Parse(parts[3]));
                sueProps.Add(parts[4], int.Parse(parts[5]));
                sueProps.Add(parts[6], int.Parse(parts[7]));

                int matches = 0;
                foreach (var pair in sueProps)
                {
                    string prop = pair.Key;
                    if ((prop == "cats" || prop == "trees"))
                    {
                        if (theSue[prop] < pair.Value) { matches++; }
                        continue;
                    }
                    if ((prop == "pomeranians" || prop == "goldfish"))
                    {
                        if (theSue[prop] > pair.Value) { matches++; }

                        continue;
                    }
                    if (theSue[prop] == pair.Value)
                    {
                        matches++;
                    }
                }
                if (matches == 3)
                {
                    answer = sueNumber;
                    break;
                }
            }

            return answer.ToString();
        }

        private Dictionary<string, int> Part1Sue()
        {
            string input = @"children: 3
cats: 7
samoyeds: 2
pomeranians: 3
akitas: 0
vizslas: 0
goldfish: 5
trees: 3
cars: 2
perfumes: 1";
            var dict = new Dictionary<string, int>();
            var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                var parts = line.Split(new char[] { ':', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                dict.Add(parts[0], int.Parse(parts[1]));
            }
            return dict;
        }

        public string GetInput()
        {
            return new Inputs.Year2015.Day16().Input;
        }
    }
}