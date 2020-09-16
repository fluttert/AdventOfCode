using AdventOfCode.Year2015;
using Xunit;

namespace AdventOfCodeTests
{
    public class Day10Tests
    {
        private readonly Day10 day = new Day10();

        [Fact]
        public void Part01()
        {
            Assert.Equal("11", day.LookAndSay("1",1));
            Assert.Equal("21", day.LookAndSay("1", 2));
            Assert.Equal("1211", day.LookAndSay("1", 3));
            Assert.Equal("111221", day.LookAndSay("1", 4));
            Assert.Equal("312211", day.LookAndSay("1", 5));
        }

        [Fact]
        public void Part02()
        {
            // no tests
        }
    }
}