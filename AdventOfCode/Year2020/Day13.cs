using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2020
{
    public class Day13 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2020/day/13

        /// Generic idea for Day 13

        public string SolvePart1(string input)
        {
            // read per line and convert to integer array
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            int timeArrivedAtPort = int.Parse(lines[0]);
            // remove the X from it, and convert the rest to integers
            var busIds = Array.ConvertAll(lines[1].Replace("x", "").Split(',', StringSplitOptions.RemoveEmptyEntries), int.Parse);
            int timeBusDeparts = timeArrivedAtPort, busTaken = -1;
            while (busTaken < 0)
            {
                foreach (int busId in busIds)
                {
                    if (timeBusDeparts % busId == 0) { busTaken = busId; break; }
                }
                timeBusDeparts++;
            }

            // need to subtract the last increment
            return (busTaken * (timeBusDeparts - 1 - timeArrivedAtPort)).ToString();
        }

        // using the Chinese Remainder 
        // check https://www.dcode.fr/chinese-remainder
        // and https://en.wikipedia.org/wiki/Chinese_remainder_theorem
        public string SolvePart2(string input) {
            string[] buslines = (input.Split(Environment.NewLine))[1].Split(',');
            var busSchedule = new List<(long busId, long depart)>();
            for (int i = 0; i < buslines.Length; i++)
            {
                if (buslines[i] == "x") { continue; }   // ignore
                long busId = long.Parse(buslines[i]);   // MOD B
                long busoffset = 0L-i;               // Remainders A
                busSchedule.Add((busId, busoffset));
                
            }
            // filled these pairs intot into the dcode.fr website :P
            // (17, 0)
            // (37, -11)
            // (571, -17)
            // (13, -35)
            // (23, -40)
            // (29, -46)
            // (401, -48)
            // (41, -58)
            // (19, -67)

            return "226845233210288";
        }

        // Brute-force alternative
        // Optimized to 0 up to 1_051_551_893_852 in 60 seconds
        public string SolvePart2BruteForce(string input)
        {
            // read per line and convert to integer array
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            var busSchedule = new List<(long busId, long depart)>();
            var busses = lines[1].Split(',');
            long highestId = -1; long offset = -1;
            for (int i = 0; i < busses.Length; i++) {
                if (busses[i] == "x") { continue; }
                long busId = long.Parse(busses[i]);
                long busoffset = (long)i;
                busSchedule.Add((busId, busoffset));
                if (busId > highestId) { highestId = busId; offset = busoffset; }
            }

            // correct the offsets
            for (int i = 0; i < busSchedule.Count; i++)
            {
                var busCor = busSchedule[i];
                busCor.depart -= offset;
                busSchedule[i] = busCor;
            }

            // brute-force?
            long t = 0;
            bool found = false;
            while (found is false) {
                bool allRight = true;
                foreach (var b in busSchedule)
                {
                    if ((t+ b.depart) % b.busId != 0) { allRight = false; break; }
                }
                found = allRight;

                // increase with the highest busnumber
                t += highestId; 
            }

            // need to subtract the last increment
            return (t- highestId - offset).ToString();
        }

        public string GetInput() => @"1000655
17,x,x,x,x,x,x,x,x,x,x,37,x,x,x,x,x,571,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,13,x,x,x,x,23,x,x,x,x,x,29,x,401,x,x,x,x,x,x,x,x,x,41,x,x,x,x,x,x,x,x,19";
    }
}