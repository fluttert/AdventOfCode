using AdventOfCode.Year2020;
using Xunit;

namespace AdventOfCodeTests.Year2020
{
    public class Day14Tests
    {
        private readonly Day14 day = new Day14();

        [Fact]
        public void Part01Test1()
        {
            string input = @"mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X
mem[8] = 11
mem[7] = 101
mem[8] = 0";
            Assert.Equal("165", day.SolvePart1(input));
        }

        [Fact]
        public void Part01Test2()
        {
            string input = @"mask = 000000000000000000000000000000X1001X
mem[42] = 100
mask = 00000000000000000000000000000000X0XX
mem[26] = 1";
            Assert.Equal("208", day.SolvePart2(input));
        }
    }
}