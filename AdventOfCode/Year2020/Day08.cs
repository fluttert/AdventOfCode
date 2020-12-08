using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2020
{
    public class Day08 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2020/day/8

        /// Generic idea for Day 8
        ///

        public string SolvePart1(string input)
        {
            // read per line
            string[] lines = input.Split(Environment.NewLine);
            var (accumulator, infiniteLoop) = RunBootCode(lines);
            return accumulator.ToString();
        }

        public string SolvePart2(string input)
        {
            // read per line
            string[] lines = input.Split(Environment.NewLine);
            long result = -1;
            // let's change instructions!
            for (int i = 0; i < lines.Length; i++)
            {
                string instr = lines[i][0..3];

                //skip if the instruction is not nop or jmp
                if (instr != "nop" && instr != "jmp") { continue; }

                // copy the lines & alter the instruction
                var alteredInput = Utils.Utils.Duplicate(lines);
                if (instr == "nop") { alteredInput[i] = lines[i].Replace("nop", "jmp"); }
                if (instr == "jmp") { alteredInput[i] = lines[i].Replace("jmp", "nop"); }

                // determine the desired result (not an infinite loop)
                var alteredResult = RunBootCode(alteredInput);
                if (alteredResult.infiniteLoop is false)
                {
                    result = alteredResult.accumulator;
                    break;
                }
            }

            return result.ToString();
        }

        /// <summary>
        /// Runs the BootCode of the Handheld Console, which is crashing due to an infinite loop
        /// </summary>
        /// <param name="lines">The lines of instructions to process</param>
        /// <returns>Tuple with the accumlated result, and indication if there was an infinite loop</returns>
        public (long accumulator, bool infiniteLoop) RunBootCode(string[] lines)
        {
            long accumulator = 0;                           // the accumulator (a result)
            int instruction = 0;                            // instruction index
            var instructionsVisited = new HashSet<int>();   // remember if this instruction was executed before
            bool infiniteLoop = true;                       // assume there is an infinite loop
            while (true)
            {
                // infinite loop detection (super advanced)
                if (instruction >= lines.Length) { infiniteLoop = false; break; }
                
                if (instructionsVisited.Contains(instruction)) { break; } // have been here before, EXIT!
                instructionsVisited.Add(instruction);               // add it to our history
                string instr = lines[instruction][0..3];            // get first 3 characters
                int number = int.Parse(lines[instruction][4..]);    // parse the number

                // process instruction
                switch (instr)
                {
                    case "nop": instruction++; break;
                    case "acc": accumulator += number; instruction++; break;
                    case "jmp": instruction += number; break;

                    default:
                        Console.WriteLine(@"This instruction {instr} doesnt compute");
                        break;
                }
            }
            return (accumulator, infiniteLoop);
        }

        public string GetInput()
        {
            return new Inputs.Year2020.Day08().Input;
        }
    }
}