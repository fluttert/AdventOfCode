using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2020
{
    public class Day24 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2020/day/24

        /// Generic idea for Day 24
        /// Check out https://www.redblobgames.com/grids/hexagons/ explaining hexagon grids
        public string SolvePart1(string input)
        {
            return LobbyLayout(input).Count.ToString();
        }

        // in progress
        public string SolvePart2(string input)
        {
            // parse input
            var tiles = LobbyLayout(input);     // day 0
            for (int i = 1; i <= 100; i++)
            {    // 100 days
                // add all tiles + neighbours (unique tiles)
                var fringeTiles = GetNeighbours(tiles);

                // determine if tiles are switched on or not
                var updatedTiles = new HashSet<(int x, int y, int z)>();
                foreach (var ft in fringeTiles)
                {
                    int blackTileNeightbour = CountBlackTileNeighbours(ft, tiles);
                    if (tiles.Contains(ft))
                    {       // black tile
                        if (blackTileNeightbour == 1 || blackTileNeightbour ==2) { updatedTiles.Add(ft); }
                    }
                    else
                    {  // white tile
                        if (blackTileNeightbour == 2) { updatedTiles.Add(ft); }
                    }
                }

                tiles = updatedTiles;
                Console.WriteLine($"Day: {i} has {tiles.Count} black tiles");
            }

            return tiles.Count.ToString();
        }

        public int CountBlackTileNeighbours((int x, int y, int z) tile, HashSet<(int x, int y, int z)> tiles)
        {
            int blackTiles = 0;
            var east = (x: tile.x + 1, y: tile.y - 1, z: tile.z);
            var west = (x: tile.x - 1, y: tile.y + 1, z: tile.z);
            var southWest = (x: tile.x - 1, y: tile.y, z: tile.z + 1);
            var southEast = (x: tile.x, y: tile.y - 1, z: tile.z + 1);
            var NorthWest = (x: tile.x, y: tile.y + 1, z: tile.z - 1);
            var NorthEast = (x: tile.x + 1, y: tile.y, z: tile.z - 1);
            if (tiles.Contains(east)) { blackTiles++; }
            if (tiles.Contains(west)) { blackTiles++; }
            if (tiles.Contains(southWest)) { blackTiles++; }
            if (tiles.Contains(southEast)) { blackTiles++; }
            if (tiles.Contains(NorthEast)) { blackTiles++; }
            if (tiles.Contains(NorthWest)) { blackTiles++; }

            return blackTiles;
        }

        public HashSet<(int x, int y, int z)> GetNeighbours(HashSet<(int x, int y, int z)> blackTiles)
        {
            var tiles = new HashSet<(int x, int y, int z)>();
            foreach (var tile in blackTiles)
            {
                var east = (x: tile.x + 1, y: tile.y - 1, z: tile.z);
                var west = (x: tile.x - 1, y: tile.y + 1, z: tile.z);
                var southWest = (x: tile.x - 1, y: tile.y, z: tile.z + 1);
                var southEast = (x: tile.x, y: tile.y - 1, z: tile.z + 1);
                var NorthWest = (x: tile.x, y: tile.y + 1, z: tile.z - 1);
                var NorthEast = (x: tile.x + 1, y: tile.y, z: tile.z - 1);
                tiles.Add(tile); // add yourself
                tiles.Add(east); tiles.Add(west); tiles.Add(southEast); tiles.Add(southWest); tiles.Add(NorthEast); tiles.Add(NorthWest);
            }
            return tiles;
        }

        public HashSet<(int x, int y, int z)> LobbyLayout(string input)
        {
            // parse input
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            var tiles = new HashSet<(int x, int y, int z)>();
            foreach (var line in lines)
            {
                int index = 0;
                var tile = (x: 0, y: 0, z: 0);
                while (index < line.Length)
                {
                    if (line[index] == 's')
                    {
                        if (line[index + 1] == 'w')
                        {   // south west
                            tile.z++;
                            tile.x--;
                        }
                        else
                        {   // south east
                            tile.z++;
                            tile.y--;
                        }
                        index += 2;
                        continue;
                    }
                    if (line[index] == 'n')
                    {
                        if (line[index + 1] == 'w')
                        {   // north west
                            tile.z--;
                            tile.y++;
                        }
                        else
                        {   // north east
                            tile.z--;
                            tile.x++;
                        }
                        index += 2;
                        continue;
                    }
                    if (line[index] == 'w')
                    {   // west
                        tile.x--;
                        tile.y++;
                    }
                    else
                    { // east
                        tile.x++;
                        tile.y--;
                    }
                    index++;
                }
                // check if this tile was already flipped
                if (tiles.Contains(tile)) { tiles.Remove(tile); }
                else { tiles.Add(tile); }
            }
            return tiles;
        }

        public string GetInput()
        {
            return new Inputs.Year2020.Day24().Input;
        }
    }
}