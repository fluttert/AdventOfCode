using AdventOfCode.Year2015;
using System;
using Xunit;

namespace AdventOfCodeTests
{
    public class Day01Tests
    {
        private readonly Day01 day01 = new Day01();
        
        [Fact]
        public void Part01()
        {
            Assert.Equal("0", day01.Part1("(())"));
            Assert.Equal("0", day01.Part1("()()"));
            Assert.Equal("3", day01.Part1("((("));
            Assert.Equal("3", day01.Part1("(()(()("));
            Assert.Equal("3", day01.Part1("))((((("));
            Assert.Equal("-1", day01.Part1("())"));
            Assert.Equal("-1", day01.Part1("))("));
            Assert.Equal("-3", day01.Part1(")))"));
            Assert.Equal("-3", day01.Part1(")())())"));
        }
    }
}
