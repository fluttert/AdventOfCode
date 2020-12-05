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
            Assert.Equal("357", day.SolvePart1("FBFBBFFRLR"));
        }

        [Fact]
        public void ConvertionToBinary()
        {
            Assert.Equal(0, day.ConvertSeatingToNumber("F"));
            Assert.Equal(1, day.ConvertSeatingToNumber("B"));
            Assert.Equal(0, day.ConvertSeatingToNumber("FF"));
            Assert.Equal(1, day.ConvertSeatingToNumber("FB"));
            Assert.Equal(2, day.ConvertSeatingToNumber("BF"));
            Assert.Equal(3, day.ConvertSeatingToNumber("BB"));
            Assert.Equal(44, day.ConvertSeatingToNumber("FBFBBFF"));
            Assert.Equal(5, day.ConvertSeatingToNumber("RLR"));
        }

        [Fact]
        public void BinarySeatTest()
        {
            Assert.Equal(0, day.BinarySeats("F"));
            Assert.Equal(1, day.BinarySeats("B"));
            Assert.Equal(0, day.BinarySeats("FF"));
            Assert.Equal(1, day.BinarySeats("FB"));
            Assert.Equal(2, day.BinarySeats("BF"));
            Assert.Equal(3, day.BinarySeats("BB"));
            Assert.Equal(44, day.BinarySeats("FBFBBFF"));
            Assert.Equal(5, day.BinarySeats("RLR"));
        }
    }
}