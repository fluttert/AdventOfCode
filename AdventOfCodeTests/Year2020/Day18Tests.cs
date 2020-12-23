using AdventOfCode.Year2020;
using Xunit;

namespace AdventOfCodeTests.Year2020
{
    public class Day18Tests
    {
        private readonly Day18 day = new Day18();

        [Fact]
        public void Part01Test1()
        {
            string input = @"1 + 2 * 3 + 4 * 5 + 6";
            Assert.Equal("71", day.SolvePart1(input));
        }
        [Fact]
        public void Part01Test2()
        {
            string input = @"1 + (2 * 3) + (4 * (5 + 6))";
            Assert.Equal("51", day.SolvePart1(input));
        }

        [Fact]
        public void Part01Test3()
        {
            string input = @"2 * 3 + (4 * 5)";
            Assert.Equal("26", day.SolvePart1(input));
        }

        [Fact]
        public void Part01Test4()
        {
            string input = @"5 + (8 * 3 + 9 + 3 * 4 * 3)";
            Assert.Equal("437", day.SolvePart1(input));
        }

        [Fact]
        public void Part01Test5()
        {
            string input = @"5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))";
            Assert.Equal("12240", day.SolvePart1(input));
        }

        [Fact]
        public void Part01Test6()
        {
            string input = @"((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2";
            Assert.Equal("13632", day.SolvePart1(input));
        }

        [Fact]
        public void Part02Test1()
        {
            string input = @"((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2";
            Assert.Equal("23340", day.SolvePart2(input));
        }

        [Fact]
        public void Part02Test1a()
        {
            string input = @"(5 + 6)";
            Assert.Equal("11", day.SolvePart2(input));
        }

        [Fact]
        public void Part02Test2()
        {
            string input = @"1 + (2 * 3) + (4 * (5 + 6))";
            Assert.Equal("51", day.SolvePart2(input));
        }

        [Fact]
        public void Part02Test3()
        {
            string input = @"2 * 3 + (4 * 5)";
            Assert.Equal("46", day.SolvePart2(input));
        }

        [Fact]
        public void Part02Test4()
        {
            string input = @"5 + (8 * 3 + 9 + 3 * 4 * 3)";
            Assert.Equal("1445", day.SolvePart2(input));
        }

        [Fact]
        public void Part02Test5()
        {
            string input = @"5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))";
            Assert.Equal("669060", day.SolvePart2(input));
        }


    }
}