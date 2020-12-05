using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2020
{
    public class Day04 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2020/day/4

        /// Generic idea for Day 4
        /// It's all about correct parsing and reading carefully

        public string SolvePart1(string input)
        {
            // read per line, and do NOT remove empty entries (e.g. empty lines)
            string[] lines = input.Split(Environment.NewLine);

            // Have an boolean array per passport, to check the required fields (and ignore everything else)
            // byr(Birth Year)       = 0
            // iyr(Issue Year)       = 1
            // eyr(Expiration Year)  = 2
            // hgt(Height)           = 3
            // hcl(Hair Color)       = 4
            // ecl(Eye Color)        = 5
            // pid(Passport ID)      = 6
            // cid(Country ID) -> optional, so ignored
            // any other field will be ignored

            bool[] passport = new bool[7]; // will be initialized as false (to all)
            int totalValidPassports = 0;         
            for (int i = 0; i < lines.Length; i++)
            {
                // reset the passport at every empty line
                if (lines[i] == "")
                {
                    passport = new bool[7];
                }

                // else parse the pieces, and check the field-labels (which is in this case all the even pieces)
                string[] pieces = lines[i].Split(new char[] { ' ', ':' }, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < pieces.Length; j += 2)
                {
                    if (pieces[j] == "byr") { passport[0] = true; }
                    if (pieces[j] == "iyr") { passport[1] = true; }
                    if (pieces[j] == "eyr") { passport[2] = true; }
                    if (pieces[j] == "hgt") { passport[3] = true; }
                    if (pieces[j] == "hcl") { passport[4] = true; }
                    if (pieces[j] == "ecl") { passport[5] = true; }
                    if (pieces[j] == "pid") { passport[6] = true; }
                }

                // check if we have a valid passport!
                bool validPass = true;
                for (int j = 0; j < passport.Length; j++)
                {
                    // not complete? then continue
                    if (!passport[j]) { validPass = false; break; }
                }

                // have a valid passport (all 7 required fields)
                if (validPass)
                {
                    totalValidPassports++;
                    // and ofc reset the passport, not to count something double 
                    passport = new bool[7]; 
                }
            }

            return totalValidPassports.ToString();
        }

        public string SolvePart2(string input)
        {
            // read per line
            string[] lines = input.Split(Environment.NewLine);

            // Have an boolean array per passport, to check the required fields (and ignore everything else)
            // byr(Birth Year)       = 0 - four digits; at least 1920 and at most 2002
            // iyr(Issue Year)       = 1 - four digits; at least 2010 and at most 2020
            // eyr(Expiration Year)  = 2 - four digits; at least 2020 and at most 2030
            // hgt(Height)           = 3 - a number followed by either cm or in: If cm, the number must be at least 150 and at most 193. If in, the number must be at least 59 and at most 76
            // hcl(Hair Color)       = 4 - a # followed by exactly six characters 0-9 or a-f
            // ecl(Eye Color)        = 5 - amb blu brn gry grn hzl oth
            // pid(Passport ID)      = 6 - a nine-digit number, including leading zeroes
            // cid(Country ID) -> optional
            // ignore everything else

            bool[] passport = new bool[7];
            int totalValidPassports = 0;
            var eyeColors = new HashSet<string>() { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

            for (int i = 0; i < lines.Length; i++)
            {
                // reset on newline
                if (lines[i] == "")
                {
                    passport = new bool[7];
                }

                // else parse the pieces, and check the EVEN pieces
                string[] pieces = lines[i].Split(new char[] { ' ', ':' }, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < pieces.Length; j += 2)
                {
                    if (pieces[j] == "byr") { int year = int.Parse(pieces[j + 1]); if (1920 <= year && year <= 2002) { passport[0] = true; } }
                    if (pieces[j] == "iyr") { int issueYear = int.Parse(pieces[j + 1]); if (2010 <= issueYear && issueYear <= 2020) { passport[1] = true; } }
                    if (pieces[j] == "eyr") { int expireYear = int.Parse(pieces[j + 1]); if (2020 <= expireYear && expireYear <= 2030) { passport[2] = true; } }
                    if (pieces[j] == "hgt") { if (ValidHeight(pieces[j + 1])) { passport[3] = true; } }
                    if (pieces[j] == "hcl") { if (ValidHairColor(pieces[j + 1])) { passport[4] = true; } }
                    if (pieces[j] == "ecl") { if (eyeColors.Contains(pieces[j + 1])) { passport[5] = true; } }
                    if (pieces[j] == "pid") { if (ValidPassportNumber(pieces[j + 1])) { passport[6] = true; } }
                }

                // check if we have a valid passport!
                bool validPass = true;
                for (int j = 0; j < passport.Length; j++)
                {
                    if (!passport[j]) { validPass = false; break; }
                }
                if (validPass)
                {
                    totalValidPassports++;
                    passport = new bool[7];
                }
            }

            return totalValidPassports.ToString();
        }

        public bool ValidHeight(string input)
        {
            // a number followed by either cm or in, eg 164cm or 60in
            // If cm, the number must be at least 150 and at most 193. 
            // If in, the number must be at least 59 and at most 76
            bool validHeight = false;
            if (input.Length > 2)
            {
                string last2chars = input[^2..]; // the shiny new RANGE operator
                int number = int.Parse(input[0..^2]);
                if (last2chars == "cm" && 150 <= number && number <= 193) { validHeight = true; }
                if (last2chars == "in" && 59 <= number && number <= 76) { validHeight = true; }
            }
            return validHeight;
        }

        public bool ValidPassportNumber(string input)
        {
            //a nine-digit number, including leading zeroes
            bool lengthNine = input.Length == 9;
            bool allDigits = true;
            for (int i = 0; i < input.Length; i++)
            {
                // if there is a non-digit in there, this is not a valid number
                if (!char.IsDigit(input[i])) { allDigits = false; break; }
            }
            return lengthNine && allDigits;
        }

        public bool ValidHairColor(string input)
        {
            //a # followed by exactly six characters 0-9 or a-f

            var validChars = new HashSet<char>() { 'a', 'b', 'c', 'd', 'e', 'f', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            bool lengthSeven = input.Length == 7;
            bool hexadecimalCode = true; ;

            for (int i = 1; i < input.Length; i++)
            {
                // if we detect a character which is NOT a digit or a-f, then this is not a valid haircolor.
                if (!validChars.Contains(input[i])) { hexadecimalCode = false; break; }
            }

            return lengthSeven && hexadecimalCode;
        }

        public string GetInput()
        {
            return new Inputs.Year2020.Day04().Input;
        }
    }
}