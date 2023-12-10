using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2023;

public class Day08 : IAoC
{
    // Puzzle can be found on: https://adventofcode.com/2023/day/8
    public string SolvePart1(string input)
    {
        string[] lines = input.Split(Environment.NewLine + Environment.NewLine);    // get input
        string path = lines[0];                                                     // route given eg RLRLRLRLRL
        Dictionary<string, string[]> nodes = ParseTree(lines[1]);                   // create simplified tree with nodes
        int index = 0, pathLength = path.Length;                          // Steps taken
        bool nodeZZZFound = false;                                                  // finished when reached
        string currentNode = "AAA";                                                 // start point
        while (nodeZZZFound is false)
        {
            char leftright = path[index % pathLength];
            currentNode = leftright is 'L' ? nodes[currentNode][0] : nodes[currentNode][1];
            index++;
            if (currentNode is "ZZZ") { nodeZZZFound = true; }
        }
        return "" + index;
    }

    public string SolvePart2(string input)
    {
        string[] lines = input.Split(Environment.NewLine + Environment.NewLine);    // get input
        string path = lines[0];                                                     // route given eg RLRLRLRLRL
        Dictionary<string, string[]> nodes = ParseTree(lines[1]);                   // create simplified tree with nodes
        //List<string> startingNodes = nodes.Keys.Where(p => p.EndsWith("A") || p.EndsWith("B")).ToList();
        List<string> startingNodes = nodes.Keys.Where(p => p.EndsWith("A")).ToList();
        HashSet<string> endNodes = nodes.Keys.Where(p => p.EndsWith("Z")).ToHashSet();
        HashSet<long> cycleLenghts = new();
        List<(int offset, int cycle)> ghostCycles = new();
        Dictionary<string, List<int>> ghostCycles2 = new();

        //Dictionary<string, HashSet<string>> ghostCycles3 = new();
        long smallestCycle = 1;
        for (int i = 0; i < startingNodes.Count; i++)                         // per Ghost
        {
            int index = 0, pathLength = path.Length;                          // Steps taken
            string currentNode = startingNodes[i];                            // start point
            ghostCycles2.Add(startingNodes[i], new());
            //ghostCycles3.Add(startingNodes[i], new());
            int offset = -1;
            int cycle = -1;
            while (index < 50_000)
            {
                char leftright = path[index % pathLength];
                currentNode = leftright is 'L' ? nodes[currentNode][0] : nodes[currentNode][1];
                index++;
                if (endNodes.Contains(currentNode))
                {
                    //ghostCycles3[startingNodes[i]].Add(currentNode);
                    //if (ghostCycles3[startingNodes[i]].Count > 2){
                    //    Console.WriteLine($"Node: {startingNodes[i]} has multiple end nodes being: {ghostCycles3[startingNodes[i]].ToString()}");
                    //}
                    if (offset is -1) { offset = index; }
                    else
                    {
                        cycle = index - offset;
                        cycleLenghts.Add(cycle);
                        //Console.WriteLine($"Node: {startingNodes[i]} - {currentNode} has cycle length: {cycle} , and {offset} offset");
                        break;
                        
                        int cycleLength = index - ghostCycles2[startingNodes[i]].Last();
                        Console.WriteLine($"Node: {startingNodes[i]} - {currentNode} has cycle length: {cycleLength} , and {offset} offset");
                        
                    }
                    //ghostCycles2[startingNodes[i]].Add(index);
                }
            }
            ghostCycles.Add((offset, cycle));
        }
        //var cycleLenghtsArray = cycleLenghts.ToArray();
        //for (int i = 1; i < cycleLenghtsArray.Length; i++)
        //{
        //    smallestCycle = Utils.Utils.LeastCommonMultiple(cycleLenghtsArray[i-1], cycleLenghtsArray[i]);
        //}
        foreach (long cycleLength in cycleLenghts) { 
            smallestCycle = Utils.Utils.LeastCommonMultiple(smallestCycle, cycleLength);
        }
        // 19185263738117
        return "" +smallestCycle;
    }

    public Dictionary<string, string[]> ParseTree(string input)
    {
        Dictionary<string, string[]> tree = new();
        string[] lines = input.Split(Environment.NewLine);
        foreach (string line in lines)
        {
            string[] parts = line.Split(new char[] { ' ', '=', '(', ',', ')' }, StringSplitOptions.RemoveEmptyEntries);
            tree.Add(parts[0], new string[] { parts[1], parts[2] });
        }
        return tree;
    }

    public string GetInput()
    {
        return new Inputs.Year2023.Day08Input().Input;
        return """
LLR

AAA = (BBB, BBB)
BBB = (AAA, ZZZ)
ZZZ = (ZZZ, ZZZ)
""";
    }
}