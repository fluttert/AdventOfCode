using System;
using System.Collections.Generic;

namespace AdventOfCode.Year2022
{
    public class Day11 : IAoC
    {
        // Puzzle can be found on: https://adventofcode.com/2022/day/11

       

        public string SolvePart1(string input)
        {
            // parse
            string[] lines = input.Split(Environment.NewLine + Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            Monkey[] monkeys; monkeys = new Monkey[lines.Length];
            for (int i = 0; i < monkeys.Length; i++) { monkeys[i] = new Monkey(); }
            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
                // set items
                long[] items = Array.ConvertAll((parts[1][17..]).Split(','), long.Parse);
                for (int j = 0; j < items.Length; j++) { monkeys[i].Items.Enqueue(items[j]); }
                // set operation
                string[] operationParts = (parts[2][23..]).Split(' ', StringSplitOptions.RemoveEmptyEntries);
                monkeys[i].OperationPlus = operationParts[0][0] == '+';
                monkeys[i].OperationNumberIsOld = operationParts[1] == "old";
                if (monkeys[i].OperationNumberIsOld is false) { monkeys[i].OperationNumber = long.Parse(operationParts[1]); }
                // set test
                monkeys[i].TestDivisor = long.Parse(parts[3][21..]);
                monkeys[i].TestTrue = monkeys[int.Parse(parts[4][29..])];
                monkeys[i].TestFalse = monkeys[int.Parse(parts[5][30..])];
            }

            // simulate
            for (int i = 0; i < 20; i++) {
                for (int j = 0; j < monkeys.Length; j++) {
                    monkeys[j].PlayRound(true);
                }
            }

            // get the highest numbers
            long[] inspectedItems = new long[monkeys.Length];
            for (int i = 0; i < monkeys.Length; i++)
            {
                inspectedItems[i] = monkeys[i].ItemsInpected;
            }
            Array.Sort(inspectedItems);
            Array.Reverse(inspectedItems);

            return "" + (inspectedItems[0] * inspectedItems[1]);
        }

        public string SolvePart2(string input)
        {
            // parse
            string[] lines = input.Split(Environment.NewLine + Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            Monkey[] monkeys; monkeys = new Monkey[lines.Length];
            long commonDivisor = 1;
            for (int i = 0; i < monkeys.Length; i++) { monkeys[i] = new Monkey(); }
            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
                // set items
                long[] items = Array.ConvertAll((parts[1][17..]).Split(','), long.Parse);
                for (int j = 0; j < items.Length; j++) { monkeys[i].Items.Enqueue(items[j]); }
                // set operation
                string[] operationParts = (parts[2][23..]).Split(' ', StringSplitOptions.RemoveEmptyEntries);
                monkeys[i].OperationPlus = operationParts[0][0] == '+';
                monkeys[i].OperationNumberIsOld = operationParts[1] == "old";
                if (monkeys[i].OperationNumberIsOld is false) { monkeys[i].OperationNumber = long.Parse(operationParts[1]); }
                // set test
                monkeys[i].TestDivisor = long.Parse(parts[3][21..]);
                commonDivisor *= monkeys[i].TestDivisor;
                monkeys[i].TestTrue = monkeys[int.Parse(parts[4][29..])];
                monkeys[i].TestFalse = monkeys[int.Parse(parts[5][30..])];
            }
            // simulate
            for (int i = 0; i < (10_000); i++)
            {
                for (int j = 0; j < monkeys.Length; j++)
                {
                    monkeys[j].PlayRound(false, commonDivisor);
                }
                //if (i == 0 || i == 19 || i == 999 || i == 1999)
                //{
                //    Console.WriteLine($"Round: {i + 1}");
                //    Console.WriteLine($"Monkey 0 has inspected {monkeys[0].ItemsInpected}");
                //    Console.WriteLine($"Monkey 1 has inspected {monkeys[1].ItemsInpected}");
                //    Console.WriteLine($"Monkey 2 has inspected {monkeys[2].ItemsInpected}");
                //    Console.WriteLine($"Monkey 3 has inspected {monkeys[3].ItemsInpected}");
                //    Console.WriteLine($"");
                //}
            }

            // get the highest numbers
            long[] inspectedItems = new long[monkeys.Length];
            for (int i = 0; i < monkeys.Length; i++)
            {
                inspectedItems[i] = monkeys[i].ItemsInpected;
            }
            Array.Sort(inspectedItems);
            Array.Reverse(inspectedItems);

            return "" + (inspectedItems[0] * inspectedItems[1]);
        }

        public string GetInput()
        {
            return new Inputs.Year2022.Day11().Input;
        }
    }

    public class Monkey
    {
        public long ItemsInpected = 0;
        public Queue<long> Items = new();
        public bool OperationNumberIsOld = false;
        public long OperationNumber = 0;
        public bool OperationPlus = true;
        public long TestDivisor = 0;
        public Monkey TestTrue = null;
        public Monkey TestFalse = null;

        public void PlayRound(bool relief = true, long commonDivisor = 1)
        {
            while (Items.Count > 0)
            {
                // monkey inspects an item
                long item = Items.Dequeue();
                ItemsInpected++;

                // monkey increases worry level
                if (OperationNumberIsOld)
                {
                    item = OperationPlus ? item + item : item * item;
                }
                else
                {
                    item = OperationPlus ? item + OperationNumber : item * OperationNumber;

                }
                item %= commonDivisor;


                // monkey gets bored, divide by 3 and round to nearest integer
                if (relief) { item /= 3; }

                // test
                if (item % TestDivisor == 0)
                {
                    TestTrue.Items.Enqueue(item);
                }
                else
                {
                    TestFalse.Items.Enqueue(item);
                }
            }
            //return true
        }
    }
}