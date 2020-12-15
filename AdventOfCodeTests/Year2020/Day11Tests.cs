using AdventOfCode.Year2020;
using Xunit;

namespace AdventOfCodeTests.Year2020
{
    public class Day11Tests
    {
        private readonly Day11 day = new Day11();

        [Fact]
        public void Part01Test1()
        {
            string input = @"L.LL.LL.LL
LLLLLLL.LL
L.L.L..L..
LLLL.LL.LL
L.LL.LL.LL
L.LLLLL.LL
..L.L.....
LLLLLLLLLL
L.LLLLLL.L
L.LLLLL.LL";
            Assert.Equal("37", day.SolvePart1(input));
        }

      
       
    }
}