using AdventOfCode.Inputs.Year2023;
using System;

namespace AdventOfCode.Year2023
{
    public class Day02 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2023/day/2

        public string SolvePart1(string input)
        {
            // parameters
            int total = 0;
            int red = 12, green = 13, blue = 14;

            // parse games
            string[] lines = GetInput().Split(Environment.NewLine);
            for (int i = 0; i < lines.Length; i++)
            {
                // split game part from the rest
                // (Game 1:) (3 blue, 4 red); (1 red, 2 green, 6 blue); (2 green)
                string[] game = lines[i].Split(new char[] { ':', ';' }, StringSplitOptions.RemoveEmptyEntries);
                int gameId = int.Parse((game[0].Split(' '))[1]);

                bool possible = true;
                // parse each round
                for (int j = 1; j < game.Length; j++)
                {
                    //(3 blue, 4 red)
                    string[] parts = game[j].Split(',', StringSplitOptions.RemoveEmptyEntries);
                    foreach (string part in parts)
                    {
                        string[] token = part.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        // (3) (blue)
                        int amount = int.Parse(token[0]);
                        if (token[1] == "red" && amount > red) { possible = false; break; }
                        if (token[1] == "green" && amount > green) { possible = false; break; }
                        if (token[1] == "blue" && amount > blue) { possible = false; break; }
                    }
                    if (possible is false) { break; }
                }

                total += possible ? gameId : 0;
            }

            return "" + total;
        }

        public string SolvePart2(string input)
        {
            // parameters
            int total = 0;

            // parse games
            string[] lines = GetInput().Split(Environment.NewLine);
            for (int i = 0; i < lines.Length; i++)
            {
                // split game part from the rest
                // (Game 1:) (3 blue, 4 red); (1 red, 2 green, 6 blue); (2 green)
                string[] game = lines[i].Split(new char[] { ':', ';' }, StringSplitOptions.RemoveEmptyEntries);
                int gameId = int.Parse((game[0].Split(' '))[1]);
                int red = 0, green = 0, blue = 0;
                bool possible = true;
                // parse each round
                for (int j = 1; j < game.Length; j++)
                {
                    //(3 blue, 4 red)
                    string[] parts = game[j].Split(',', StringSplitOptions.RemoveEmptyEntries);
                    foreach (string part in parts)
                    {
                        string[] token = part.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        // (3) (blue)
                        int amount = int.Parse(token[0]);
                        if (token[1] == "red" && amount > red) { red = amount; }
                        if (token[1] == "green" && amount > green) { green = amount; }
                        if (token[1] == "blue" && amount > blue) { blue = amount; }
                    }
                }

                total += (red * blue * green);
            }

            return "" + total;
        }

        public string GetInput()
        {
            return new Day02Input().Input;
            //return """
            //Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
            //Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
            //Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
            //Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
            //Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green
            //""";
        }
    }
}