using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2015
{
    public class Day07 : IAoC
    {
        // Challenge can be found on https://adventofcode.com/2015/day/7

        public string SolvePart1(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            // A wire has unsigned short aka unsigned 16 bit integer, range 0-65635
            // more types? see https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/integral-numeric-types
            // also the built-in conversion are a must read https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/numeric-conversions

            var wires = new Dictionary<string, ushort>();

            foreach (string line in lines)
            {
                string[] parts = line.Split(new char[] { '-', '>', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                // Assign new values
                if (parts.Length == 2)
                {
                    // if the input comes from a wire, but is not yet set, then ignore this
                    if (!char.IsDigit(parts[0][0]) && !wires.ContainsKey(parts[0])) { continue; }
                    
                    if (!wires.ContainsKey(parts[1])) { wires.Add(parts[1], 0); }
                    ushort value = char.IsDigit(parts[0][0]) ? ushort.Parse(parts[0]) : wires[parts[0]];
                    wires[parts[1]] = value ;
                    continue;
                }

                // operators
                if (parts[0] == "NOT" && wires.ContainsKey(parts[1])) { wires[parts[2]] = bitwiseNot(wires[parts[1]]); continue; }
                if (parts[1] == "AND" && wires.ContainsKey(parts[0]) && wires.ContainsKey(parts[2]))
                {
                    wires[parts[3]] = bitwiseAnd(wires[parts[0]], wires[parts[2]]); continue;
                }
                if (parts[1] == "OR" && wires.ContainsKey(parts[0]) && wires.ContainsKey(parts[2])) { wires[parts[3]] = bitwiseOr(wires[parts[0]], wires[parts[2]]); continue; }
                if (parts[1] == "LSHIFT" && wires.ContainsKey(parts[1])) { wires[parts[3]] = bitwiseLeftShift(wires[parts[0]], int.Parse(parts[2])); continue; }
                if (parts[1] == "RSHIFT" && wires.ContainsKey(parts[1])) { wires[parts[3]] = bitwiseRightshift(wires[parts[0]], int.Parse(parts[2])); continue; }
            }

            return wires["a"].ToString();
        }

        public string SolvePart2(string input)
        {
            return null;
        }

        public ushort bitwiseAnd(ushort val1, ushort val2)
        {
            return (ushort)(val1 & val2);
        }

        public ushort bitwiseOr(ushort val1, ushort val2)
        {
            return (ushort)(val1 ^ val2);
        }

        public ushort bitwiseLeftShift(ushort val, int offset)
        {
            return (ushort)(val << offset);
        }

        public ushort bitwiseRightshift(ushort val, int offset)
        {
            return (ushort)(val >> offset);
        }

        public ushort bitwiseNot(ushort val)
        {
            // NOT operator is only there for int/uint/long/ulong, casting is necessary
            return (ushort)(~val);
        }

        public string GetInput()
        {
            return new Inputs.Year2015.Day07().Input;
        }
    }
}