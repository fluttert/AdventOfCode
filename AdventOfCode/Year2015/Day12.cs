using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Year2015
{
    public class Day12 : IAoC
    {
        // Challenge can be found on https://adventofcode.com/2015/day/12

        public string SolvePart1(string input)
        {
            // try to solve without parsing to JSON
            int total = 0;
            List<char> possibleNumber = new List<char>();
            for (int i = 0; i < input.Length; i++)
            {
                // detect any valid characters for numbers
                if (input[i] == '-' || (input[i] >= '0' && input[i] <= '9'))
                {
                    possibleNumber.Add(input[i]);
                    continue;
                }

                // nothing to parse
                if (possibleNumber.Count == 0) { continue; }

                // else parse to a number
                total += int.Parse(new string(possibleNumber.ToArray()));

                // clear the chars
                possibleNumber.Clear();
            }
            return total.ToString(); ;
        }

        public string SolvePart2(string input)
        {
            StringBuilder sb = new StringBuilder();

            bool inputFiltered = false;
            while (!inputFiltered)
            {
                int loc = input.IndexOf(":\"red\"");
                if (loc == -1) { inputFiltered = true; break; }

                // search curly braces
                int openingBracket = -1;
                int closingBracket = -1;

                int extra = 0;
                for (int i = loc; i < input.Length; i++)
                {
                    if (input[i] == '{') { extra++; }
                    if (input[i] == '}')
                    {
                        if (extra > 0) { extra--; continue; }
                        closingBracket = i; break;
                    }
                }

                extra = 0;
                for (int i = loc - 1; i >= 0; i--)
                {

                    if (input[i] == '}') { extra++; }
                    if (input[i] == '{') {
                        if (extra > 0) { extra--; continue; }
                        openingBracket = i; break; 
                    }
                }

                sb.Append(input.Substring(0, openingBracket));
                sb.Append(input.Substring(closingBracket + 1));
                input = sb.ToString();
                sb.Clear();
            }
            return SolvePart1(input);
        }

        public string GetInput()
        {
            return new Inputs.Year2015.Day12().Input;
        }
    }
}