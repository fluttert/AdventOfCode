using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2020
{
    public class Day18 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2020/day/18

        /// Generic idea for Day 18
        /// String manipulation for the parentheses
        /// after that pure interpretation
        /// I think this could be much more elegant
        /// perhaps using a stack or such?
        public string SolvePart1(string input)
        {
            // read per line and convert to integer array
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            long sumOfHomework = 0;
            foreach (var line in lines)
            {
                var homework = string.Concat(line.Where(c => !char.IsWhiteSpace(c)));

                while (homework.IndexOf(')') >= 0)
                {
                    int endP = homework.IndexOf(')');
                    int beginP = -1;
                    for (int i = endP; i >= 0; i--)
                    {
                        if (homework[i] == '(') { beginP = i; break; }
                    }
                    long solveParentheses = SolveEquationLeftToRight(homework.Substring(beginP + 1, endP - beginP - 1));
                    string update = homework[0..beginP] + solveParentheses.ToString() + homework[^(homework.Length - endP - 1)..];
                    homework = update;
                }

                sumOfHomework += SolveEquationLeftToRight(homework);
                // no more parentheses, go from left to right
            }
            return sumOfHomework.ToString();
        }

        public long SolveEquationLeftToRight(string equation)
        {
            long total = 0, digit = -1;
            bool add = true;
            var digits = new List<char>();
            for (int i = 0; i < equation.Length; i++)
            {
                // if it's a digit, add the number
                if (char.IsDigit(equation[i])) { digits.Add(equation[i]); continue; }
                // else complete expression
                digit = long.Parse(new string(digits.ToArray()));
                total = add ? total + digit : total * digit;
                digits.Clear();

                if (equation[i] == '+') { add = true; continue; }
                if (equation[i] == '*') { add = false; continue; }
            }
            // once more for the last number
            digit = long.Parse(new string(digits.ToArray()));
            total = add ? total + digit : total * digit;
            return total;
        }

        public string SolvePart2(string input)
        {
            // read per line and convert to integer array
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            long sumOfHomework = 0;
            foreach (var line in lines)
            {
                var homework = string.Concat(line.Where(c => !char.IsWhiteSpace(c)));

                while (homework.IndexOf(')') >= 0)
                {
                    int endP = homework.IndexOf(')');
                    int beginP = -1;
                    for (int i = endP; i >= 0; i--)
                    {
                        if (homework[i] == '(') { beginP = i; break; }
                    }
                    long solveParentheses = SolveEquationAddPrecedence(homework.Substring(beginP + 1, endP - beginP - 1));
                    string update = homework[0..beginP] + solveParentheses.ToString() + homework[^(homework.Length - endP - 1)..];
                    homework = update;
                }

                sumOfHomework += SolveEquationAddPrecedence(homework);
                // no more parentheses, go from left to right
            }
            return sumOfHomework.ToString();
        }

        public long SolveEquationAddPrecedence(string equation)
        {
            var numbers = Array.ConvertAll(equation.Split(new char[] { '+', '*' }), long.Parse).ToList();
            var ops = equation.Where(c => !char.IsDigit(c)).ToList();

            while (ops.IndexOf('+') > -1)
            {
                int index = ops.IndexOf('+');
                long sum = numbers[index] + numbers[index + 1];
                numbers[index + 1] = sum;
                numbers.RemoveAt(index);
                ops.RemoveAt(index);
            }

            // the rest is multiplication
            long total = 1;
            foreach (var number in numbers) { total *= number; }

            return total;
        }

        public string GetInput()
        {
            return new Inputs.Year2020.Day18().Input;
        }
    }
}