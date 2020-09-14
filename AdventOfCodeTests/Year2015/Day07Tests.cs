using AdventOfCode.Year2015;
using Xunit;

namespace AdventOfCodeTests
{
    public class Day07Tests
    {
        private readonly Day07 day = new Day07();

        [Fact]
        public void Part01()
        {
            string testInput = @"123 -> x
456 -> y
x AND y -> d
x OR y -> e
x LSHIFT 2 -> f
y RSHIFT 2 -> a
NOT x -> h
NOT y -> i";
            Assert.Equal("114", day.SolvePart1(testInput));

        }

        [Fact]
        public void BitwiseNot() {
            Assert.Equal((ushort)65412, day.bitwiseNot((ushort)123));
            Assert.Equal((ushort)65079, day.bitwiseNot((ushort)456));
        }

        [Fact]
        public void Part02()
        {
            //Assert.Equal("0", day.SolvePart2("ab"));

        }
    }
}