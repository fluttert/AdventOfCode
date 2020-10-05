using AdventOfCode.Year2015;
using Xunit;

namespace AdventOfCodeTests.Year2015
{
    public class Day19Tests
    {
        private readonly Day19 day = new Day19();

        [Fact]
        public void Part01()
        {
            string input = @"H => HO
H => OH
O => HH

HOH";

            Assert.Equal("4", day.SolvePart1(input));

            input = @"H => HO
H => OH
O => HH

HOHOHO";

            Assert.Equal("7", day.SolvePart1(input));
        }

        [Fact]
        public void Part02()
        {
            string input = @"e => H
e => O
H => HO
H => OH
O => HH

HOH";

            Assert.Equal("3", day.SolvePart2(input));

            input = @"e => H
e => O
H => HO
H => OH
O => HH

HOHOHO";

            Assert.Equal("6", day.SolvePart2(input));
        }
    }
}