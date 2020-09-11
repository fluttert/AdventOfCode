using AdventOfCode.Year2015;
using Xunit;

namespace AdventOfCodeTests
{
    public class Day02Tests
    {
        private readonly Day02 day = new Day02();

        [Fact]
        public void Part01()
        {
            Assert.Equal("58", day.Part1("2x3x4"));
            Assert.Equal("43", day.Part1("1x1x10"));
        }

        [Fact]
        public void Part02()
        {
            Assert.Equal("34", day.Part2("2x3x4"));
            Assert.Equal("14", day.Part2("1x1x10"));
        }
    }
}