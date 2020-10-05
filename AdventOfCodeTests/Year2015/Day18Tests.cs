using AdventOfCode.Year2015;
using Xunit;

namespace AdventOfCodeTests.Year2015
{
    public class Day18Tests
    {
        private readonly Day18 day = new Day18();

        [Fact]
        public void Part01()
        {
            string input = @".#.#.#
...##.
#....#
..#...
#.#..#
####..";
            var grid = day.ConvertToLightGrid(input);
            grid = day.AnimateGrid(grid, 4);
            Assert.Equal(4, day.CountLights(grid));
        }

        [Fact]
        public void Part02()
        {
            string input = @".#.#.#
...##.
#....#
..#...
#.#..#
####..";
            var grid = day.ConvertToLightGrid(input);
            grid = day.AnimateGrid(grid, steps: 5, lightsOnInCorner: true);
            Assert.Equal(17, day.CountLights(grid));
        }
    }
}