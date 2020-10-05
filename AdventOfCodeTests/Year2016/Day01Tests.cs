using AdventOfCode.Year2016;
using Xunit;

namespace AdventOfCodeTests.Year2016
{
    public class Day01Tests
    {
        private readonly Day01 day01 = new Day01();

        [Fact]
        public void Part01()
        {
            Assert.Equal("5", day01.SolvePart1("R2, L3"));
            Assert.Equal("2", day01.SolvePart1("R2, R2, R2"));
            Assert.Equal("12", day01.SolvePart1("R5, L5, R5, R3"));
        }

        [Fact]
        public void Part02()
        {
            Assert.Equal("4", day01.SolvePart2("R8, R4, R4, R8"));
        }
    }
}