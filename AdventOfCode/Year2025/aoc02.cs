#!/usr/bin/env dotnet

Console.WriteLine("2025 - Day02 Start");

long answer = 0;
string[] txt = new AoC().testInput
    .Split(new[] {',' }, StringSplitOptions.RemoveEmptyEntries)
    .Select(s => s.Trim())
    .ToArray();

Console.WriteLine($"Lines parsed: {txt.Length}");

// PART 1
foreach (string line in txt)
{
    string[] range = line.Split('-');
    long start = long.Parse(range[0]);
    long end = long.Parse(range[1]);
    for (long num = start; num <= end; num++)
    {
        string number = ""+num;
        if(number.Length%2!=0) { continue; } // must be even length
        string numberpart1 = number.Substring(0, number.Length / 2);
        string numberpart2 = number.Substring(number.Length / 2, number.Length / 2);
        if(numberpart1==numberpart2)
        {
            answer+=num;
        }
    }
}
Console.WriteLine($"Answer 1: {answer}");

// PART 2
answer = 0;
foreach (string line in txt)
{
    string[] range = line.Split('-');
    long start = long.Parse(range[0]);
    long end = long.Parse(range[1]);
    // loop numbers in range
    for (long num = start; num <= end; num++)
    {
        string number = ""+num;
        int halfLength = number.Length / 2;
        string part = "";
        for(int i = 0 ; i<halfLength; i++)
        {
            part+=number[i]; // current part to match
            
            if(number.Length % part.Length != 0) { continue; } // part must divide number length evenly
            //Console.WriteLine($"Checking number {num} with part {part}");
            bool onlyMatches = true;
            for(int j = part.Length; j<number.Length; j+=part.Length)
            {
                string compare = number.Substring(j, part.Length);
                //Console.WriteLine($"  Comparing part {part} with {compare}");
                if(part != compare)
                {
                    onlyMatches = false;
                    break;
                }
            }
            if(onlyMatches)
            {
                Console.WriteLine($"Number {num} matches with part {part}");
                answer+=num;
                break;
            }

        }
    }
}
Console.WriteLine($"Answer 2: {answer}");


Console.WriteLine("2025 - Day 02 End");
public class AoC
{
    public string testInput = @"11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124";

    public string input = @"";
}