using AdventOfCode.Year2015;
using Xunit;

namespace AdventOfCodeTests.Year2015
{
    public class Day03Tests
    {
        private readonly Day03 day = new Day03();

        [Fact]
        public void Part01()
        {
            Assert.Equal("2", day.SolvePart1(">"));
            Assert.Equal("4", day.SolvePart1("^>v<"));
            Assert.Equal("2", day.SolvePart1("^v^v^v^v^v"));
        }

        [Fact]
        public void Part02()
        {
            Assert.Equal("3", day.SolvePart2("^v"));
            Assert.Equal("3", day.SolvePart2("^>v<"));
            Assert.Equal("11", day.SolvePart2("^v^v^v^v^v"));
        }
    }
}