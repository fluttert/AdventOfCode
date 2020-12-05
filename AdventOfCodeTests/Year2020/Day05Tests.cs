using AdventOfCode.Year2020;
using Xunit;

namespace AdventOfCodeTests.Year2020
{
    public class Day05Tests
    {
        private readonly Day05 day = new Day05();

        [Fact]
        public void Part01()
        {
            Assert.Equal(44, day.BinarySeats("FBFBBFF"));
            Assert.Equal(5, day.BinarySeats("RLR"));
        }

        [Fact]
        public void Part02()
        {
            // empty
        }
    }
}