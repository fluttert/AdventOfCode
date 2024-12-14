using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Year2024
{
    public class Day07 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2024/day/7

        public string SolvePart1(string input)
        {
            long result = 0;
            string[] lines = input.Split(Environment.NewLine);
            foreach (string line in lines)
            {
                long[] nums = Array.ConvertAll(line.Split(new char[] { ':', ' ' }, StringSplitOptions.RemoveEmptyEntries), long.Parse);
                long testValue = nums[0];
                long[] equation = nums[1..];
                long lastIndex = equation.Length;
                Stack<(long val, long index)> stack = new();
                stack.Push((nums[1], 0));
                while (stack.Count > 0)
                {
                    var cur = stack.Pop();
                    long nextIndex = cur.index + 1;

                    // EXIT on value found, or skip this when value is bigger then the test value
                    if (nextIndex == lastIndex || cur.val > testValue)
                    {
                        if (cur.val == testValue) { result += testValue; stack = null; break; }
                        continue;
                    }

                    // otherwise add to stack
                    long next1 = (cur.val + equation[nextIndex]);
                    long next2 = (cur.val * equation[nextIndex]);
                    stack.Push((next1, nextIndex));
                    stack.Push((next2, nextIndex));
                }
            }
            return "" + result;
        }

        public string SolvePart2(string input)
        {
            long result = 0;
            string[] lines = input.Split(Environment.NewLine);
            foreach (string line in lines)
            {
                long[] nums = Array.ConvertAll(line.Split(new char[] { ':', ' ' }, StringSplitOptions.RemoveEmptyEntries), long.Parse);
                long testValue = nums[0];
                long[] equation = nums[1..];
                long lastIndex = equation.Length;
                Stack<(long val, long index)> stack = new();
                stack.Push((nums[1], 0));
                while (stack.Count > 0)
                {
                    var cur = stack.Pop();
                    long nextIndex = cur.index + 1;

                    // EXIT on value found, or skip this when value is bigger then the test value
                    if (nextIndex == lastIndex || cur.val > testValue)
                    {
                        if (cur.val == testValue) { result += testValue; stack = null; break; }
                        continue;
                    }

                    // otherwise add to stack
                    long next1 = (cur.val + equation[nextIndex]);
                    long next2 = (cur.val * equation[nextIndex]);
                    long next3 = long.Parse((""+cur.val + equation[nextIndex]));
                    stack.Push((next1, nextIndex));
                    stack.Push((next2, nextIndex));
                    stack.Push((next3, nextIndex));
                }
            }
            return "" + result;
        }

        public string GetInput()
        {
            var file_contents = File.ReadAllText("./input.txt");
            return file_contents;
        }
    }
}