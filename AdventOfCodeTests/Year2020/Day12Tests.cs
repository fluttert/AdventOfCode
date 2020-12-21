using AdventOfCode.Year2020;
using Xunit;

namespace AdventOfCodeTests.Year2020
{
    public class Day12Tests
    {
        private readonly Day12 day = new Day12();

        [Fact]
        public void Part02Test1()
        {
            string input = @"F10
N3
F7
R90
F11";
            Assert.Equal("286", day.SolvePart2(input));
        }

        [Fact]
        public void Part02Test2()
        {
            string input = @"R90
R90
R90
R90
R180
R180
F10";
            Assert.Equal("110", day.SolvePart2(input));
        }
    }
}