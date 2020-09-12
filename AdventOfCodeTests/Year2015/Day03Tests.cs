using AdventOfCode.Year2015;
using Xunit;

namespace AdventOfCodeTests
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
           
        }
    }
}