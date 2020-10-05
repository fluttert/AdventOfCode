using AdventOfCode.Year2015;
using Xunit;

namespace AdventOfCodeTests.Year2015
{
    public class Day02Tests
    {
        private readonly Day02 day = new Day02();

        [Fact]
        public void Part01()
        {
            Assert.Equal("58", day.SolvePart1("2x3x4"));
            Assert.Equal("43", day.SolvePart1("1x1x10"));
        }

        [Fact]
        public void Part02()
        {
            Assert.Equal("34", day.SolvePart2("2x3x4"));
            Assert.Equal("14", day.SolvePart2("1x1x10"));
        }
    }
}