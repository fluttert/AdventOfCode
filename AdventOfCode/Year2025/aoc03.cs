#!/usr/bin/env dotnet

Console.WriteLine("2025 - Day03 Start");

long answer = 0;
string[] txt = new AoC().input
    .Split(System.Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
    .Select(s => s.Trim())
    .ToArray();

Console.WriteLine($"Lines parsed: {txt.Length}");

// PART 1
foreach (string line in txt)
{
    string joltage = "";
    int indexHigh = 0;
    int indexNumber =-1;
    // highest number cannot be last number
    for(int i =0; i<line.Length-1; i++)
    {
        if(line[i]>indexNumber){indexHigh=i;indexNumber=line[i];}
    }
    joltage+=line[indexHigh];
    
    // reset and do it again
    indexNumber = -1;
    for(int i = indexHigh+1; i<line.Length; i++)
    {
        if(line[i]>indexNumber){indexHigh=i;indexNumber=line[i];}
    }
    joltage+=line[indexHigh];
    Console.WriteLine($"Joltage for line {line} is {joltage}");
    answer+=long.Parse(joltage);
}
Console.WriteLine($"Answer 1: {answer}");

// PART 2
answer = 0;
foreach (string line in txt)
{
    string joltage = "";
    int startingIndex = 0;
    int remainingLength = 12;
    while(remainingLength>0)
    {
        // reset to the first digit (C# compares CHARS as INT)
        char currentDigit=line[startingIndex];
        // Don't check beyond the remaining length of the number
        for(int i = startingIndex; i<line.Length-(remainingLength-1); i++)
        {
            if(line[i]>currentDigit)
            {
                currentDigit=line[i];
                startingIndex=i;
            }
        }
        joltage+=currentDigit;
        remainingLength--;
        startingIndex++;
    }
    //Console.WriteLine($"Joltage for line {line} is {joltage} with length {joltage.Length}");
    answer+=long.Parse(joltage);
}
Console.WriteLine($"Answer 2: {answer}");

Console.WriteLine("2025 - Day 03 End");
public class AoC
{
    public string testInput = @"987654321111111
811111111111119
234234234234278
818181911112111";

    public string input = @"";
}