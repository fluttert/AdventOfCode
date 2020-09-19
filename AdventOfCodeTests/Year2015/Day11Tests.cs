using AdventOfCode.Year2015;
using Xunit;

namespace AdventOfCodeTests
{
    public class Day11Tests
    {
        private readonly Day11 day = new Day11();

        [Fact]
        public void Part01()
        {
            Assert.False(day.PasswordRequirements("vzbxppqr".ToCharArray()));
            Assert.False(day.PasswordRequirements("hijklmmn".ToCharArray()));
            Assert.False(day.PasswordRequirements("abbceffg".ToCharArray()));
            Assert.False(day.PasswordRequirements("abbcegjk".ToCharArray()));
        }

        [Fact]
        public void Part02()
        {
            // no tests
        }
    }
}