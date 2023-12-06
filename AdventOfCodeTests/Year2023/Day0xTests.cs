using AdventOfCode.Year2023;
using Xunit;

namespace AdventOfCodeTests.Year2023
{
    public class Day0xTests
    {
        private readonly Day0x day = new ();

        [Fact]
        public void Part01Test1()
        {
            string input = """
testdata
""";
            Assert.Equal("", day.SolvePart1(input));
        }

        public void Part02Test1()
        {
            string input = """
testdata
""";
            Assert.Equal("", day.SolvePart2(input));
        }
    }
}