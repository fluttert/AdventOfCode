using AdventOfCode.Year2015;
using Xunit;

namespace AdventOfCodeTests
{
    public class Day08Tests
    {
        private readonly Day08 day = new Day08();

        [Fact]
        public void Part01()
        {
            // in Verbatim/literal string the " is escaped by "" :|
            string testInput = @"""""
""abc""
""aaa\""aaa""
""\x27""";
            Assert.Equal("12", day.SolvePart1(testInput));
            Assert.Equal("5", day.SolvePart1(@"""\x27"""));
            Assert.Equal("2", day.SolvePart1(@""""""));
            Assert.Equal("3", day.SolvePart1(@"""aaa\""aaa"""));
            Assert.Equal("2", day.SolvePart1(@"""aaa"""));
        }

        [Fact]
        public void Part02()
        {
            Assert.Equal("4", day.SolvePart2(@""""""));
            Assert.Equal("4", day.SolvePart2(@"""aaa"""));
            Assert.Equal("5", day.SolvePart2(@"""\x27"""));
            
            Assert.Equal("6", day.SolvePart2(@"""aaa\""aaa"""));
            
        }
    }
}