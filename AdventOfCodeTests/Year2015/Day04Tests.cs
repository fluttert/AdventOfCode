using AdventOfCode.Year2015;
using Xunit;

namespace AdventOfCodeTests
{
    public class Day04Tests
    {
        private readonly Day04 day = new Day04();

        [Fact]
        public void Part01()
        {
            Assert.Equal("609043", day.SolvePart1("abcdef"));
            Assert.Equal("1048970", day.SolvePart1("pqrstuv"));
        }

        [Fact]
        public void Part02()
        {
           // no tests
        }
    }
}