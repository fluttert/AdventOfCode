using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2016
{
    public class Day10 : IAoC
    {
        // Challenge can be found on https://adventofcode.com/2016/day/10

        public string SolvePart1(string input)
        {
            var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            int compareLow = 17, compareHigh = 61;
            var processed = new HashSet<string>();
            var bots = new Dictionary<int, Bot>();
            int comparerBot = -1;
            while (processed.Count < lines.Length && comparerBot == -1)
            {
                foreach (string line in lines)
                {
                    // case 0: already processed
                    if (processed.Contains(line)) { continue; }

                    // case 1, process value to bot line
                    if (line.StartsWith("value"))
                    {
                        int[] valueToBot = Array.ConvertAll(line.Replace("goes to bot ", "").Substring(5).Trim().Split(' '), int.Parse);
                        if (!bots.ContainsKey(valueToBot[1])) { bots.Add(valueToBot[1], new Bot()); }
                        bots[valueToBot[1]].Add(valueToBot[0]);
                        processed.Add(line);
                        continue;
                    }

                    // case 2: bot distributes to output
                    bool lowToBot = line.IndexOf("low to bot") != -1;
                    bool highToBot = line.IndexOf("high to bot") != -1;

                    // case 3: bot distributes to at least 1 other bot, but hasnt got 2 values
                    string reduceLine = line.Substring(4).Replace(" gives low to ", "").Replace(" and high to ", "").Replace("bot", "").Replace("output", "").Trim();
                    int[] giveChipsToBots = Array.ConvertAll(reduceLine.Split(' '), int.Parse);
                    if (!bots.ContainsKey(giveChipsToBots[0]) || !bots[giveChipsToBots[0]].HasTwoValues)
                    {
                        continue;
                    }

                    // case 4: bot distributes to at least 1 othert bot, and has 2 values
                    Bot curBot = bots[giveChipsToBots[0]];

                    if (lowToBot)
                    {
                        if (!bots.ContainsKey(giveChipsToBots[1])) { bots.Add(giveChipsToBots[1], new Bot()); }
                        bots[giveChipsToBots[1]].Add(curBot.GetLowValue);
                    }
                    if (highToBot)
                    {
                        if (!bots.ContainsKey(giveChipsToBots[2])) { bots.Add(giveChipsToBots[2], new Bot()); }
                        bots[giveChipsToBots[2]].Add(curBot.GetHighValue);
                    }
                    // Are these the bots we are looking for?
                    if (curBot.GetLowValue == compareLow && curBot.GetHighValue == compareHigh)
                    {
                        comparerBot = giveChipsToBots[0];
                        break;
                    }

                    processed.Add(line);
                }
            }

            return comparerBot.ToString();
        }

        public string SolvePart2(string input)
        {
            var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            var processed = new HashSet<string>();
            var bots = new Dictionary<int, Bot>();
            var outputBins = new Dictionary<int, int>();
            int comparerBot = -1;
            while (processed.Count < lines.Length && comparerBot == -1)
            {
                foreach (string line in lines)
                {
                    // case 0: already processed
                    if (processed.Contains(line)) { continue; }

                    // case 1, process value to bot line
                    if (line.StartsWith("value"))
                    {
                        int[] valueToBot = Array.ConvertAll(line.Replace("goes to bot ", "").Substring(5).Trim().Split(' '), int.Parse);
                        if (!bots.ContainsKey(valueToBot[1])) { bots.Add(valueToBot[1], new Bot()); }
                        bots[valueToBot[1]].Add(valueToBot[0]);
                        processed.Add(line);
                        continue;
                    }

                    // case 2: bot distributes to output
                    bool lowToBot = line.IndexOf("low to bot") != -1;
                    bool highToBot = line.IndexOf("high to bot") != -1;

                    // case 3: bot distributes to at least 1 other bot, but hasnt got 2 values
                    string reduceLine = line.Substring(4).Replace(" gives low to ", "").Replace(" and high to ", "").Replace("bot", "").Replace("output", "").Trim();
                    int[] giveChipsToBots = Array.ConvertAll(reduceLine.Split(' '), int.Parse);
                    if (!bots.ContainsKey(giveChipsToBots[0]) || !bots[giveChipsToBots[0]].HasTwoValues)
                    {
                        continue;
                    }

                    // case 4: bot distributes to at least 1 othert bot, and has 2 values
                    Bot curBot = bots[giveChipsToBots[0]];

                    if (lowToBot)
                    {
                        if (!bots.ContainsKey(giveChipsToBots[1])) { bots.Add(giveChipsToBots[1], new Bot()); }
                        bots[giveChipsToBots[1]].Add(curBot.GetLowValue);
                    }
                    else
                    {
                        outputBins.Add(giveChipsToBots[1], curBot.GetLowValue);
                    }

                    if (highToBot)
                    {
                        if (!bots.ContainsKey(giveChipsToBots[2])) { bots.Add(giveChipsToBots[2], new Bot()); }
                        bots[giveChipsToBots[2]].Add(curBot.GetHighValue);
                    }
                    else
                    {
                        outputBins.Add(giveChipsToBots[2], curBot.GetHighValue);
                    }
                    processed.Add(line);
                }
            }

            return (outputBins[0] * outputBins[1] * outputBins[2]).ToString();
        }

        public class Bot
        {
            // internal state
            private int LowValue = -1;

            private int HighValue = -1;

            // simple methods
            public int GetLowValue => LowValue;

            public int GetHighValue => HighValue;
            public bool HasTwoValues => LowValue != -1 && HighValue != -1;

            // add new stuff
            public bool Add(int val)
            {
                if (LowValue != -1)
                {
                    int tmp = LowValue;
                    LowValue = tmp < val ? tmp : val;
                    HighValue = tmp > val ? tmp : val;
                }
                else { LowValue = val; }
                return HasTwoValues;
            }

            public void Clear()
            {
                LowValue = -1;
                HighValue = -1;
            }
        }

        public string GetInput()
        {
            return new Inputs.Year2016.Day10().Input;
        }
    }
}