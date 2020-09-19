using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Year2015
{
    public class Day13 : IAoC
    {
        // Challenge can be found on https://adventofcode.com/2015/day/13

        public string SolvePart1(string input)
        {
            return CalculateTableHappiness(input);
        }

        public string SolvePart2(string input)
        {
            // add input
            string extraInput = @"
Me would gain 0 happiness units by sitting next to Alice.
Me would gain 0 happiness units by sitting next to Bob.
Me would gain 0 happiness units by sitting next to Carol.
Me would gain 0 happiness units by sitting next to David.
Me would gain 0 happiness units by sitting next to Eric.
Me would gain 0 happiness units by sitting next to Frank.
Me would gain 0 happiness units by sitting next to George.
Me would gain 0 happiness units by sitting next to Mallory.
Alice would gain 0 happiness units by sitting next to Me.
Bob would gain 0 happiness units by sitting next to Me.
Carol would gain 0 happiness units by sitting next to Me.
David would gain 0 happiness units by sitting next to Me.
Eric would gain 0 happiness units by sitting next to Me.
Frank would gain 0 happiness units by sitting next to Me.
George would gain 0 happiness units by sitting next to Me.
Mallory would gain 0 happiness units by sitting next to Me.";

            return SolvePart1(input+extraInput);
        }

        internal string CalculateTableHappiness(string input)
        {
            string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            // create information
            var seatingInfo = new Dictionary<string, Dictionary<string, int>>();
            var people = new HashSet<string>();

            // prep each person
            foreach (string line in lines)
            {
                string[] parts = line.Split(new char[] { ' ', '.' }, StringSplitOptions.RemoveEmptyEntries);
                // add person
                if (!seatingInfo.ContainsKey(parts[0])) { seatingInfo.Add(parts[0], new Dictionary<string, int>()); }
                int happiness = int.Parse(parts[3]) * (parts[2] == "gain" ? 1 : -1);
                seatingInfo[parts[0]].Add(parts[^1], happiness);
                people.Add(parts[0]);
            }

            string[] persons = people.ToArray();
            int personAmount = persons.Length;

            // permutate!
            var permutations = GetPermutations<string>(persons, personAmount);
            int highestHappiness = int.MinValue;

            foreach (var permutation in permutations)
            {
                List<string> table = permutation.ToList<string>();
                int happiness = 0;

                for (int i = 0; i < table.Count; i++)
                {
                    int left = (personAmount + i - 1) % personAmount;
                    int right = (personAmount + i + 1) % personAmount;

                    //string person = table[i];
                    //string personLeft = table[left];
                    //string personRight = table[right];

                    happiness += seatingInfo[table[i]][table[left]];
                    happiness += seatingInfo[table[i]][table[right]];
                }
                if (happiness > highestHappiness)
                {
                    highestHappiness = happiness;
                }
            }

            return highestHappiness.ToString();
        }

        public string GetInput()
        {
            return new Inputs.Year2015.Day13().Input;
        }

        // thank you https://stackoverflow.com/questions/756055/listing-all-permutations-of-a-string-integer
        internal static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }
    }
}