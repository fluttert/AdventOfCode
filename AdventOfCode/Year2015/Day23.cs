using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace AdventOfCode.Year2015
{
    // Challenge can be found on: https://adventofcode.com/2015/day/23
    public class Day23 : IAoC
    {
        public string SolvePart1(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            var output = Computer(lines,0);

            return output.registerB.ToString();
        }

        public string SolvePart2(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            var output = Computer(lines,1);

            return output.registerB.ToString();
        }


        public (int registerA, int registerB) Computer(string[] instructions, int initA = 0) {
            int regA = initA, regB = 0, index = 0;

            while (index < instructions.Length) {
                string[] instruction = instructions[index].Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

                switch (instruction[0])
                {
                    case "hlf":
                        if (instruction[1] == "a") { regA /= 2; }
                        else { regB /= 2; }
                        index++;
                        break;

                    case "tpl":
                        if (instruction[1] == "a") { regA *= 3; }
                        else { regB *= 3; }
                        index++;
                        break;

                    case "inc":
                        if (instruction[1] == "a") { regA += 1; }
                        else { regB +=1; }
                        index++;
                        break;

                    case "jmp":
                        index+= int.Parse(instruction[1]);
                        break;

                    // jump if even
                    case "jie":
                        bool even = instruction[1] == "a" ? regA % 2 == 0 : regB % 2 == 0;
                        if (even)
                        {
                            index += int.Parse(instruction[2]);
                        }
                        else { index++; }
                        break;

                    // jump if one
                    case "jio":
                        bool one = instruction[1] == "a" ? regA ==1 : regB == 1;
                        if (one)
                        {
                            index += int.Parse(instruction[2]);
                        }
                        else { index++; }
                        break;

                    default:
                        break;
                }


            }

            return (regA, regB);
        
        }

        public string GetInput()
        {
            return new Inputs.Year2015.Day23().Input;
        }
    }
}