using AdventOfCode.Year2022;
using Xunit;

namespace AdventOfCodeTests.Year2022
{
    public class Day13Tests
    {
        private readonly Day13 day = new Day13();

//        [Fact]
//        public void Part01Test1()
//        {
//            string input = """
//[1,1,3,1,1]
//[1,1,5,1,1]
//""";
//            Assert.Equal("1", day.SolvePart1(input));
//        }

        public void Part01Test2()
        {
            string input = """
[[1],[2,3,4]]
[[1],4]
""";
            Assert.Equal("1", day.SolvePart1(input));
        }
    }
}