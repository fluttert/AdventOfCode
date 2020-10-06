using System;

namespace AdventOfCode.Year2016
{
    public class Day08 : IAoC
    {
        // Challenge can be found on https://adventofcode.com/2016/day/8

        public string SolvePart1(string input)
        {
            var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            return Keypad(lines).ToString();
        }

        // the output is actually readible in the screen :) no coding required
        public string SolvePart2(string input)
        {
            var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            Keypad(lines);
            PrintScreenToConsole(screen);
            return string.Empty;
        }

        public char[][] screen;

        public int Keypad(string[] input, int displayWidth = 50, int displayHeight = 6)
        {
            // init screen
            screen = new char[displayHeight][];
            for (int i = 0; i < displayHeight; i++)
            {
                screen[i] = new string('.', displayWidth).ToCharArray();
            }

            // process input
            foreach (string line in input)
            {
                if (line.Trim().IndexOf("rect") == 0)
                {
                    screen = LitRectangle(line, screen);
                }
                if (line.Trim().IndexOf("rotate column") == 0)
                {
                    screen = RotateColumn(line, screen);
                }
                if (line.Trim().IndexOf("rotate row") == 0)
                {
                    screen = RotateRow(line, screen);
                }
            }
            return PixelsLitOnScreen(screen);
        }

        private int PixelsLitOnScreen(char[][] screen)
        {
            int pixelsLit = 0;
            for (int i = 0; i < screen.Length; i++)
            {
                for (int j = 0; j < screen[0].Length; j++)
                {
                    if (screen[i][j] == '#') { pixelsLit++; }
                }
            }
            return pixelsLit;
        }

        private char[][] RotateRow(string line, char[][] screen)
        {
            var input = line.Trim().Split(new char[] { ' ', '=' });
            int row = int.Parse(input[3]);
            int rotations = int.Parse(input[5]);
            char[] origValues = new char[screen[0].Length];
            Array.Copy(screen[row], origValues, screen[0].Length);
            for (int i = 0; i < screen[0].Length; i++)
            {
                screen[row][(rotations + i) % screen[0].Length] = origValues[i];
            }
            return screen;
        }

        private char[][] RotateColumn(string line, char[][] screen)
        {
            var input = line.Trim().Split(new char[] { ' ', '=' });
            int column = int.Parse(input[3]);
            int rotations = int.Parse(input[5]);
            char[] origValues = new char[screen.Length];
            for (int i = 0; i < screen.Length; i++)
            {
                origValues[i] = screen[i][column];
            }
            for (int i = 0; i < screen.Length; i++)
            {
                screen[(rotations + i) % screen.Length][column] = origValues[i];
            }
            return screen;
        }

        private char[][] LitRectangle(string line, char[][] screen)
        {
            var input = line.Trim().Split(new char[] { ' ', 'x' });
            int width = int.Parse(input[1]);
            int height = int.Parse(input[2]);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    screen[i][j] = '#';
                }
            }
            return screen;
        }

        public string[] PrintScreenToConsole(char[][] screen)
        {
            string[] output = new string[screen.Length];
            for (int i = 0; i < screen.Length; i++)
            {
                output[i] = new string(screen[i]);
                System.Diagnostics.Debug.WriteLine(output[i]);
                Console.WriteLine(output[i]);
            }
            return output;
        }

        public string GetInput()
        {
            return new Inputs.Year2016.Day08().Input;
        }
    }
}