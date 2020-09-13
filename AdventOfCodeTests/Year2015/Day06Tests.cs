using AdventOfCode.Year2015;
using Xunit;

namespace AdventOfCodeTests
{
    public class Day06Tests
    {
        private readonly Day06 day = new Day06();

        [Fact]
        public void Part01()
        {
            Assert.Equal("1000000", day.SolvePart1("turn on 0,0 through 999,999"));
            Assert.Equal("4", day.SolvePart1("turn on 499,499 through 500,500"));
            Assert.Equal("1000", day.SolvePart1("toggle 0,0 through 999,0"));

        }

        [Fact]
        public void Part02()
        {
            //Assert.Equal("0", day.SolvePart2("ab"));

        }
    }
}