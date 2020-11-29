using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2016
{
    public class Day20 : IAoC
    {
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

        public string GetInput() =>  new Inputs.Year2016.Day20().Input;

        private class IpRange

        {
            public long Start;
            public long End;
        }

        public long Part1(string[] input, long maxIp = 4294967295, bool Part1 = true)
        {
            // sorting is the magic step here
            var dict = new List<KeyValuePair<long, long>>();
            foreach (var row in input)
            {
                var curRow = Array.ConvertAll(row.Trim().Split('-'), long.Parse);
                dict.Add(new KeyValuePair<long, long>(curRow[0], curRow[1]));
            }
            dict = dict.OrderBy(e => e.Key).ToList();
            var blacklist = new List<IpRange>();

            // stitch the ranges together
            IpRange iprange = null;
            for (int i = 0; i < input.Length; i++)
            {
                var currentBlacklistRange = dict[i];
                if (iprange == null)
                {
                    iprange = new IpRange();
                    iprange.Start = currentBlacklistRange.Key;
                    iprange.End = currentBlacklistRange.Value;
                    continue;
                }

                // ignore encapsulated ranges
                if (currentBlacklistRange.Key <= iprange.End && currentBlacklistRange.Value <= iprange.End)
                {
                    continue;
                }

                // extend range
                if ((currentBlacklistRange.Key <= iprange.End || (currentBlacklistRange.Key + 1) == iprange.End) && currentBlacklistRange.Value > iprange.End)
                {
                    iprange.End = currentBlacklistRange.Value;
                    continue;
                }

                // else start new range
                blacklist.Add(iprange);
                iprange = new IpRange();
                iprange.Start = currentBlacklistRange.Key;
                iprange.End = currentBlacklistRange.Value;
            }
            blacklist.Add(iprange); // add last

            // loop over the ranges

            long count = 0;
            long first = -1;
            if (blacklist[0].Start > 0)
            {
                count = blacklist[0].Start;
                first = 0;
            }

            for (int i = 1; i < blacklist.Count; i++)
            {
                long gap = blacklist[i].Start - blacklist[i - 1].End - 1;
                if (gap == 0) { continue; } // no gap, just 1 difference, like end on 5 and start 6
                if (first == -1) { first = blacklist[i - 1].End + 1; }
                count += gap;
            }
            var lastBlacklistRange = blacklist[(blacklist.Count() - 1)];
            if (lastBlacklistRange.End < maxIp)
            {
                count += maxIp - lastBlacklistRange.End;
            }

            return Part1 ? first : count;
        }

        public long Part2(string[] input, long maxIp = 4294967295)
        {
            return Part1(input, maxIp, false);
        }
    }
}