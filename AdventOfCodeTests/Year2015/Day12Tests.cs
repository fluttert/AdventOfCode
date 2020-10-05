using AdventOfCode.Year2015;
using Xunit;

namespace AdventOfCodeTests.Year2015
{
    public class Day12Tests
    {
        private readonly Day12 day = new Day12();

        [Fact]
        public void Part01()
        {
            Assert.Equal("6", day.SolvePart1("[1,2,3]"));
            Assert.Equal("6", day.SolvePart1("{\"a\":2,\"b\":4}"));
            Assert.Equal("3", day.SolvePart1("[[[3]]]"));
            Assert.Equal("3", day.SolvePart1("{\"a\":{\"b\":4},\"c\":-1}"));
            Assert.Equal("3", day.SolvePart1("{\"a\":{\"b\":4},\"c\":-1}"));
            Assert.Equal("0", day.SolvePart1("{\"a\":[-1,1]}"));
            Assert.Equal("0", day.SolvePart1("[-1,{\"a\":1}]"));
            Assert.Equal("0", day.SolvePart1("[]"));
            Assert.Equal("0", day.SolvePart1("{}"));
        }

        [Fact]
        public void Part02()
        {
            Assert.Equal("6", day.SolvePart2("[1,\"red\",5]"));
            Assert.Equal("4", day.SolvePart2("[1,{\"c\":\"red\",\"b\":2},3]"));
            Assert.Equal("0", day.SolvePart2("{\"d\":\"red\",\"e\":[1,2,3,4],\"f\":5}"));
            Assert.Equal("6", day.SolvePart2("[1,2,3]"));
        }
    }
}