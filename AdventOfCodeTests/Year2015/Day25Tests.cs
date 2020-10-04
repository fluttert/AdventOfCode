using AdventOfCode.Year2015;
using Xunit;

namespace AdventOfCodeTests
{
    public class Day25Tests
    {
        private readonly Day25 day = new Day25();

        [Fact]
        public void Part01()
        {
        }

        [Fact]
        public void Part02()
        {
        }

        [Fact]
        public void TestGrid()
        {
            var grid = day.CreateGrid(6, 1);
            Assert.Equal(1, grid[0][0]);
            Assert.Equal(2, grid[1][0]);
            Assert.Equal(3, grid[0][1]);
            Assert.Equal(12, grid[3][1]);
            Assert.Equal(15, grid[0][4]);
            Assert.Equal(21, grid[0][5]);
        }
    }
}