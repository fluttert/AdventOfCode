#!/usr/bin/env dotnet

Console.WriteLine("2025 - Day01 Start");

int dial = 50;
int answer = 0;
string[] txt = new AoC().testInput
    .Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries)
    .Select(s => s.Trim())
    .ToArray();

Console.WriteLine($"Lines parsed: {txt.Length}");
foreach (var line in txt)
{
    char turnDir = line[0];
    int dist = int.Parse(line.Substring(1));
    if (turnDir == 'L') { dial -= dist; }
    else if (turnDir == 'R') { dial += dist; }
    else { throw new Exception("Invalid turn direction"); }
    dial = (100 + dial) % 100; // normalize
    if (dial == 0) { answer++; }

}
Console.WriteLine($"Final dial position: {dial} and answer1: {answer}");

dial = 50;
answer = 0;
foreach (var line in txt)
{
    int dialBefore = dial;
    bool crossedZero = false;
    char turnDir = line[0];
    int dist = int.Parse(line.Substring(1));
    if (turnDir == 'L') { dial -= dist; }
    else if (turnDir == 'R') { dial += dist; }
    else { throw new Exception("Invalid turn direction"); }
    
    int fullRotations = dist / 100;
    dial = ((dial % 100) + 100) % 100;                                      // Always positive remainder after modulo operator
    answer+=fullRotations   ;                                                // add the amount of rotations (floor of dist/100), eg 430 = 4 full rotations
    if (dial == 0) { answer++; } 
    else{                                               // dial is EXACTLY at zero
        if (dialBefore != 0 && turnDir=='L' && dialBefore<dial ) { answer++; }  // did we cross zero going left
        if (dialBefore != 0 && turnDir=='R' && dialBefore>dial ) { answer++; }  // did we cross zero going right
    }

    Console.WriteLine($"Line: {line}, Dial before: {dialBefore}, Dial after: {dial}, Full rotations: {fullRotations}, Answer so far: {answer}");

}
Console.WriteLine($"Final dial position: {dial} and answer2: {answer}");
Console.WriteLine("2025 - Day 01 End");
public class AoC
{
    public string testInput = @"L68
    L30
    R48
    L5
    R60
    L55
    L1
    L99
    R14
    L82";

    public string input = @"";
}