using AdventOfCode.Year2020;
using Xunit;

namespace AdventOfCodeTests.Year2020
{
    public class Day25Tests
    {
        private readonly Day25 day = new Day25();

        [Fact]
        public void Part01SecretLoop()
        {
            Assert.Equal(5764801, day.SecretLoop(7, 8));
            Assert.Equal(17807724, day.SecretLoop(7, 11));
        }

        [Fact]
        public void Part01ReverseSecretLoop()
        {
            Assert.Equal(8, day.ReverseSecretLoop(5764801));
            Assert.Equal(11, day.ReverseSecretLoop(17807724));
        }
    }
}