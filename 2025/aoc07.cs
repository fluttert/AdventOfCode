#!/usr/bin/env dotnet

using System.Diagnostics;

string day = "Day 07";
Console.WriteLine($"2025 - {day} Start");
Stopwatch sw = Stopwatch.StartNew();

// input parsing
string[] lines = new AoC().testInput
    .Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

// convert to grid
char[][] grid = new char[lines.Length][];
for (int i = 0; i < lines.Length; i++) { grid[i] = lines[i].ToCharArray(); }
Console.WriteLine($"Lines parsed: {lines.Length} in {sw.ElapsedMilliseconds} ms");
sw.Restart();

// set up general variables
long answer = 0;
(int x, int y) pos = (0, 0);

// Solve PART 1

Queue<(int x, int y)> queue = new();
queue.Enqueue((lines[0].IndexOf('S'), 0)); // find start position
HashSet<(int x, int y)> visited = new();

while (queue.Count > 0)
{
    pos = queue.Dequeue();
    // cache check => if visited before, skip it
    if (visited.Contains(pos) || pos.x < 0 || pos.x >= grid.Length || pos.y >= grid[0].Length) { continue; }

    // not been here before, mark as visited
    visited.Add(pos);

    char cell = grid[pos.y][pos.x];
    if (cell == '^')
    {
        // found a tree, count and do not continue from here
        answer++;
        queue.Enqueue((pos.x - 1, pos.y + 1)); // add a pipe left
        queue.Enqueue((pos.x + 1, pos.y + 1)); // add a pipe right

    }
    else
    {
        queue.Enqueue((pos.x, pos.y + 1)); // add a pipe done
    }

}


Console.WriteLine($"Answer 1: {answer} in {sw.ElapsedMilliseconds} ms");
sw.Restart();

// Solve PART 2
answer = 0;
long[][] beams = new long[grid.Length][];           // create duplicate GRID with numbers only
for (int i = 0; i < grid.Length; i++) { beams[i] = new long[grid[0].Length]; } // defaults to 0
beams[0][lines[0].IndexOf('S')] = 1; // start with 1 beam at the start position

for(int i = 1; i < grid.Length; i++) // for each row
{
    for(int j = 0; j < grid[0].Length; j++) // for each column
    {
        long beamCount = beams[i - 1][j];
        if(beamCount == 0) { continue; } // no beams to propagate

        char cell = grid[i][j];
        if (cell == '^')
        {
            // found a tree, split beams left and right
            if(j - 1 >= 0) { beams[i][j - 1] += beamCount; } // add beams left
            if(j + 1 < grid[0].Length) { beams[i][j + 1] += beamCount; } // add beams right
            beams[i][j] = 0; // no beams continue down
        }
        else
        {
            // empty space, propagate beams down
            beams[i][j] += beamCount;
        }
    }
}

// sum all beams that reached the end
for (int y = 0; y < beams[0].Length; y++)
{
    answer += beams[grid.Length - 1][y];
}


// // EARLIER tries
// HashSet<(int x, int y)> inQueue = new();                                   // reset visited
// long[][] beams = new long[grid.Length][];           // create duplicate GRID with numbers only
// for (int i = 0; i < grid.Length; i++) { beams[i] = new long[grid[0].Length]; } // defaults to 0

// pos = (0, lines[0].IndexOf('S'));
// queue.Enqueue(pos);          // queue start position
// while (queue.Count > 0)
// {
//     pos = queue.Dequeue();      // next position
//     //Console.WriteLine($"Processing position: {pos}");
//     beams[pos.x][pos.y]++;      // increase value

//     // determine next positions
//     char cell = grid[pos.x][pos.y];
//     if (cell == '^')
//     {
//         (int x, int y) left = (pos.x + 2, pos.y - 1);
//         (int x, int y) right = (pos.x + 2, pos.y + 1);

//         if(inQueue.Contains(left)){ beams[left.x][left.y]++;}
//         if(inQueue.Contains(right)){ beams[right.x][right.y]++;}

//         if(left.x < grid.Length) {
//             // bounds check
//             if (left.y >= 0) { queue.Enqueue(left); inQueue.Add(left); }              // add a pipe left
//             if (right.y < grid[0].Length) { queue.Enqueue(right); inQueue.Add(right); }// add a pipe right
//         }

//     }
//     else
//     {
//         (int x, int y) down = (pos.x + 2, pos.y);
//         if(down.x < grid.Length) { 
//             queue.Enqueue(down); // add a pipe down
//         }
//     }

// }
// // sum all beams that reached the end
// for (int y = 0; y < grid[0].Length; y++)
// {
//     answer += beams[grid.Length - 2][y];
// }

Console.WriteLine($"Answer 2: {answer} in {sw.ElapsedMilliseconds} ms");

Console.WriteLine($"2025 - {day} end");
public class AoC
{
    public string testInput = @".......S.......
...............
.......^.......
...............
......^.^......
...............
.....^.^.^.....
...............
....^.^...^....
...............
...^.^...^.^...
...............
..^...^.....^..
...............
.^.^.^.^.^...^.
...............";

    public string input = @"";
}