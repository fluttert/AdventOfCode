using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2020
{
    public class Day17 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2020/day/17

        /// Generic idea for Day 17
        public string SolvePart1(string input)
        {
            // read per line and convert to integer array
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            var activeCubes = new HashSet<(int x, int y, int z)>();
            // read input
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    if (lines[y][x] == '#')
                    {
                        var cube = (x, y, 0);
                        activeCubes.Add(cube);
                    }
                }
            }

            // six cycle boot process
            int cycle = 0;
            while (cycle < 6)
            {
                var updatedActiveCubes = new HashSet<(int x, int y, int z)>();
                // get all neighbours (including non-active), this will be all the cubes to consider
                var markedCubes = GetAllUnique3DNeighbours(activeCubes);

                foreach (var cube in markedCubes)
                {
                    var n = CheckDirect3DNeighbours(cube, activeCubes);
                    if (activeCubes.Contains(cube))
                    {   // check the active
                        if (n.active == 2 || n.active == 3)
                        {
                            updatedActiveCubes.Add(cube);
                        }
                    }
                    else
                    {   // check the in-active cube rule
                        if (n.active == 3) { updatedActiveCubes.Add(cube); }
                    }
                }
                activeCubes = updatedActiveCubes;
                cycle++;
            }

            return activeCubes.Count.ToString();
        }

        /// <summary>
        /// Check all direct neighbours in 3 dimensions if they are active or inactive
        /// </summary>
        /// <param name="cube">The cube to be inspected</param>
        /// <param name="activeCubes">List of current active cubes</param>
        /// <returns>The amount of active and inactive neighbours</returns>
        public (int active, int inactive) CheckDirect3DNeighbours((int x, int y, int z) cube, HashSet<(int x, int y, int z)> activeCubes)
        {
            int lit = 0, dark = 0;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    for (int k = -1; k <= 1; k++)
                    {
                        if (i == 0 && j == 0 && k == 0) { continue; }

                        if (activeCubes.Contains((cube.x + i, cube.y + j, cube.z + k))) { lit++; }
                        else { dark++; }
                    }
                }
            }

            return (active: lit, inactive: dark);
        }

        /// <summary>
        /// Check all direct neighbours in 4 dimensions if they are active or inactive
        /// </summary>
        /// <param name="cube">The cube to be inspected</param>
        /// <param name="activeCubes">List of current active cubes</param>
        /// <returns>The amount of active and inactive neighbours</returns>
        public (int active, int inactive) CheckDirect4DNeighbours((int x, int y, int z, int w) cube, HashSet<(int x, int y, int z, int w)> activeCubes)
        {
            int lit = 0, dark = 0;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    for (int k = -1; k <= 1; k++)
                    {
                        for (int m = -1; m <= 1; m++)
                        {
                            // skip yourself
                            if (i == 0 && j == 0 && k == 0 && m == 0) { continue; }

                            if (activeCubes.Contains((cube.x + i, cube.y + j, cube.z + k, cube.w + m))) { lit++; }
                            else { dark++; }
                        }
                    }
                }
            }
            return (active: lit, inactive: dark);
        }

        /// <summary>
        /// Get all unique neighbours (coordinates) in 3D based on the input list
        /// </summary>
        /// <param name="activeCubes"></param>
        /// <returns>All unique neighbours, including the start-positions</returns>
        public HashSet<(int x, int y, int z)> GetAllUnique3DNeighbours(IEnumerable<(int x, int y, int z)> activeCubes)
        {
            var allNeighbours = new HashSet<(int x, int y, int z)>();
            foreach (var cube in activeCubes)
            {
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        for (int k = -1; k <= 1; k++)
                        {
                            allNeighbours.Add((cube.x + i, cube.y + j, cube.z + k));
                        }
                    }
                }
            }
            return allNeighbours;
        }

        /// <summary>
        /// Get all unique neighbours (coordinates) in 3D based on the input list
        /// </summary>
        /// <param name="activeCubes"></param>
        /// <returns>All unique neighbours, including the start-positions</returns>
        public HashSet<(int x, int y, int z, int w)> GetAllUnique4DNeighbours(IEnumerable<(int x, int y, int z, int w)> activeCubes)
        {
            var allNeighbours = new HashSet<(int x, int y, int z, int w)>();
            foreach (var cube in activeCubes)
            {
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        for (int k = -1; k <= 1; k++)
                        {
                            for (int m = -1; m <= 1; m++)
                            {
                                allNeighbours.Add((cube.x + i, cube.y + j, cube.z + k, cube.w + m));
                            }
                        }
                    }
                }
            }
            return allNeighbours;
        }

        public string SolvePart2(string input)
        {
            // read per line and convert to integer array
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            var activeCubes = new HashSet<(int x, int y, int z, int w)>();
            // read input
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    if (lines[y][x] == '#')
                    {
                        var cube = (x, y, 0, 0);
                        activeCubes.Add(cube);
                    }
                }
            }

            // six cycle boot process
            int cycle = 0;
            while (cycle < 6)
            {
                var updatedActiveCubes = new HashSet<(int x, int y, int z, int w)>();
                // get all neighbours (including non-active), this will be all the cubes to consider
                var markedCubes = GetAllUnique4DNeighbours(activeCubes);

                foreach (var cube in markedCubes)
                {
                    var n = CheckDirect4DNeighbours(cube, activeCubes);
                    if (activeCubes.Contains(cube))
                    {   // check the active
                        if (n.active == 2 || n.active == 3)
                        {
                            updatedActiveCubes.Add(cube);
                        }
                    }
                    else
                    {   // check the in-active cube rule
                        if (n.active == 3) { updatedActiveCubes.Add(cube); }
                    }
                }
                activeCubes = updatedActiveCubes;
                cycle++;
            }

            return activeCubes.Count.ToString();
        }

        public string GetInput()
        {
            return new Inputs.Year2020.Day17().Input;
        }
    }
}