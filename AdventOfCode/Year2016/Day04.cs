using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2016
{
    public class Day04 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2016/day/3

        public string SolvePart1(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            int sumOfSectorIds = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                sumOfSectorIds += RealRoomSectorId(lines[i]);
            }
            return sumOfSectorIds.ToString();
        }

        public string SolvePart2(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            string match = "northpole";
            // the roomname = northpole object storage
            int sectorIdOfNorthPole = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                sectorIdOfNorthPole = RealRoomSectorId(lines[i]);

                // 0 = decoy chamber
                if (sectorIdOfNorthPole == 0) { continue; }

                char[] encodedLine = lines[i].ToCharArray();
                for (int j = 0; j < encodedLine.Length; j++)
                {
                    if (encodedLine[j] == '-')
                    {
                        encodedLine[j] = ' ';
                        continue;
                    }
                    if (encodedLine[j] < 'a' || encodedLine[j] > 'z') { continue; }

                    // shift -> add the sector id, then modulo 26, while subtracting and adding a
                    int newCharIndex = (((int)encodedLine[j] + sectorIdOfNorthPole - (int)'a') % 26) + 'a';
                    encodedLine[j] = (char)newCharIndex;
                }
                string RoomName = new string(encodedLine);
                if (RoomName.IndexOf(match) != -1)
                {
                    return sectorIdOfNorthPole.ToString();
                }
            }
            return sectorIdOfNorthPole.ToString();
        }

        private int RealRoomSectorId(string encodedData)
        {
            string sectorId = "", checkSum = string.Empty;
            int index = -1;

            var letterCount = new Dictionary<char, int>();
            for (int i = 'a'; i <= 'z'; i++)
            {
                letterCount.Add((char)i, 0);
            }

            // parse the string
            while (index < (encodedData.Length - 1))
            {
                index++;
                char currentChar = encodedData[index];
                if (currentChar == '-') { continue; }
                if (currentChar >= '0' && currentChar <= '9')
                {
                    sectorId += currentChar; continue;
                }
                if (currentChar == '[')
                {
                    checkSum = encodedData.Substring(index + 1, 5);
                    index += 7; continue;
                }
                //if (!dict.ContainsKey(currentChar)) { dict.Add(currentChar, 0); }
                letterCount[currentChar]++;
            }

            var tmp = letterCount.OrderByDescending(kv => kv.Value).Select(kv => kv.Key).Take(5).ToArray();
            var fivePopular = new string(tmp);

            return fivePopular == checkSum ? int.Parse(sectorId) : 0;
        }

        public string GetInput()
        {
            return new Inputs.Year2016.Day04().Input;
        }
    }
}