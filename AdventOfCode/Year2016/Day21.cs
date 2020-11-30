using System;
using System.Linq;

namespace AdventOfCode.Year2016
{
    public class Day21 : IAoC
    {
        public string SolvePart1(string input)
        {
            var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            return ScramblePasswor(lines).ToString();
        }

        public string SolvePart2(string input)
        {
            var lines1 = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            // reverse lines!
            var lines = lines1.Reverse().ToArray();
            return ScramblePasswor(lines, "fbgdceah").ToString();
        }

        public string GetInput() => new Inputs.Year2016.Day21().Input;

        public string ScramblePasswor(string[] input, string toScramble = "abcdefgh")
        {
            char[] password = toScramble.ToCharArray();
            for (int i = 0; i < input.Length; i++)
            {
                string line = input[i];
                string[] pieces = line.Split(' ');
                if (line.IndexOf("swap position") == 0)
                {
                    int x = int.Parse(pieces[2]);
                    int y = int.Parse(pieces[5]);
                    char tmp = password[x];
                    password[x] = password[y];
                    password[y] = tmp;
                }
                if (line.IndexOf("swap letter") == 0)
                {
                    password = SwapLetter(pieces, password);
                }
                if (line.IndexOf("rotate left") == 0)
                {
                    password = RotateLeft(pieces, password);
                }
                if (line.IndexOf("rotate right") == 0)
                {
                    password = RotateRight(pieces, password);
                }

                if (line.IndexOf("rotate based on position of letter") == 0)
                {
                    password = RotateBasedOnLetter(pieces, password);
                }
                if (line.IndexOf("reverse positions") == 0)
                {
                    password = ReversePosition(pieces, password);
                }
                if (line.IndexOf("move position") == 0)
                {
                    password = MovePosition(pieces, password);
                }
            }
            return new string(password);
        }

        public char[] SwapLetter(string[] pieces, char[] password)
        {
            // swap letter X with letter Y
            char x = pieces[2][0], y = pieces[5][0];
            for (int i = 0; i < password.Length; i++)
            {
                if (password[i] == x) { password[i] = y; continue; }
                if (password[i] == y) { password[i] = x; continue; }
            }
            return password;
        }

        public char[] RotateLeft(string[] pieces, char[] password)
        {
            char[] rotated = new char[password.Length];
            int offset = int.Parse(pieces[2]);
            for (int i = 0; i < password.Length; i++)
            {
                rotated[i] = password[(password.Length + i + offset) % password.Length];
            }
            return rotated;
        }

        public char[] RotateRight(string[] pieces, char[] password)
        {
            int arrLength = password.Length;
            char[] rotated = new char[arrLength];
            int offset = int.Parse(pieces[2]);
            for (int i = 0; i < arrLength; i++)
            {
                rotated[i] = password[(arrLength + i - offset) % arrLength];
            }
            return rotated;
        }

        public char[] RotateBasedOnLetter(string[] pieces, char[] password)
        {
            char x = pieces[6][0];
            int position = 0;
            for (int i = 0; i < password.Length; i++)
            {
                if (password[i] == x) { position = i; break; }
            }
            int offset = position + 1;
            if (position >= 4) { offset++; }

            // now rotate right
            int arrLength = password.Length;
            char[] rotated = new char[arrLength];
            for (int i = 0; i < arrLength; i++)
            {
                rotated[i] = password[((2 * arrLength) + i - offset) % arrLength];
            }
            return rotated;
        }

        public char[] ReversePosition(string[] pieces, char[] password)
        {
            int from = int.Parse(pieces[2]), upTo = int.Parse(pieces[4]);
            char[] output = new string(password).ToCharArray(); // easy copy

            int pointer = from;
            int reversePointer = upTo;
            while (pointer <= upTo)
            {
                output[pointer] = password[reversePointer];
                pointer++;
                reversePointer--;
            }

            return output;
        }

        public char[] MovePosition(string[] pieces, char[] password)
        {
            int x = int.Parse(pieces[2]), y = int.Parse(pieces[5]), index = 0;

            // remove
            char[] tmp = new char[password.Length - 1];
            for (int i = 0; i < password.Length; i++)
            {
                if (i == x) { continue; }
                tmp[index] = password[i];
                index++;
            }

            // add back in on position Y
            char[] output = new char[password.Length];
            index = 0;
            for (int i = 0; i < tmp.Length; i++)
            {
                if (index == y)
                {
                    output[index] = password[x];
                    index++;i--;
                    continue;
                }
                output[index] = tmp[i];
                index++;
            }
            if (y == password.Length - 1) { output[password.Length - 1] = password[x]; }

            return output;
        }

        
    }
}