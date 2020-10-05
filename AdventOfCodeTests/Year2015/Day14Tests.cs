using AdventOfCode.Year2015;
using Xunit;

namespace AdventOfCodeTests.Year2015
{
    public class Day14Tests
    {
        private readonly Day14 day = new Day14();

        [Fact]
        public void Part01()
        {
            string input = @"Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds.
Dancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds.";
            
            Assert.Equal("1120", day.SolvePart1(input,1000));
           
        }

        [Fact]
        public void Part02()
        {
            string input = @"Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds.
Dancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds.";

            Assert.Equal("689", day.SolvePart2(input, 1000));
        }
    }
}