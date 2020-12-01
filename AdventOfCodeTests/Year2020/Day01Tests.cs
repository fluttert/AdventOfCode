using AdventOfCode.Year2020;
using Xunit;

namespace AdventOfCodeTests.Year2020
{
    public class Day01Tests
    {
        private readonly Day01 day01 = new Day01();

        [Fact]
        public void Part01()
        {
            string input = @"1721
979
366
299
675
1456";


            Assert.Equal("514579", day01.SolvePart1(input));
        }

        [Fact]
        public void Part02()
        {
            // empty
        }
    }
}