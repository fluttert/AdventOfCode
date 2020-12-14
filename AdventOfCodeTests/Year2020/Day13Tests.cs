using AdventOfCode.Year2020;
using Xunit;

namespace AdventOfCodeTests.Year2020
{
    public class Day13Tests
    {
        private readonly Day13 day = new Day13();

        [Fact]
        public void Part01Test1()
        {
            string input = @"939
7,13,x,x,59,x,31,19";
            Assert.Equal("295", day.SolvePart1(input));
        }

//        [Fact]
//        public void Part02Test1()
//        {
//            string input = @"bla
//17,x,13,19";
//            Assert.Equal("3417", day.SolvePart2(input));
//        }

//        [Fact]
//        public void Part02Test2()
//        {
//            string input = @"939
//7,13,x,x,59,x,31,19";
//            Assert.Equal("1068781", day.SolvePart2(input));
//        }

    }
}