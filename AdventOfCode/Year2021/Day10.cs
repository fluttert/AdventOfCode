using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2021
{
    public class Day10 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2021/day/10

        /**
         * Key Insights
         * 1: Pattern matching, use a STACK! push & pop
         * 2: Use LONG (int64) for part 2
         *
         */

        public string SolvePart1(string input)
        {
            // put this in numbers please
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            int sumOfErrorCodes = 0;
            foreach (string line in lines)
            {
                var stack = new Stack<char>();
                for (int i = 0; i < line.Length; i++)
                {
                    char c = line[i];
                    // case 1: opening chunk == always allowed -> push to stack
                    if (c == '(' || c == '[' || c == '{' || c == '<') {
                        stack.Push(c);
                        continue;
                    }
                    
                    // case 2: ending chunk correct -> pop the opening tag from the stack
                    char p = stack.Peek(); // see what the last char was
                    if (c == ')' && p == '(') { stack.Pop(); continue; }
                    if (c == ']' && p == '[') { stack.Pop(); continue; }
                    if (c == '}' && p == '{') { stack.Pop(); continue; }
                    if (c == '>' && p == '<') { stack.Pop(); continue; }

                    // case 3 corruption! Mismatch!
                    if (c == ')' && p != '(') { sumOfErrorCodes += 3;  break; }
                    if (c == ']' && p != '[') { sumOfErrorCodes += 57; break; }
                    if (c == '}' && p != '{') { sumOfErrorCodes +=1197; break; }
                    if (c == '>' && p != '<') { sumOfErrorCodes += 25137; break; }
                }
            }

            return "" + sumOfErrorCodes;
        }

        public string SolvePart2(string input)
        {
            // put this in numbers please
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

;           List<long> completionChallenge = new List<long>();
            foreach (string line in lines)
            {
                var stack = new Stack<char>();
                bool corrupted = false;
                for (int i = 0; i < line.Length; i++)
                {
                    char c = line[i];
                    // case 1: opening chunk == always allowed
                    if (c == '(' || c == '[' || c == '{' || c == '<')
                    {
                        stack.Push(c);
                        continue;
                    }
                    // case 2: ending chunk correct
                    char p = stack.Peek(); // see what the last char was
                    if (c == ')' && p == '(') { stack.Pop(); continue; }
                    if (c == ']' && p == '[') { stack.Pop(); continue; }
                    if (c == '}' && p == '{') { stack.Pop(); continue; }
                    if (c == '>' && p == '<') { stack.Pop(); continue; }

                    // case 3 corruption!
                    if (c == ')' && p != '(') { corrupted = true; break; }
                    if (c == ']' && p != '[') { corrupted = true; break; }
                    if (c == '}' && p != '{') { corrupted = true; break; }
                    if (c == '>' && p != '<') { corrupted = true; break; }
                }

                // part 2 = unfished lines, should be completed
                if (corrupted is false && stack.Count > 0) {
                    long sum = 0;
                    while (stack.Count > 0) {
                        char p = stack.Pop(); // see what the last char was
                        sum *= 5; // sum = sum*5
                        if (p == '(') { sum += 1; continue; }
                        if (p == '[') { sum += 2; continue; }
                        if (p == '{') { sum += 3; continue; }
                        if (p == '<') { sum += 4; continue; }
                    }
                    completionChallenge.Add(sum);
                
                }
            }

            completionChallenge.Sort();
            // integer will round down (eg 1.5 => 1), and this is what we need for a 0-based array
            // example array of 5 (elements 0 - 4); The middle = 5/2 (2,5, rounded down to 2) = 2
            int middleElement = completionChallenge.Count / 2; 

            return "" + completionChallenge[middleElement];
        }

        public string GetInput()
        {
            return new Inputs.Year2021.Day10().Input;
        }
    }
}