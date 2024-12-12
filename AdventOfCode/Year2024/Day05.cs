using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Year2024
{
    public class Day05 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2024/day/5

        public string SolvePart1(string input)
        {
            long result = 0;
            // parse the book
            string[] parts = input.Split(System.Environment.NewLine + System.Environment.NewLine);
            string[] pageRules = parts[0].Split(System.Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            string[] pagesProduced = parts[1].Split(System.Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            // inverse storage => e.g. 47|53, is stored as (53,[47]) => if 53 is found , then 47 cannot be afterwards
            Dictionary<int, HashSet<int>> rules = new();
            foreach (string pageRule in pageRules)
            {
                int[] tokens = Array.ConvertAll(pageRule.Split('|'), int.Parse);
                if (rules.ContainsKey(tokens[1]) is false) { rules.Add(tokens[1], new HashSet<int>()); }
                rules[tokens[1]].Add(tokens[0]);
            }

            // pages!
            foreach (string pageOrder in pagesProduced)
            {
                int[] pages = Array.ConvertAll(pageOrder.Split(',', StringSplitOptions.RemoveEmptyEntries), int.Parse);
                result += IsValidPageOrder(pages, rules) ? pages[(pages.Length / 2)] : 0;
            }
            return "" + result;
        }

        internal bool IsValidPageOrder(int[] pages, Dictionary<int, HashSet<int>> inverseRules)
        {
            bool rightOrder = true;

            for (int i = 0; i < pages.Length && rightOrder; i++)
            {
                if (inverseRules.ContainsKey(pages[i]))
                { // found a page that requires a rules
                    for (int j = i + 1; j < pages.Length; j++)
                    {
                        if (inverseRules[pages[i]].Contains(pages[j]))
                        {
                            rightOrder = false; break;
                        }
                    }
                }
            }
            return rightOrder;
        }

        public string SolvePart2(string input)
        {
            long result = 0;
            // parse the book
            string[] parts = input.Split(System.Environment.NewLine + System.Environment.NewLine);
            string[] pageRules = parts[0].Split(System.Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            string[] pagesProduced = parts[1].Split(System.Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            // inverse storage => e.g. 47|53, is stored as (53,[47]) => if 53 is found , then 47 cannot be after wards
            Dictionary<int, HashSet<int>> rules = new();
            Dictionary<int, HashSet<int>> inverseRules = new();
            HashSet<int> allPages = new();
            foreach (string pageRule in pageRules)
            {
                int[] tokens = Array.ConvertAll(pageRule.Split('|'), int.Parse);
                // store in hashset => we are not interested in any in between number
                allPages.Add(tokens[0]); allPages.Add(tokens[1]);
                if (rules.ContainsKey(tokens[0]) is false) { rules.Add(tokens[0], new HashSet<int>()); }
                if (rules.ContainsKey(tokens[1]) is false) { rules.Add(tokens[1], new HashSet<int>()); }
                rules[tokens[0]].Add(tokens[1]);
                if (inverseRules.ContainsKey(tokens[1]) is false) { inverseRules.Add(tokens[1], new HashSet<int>()); }
                inverseRules[tokens[1]].Add(tokens[0]);
            }

            // pages!
            foreach (string pageOrder in pagesProduced)
            {
                int[] pages = Array.ConvertAll(pageOrder.Split(',', StringSplitOptions.RemoveEmptyEntries), int.Parse);
                if (IsValidPageOrder(pages, inverseRules) is false)
                {
                    // make me the correct order
                    var sp = SortedPages(pages, rules);
                    result += sp[(sp.Length / 2)]; // add middle number
                }
            }
            return "" + result;
        }

        internal int[] SortedPages(int[] thesePages, Dictionary<int, HashSet<int>> rules)
        {
            List<int> sortedPages = new();
            HashSet<int> pagesToBeSorted = new();
            HashSet<int> processedNumbers = new();

            // simple algo, each time add 1 number to the list
            while (sortedPages.Count < thesePages.Length)
            {
                // all pages that will NOT be accepted,
                HashSet<int> nonCandidate = new();
                foreach (int page in thesePages)
                {
                    if (processedNumbers.Contains(page) is false)
                    {
                        foreach (int p in rules[page]) { nonCandidate.Add(p); }
                    }
                }

                // pick the number that is neither processed nor referenced
                for (int i = 0; i < thesePages.Length; i++)
                {
                    int candidate = thesePages[i];
                    if (processedNumbers.Contains(candidate) is false && nonCandidate.Contains(candidate) is false)
                    {
                        sortedPages.Add(candidate);
                        processedNumbers.Add(candidate);
                    }
                }
            }

            return sortedPages.ToArray();
        }

        public string GetInput()
        {
            var file_contents = File.ReadAllText("./input.txt");
            return file_contents;
        }
    }
}