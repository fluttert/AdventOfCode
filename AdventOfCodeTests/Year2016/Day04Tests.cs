using AdventOfCode.Year2016;
using Xunit;

namespace AdventOfCodeTests.Year2016
{
    public class Day04Tests
    {
        private readonly Day04 day = new Day04();

        [Fact]
        public void Part01()
        {
            string input = @"aaaaa-bbb-z-y-x-123[abxyz]
a-b-c-d-e-f-g-h-987[abcde]
not-a-real-room-404[oarel]
totally-real-room-200[decoy]";
            Assert.Equal("1514", day.SolvePart1(input));
        }

        [Fact]
        public void Part02()
        {
            // no tests
        }
    }
}