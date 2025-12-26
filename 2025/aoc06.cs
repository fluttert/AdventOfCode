#!/usr/bin/env dotnet

using System.Diagnostics;

string day = "Day 06";
Console.WriteLine($"2025 - {day} Start");
Stopwatch sw = Stopwatch.StartNew();

// input parsing
string[] txt = new AoC().testInput
    .Split(System.Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

string[][] grid = new string[txt.Length][];
for (int i = 0; i < txt.Length; i++)
{
    grid[i] = txt[i].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
}
Console.WriteLine($"Lines parsed: {txt.Length} in {sw.ElapsedMilliseconds} ms");
sw.Restart();

// set up general variables
long answer = 0;

// Solve PART 1
for (int i = 0; i < grid[0].Length; i++)
{
    long intermediateResult = long.Parse(grid[0][i]);
    for (int j = 1; j < grid.Length - 1; j++)
    {
        bool sum = grid[grid.Length - 1][i] == "+"; // check if we are summing or multiplying
        intermediateResult = sum ? intermediateResult + long.Parse(grid[j][i]) : intermediateResult * long.Parse(grid[j][i]);
        //Console.WriteLine($"Processing cell ({i},{j}) with values {grid[j][i]} and operation {grid[grid.Length-1][i]}");
    }
    answer += intermediateResult;
    //Console.WriteLine($"Intermediate result for column {i}: {intermediateResult}");
    //bool sum = grid[0][i][0];
}

Console.WriteLine($"Answer 1: {answer} in {sw.ElapsedMilliseconds} ms");
sw.Restart();

// Solve PART 2
answer = 0;
// reparse the input to get a grid array
char[][] grid2 = new char[txt.Length][];
for (int i = 0; i < txt.Length; i++)
{
    grid2[i] = txt[i].ToCharArray();
}

// determine the width of each column
bool sum2 = true; // operator for the current column
long intermediateResult2 = 0;
for (int i = 0; i < grid2[0].Length; i++)
{

    // new start of column, update operator & restart intermediate result
    if (grid2[grid2.Length - 1][i] != ' ')
    {
        answer += intermediateResult2;
        sum2 = grid2[grid2.Length - 1][i] == '+';   // check if we are summing or multiplying
        intermediateResult2 = sum2 ? 0 : 1;         // sum starts at 0, multiplication starts at 1 (otherwise 0*number*number etc = 0)
    }

    string number = "";                                         // String to form numbers
    for (int j = 0; j < grid2.Length - 1; j++)
    {
        number += char.IsDigit(grid2[j][i]) ? grid2[j][i] : ""; // only add digits
    }
    if (number == "") continue;                                 // skip empty columns
    intermediateResult2 = sum2 ? intermediateResult2 + long.Parse(number) : intermediateResult2 * long.Parse(number);
    Console.WriteLine($"Intermediate result for column {i}: {intermediateResult2} with number {number}");
}
// don't forget to add the last one
answer += intermediateResult2;


Console.WriteLine($"Answer 2: {answer} in {sw.ElapsedMilliseconds} ms");

Console.WriteLine($"2025 - {day} end");
public class AoC
{
    public string testInput = @"123 328  51 64 
 45 64  387 23 
  6 98  215 314
*   +   *   +  ";

    public string input = @"";
}