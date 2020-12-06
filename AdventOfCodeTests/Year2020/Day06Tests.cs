using AdventOfCode.Year2020;
using Xunit;

namespace AdventOfCodeTests.Year2020
{
    public class Day06Tests
    {
        private readonly Day06 day = new Day06();

        [Fact]
        public void Part02()
        {
            string input = @"abc

a
b
c

ab
ac

a
a
a
a

b
";
            Assert.Equal("6", day.SolvePart2(input));
        }

       
    }
}