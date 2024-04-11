using AdventOfCode.Year2016;
using Xunit;

namespace AdventOfCodeTests.Year2016
{
    public class Day21Tests
    {
        private readonly Day21 day = new Day21();

        [Fact]
        public void SwapLetterTest()
        {
            char[] line = "ebcda".ToCharArray();
            string[] pieces = "swap letter d with letter b".Split(' ');
            char[] result = new char[] { 'e', 'd', 'c', 'b', 'a' };
            Assert.Equal(result, day.SwapLetter(pieces, line));
        }

        [Fact]
        public void RotateLeftTest()
        {
            char[] line = "abcde".ToCharArray();
            string[] pieces = "rotate left 1 step".Split(' ');
            char[] result = "bcdea".ToCharArray();
            Assert.Equal(result, day.RotateLeft(pieces, line));
        }

        [Fact]
        public void RotateRightTest()
        {
            char[] line = "abcd".ToCharArray();
            string[] pieces = "rotate right 1 step".Split(' ');
            char[] result = "dabc".ToCharArray();
            Assert.Equal(result, day.RotateRight(pieces, line));
        }

        [Fact]
        public void RotateBasedOnLetterTest() {
            char[] password = "ecabd".ToCharArray();
            string[] pieces = "rotate based on position of letter d".Split(' ');
            char[] result = "decab".ToCharArray();
            Assert.Equal(result, day.RotateBasedOnLetter(pieces, password));

            char[] password1 = "abdec".ToCharArray();
            string[] pieces1 = "rotate based on position of letter b".Split(' ');
            char[] result1 = "ecabd".ToCharArray();
            Assert.Equal(result1, day.RotateBasedOnLetter(pieces1, password1));
        }

        [Fact]
        public void ReverseTest() {
            char[] password = "abcde".ToCharArray();
            string[] pieces = "reverse positions 1 through 3".Split(' ');
            char[] result = "adcbe".ToCharArray();
            Assert.Equal(result, day.ReversePosition(pieces, password));
        }

        [Fact]
        public void MovePosition() {
            char[] password = "bcdea".ToCharArray();
            string[] pieces = "move position 1 to position 4".Split(' ');
            char[] result = "bdeac".ToCharArray();
            Assert.Equal(result, day.MovePosition(pieces, password));

            char[] password1 = "bdeac".ToCharArray();
            string[] pieces1 = "move position 3 to position 0".Split(' ');
            char[] result1 = "abdec".ToCharArray();
            Assert.Equal(result1, day.MovePosition(pieces1, password1));

        }

       
        [Fact]
        public void Part02()
        {
            // no tests
        }
    }
}