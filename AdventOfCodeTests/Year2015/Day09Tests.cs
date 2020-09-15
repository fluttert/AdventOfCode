using AdventOfCode.Year2015;
using Xunit;

namespace AdventOfCodeTests
{
    public class Day09Tests
    {
        private readonly Day09 day = new Day09();

        [Fact]
        public void Part01()
        {
            // in Verbatim/literal string the " is escaped by "" :|
            string testInput = @"London to Dublin = 464
London to Belfast = 518
Dublin to Belfast = 141";
            Assert.Equal("605", day.SolvePart1(testInput));
           
        }

        [Fact]
        public void Part02()
        {
         
            
        }
    }
}