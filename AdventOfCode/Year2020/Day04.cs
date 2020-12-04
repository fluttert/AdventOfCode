using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2020
{
    public class Day04 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2020/day/4

        public string SolvePart1(string input)
        {
            // read per line
            string[] lines = input.Split(Environment.NewLine);

            //byr(Birth Year)       = 0
            //iyr(Issue Year)       = 1
            //eyr(Expiration Year)  = 2
            //hgt(Height)           = 3
            //hcl(Hair Color)       = 4
            //ecl(Eye Color)        = 5 
            //pid(Passport ID)      = 6
            //cid(Country ID) -> optional

            bool[] passport= new bool[7];
            int validPassports = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                // reset on newline
                if(lines[i] == ""){
                    passport = new bool[7];
                }

                // else parse the pieces, and check the EVEN pieces
                string[] pieces = lines[i].Split(new char[] { ' ', ':' }, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < pieces.Length; j+=2)
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
                    if (!passport[j]) { validPass = false; break; }
                }
                if (validPass) { 
                    validPassports++;
                    passport = new bool[7];
                }
            }

            return validPassports.ToString();
        }

        public string SolvePart2(string input)
        {
            // read per line
            string[] lines = input.Split(Environment.NewLine);

            //byr(Birth Year)       = 0 - four digits; at least 1920 and at most 2002
            //iyr(Issue Year)       = 1 - four digits; at least 2010 and at most 2020
            //eyr(Expiration Year)  = 2 - four digits; at least 2020 and at most 2030
            //hgt(Height)           = 3 - a number followed by either cm or in: If cm, the number must be at least 150 and at most 193. If in, the number must be at least 59 and at most 76
            //hcl(Hair Color)       = 4 - a # followed by exactly six characters 0-9 or a-f
            //ecl(Eye Color)        = 5 - amb blu brn gry grn hzl oth
            //pid(Passport ID)      = 6 - a nine-digit number, including leading zeroes
            //cid(Country ID) -> optional

            bool[] passport = new bool[7];
            int validPassports = 0;
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
                    if (pieces[j] == "hgt") { if(validHeight(pieces[j+1])){ passport[3] = true; } }
                    if (pieces[j] == "hcl") { if ( validHairColor(pieces[j+1]) ) { passport[4] = true; } }
                    if (pieces[j] == "ecl") { if ( eyeColors.Contains(pieces[j+1]) ) { passport[5] = true; } }
                    if (pieces[j] == "pid") { if ( validPassportNumber(pieces[j+1])) { passport[6] = true; } }
                }

                // check if we have a valid passport!
                bool validPass = true;
                for (int j = 0; j < passport.Length; j++)
                {
                    if (!passport[j]) { validPass = false; break; }
                }
                if (validPass)
                {
                    validPassports++;
                    passport = new bool[7];
                }
            }

            return validPassports.ToString();
        }

        public bool validHeight(string input) {
            // a number followed by either cm or in: If cm, the number must be at least 150 and at most 193. If in, the number must be at least 59 and at most 76
            bool validHeight = false;
            if (input.Length > 2) {
                string last2chars = input[^2..]; // the shiny new RANGE operator
                int number = int.Parse(input[0..^2]);
                if (last2chars == "cm" && 150 <= number && number <= 193) { validHeight = true; }
                if (last2chars == "in" && 59 <= number && number <= 76) { validHeight = true; }
            }
            return validHeight;
        }

        public bool validPassportNumber(string input)
        {
            //a nine-digit number, including leading zeroes
            bool lengthNine = input.Length == 9;
            bool allDigits = true;
            for (int i = 0; i < input.Length; i++) {
                if (!char.IsDigit(input[i])){ allDigits = false; break; }
            }
            return lengthNine && allDigits;
        }


        public bool validHairColor(string input) {
            //a # followed by exactly six characters 0-9 or a-f

            var validChars = new HashSet<char>() { 'a', 'b', 'c', 'd', 'e', 'f', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            bool lengthSeven = input.Length == 7;
            bool hexadecimalCode = true; ;
            
                
                for (int i = 1; i < input.Length; i++)
                {
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