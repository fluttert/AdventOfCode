#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2015
{
    public class Node
    {
        public string id;
        public string circuit;
        public ushort signal = 0;
        public bool signalReady = false;
        public bool hasParent = false;

        public Node(string id, string circuit)
        {
            this.id = id;
            this.circuit = circuit;
        }
    }

    public class Day07 : IAoC
    {
        // Challenge can be found on https://adventofcode.com/2015/day/7

        public string SolvePart1(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            // A wire has unsigned short aka unsigned 16 bit integer, range 0-65635
            // more types? see https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/integral-numeric-types
            // also the built-in conversion are a must read https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/numeric-conversions

            var wiresValues = new Dictionary<string, ushort>();
            var nodes = new Dictionary<string, Node>();

            // add all wires (nodes)
            foreach (string line in lines)
            {
                // have a key for each node
                string nodeId = (line.Split(new char[] { ' ' })).Last();
                nodes.Add(nodeId, new Node(nodeId, line));
            }

            var stack = new Stack<string>();
            stack.Push("a");
            while (stack.Count > 0)
            {
                Node wire = nodes[stack.Peek()];
                if (wire.signalReady)
                {
                    if (!wiresValues.ContainsKey(wire.id))
                    {
                        wiresValues.Add(wire.id, wire.signal);
                    }
                    stack.Pop();
                    continue;
                }

                // else process the node
                string[] parts = wire.circuit.Split(new char[] { '-', '>', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                // Assign
                if (parts.Length == 2)
                {
                    if (char.IsDigit(parts[0][0]))
                    {
                        wire.signal = ushort.Parse(parts[0]);
                        wire.signalReady = true;
                        continue;
                    }
                    if (wiresValues.ContainsKey(parts[0]))
                    {
                        wire.signal = wiresValues[parts[0]];
                        wire.signalReady = true;
                        continue;
                    }
                    stack.Push(parts[0]);
                    continue;
                }
                // NOT
                if (parts[0] == "NOT")
                {
                    if (wiresValues.ContainsKey(parts[1]))
                    {
                        wire.signal = bitwiseNot(wiresValues[parts[1]]);
                        wire.signalReady = true;
                    }
                    else { stack.Push(parts[1]); }
                    continue;
                }

                // AND
                if (parts[1] == "AND") {
                    if (char.IsDigit(parts[0][0]) && wiresValues.ContainsKey(parts[2])) {
                        wire.signal = bitwiseAnd(ushort.Parse(parts[0]), wiresValues[parts[2]]);
                        wire.signalReady = true;
                        continue;
                    }

                    if (wiresValues.ContainsKey(parts[0]) && wiresValues.ContainsKey(parts[2])) {
                        wire.signal = bitwiseAnd(wiresValues[parts[0]], wiresValues[parts[2]]);
                        wire.signalReady = true;
                    }
                    if (!char.IsDigit(parts[0][0]) && !wiresValues.ContainsKey(parts[0])) { stack.Push(parts[0]); }
                    if (!wiresValues.ContainsKey(parts[2])) { stack.Push(parts[2]); }
                    continue;
                }

                // OR
                if (parts[1] == "OR")
                {
                    if (wiresValues.ContainsKey(parts[0]) && wiresValues.ContainsKey(parts[2]))
                    {
                        wire.signal = bitwiseOr(wiresValues[parts[0]], wiresValues[parts[2]]);
                        wire.signalReady = true;
                    }
                    if (!wiresValues.ContainsKey(parts[0])) { stack.Push(parts[0]); }
                    if (!wiresValues.ContainsKey(parts[2])) { stack.Push(parts[2]); }
                    continue;
                }

                // SHIFT LEFT
                if (parts[1] == "LSHIFT") {
                    if (wiresValues.ContainsKey(parts[0]))
                    {
                        wire.signal = bitwiseLeftShift(wiresValues[parts[0]], int.Parse(parts[2]));
                        wire.signalReady = true;
                    }
                    else { stack.Push(parts[0]); }
                    continue;
                }
                // SHIFT LEFT
                if (parts[1] == "RSHIFT")
                {
                    if (wiresValues.ContainsKey(parts[0]))
                    {
                        wire.signal = bitwiseRightshift(wiresValues[parts[0]], int.Parse(parts[2]));
                        wire.signalReady = true;
                    }
                    else { stack.Push(parts[0]); }
                }
            }
            return wiresValues["a"].ToString();
        }

        public string SolvePart2(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            var wiresValues = new Dictionary<string, ushort>();
            var nodes = new Dictionary<string, Node>();

            // add all wires (nodes)
            foreach (string line in lines)
            {
                // have a key for each node
                string nodeId = (line.Split(new char[] { ' ' })).Last();
                nodes.Add(nodeId, new Node(nodeId, line));
            }

            // add node B
            wiresValues.Add("b", 956);


            var stack = new Stack<string>();
            stack.Push("a");
            while (stack.Count > 0)
            {
                Node wire = nodes[stack.Peek()];
                if (wire.signalReady)
                {
                    if (!wiresValues.ContainsKey(wire.id))
                    {
                        wiresValues.Add(wire.id, wire.signal);
                    }
                    stack.Pop();
                    continue;
                }

                // else process the node
                string[] parts = wire.circuit.Split(new char[] { '-', '>', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                // Assign
                if (parts.Length == 2)
                {
                    if (char.IsDigit(parts[0][0]))
                    {
                        wire.signal = ushort.Parse(parts[0]);
                        wire.signalReady = true;
                        continue;
                    }
                    if (wiresValues.ContainsKey(parts[0]))
                    {
                        wire.signal = wiresValues[parts[0]];
                        wire.signalReady = true;
                        continue;
                    }
                    stack.Push(parts[0]);
                    continue;
                }
                // NOT
                if (parts[0] == "NOT")
                {
                    if (wiresValues.ContainsKey(parts[1]))
                    {
                        wire.signal = bitwiseNot(wiresValues[parts[1]]);
                        wire.signalReady = true;
                    }
                    else { stack.Push(parts[1]); }
                    continue;
                }

                // AND
                if (parts[1] == "AND")
                {
                    if (char.IsDigit(parts[0][0]) && wiresValues.ContainsKey(parts[2]))
                    {
                        wire.signal = bitwiseAnd(ushort.Parse(parts[0]), wiresValues[parts[2]]);
                        wire.signalReady = true;
                        continue;
                    }

                    if (wiresValues.ContainsKey(parts[0]) && wiresValues.ContainsKey(parts[2]))
                    {
                        wire.signal = bitwiseAnd(wiresValues[parts[0]], wiresValues[parts[2]]);
                        wire.signalReady = true;
                    }
                    if (!char.IsDigit(parts[0][0]) && !wiresValues.ContainsKey(parts[0])) { stack.Push(parts[0]); }
                    if (!wiresValues.ContainsKey(parts[2])) { stack.Push(parts[2]); }
                    continue;
                }

                // OR
                if (parts[1] == "OR")
                {
                    if (wiresValues.ContainsKey(parts[0]) && wiresValues.ContainsKey(parts[2]))
                    {
                        wire.signal = bitwiseOr(wiresValues[parts[0]], wiresValues[parts[2]]);
                        wire.signalReady = true;
                    }
                    if (!wiresValues.ContainsKey(parts[0])) { stack.Push(parts[0]); }
                    if (!wiresValues.ContainsKey(parts[2])) { stack.Push(parts[2]); }
                    continue;
                }

                // SHIFT LEFT
                if (parts[1] == "LSHIFT")
                {
                    if (wiresValues.ContainsKey(parts[0]))
                    {
                        wire.signal = bitwiseLeftShift(wiresValues[parts[0]], int.Parse(parts[2]));
                        wire.signalReady = true;
                    }
                    else { stack.Push(parts[0]); }
                    continue;
                }
                // SHIFT LEFT
                if (parts[1] == "RSHIFT")
                {
                    if (wiresValues.ContainsKey(parts[0]))
                    {
                        wire.signal = bitwiseRightshift(wiresValues[parts[0]], int.Parse(parts[2]));
                        wire.signalReady = true;
                    }
                    else { stack.Push(parts[0]); }
                }
            }
            return wiresValues["a"].ToString();
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
            return (ushort)(~val);
        }

        public string GetInput()
        {
            return new Inputs.Year2015.Day07().Input;
        }
    }
}