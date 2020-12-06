using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2017
{
    public class Day13 : IAoC
    {
        public string SolvePart1(string input)
        {
            Dictionary<int, int> firewall =
                input.Split(Environment.NewLine)
                .Select(x => Array.ConvertAll(x.Split(new char[] { ' ', ':' }, StringSplitOptions.RemoveEmptyEntries), int.Parse))
                .ToDictionary(x => x[0], x => x[1]);

            int result = 0;
            int firewallEnd = firewall.Keys.Max();
            for (int picosecond = 0; picosecond <= firewallEnd; picosecond++)
            {
                if (!firewall.ContainsKey(picosecond)) { continue; }
                int scannerPosition = picosecond % (2 * firewall[picosecond] - 2);
                result += scannerPosition == 0 ? picosecond * firewall[picosecond] : 0;
            }

            return result.ToString();
        }

        public string SolvePart2(string input)
        {
            Dictionary<int, int> firewall =
                input.Split(Environment.NewLine)
                .Select(x => Array.ConvertAll(x.Split(new char[] { ' ', ':' }, StringSplitOptions.RemoveEmptyEntries), int.Parse))
                .ToDictionary(x => x[0], x => x[1]);

            int delay = 0;
            int firewallLength = firewall.Keys.Max();
            bool caught = true;
            while (caught)
            {
                bool caughtWithThisDelay = false;
                for (int picosecond = 0; picosecond <= firewallLength; picosecond++)
                {
                    if (!firewall.ContainsKey(picosecond)) { continue; }
                    int scannerPosition = (picosecond + delay) % (2 * firewall[picosecond] - 2);
                    if (scannerPosition == 0) { caughtWithThisDelay = true; break; }
                }

                if (!caughtWithThisDelay) { break; }

                delay++;
            }

            return delay.ToString();
        }

        public string GetInput() => @"0: 3
1: 2
2: 5
4: 4
6: 6
8: 4
10: 8
12: 8
14: 6
16: 8
18: 6
20: 6
22: 8
24: 12
26: 12
28: 8
30: 12
32: 12
34: 8
36: 10
38: 9
40: 12
42: 10
44: 12
46: 14
48: 14
50: 12
52: 14
56: 12
58: 12
60: 14
62: 14
64: 12
66: 14
68: 14
70: 14
74: 24
76: 14
80: 18
82: 14
84: 14
90: 14
94: 17";
    }
}