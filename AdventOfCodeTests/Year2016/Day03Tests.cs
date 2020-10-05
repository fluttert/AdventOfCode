using AdventOfCode.Year2016;
using Xunit;

namespace AdventOfCodeTests.Year2016
{
    public class Day03Tests
    {
        private readonly Day03 day = new Day03();

        [Fact]
        public void Part01()
        {
            string input = @"5 10 25
25 10 5
5 10 12
10 15 5";
            Assert.Equal("1", day.SolvePart1(input));
        }

        [Fact]
        public void Part02()
        {
            // no tests
        }
    }
}