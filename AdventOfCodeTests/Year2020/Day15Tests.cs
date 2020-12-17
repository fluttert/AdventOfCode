using AdventOfCode.Year2020;
using Xunit;

namespace AdventOfCodeTests.Year2020
{
    public class Day15Tests
    {
        private readonly Day15 day = new Day15();

        [Fact]
        public void Part01Test1()
        {
            string input = @"1,3,2";
            Assert.Equal("1", day.SolvePart1(input));
        }

        [Fact]
        public void Part01Test2()
        {
            string input = @"0,3,6";
            Assert.Equal(0, day.MemoryGame(input,10));
        }
    }
}