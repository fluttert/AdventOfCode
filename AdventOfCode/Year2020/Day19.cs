using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2020
{
    public class Day19 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2020/day/19

        /// Generic idea for Day 19
        ///

        public string SolvePart1(string input)
        {
            // read per line
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<int, Rule> rules = new Dictionary<int, Rule>();
            var messages = new List<string>();
            foreach (var line in lines)
            {
                if (char.IsDigit(line[0]))  // rule
                {
                    var pieces = line.Split(':');
                    // parse ID + create node if not already there
                    int id = int.Parse(pieces[0]);
                    if (rules.ContainsKey(id) is false)
                    {
                        rules.Add(id, new Rule(id));
                    }
                    Rule node = rules[id];
                    // parse the line when leaf
                    if (pieces[1].IndexOf('"') > 0)
                    {
                        //var temp = pieces[1].Split(new char[] { ' ', '"' }, StringSplitOptions.RemoveEmptyEntries)[0];
                        node.value = pieces[1].Split(new char[] { ' ', '"' }, StringSplitOptions.RemoveEmptyEntries)[0][0];
                        continue;
                    }
                    // else it contains patterns
                    var patterns = pieces[1].Split('|');
                    for (int i = 0; i < patterns.Length; i++)
                    {
                        var pat = Array.ConvertAll(patterns[i].Split(' ', StringSplitOptions.RemoveEmptyEntries), int.Parse);
                        var ruleList = new List<Rule>();
                        foreach (var number in pat)
                        {
                            if (rules.ContainsKey(number) is false) { rules.Add(number, new Rule(number)); }
                            ruleList.Add(rules[number]);
                        }
                        node.AddPattern(ruleList);
                    }
                }
                else
                {
                    messages.Add(line); // message start with 'a' or 'b'
                }
            }

            int validMessages = 0;
            foreach (var message in messages)
            {
                int checkLength = rules[0].CheckPattern(0, message,0);
                if (checkLength == message.Length) { validMessages++; }
            }
            return "" + validMessages;
        }

        public string SolvePart2(string input)
        {
            return SolvePart2(new Inputs.Year2020.Day19().Input2);
        }

        public string GetInput()
        {
            return new Inputs.Year2020.Day19().Input;
        }
    }

    public class Rule
    {
        public int id;              // rule ID
        public char value;          // value (A or B)
        public List<List<Rule>> patterns = new List<List<Rule>>();

        public bool isLeaf => patterns.Count == 0;

        public Rule(int id)
        {
            this.id = id;
        }

        public bool AddPattern(List<Rule> pattern)
        {
            patterns.Add(pattern);
            return true;
        }

        public int CheckPattern(int index, string pattern, int depth)
        {
            if (this.id == 8 || this.id == 42)
            {
                Console.WriteLine($"Rule: {this.id} index: {index} , depth: {depth} pattern:{pattern}");
                System.Diagnostics.Debug.WriteLine($"Rule: {this.id} index: {index} , depth: {depth} pattern:{pattern}");
            }
            if (index>= pattern.Length || depth>pattern.Length+10) { 
                return -1; 
            } // anti loop

            // leaf check first => return
            if (isLeaf)
            {
                if (pattern[index] == this.value) { return index + 1; }
                else { return -1; }
            }

            // Pattern time!
            int result = -1;
            foreach (var pat in patterns)
            {
                int patIndex = index;

                for (int i = 0; i < pat.Count; i++)
                {
                    int nodeResult = pat[i].CheckPattern(patIndex, pattern, depth+1);
                    patIndex = nodeResult;
                    if (nodeResult == -1) { break; }    // early exit, try next
                }
                // this worked! return result
                result = patIndex;

                // no other patterns needs to be matched on this point
                if (result > 0) { break;  }
                
            }
            return result;  // no match
        }
    }
}