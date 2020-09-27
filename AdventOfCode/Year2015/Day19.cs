using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2015
{
    // Challenge can be found on: https://adventofcode.com/2015/day/19
    public class Day19 : IAoC
    {
        public string SolvePart1(string input)
        {
            var inputs = ProcessInputPart1(input);
            var distinctMolecules = new HashSet<string>();

            foreach (var (from, to) in inputs.replacements)
            {
                int length = from.Length;
                int lastIndexOf = 0; // if not found this will turn to -1
                while (lastIndexOf >= 0)
                {
                    lastIndexOf = inputs.input.IndexOf(from, lastIndexOf);
                    if (lastIndexOf == -1) { continue; } // none found

                    // construct new molecule
                    //string firstPart = inputs.input.Substring(0, lastIndexOf);
                    //string newPart = replacement.to;
                    //string lastPart = inputs.input.Substring(lastIndexOf + length);

                    string molecule = inputs.input.Substring(0, lastIndexOf) + to + inputs.input.Substring(lastIndexOf + length);
                    distinctMolecules.Add(molecule);

                    lastIndexOf++; // move to next character/
                }
            }

            return distinctMolecules.Count.ToString();
        }

        public string SolvePart2(string input)
        {
            // prep input
            var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            var reducements = new Dictionary<string, string>(); // this is reducing the molecule
            var beginStates = new HashSet<string>();            // if it reaches this state, it's done
            var start = lines[^1];                              // starting molecule
            for (int i = 0; i < lines.Length - 1; i++)
            {
                var parts = lines[i].Split(new char[] { ' ', '=', '>' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts[0] == "e") { beginStates.Add(parts[1]); }
                else { reducements.Add(parts[1], parts[0]); }
            }

            // start reducing
            var uniqueMolecules = new HashSet<string>();        // backtrack if you already processed it
            var queue = new Queue<(string molecule, int steps)>();
            int minimumSteps = int.MaxValue;
            queue.Enqueue((start, 0));
            while (queue.Count > 0)
            {
                var mol = queue.Dequeue();
                if (beginStates.Contains(mol.molecule)) { minimumSteps = mol.steps + 1; break; }
                int steps = mol.steps;
                string updatedMolecule = mol.molecule;
                foreach (var reduce in reducements)
                {
                    // each pass over the molecule will do all reduce possibilities in order
                    // no intermediate state is saved

                    int length = reduce.Key.Length;
                    int lastIndexOf = 0; // if not found this will turn to -1
                    while (lastIndexOf >= 0)
                    {
                        lastIndexOf = updatedMolecule.IndexOf(reduce.Key, lastIndexOf);
                        if (lastIndexOf == -1) { continue; } // none found

                        // construct reduced molecule
                        string molecule = updatedMolecule.Substring(0, lastIndexOf) + reduce.Value + updatedMolecule.Substring(lastIndexOf + length);
                        updatedMolecule = molecule;

                        steps++;
                        lastIndexOf++; // move to next character/
                    }
                }
                if (!uniqueMolecules.Contains(updatedMolecule))
                {
                    uniqueMolecules.Add(updatedMolecule);
                    queue.Enqueue((updatedMolecule, steps));
                }
            }

            return minimumSteps.ToString();
        }

        // This bruteforce did not finish within 10 minutes (and ate 32 GB)
        public string SolvePart2BruteForce(string input)
        {
            var (replacements, startingBlocks, result) = ProcessInputPart2(input);

            var uniqueMolecules = new HashSet<string>();
            var queue = new Queue<(string molecule, int steps)>();
            foreach (string start in startingBlocks)
            {
                queue.Enqueue((start, 1));
            }
            int stepsForMolecule = int.MaxValue;
            while (queue.Count > 0)
            {
                var mol = queue.Dequeue();

                // a molecule will only get longer, don't bother processing molecules that are same length
                if (mol.molecule.Length >= result.Length) { continue; }

                foreach (var (from, to) in replacements)
                {
                    int length = from.Length;
                    int lastIndexOf = 0; // if not found this will turn to -1
                    while (lastIndexOf >= 0)
                    {
                        lastIndexOf = mol.molecule.IndexOf(from, lastIndexOf);
                        if (lastIndexOf == -1) { continue; } // none found

                        string molecule = mol.molecule.Substring(0, lastIndexOf) + to + mol.molecule.Substring(lastIndexOf + length);

                        if (molecule == result)
                        {
                            stepsForMolecule = mol.steps;
                            break;
                        }

                        //distinctMolecules.Add(molecule);
                        if (!uniqueMolecules.Contains(molecule))
                        {
                            queue.Enqueue((molecule, mol.steps + 1));
                            uniqueMolecules.Add(molecule);
                        }

                        lastIndexOf++; // move to next character/
                    }
                }
            }
            return stepsForMolecule.ToString();
        }

        internal (List<(string from, string to)> replacements, string input) ProcessInputPart1(string input)
        {
            var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            var replacements = new List<(string from, string to)>();
            for (int i = 0; i < lines.Length - 1; i++)
            {
                var parts = lines[i].Split(new char[] { ' ', '=', '>' }, StringSplitOptions.RemoveEmptyEntries);
                replacements.Add((from: parts[0], to: parts[1]));
            }
            return (replacements, lines[^1]);
        }

        internal (List<(string from, string to)> replacements, List<string> startingBlocks, string result) ProcessInputPart2(string input)
        {
            var firstPass = ProcessInputPart1(input);
            var replacements = new List<(string from, string to)>();
            var result = firstPass.input;
            var startingBlocks = new List<string>();
            foreach (var replacement in firstPass.replacements)
            {
                if (replacement.from == "e")
                {
                    startingBlocks.Add(replacement.to);
                }
                else
                {
                    replacements.Add(replacement);
                }
            }

            return (replacements, startingBlocks, result);
        }

        public string GetInput()
        {
            return new Inputs.Year2015.Day19().Input;
        }
    }
}