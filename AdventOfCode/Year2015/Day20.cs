namespace AdventOfCode.Year2015
{
    internal class Day20 : IAoC
    {
        // Challenge can be found on: https://adventofcode.com/2015/day/19

        public string SolvePart1(string input)
        {
            int presentsTarget = int.Parse(input);

            // create houses
            var houses = new int[(presentsTarget / 10)];
            int houseNumber = int.MaxValue;
            for (int elf = 1; elf <= houses.Length; elf++)
            {
                // step per elf
                for (int step = elf; step < houses.Length; step += elf)
                {
                    int presents = houses[step] + elf * 10;
                    if (presents >= presentsTarget && step < houseNumber)
                    {
                        houseNumber = step;
                    }
                    houses[step] = presents;
                }
            }

            return houseNumber.ToString();
        }

        public string SolvePart2(string input)
        {
            long presentsTarget = long.Parse(input);

            // create houses
            var houses = new long[(presentsTarget/10)];
            long houseNumber = long.MaxValue;
            for (long elf = 1; elf <= houses.Length; elf++)
            {
                long presentsDelivered = 0;
                long house = elf;
                while (presentsDelivered < 50)
                {
                    // which house to deliver presents
                    long curHouse = elf + house * presentsDelivered;
                    if (curHouse >= houses.Length) { break; } // stop this loop if we have surpasses the houses

                    houses[curHouse] += (elf * 11);

                    if ((houses[curHouse] >= presentsTarget) && (houseNumber > curHouse))
                    {
                        houseNumber = curHouse;
                    }

                    presentsDelivered++;
                }
            }

            return houseNumber.ToString();
        }

        public string GetInput()
        {
            return "36000000";
        }
    }
}