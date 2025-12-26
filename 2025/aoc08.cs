#!/usr/bin/env dotnet

using System.Diagnostics;

string day = "Day 08";
Console.WriteLine($"2025 - {day} Start");
Stopwatch sw = Stopwatch.StartNew();

// input parsing
string[] lines = new AoC().input
    .Split(System.Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

var boxes = new List<(int x, int y, int z)>();
for (int i = 0; i < lines.Length; i++)
{
    string[] parts = lines[i].Split(',');
    boxes.Add((int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2])));
}

Console.WriteLine($"Lines parsed: {lines.Length} in {sw.ElapsedMilliseconds} ms");
sw.Restart();

// set up general variables
long answer = 1;
int circuitsCount = 0;
Dictionary<(int x, int y, int z), int> circuits = new();
Dictionary<int, int> circuitSizes = new();

// Using the Generic SortedDictionary which sorts by key automatically
SortedDictionary<double, List<(int x, int y, int z)>> distanceMap = new();
for (int i = 0; i < boxes.Count; i++)
{
    for (int j = i + 1; j < boxes.Count; j++)
    {
        double distance = AoC.Distance3D(boxes[i], boxes[j]);
        // due to double (in)accruracy, the same distance is unlikely to occur twice 
        distanceMap.Add(distance, new List<(int x, int y, int z)>() { boxes[i], boxes[j] });
    }
}

// Solve PART 1
for (int i = 0; i < 1000; i++)
{
    var entry = distanceMap.ElementAt(i);
    //Console.WriteLine($"Distance {entry.Key} between boxes ({entry.Value[0].x},{entry.Value[0].y},{entry.Value[0].z}) and ({entry.Value[1].x},{entry.Value[1].y},{entry.Value[1].z})");

    (int, int, int) pos1 = entry.Value[0];
    (int, int, int) pos2 = entry.Value[1];

    // both boxes already belong to a circuit, but maybe need to merge these nets
    if (circuits.ContainsKey(pos1) && circuits.ContainsKey(pos2))
    {
        int net1 = circuits[pos1];
        int net2 = circuits[pos2];
        if (net1 == net2) { continue; }
        // else we are going to MERGE the nets
        foreach (var circuit in circuits)
        {
            if (circuit.Value == net2) { circuits[circuit.Key] = net1; }
        }
        continue;
    }

    // 1 of the boxes already is in a circuit
    if (circuits.ContainsKey(pos1)) { circuits.Add(pos2, circuits[pos1]); continue; }
    if (circuits.ContainsKey(pos2)) { circuits.Add(pos1, circuits[pos2]); continue; }

    // new circuit
    circuits.Add(pos1, circuitsCount);
    circuits.Add(pos2, circuitsCount);
    circuitsCount++;

}
// count the sizes of the circuits
foreach (var circuit in circuits)
{
    if (circuitSizes.ContainsKey(circuit.Value)) { circuitSizes[circuit.Value]++; }
    else { circuitSizes.Add(circuit.Value, 1); }
}
// get to the answer
var largestCircuit = circuitSizes.OrderByDescending(x => x.Value).Take(3).ToList();
foreach (var circuit in largestCircuit)
{
    Console.WriteLine($"Circuit {circuit.Key} has size {circuit.Value}");
    answer *= circuit.Value;
}

Console.WriteLine($"Answer 1: {answer} in {sw.ElapsedMilliseconds} ms");
sw.Restart();

// Solve PART 2
answer = 0;
circuitsCount = 0;
circuits = new();
for (int i = 0; i < distanceMap.Count; i++)
{
    var entry = distanceMap.ElementAt(i);
    //Console.WriteLine($"Distance {(int)entry.Key} between boxes ({entry.Value[0].x},{entry.Value[0].y},{entry.Value[0].z}) and ({entry.Value[1].x},{entry.Value[1].y},{entry.Value[1].z})");

    (int x, int y, int z) pos1 = entry.Value[0];
    (int x, int y, int z) pos2 = entry.Value[1];
    bool processed = false;
    // both boxes already belong to a circuit, but maybe need to merge these nets
    if (circuits.ContainsKey(pos1) && circuits.ContainsKey(pos2))
    {
        int net1 = circuits[pos1];
        int net2 = circuits[pos2];
        if (net1 == net2) { continue; }
        // else we are going to MERGE the nets
        foreach (var circuit in circuits)
        {
            if (circuit.Value == net2) { circuits[circuit.Key] = net1; }
        }
        processed = true;
    }

    // 1 of the boxes already is in a circuit
    if (processed is false && circuits.ContainsKey(pos1)) { circuits.Add(pos2, circuits[pos1]); processed = true; }
    if (processed is false && circuits.ContainsKey(pos2)) { circuits.Add(pos1, circuits[pos2]); processed = true; }
    if (processed is false)
    {
        // new circuit
        circuits.Add(pos1, circuitsCount);
        circuits.Add(pos2, circuitsCount);
        circuitsCount++;
    }
    // check if all boxes are connected AND in the same net
    if (circuits.Count == boxes.Count && circuits.GroupBy(x => x.Value).Count() == 1)
    {
        // all boxes are now connected
        answer = (long)pos1.x * (long)pos2.x;
        Console.WriteLine($"All boxes connected at distance {(int)entry.Key} between boxes ({pos1.x},{pos1.y},{pos1.z}) and ({pos2.x},{pos2.y},{pos2.z})");
        break;
    }

}


Console.WriteLine($"Answer 2: {answer} in {sw.ElapsedMilliseconds} ms");

Console.WriteLine($"2025 - {day} end");
public class AoC
{
    public static double Distance3D((int x, int y, int z) a, (int x, int y, int z) b)
    {   // based on https://en.wikipedia.org/wiki/Euclidean_distance
        return (double)Math.Sqrt(Math.Pow(a.x - b.x, 2) + Math.Pow(a.y - b.y, 2) + Math.Pow(a.z - b.z, 2));
    }

    public string testInput = @"162,817,812
57,618,57
906,360,560
592,479,940
352,342,300
466,668,158
542,29,236
431,825,988
739,650,466
52,470,668
216,146,977
819,987,18
117,168,530
805,96,715
346,949,466
970,615,88
941,993,340
862,61,35
984,92,344
425,690,689";

    public string input = @"";
}