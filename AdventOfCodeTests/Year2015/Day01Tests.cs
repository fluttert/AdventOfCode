using AdventOfCode.Year2015;
using Xunit;

namespace AdventOfCodeTests.Year2015
{
    public class Day01Tests
    {
        private readonly Day01 day01 = new Day01();

        [Fact]
        public void Part01()
        {
            Assert.Equal("0", day01.SolvePart1("(())"));
            Assert.Equal("0", day01.SolvePart1("()()"));
            Assert.Equal("3", day01.SolvePart1("((("));
            Assert.Equal("3", day01.SolvePart1("(()(()("));
            Assert.Equal("3", day01.SolvePart1("))((((("));
            Assert.Equal("-1", day01.SolvePart1("())"));
            Assert.Equal("-1", day01.SolvePart1("))("));
            Assert.Equal("-3", day01.SolvePart1(")))"));
            Assert.Equal("-3", day01.SolvePart1(")())())"));
        }

        [Fact]
        public void Part02()
        {
            Assert.Equal("1", day01.SolvePart2(")"));
            Assert.Equal("5", day01.SolvePart2("()())"));
        }
    }
}