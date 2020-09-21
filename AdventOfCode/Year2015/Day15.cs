using System;

namespace AdventOfCode.Year2015
{
    public class Day15 : IAoC
    {
        // Challenge can be found on https://adventofcode.com/2015/day/15

        public string SolvePart1(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            int[][] ip = new int[lines.Length][]; // ingredient properties

            for (int i = 0; i < lines.Length; i++)
            {
                var parts = lines[i].Split(new char[] { ':', ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                ip[i] = new int[5];
                ip[i][0] = int.Parse(parts[2]); // capacity
                ip[i][1] = int.Parse(parts[4]); // durability
                ip[i][2] = int.Parse(parts[6]); // flavor
                ip[i][3] = int.Parse(parts[8]); // texture
                ip[i][4] = int.Parse(parts[10]); // calories
            }

            // brute force baby!
            int maxScore = int.MinValue;
            for (int i = 0; i <= 100; i++)
            {
                for (int j = 0; j <= 100 && i + j <= 100; j++)
                {
                    for (int k = 0; k <= 100 && i + j + k <= 100; k++)
                    {
                        for (int l = 0; l <= 100 && i + j + k + l <= 100; l++)
                        {
                            //int capacity = i * ip[0][0] + j * ip[1][0] + k * ip[2][0] + l * ip[3][0];
                            //int durability = i * ip[0][1] + j * ip[1][1] + k * ip[2][1] + l * ip[3][1];
                            //int flavor = i * ip[0][2] + j * ip[1][2] + k * ip[2][2] + l * ip[3][2];
                            //int texture = i * ip[0][3] + j * ip[1][3] + k * ip[2][3] + l * ip[3][3];
                            int score =
                                Math.Max(0, (i * ip[0][0] + j * ip[1][0] + k * ip[2][0] + l * ip[3][0]))
                                * Math.Max(0, (i * ip[0][1] + j * ip[1][1] + k * ip[2][1] + l * ip[3][1]))
                                * Math.Max(0, (i * ip[0][2] + j * ip[1][2] + k * ip[2][2] + l * ip[3][2]))
                                * Math.Max(0, (i * ip[0][3] + j * ip[1][3] + k * ip[2][3] + l * ip[3][3]));

                            maxScore = Math.Max(maxScore, score);
                        }
                    }
                }
            }
            return maxScore.ToString();
        }

        public string SolvePart2(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            int[][] ip = new int[lines.Length][]; // ingredient properties

            for (int i = 0; i < lines.Length; i++)
            {
                var parts = lines[i].Split(new char[] { ':', ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                ip[i] = new int[5];
                ip[i][0] = int.Parse(parts[2]); // capacity
                ip[i][1] = int.Parse(parts[4]); // durability
                ip[i][2] = int.Parse(parts[6]); // flavor
                ip[i][3] = int.Parse(parts[8]); // texture
                ip[i][4] = int.Parse(parts[10]); // calories
            }

            // brute force baby!
            int maxScore = int.MinValue;
            for (int i = 0; i <= 100; i++)
            {
                for (int j = 0; j <= 100 && i + j <= 100; j++)
                {
                    for (int k = 0; k <= 100 && i + j + k <= 100; k++)
                    {
                        for (int l = 0; l <= 100 && i + j + k + l <= 100; l++)
                        {
                            //int calories = i * ip[0][4] + j * ip[1][4] + k * ip[2][4] + l * ip[3][4];
                            if ((i * ip[0][4] + j * ip[1][4] + k * ip[2][4] + l * ip[3][4]) != 500) { continue; }

                            int score =
                                Math.Max(0, (i * ip[0][0] + j * ip[1][0] + k * ip[2][0] + l * ip[3][0]))
                                * Math.Max(0, (i * ip[0][1] + j * ip[1][1] + k * ip[2][1] + l * ip[3][1]))
                                * Math.Max(0, (i * ip[0][2] + j * ip[1][2] + k * ip[2][2] + l * ip[3][2]))
                                * Math.Max(0, (i * ip[0][3] + j * ip[1][3] + k * ip[2][3] + l * ip[3][3]));

                            maxScore = Math.Max(maxScore, score);

                            maxScore = Math.Max(maxScore, score);
                        }
                    }
                }
            }
            return maxScore.ToString();
        }

        public string GetInput()
        {
            //return new Inputs.Year2015.Day14().Input;
            return @"Sprinkles: capacity 5, durability -1, flavor 0, texture 0, calories 5
PeanutButter: capacity -1, durability 3, flavor 0, texture 0, calories 1
Frosting: capacity 0, durability -1, flavor 4, texture 0, calories 6
Sugar: capacity -1, durability 0, flavor 0, texture 2, calories 8
";
        }
    }
}