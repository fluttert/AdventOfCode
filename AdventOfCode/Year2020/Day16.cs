using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2020
{
    public class Day16 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2020/day/16

        /// Generic idea for Day 16
        /// Part 1 is all about parsing, I did this quick and dirty
        /// Part 2 actually parsing it the right way, and then matching the right things

        public string SolvePart1(string input)
        {
            // read per line and convert to integer array
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            bool[] allowedNumbers = new bool[1000]; // range 0-999, initally on false
            long ticketScanningErrorRate = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                // ignore own ticket; also ignore the label nearby tickets
                if (lines[i].StartsWith("your ticket")) { i++; continue; }
                if (lines[i].StartsWith("nearby tickets")) { continue; }

                // process allowed range
                if (char.IsLetter(lines[i][0]))
                {
                    var indexSecondHalf = lines[i].IndexOf(':');
                    var ranges = Array.ConvertAll(lines[i][indexSecondHalf..].Split(new char[] { ':', ' ', 'o', 'r', '-' }, StringSplitOptions.RemoveEmptyEntries), int.Parse);
                    int index = 0;
                    while (index < ranges.Length)
                    {
                        for (int j = ranges[index]; j <= ranges[index + 1]; j++) { allowedNumbers[j] = true; }
                        index += 2;
                    }
                }

                // check if ticket is valid
                if (char.IsDigit(lines[i][0]))
                {
                    int[] numbers = Array.ConvertAll(lines[i].Split(','), int.Parse);
                    foreach (int n in numbers)
                    {
                        if (allowedNumbers[n] is false) { ticketScanningErrorRate += n; }
                    }
                }
            }
            return ticketScanningErrorRate.ToString();
        }

        public string SolvePart2(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            var ticketFields = new Dictionary<string, bool[]>();
            var tickets = new List<int[]>();
            bool[] allowedNumbers = new bool[1000]; // range 0-999, initally on false
            int[] myTicket = new int[0];
            for (int i = 0; i < lines.Length; i++)
            {
                // parse my own ticket
                if (lines[i].StartsWith("your ticket"))
                {
                    myTicket = Array.ConvertAll(lines[i + 1].Split(','), int.Parse);
                    tickets.Add(myTicket);
                    i++; continue;
                }
                // ignore this line
                if (lines[i].StartsWith("nearby tickets")) { continue; }

                // process allowed range
                if (char.IsLetter(lines[i][0]))
                {
                    var range = new bool[1000];
                    var indexSecondHalf = lines[i].IndexOf(':');
                    string name = lines[i][0..indexSecondHalf];
                    var ranges = Array.ConvertAll(lines[i][indexSecondHalf..].Split(new char[] { ':', ' ', 'o', 'r', '-' }, StringSplitOptions.RemoveEmptyEntries), int.Parse);
                    int index = 0;
                    while (index < ranges.Length)
                    {
                        for (int j = ranges[index]; j <= ranges[index + 1]; j++)
                        {
                            allowedNumbers[j] = true;
                            range[j] = true;
                        }
                        index += 2;
                    }
                    ticketFields.Add(name, range);
                }

                // add the valid tickets
                if (char.IsDigit(lines[i][0]))
                {
                    int[] numbers = Array.ConvertAll(lines[i].Split(','), int.Parse);
                    bool allowed = true;
                    foreach (int n in numbers)
                    {
                        if (allowedNumbers[n] is false) { allowed = false; }
                    }
                    if (allowed)
                    {
                        tickets.Add(numbers);
                    }
                }
            }   // fininsh reading input

            // now see which field corresponds to what thing
            // the tricky part is that some things are double

            var rangesOfTickets = PivotList(tickets);   // pivot list
            var found = new Dictionary<string, int>();
            var foundIndex = new HashSet<int>();
            while (found.Count < ticketFields.Count)
            {   // check each field continously untill only 1 possibility is confirmed
                foreach (var kv in ticketFields)
                {
                    string name = kv.Key;
                    // skip fields that are already know
                    if (found.ContainsKey(name)) { continue; }

                    // double detection
                    int possibleOptions = 0;
                    int lastFoundIndex = 0;
                    for (int i = 0; i < rangesOfTickets.Count; i++)
                    {
                        // skip ranges that are known
                        if (foundIndex.Contains(i)) { continue; }

                        // else test this range
                        if (FieldToCorrespondingRange(kv.Value, rangesOfTickets[i]))
                        {
                            possibleOptions++;
                            lastFoundIndex = i;
                        }
                    }
                    // only 1 option! record it, never to test again
                    if (possibleOptions == 1)
                    {
                        found.Add(name, lastFoundIndex);
                        foundIndex.Add(lastFoundIndex);
                        Console.WriteLine($"Found Key: {name} for index {lastFoundIndex}");
                    }
                }
            }


            long result = 1;
            foreach (var kv in found) {
                if (kv.Key.StartsWith("departure")) {
                    result *= myTicket[kv.Value];
                }
            }
            return result.ToString();
        }

        public bool FieldToCorrespondingRange(bool[] fieldRange, int[] tickets)
        {
            bool corresponds = true;
            foreach (var ticket in tickets)
            {
                if (fieldRange[ticket] is false) { corresponds = false; break; }
            }
            return corresponds;
        }

        private List<int[]> PivotList(List<int[]> input)
        {
            int inputLength = input.Count;      // length of array
            int rowLength = input[0].Length;    // length of list

            // init output
            var pivoted = new List<int[]>(rowLength);
            for (int i = 0; i < rowLength; i++)
            {
                pivoted.Add(new int[inputLength]);
            }

            // flood fill
            for (int i = 0; i < inputLength; i++)
            {
                for (int j = 0; j < rowLength; j++)
                {
                    pivoted[j][i] = input[i][j];
                }
            }
            return pivoted;
        }

        public string GetInput()
        {
            return new Inputs.Year2020.Day16().Input;
        }
    }
}