using System;

namespace AdventOfCode.Year2021
{
    public class Day06 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2021/day/6

        /**
         * Key Insights
         * 1: Do not try to create the string (ever increasing)
         * 2: Only keep information on the days (the amount of fish that are in a particular day, order is not important)
         * 3: For Part 2 the number can grow very big, consider a LONG (64 bit signed integer) or BigInteger
         */

        public string SolvePart1(string input)
        {
            // Convert input to something we can understand
            int[] lanternfishes = Array.ConvertAll(input.Split(',', StringSplitOptions.RemoveEmptyEntries), x => int.Parse(x));
            int[] days = new int[9];
            foreach (int i in lanternfishes)
            {
                days[i]++;
            }

            // simulate
            int daysPassed = 0;
            while (daysPassed < 80)
            {
                int[] updatedDays = new int[9];

                updatedDays[6] = days[0];
                updatedDays[8] = days[0];
                for (int i = 1; i < updatedDays.Length; i++)
                {
                    updatedDays[i - 1] += days[i];
                }
                days = updatedDays;
                daysPassed++;
            }

            // couint total
            int totalFish = 0;
            foreach (int fish in days) { totalFish += fish; }

            return "" + totalFish;
        }

        public string SolvePart2(string input)
        {
            // Convert input to something we can understand
            long[] lanternfishes = Array.ConvertAll(input.Split(',', StringSplitOptions.RemoveEmptyEntries), x => long.Parse(x));
            long[] days = new long[9];
            foreach (int i in lanternfishes)
            {
                days[i]++;
            }

            // simulate
            int daysPassed = 0;
            while (daysPassed < 256)
            {
                long[] updatedDays = new long[9];

                updatedDays[6] = days[0];
                updatedDays[8] = days[0];
                for (int i = 1; i < updatedDays.Length; i++)
                {
                    updatedDays[i - 1] += days[i];
                }
                days = updatedDays;
                daysPassed++;
            }

            // count total
            long totalFish = 0;
            foreach (long fish in days) { totalFish += fish; }

            return "" + totalFish;
        }

        public string GetInput()
        {
            return "2,1,1,4,4,1,3,4,2,4,2,1,1,4,3,5,1,1,5,1,1,5,4,5,4,1,5,1,3,1,4,2,3,2,1,2,5,5,2,3,1,2,3,3,1,4,3,1,1,1,1,5,2,1,1,1,5,3,3,2,1,4,1,1,1,3,1,1,5,5,1,4,4,4,4,5,1,5,1,1,5,5,2,2,5,4,1,5,4,1,4,1,1,1,1,5,3,2,4,1,1,1,4,4,1,2,1,1,5,2,1,1,1,4,4,4,4,3,3,1,1,5,1,5,2,1,4,1,2,4,4,4,4,2,2,2,4,4,4,2,1,5,5,2,1,1,1,4,4,1,4,2,3,3,3,3,3,5,4,1,5,1,4,5,5,1,1,1,4,1,2,4,4,1,2,3,3,3,3,5,1,4,2,5,5,2,1,1,1,1,3,3,1,1,2,3,2,5,4,2,1,1,2,2,2,1,3,1,5,4,1,1,5,3,3,2,2,3,1,1,1,1,2,4,2,2,5,1,2,4,2,1,1,3,2,5,5,3,1,3,3,1,4,1,1,5,5,1,5,4,1,1,1,1,2,3,3,1,2,3,1,5,1,3,1,1,3,1,1,1,1,1,1,5,1,1,5,5,2,1,1,5,2,4,5,5,1,1,5,1,5,5,1,1,3,3,1,1,3,1";
        }
    }
}