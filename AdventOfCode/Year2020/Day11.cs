using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2020
{
    public class Day11 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2020/day/11

        /// Generic idea for Day 11
        /// Game of life adaptation
        /// Update the map EXACTLY on the rules

        public string SolvePart1(string input)
        {
            // read per lines
            string[] map = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            bool mapsAreStatic = false;
            int rounds = 0;

            while (mapsAreStatic is false)
            {
                rounds++;
                var updatedMap = UpdateMap(map, true);
                mapsAreStatic = MapsAreTheSame(map, updatedMap);
                map = updatedMap;
            }

            // count the chairs
            int occupiedChairs = 0;
            foreach (string line in map) { occupiedChairs += line.Count(x => x == '#'); }
            return occupiedChairs.ToString();
        }

        // in progress
        public string SolvePart2(string input)
        {
            string[] map = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            bool mapsAreStatic = false;
            int rounds = 0;

            while (mapsAreStatic is false)
            {
                rounds++;
                var updatedMap = UpdateMap(map, false);
                mapsAreStatic = MapsAreTheSame(map, updatedMap);
                map = updatedMap;
            }

            // count the chairs
            int occupiedChairs = 0;
            foreach (string line in map) { occupiedChairs += line.Count(x => x == '#'); }
            return occupiedChairs.ToString();
        }

        public string[] UpdateMap(string[] map, bool directNeighboursOnly)
        {
            string[] updatedMap = new string[map.Length];

            for (int i = 0; i < map.Length; i++)
            {
                var line = new char[map[i].Length];
                for (int j = 0; j < map[i].Length; j++)
                {
                    // floor never changes, nobody sits on the floor
                    if (map[i][j] == '.') { line[j] = '.'; continue; }

                    // If a seat is empty (L) and there are no occupied seats adjacent to it, the seat becomes occupied.
                    if (map[i][j] == 'L')
                    {
                        line[j] = OccupiedNeighbours(map, i, j, directNeighboursOnly) == 0 ? '#' : 'L';
                        continue;
                    }

                    // If a seat is occupied (#) and four or more seats adjacent to it are also occupied, the seat becomes empty.
                    if (map[i][j] == '#')
                    {
                        if (directNeighboursOnly)
                        {
                            line[j] = OccupiedNeighbours(map, i, j, directNeighboursOnly) >= 4 ? 'L' : '#';
                        }
                        else {
                            line[j] = OccupiedNeighbours(map, i, j, directNeighboursOnly) >= 5 ? 'L' : '#';
                        }
                        continue;
                    }

                    // Otherwise, the seat's state does not change.
                }
                updatedMap[i] = new string(line);
            }

            return updatedMap;
        }

        public int OccupiedNeighbours(string[] map, int line, int column, bool directSeatOnly = true)
        {
            int occupiedNeighbours = 0;
            // only 8 directions
            var directions = new List<(int line, int column)>() { (-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1) };

            foreach (var direction in directions)
            {
                bool stop = false;
                int steps = 0;
                while (stop is false)
                {
                    steps++;
                    int newLine = line + (steps * direction.line);
                    int newColumn = column + (steps * direction.column);
                    // out of bounds check
                    if (newLine < 0 || newLine >= map.Length || newColumn < 0 || newColumn >= map[newLine].Length)
                    {
                        stop = true;
                        continue;
                    }
                    // else check if this is occupied
                    if (map[newLine][newColumn] == '#')
                    {
                        occupiedNeighbours++;
                        stop = true;
                    }

                    // part 1 only considers direct neighbours
                    if (directSeatOnly) { stop = true; }
                }
            }
            return occupiedNeighbours;
        }

        /// <summary>
        /// Initial method for determining the seats around this
        /// </summary>
        public int SurroundingOccupiedSeats(string[] map, int line, int column)
        {
            int occupiedNeighbours = 0;
            // only 8 positions to check, also keep in mind of the borders
            for (int i = line - 1; i < line + 2; i++)
            {
                if (i < 0 || i >= map.Length) { continue; }    // out of bounds
                for (int j = column - 1; j < column + 2; j++)
                {
                    if (i == line && column == j) { continue; } // ignore the middle
                    if (j < 0 || j >= map[line].Length) { continue; }   // out of bounds
                    if (map[i][j] == '#') { occupiedNeighbours++; }
                }
            }
            return occupiedNeighbours;
        }

        public bool MapsAreTheSame(string[] map1, string[] map2)
        {
            bool same = map1.Length == map2.Length;
            int index = 0;
            while (same && index < map1.Length)
            {
                same = map1[index] == map2[index];
                index++;
            }
            return same;
        }

        public string GetInput()
        {
            return new Inputs.Year2020.Day11().Input;
        }
    }
}