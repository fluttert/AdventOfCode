using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace AdventOfCode.Year2023;

public class Day13 : IAoC
{
    // Puzzle can be found on: https://adventofcode.com/2023/day/13
    public string SolvePart1(string input)
    {
        string[] patterns = input.Split(Environment.NewLine + Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        foreach(string pattern in patterns)
        {
            string[] grid = pattern.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            
            int res = DetectHorizontalMirror(grid);
            Console.WriteLine(res);

        }
        return "";
    }

    public string SolvePart2(string input)
    {


        return "";
    }

    public int DetectHorizontalMirror(string[] pattern) {
        int result = 0;
        // scan until the second last line
        for (int i = 0; i < pattern.Length-1; i++) 
        {
            int start = i, compare = start + 1;
            bool mirrorDetected = true;
            while (start >= 0 && compare < pattern.Length) {
                // Only detect if there is NO mirror
                if (pattern[start] != pattern[compare]) { mirrorDetected = false; break;  }
                start--; compare++;                         // otherwise increase distance
            }
            if (mirrorDetected) { result = i+1; break; }    // up by 1 as we use zero-bases matrices
        }
        return result*100;                                  // needs to be multiplied by 100 for horizontal mirrors
    }

    public int DetectVerticalMirror(string[] pattern)
    {
        int result = 0;

        return result;

    }

    public string GetInput()
    {
        //return new Inputs.Year2023.Day13Input().Input;
        return """
#.##..##.
..#.##.#.
##......#
##......#
..#.##.#.
..##..##.
#.#.##.#.

#...##..#
#....#..#
..##..###
#####.##.
#####.##.
..##..###
#....#..#
""";
    }
}