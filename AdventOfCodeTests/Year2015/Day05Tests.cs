using AdventOfCode.Year2015;
using Xunit;

namespace AdventOfCodeTests
{
    public class Day05Tests
    {
        private readonly Day05 day = new Day05();

        [Fact]
        public void Part01()
        {
            Assert.Equal("1", day.SolvePart1("ugknbfddgicrmopn"));
            Assert.Equal("1", day.SolvePart1("aaa"));
            Assert.Equal("0", day.SolvePart1("jchzalrnumimnmhp"));
            Assert.Equal("0", day.SolvePart1("haegwjzuvuyypxyu"));
            Assert.Equal("0", day.SolvePart1("dvszwmarrgswjxmb"));
        }

        [Fact]
        public void Part02()
        {
            Assert.Equal("0", day.SolvePart2("ab"));
            Assert.Equal("1", day.SolvePart2("abab"));
            Assert.Equal("0", day.SolvePart2("aaab"));
            Assert.Equal("1", day.SolvePart2("qjhvhtzxzqqjkmpb"));
            Assert.Equal("1", day.SolvePart2("xxyxx"));
            Assert.Equal("0", day.SolvePart2("uurcxstgmygtbstg"));
            Assert.Equal("0", day.SolvePart2("ieodomkazucvgmuy"));
        }
    }
}