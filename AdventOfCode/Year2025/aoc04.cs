#!/usr/bin/env dotnet

Console.WriteLine("2025 - Day04 Start");

long answer = 0;
string[] txt = new AoC().input
    .Split(System.Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
    .Select(s => s.Trim())
    .ToArray();

Console.WriteLine($"Lines parsed: {txt.Length}");

// PART 1
List<(int,int)> directions = new List<(int,int)>
{
    (-1, -1), (-1, 0), (-1, 1),
    ( 0, -1),          ( 0, 1),
    ( 1, -1),  (1, 0), ( 1, 1)
};
for(int row=0; row<txt.Length; row++)
{
    for(int col=0; col<txt[row].Length; col++)
    {
        if(txt[row][col]=='@')
        {
            int adjecentRolls = 0;
            foreach(var dir in directions)
            {
                int newRow = row + dir.Item1;
                int newCol = col + dir.Item2;
                if(newRow>=0 && newRow<txt.Length && newCol>=0 && newCol<txt[row].Length)
                {
                    if(txt[newRow][newCol]=='@')
                    {
                        adjecentRolls++;
                    }
                }
            }
            if(adjecentRolls<4){answer++;}
        }
    }
}
Console.WriteLine($"Answer 1: {answer}");

// PART 2
answer = 0;
// convert to grid as Strings are immutable
char[][] grid = new char[txt.Length][];
for(int i=0; i<txt.Length; i++)
{
    grid[i] = txt[i].ToArray();
}

int removedRoll = 1;
var deletedRolls = new HashSet<(int,int)>();
while(removedRoll>0){
    removedRoll=0;
    foreach(var del in deletedRolls)
    {
        grid[del.Item1][del.Item2]='.'; // remove roll
    }
    deletedRolls.Clear();
    for(int row=0; row<grid.Length; row++)
    {
        for(int col=0; col<grid[row].Length; col++)
        {
            if(grid[row][col]=='@')
            {
                int adjecentRolls = 0;
                foreach(var dir in directions)
                {
                    int newRow = row + dir.Item1;
                    int newCol = col + dir.Item2;
                    if(newRow>=0 && newRow<txt.Length && newCol>=0 && newCol<txt[row].Length)
                    {
                        if(grid[newRow][newCol]=='@')
                        {
                            adjecentRolls++;
                        }
                    }
                }
                if(adjecentRolls<4){
                    answer++;           // count this roll
                    deletedRolls.Add((row,col));
                    removedRoll++;
                }
            }
        }
    }
}
Console.WriteLine($"Answer 2: {answer}");

Console.WriteLine("2025 - Day 04 End");
public class AoC
{
    public string testInput = @"..@@.@@@@.
@@@.@.@.@@
@@@@@.@.@@
@.@@@@..@.
@@.@@@@.@@
.@@@@@@@.@
.@.@.@.@@@
@.@@@.@@@@
.@@@@@@@@.
@.@.@@@.@.";

    public string input = @"";
}