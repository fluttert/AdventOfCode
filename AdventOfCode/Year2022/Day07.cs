using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2022
{
    public class Day07 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2022/day/7

        public string SolvePart1(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, Directory> tree = new();
            Directory root = new Directory { Name = "/" };
            tree.Add(root.FullName(), root);
            Directory current = null;
            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(' ');
                // command
                if (parts[0] == "$")
                {
                    // go to root
                    if (parts[1] == "cd" && parts[2] == "/")
                    {
                        current = root;
                        continue;
                    }
                    // go up
                    if (parts[1] == "cd" && parts[2] == "..")
                    {
                        current = current.Parent;
                        continue;
                    }
                    // ignore LS
                    if (parts[1] == "ls") { continue; }

                    // go to child
                    foreach (Directory child in current.Children)
                    {
                        if (child.Name == parts[2])
                        {
                            current = child;
                            break;
                        }
                    }
                    continue;
                }

                // no command -> handle Directory first
                if (parts[0] == "dir")
                {
                    Directory tmp = new Directory { Name = parts[1], Parent = current };
                    tree.Add(tmp.FullName(), tmp);
                    current.Children.Add(tmp);
                    continue;
                }

                // no command -> handle Files second
                current.AddFile(parts[1], parts[0]);
            }

            long sumOfSmallDirs = 0;
            foreach (Directory dir in tree.Values)
            {
                long dirSize = dir.DirectoryFileSize();
                if (dirSize <= 100_000) { sumOfSmallDirs += dirSize; }
            }

            return "" + sumOfSmallDirs;
        }

        public string SolvePart2(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, Directory> tree = new();
            Directory root = new Directory { Name = "/" };
            tree.Add(root.FullName(), root);
            Directory current = null;
            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(' ');
                // command
                if (parts[0] == "$")
                {
                    // go up
                    if (parts[1] == "cd" && parts[2] == "..")
                    {
                        current = current.Parent;
                        continue;
                    }
                    if (parts[1] == "ls") { continue; }

                    // go to root
                    if (parts[1] == "cd" && parts[2] == "/")
                    {
                        current = root;
                        continue;
                    }
                    // go to child
                    foreach (Directory child in current.Children)
                    {
                        if (child.Name == parts[2])
                        {
                            current = child;
                            break;
                        }
                    }
                    continue;
                }

                // no command -> handle Directory first
                if (parts[0] == "dir")
                {
                    Directory tmp = new Directory { Name = parts[1], Parent = current };
                    tree.Add(tmp.FullName(), tmp);
                    current.Children.Add(tmp);
                    continue;
                }

                // no command -> handle Files second
                current.AddFile(parts[1], parts[0]);
            }

            long totalSpace = 70_000_000;
            long unusedSpaceRequired = 30_000_000;
            long unusedSpace = totalSpace - root.DirectoryFileSize();
            long closest = long.MaxValue;
            foreach (Directory dir in tree.Values)
            {
                long dirSize = dir.DirectoryFileSize();
                if (unusedSpace + dirSize >= unusedSpaceRequired && dirSize < closest)
                {
                    closest = dirSize;
                }
            }

            return "" + closest;
        }

        public string GetInput()
        {
            return new Inputs.Year2022.Day07().Input;
        }
    }

    public class Directory
    {
        public string Name;
        public Directory Parent = null;
        public List<Directory> Children = new();
        private List<(string name, long size)> files = new();

        public string FullName()
        {
            string name = Name;
            Directory p = Parent;
            while (p != null)
            {
                name = p.Name + name;
                p = p.Parent;
            }
            return name;
        }

        public bool AddFile(string filename, string size)
        {
            return AddFile(filename, long.Parse(size));
        }

        public bool AddFile(string filename, long size)
        {
            files.Add((filename, size));
            return true;
        }

        public long DirectoryFileSize()
        {
            long total = 0;
            foreach (var (name, size) in files)
            {
                total += size;
            }
            foreach (Directory dir in Children)
            {
                total += dir.DirectoryFileSize();
            }

            return total;
        }
    }
}