namespace AdventOfCode.Year2015
{
    public class Day11 : IAoC
    {
        // Challenge can be found on https://adventofcode.com/2015/day/11

        public string SolvePart1(string input)
        {
            char[] letters = input.ToCharArray();

            // increment
            // a = 97, z=122
            while (!PasswordRequirements(letters))
            {
                letters[letters.Length - 1]++;
                // increment
                bool carry = false;
                for (int i = letters.Length - 1; i >= 0; i--)
                {
                    if (carry) { letters[i]++; carry = false; }

                    if (letters[i] > 122)
                    {
                        letters[i] = (char)(letters[i] - 26);
                        carry = true;
                    }
                }
            }

            return new string(letters);
        }

        public bool PasswordRequirements(char[] password)
        {
            bool threeIncreasingLetters = false;
            bool containsIOL = false;
            bool overlappingPairs = false;
            int firstOverlappingPair = -1;

            for (int i = 0; i < password.Length; i++)
            {
                // i = 105 o = 111 l = 108
                if (password[i] == 105 || password[i] == 108 || password[i] == 111) { containsIOL = true; break; }

                if (!overlappingPairs && i >= 1 && password[i] == password[i - 1])
                {
                    // if you have aabb , then firstoverlapping pair = 2, to the next is +2
                    if (firstOverlappingPair == -1) { firstOverlappingPair = i; }
                    if (firstOverlappingPair + 2 <= i) { overlappingPairs = true; }
                }

                if (!threeIncreasingLetters && i >= 2)
                {
                    bool increase1 = password[i - 2] + 1 == password[i - 1];
                    threeIncreasingLetters = increase1 && password[i - 2] + 2 == password[i];
                }
            }

            return threeIncreasingLetters && !containsIOL && overlappingPairs;
        }

        public string SolvePart2(string input)
        {
            var answerPart1 = SolvePart1(input).ToCharArray();
            answerPart1[answerPart1.Length - 1]++;
            return SolvePart1(new string(answerPart1));
        }

        public string GetInput()
        {
            return "vzbxkghb";
        }
    }
}