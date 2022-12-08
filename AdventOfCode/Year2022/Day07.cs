using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2022
{
    public class Day07 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2022/day/7

        private Dictionary<string, Directory> tree = new();
        private Directory root;

        public Day07()
        {
            string[] lines = GetInput().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            root = new Directory { Name = "/" };
            tree.Add(root.UniqueName, root);
            Directory current = null;
            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(' ');
                // command
                if (parts[0] == "$")
                {
                    if (parts[1] == "cd" && parts[2] == "/") { current = root; continue; } // go to Root
                    if (parts[1] == "cd" && parts[2] == "..") { current = current.Parent; continue; } // go to Parent
                    if (parts[1] == "ls") { continue; } // ignore LS

                    // else go to child
                    current = current.SubDirectories[parts[2]];
                    continue;
                }

                // no command -> handle Directory first
                if (parts[0] == "dir")
                {
                    Directory tmp = new Directory { Name = parts[1], Parent = current };
                    tree.Add(tmp.UniqueName, tmp);
                    current.SubDirectories.Add(tmp.Name, tmp);
                    continue;
                }

                // no command -> handle Files second
                current.AddFile(parts[0]);
            }

            // calc all
            Queue<Directory> queue = new();
            HashSet<string> done = new();
            // add all directories without children
            foreach (var dir in tree.Values) { if (dir.SubDirectories.Count == 0) { queue.Enqueue(dir); } }
            while (queue.Count > 0)
            {
                var d = queue.Dequeue();
                if (done.Contains(d.Name) || d.Processed) { continue; }

                bool allComplete = true;
                foreach (var dir in d.SubDirectories.Values)
                {
                    if (dir.Processed == false)
                    {
                        allComplete = false;
                        queue.Enqueue(dir);
                    }
                }
                if (allComplete is false) { queue.Enqueue(d); continue; }

                // all children done
                if (d.UniqueName != root.UniqueName)
                {
                    d.Parent.Size += d.Size;
                    queue.Enqueue(d.Parent);
                }
                d.Processed = true;
                done.Add(d.UniqueName);
            }
        }

        public string SolvePart1(string input)
        {
            long sumOfSmallDirs = 0;
            foreach (Directory dir in tree.Values)
            {
                if (dir.Size <= 100_000) { sumOfSmallDirs += dir.Size; }
            }

            return "" + sumOfSmallDirs;
        }

        public string SolvePart2(string input)
        {
            long totalSpace = 70_000_000, unusedSpaceRequired = 30_000_000;
            long unusedSpace = totalSpace - root.Size;
            long closest = long.MaxValue;
            foreach (Directory dir in tree.Values)
            {
                if (unusedSpace + dir.Size >= unusedSpaceRequired && dir.Size < closest)
                {
                    closest = dir.Size;
                }
            }

            return "" + closest;
        }

        public string GetInput()
        {
            return new Inputs.Year2022.Day07().Input;
        }
    }

    // Helper class
    public class Directory
    {
        public string Name;
        public string UniqueName = Guid.NewGuid().ToString();
        public long Size = 0;
        public Directory Parent = null; // root has Parent = NULL
        public Dictionary<string, Directory> SubDirectories = new();
        public int AmountOfFiles = 0;
        public bool Processed = false;

        public void AddFile(string size)
        {
            Size += long.Parse(size);
            AmountOfFiles++;
        }
    }
}