#!/usr/bin/env dotnet

using System.Diagnostics;

Console.WriteLine("2025 - Day05 Start");
Stopwatch sw = Stopwatch.StartNew();

// input parsing
string[] txt = new AoC().testInput
    .Split(""+System.Environment.NewLine+System.Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
    .Select(s => s.Trim())
    .ToArray();

List<(long from, long to)> ranges = new();
foreach(var line in txt[0].Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries))
{
    var parts = line.Split('-');
    ranges.Add( (long.Parse(parts[0]), long.Parse(parts[1])) );
}
ranges = ranges.OrderBy(r => r.from).ToList(); // sort ranges by from value

Console.WriteLine($"Lines parsed: {txt.Length} in {sw.ElapsedMilliseconds} ms");
sw.Restart();

// set up general variables
long answer = 0;

// Solve PART 1
foreach(var line in txt[1].Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries))
{
    long num = long.Parse(line);
    foreach(var range in ranges)
    {
        if(num>=range.from && num<=range.to)
        {
            answer++;
            break;
        }
    }
}

Console.WriteLine($"Answer 1: {answer} in {sw.ElapsedMilliseconds} ms");
sw.Restart();

// Solve PART 2
answer = 0;
long previousTo = -1;
foreach(var range in ranges)
{   // grand total of all ranges, including overlaps
    //answer+= range.to - range.from + 1;
    if(range.to<=previousTo)
    {
        // fully overlapped range, skip
        continue;
    }
    answer+= range.to - Math.Max(previousTo+1, range.from) + 1;
    previousTo = range.to;
}

Console.WriteLine($"Answer 2: {answer} in {sw.ElapsedMilliseconds} ms");

Console.WriteLine("2025 - Day 05 End");
public class AoC
{
    public string testInput = @"3-5
10-14
16-20
12-18

1
5
8
11
17
32";

    public string input = @"";
}