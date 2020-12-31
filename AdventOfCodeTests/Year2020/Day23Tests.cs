using AdventOfCode.Year2020;
using Xunit;

namespace AdventOfCodeTests.Year2020
{
    public class Day23Tests
    {
        private readonly Day23 day = new Day23();

        [Fact]
        public void InputParseTest()
        {
            string input = "389125467";
            int[] result = new int[] { 3, 8, 9, 1, 2, 5, 4, 6, 7 };
            Assert.Equal(result, day.ParseStatePart01(input));
        }

        [Fact]
        public void MovePart01Test()
        {
            int[] input = new int[] { 3, 8, 9, 1, 2, 5, 4, 6, 7 };
            int[] result = new int[] { 3, 2, 8, 9, 1, 5, 4, 6, 7 };
            var res = day.PlayRound(0, input);
            Assert.Equal(result, res.cups);
            Assert.Equal(0, res.startCup);
        }

        [Fact]
        public void MovePart02Test()
        {
            int[] input = new int[] { 3, 2, 8, 9, 1, 5, 4, 6, 7 };
            int[] result = new int[] { 3, 2, 5, 4, 6, 7, 8, 9, 1 };
            var res = day.PlayRound(1, input);
            Assert.Equal(result, res.cups);
            Assert.Equal(1, res.startCup);
        }

        [Fact]
        public void MovePart03Test()
        {
            int[] input = new int[] { 7, 2, 5, 8, 4, 1, 9, 3, 6 };
            int[] result = new int[] { 2, 5, 8, 3, 6, 7, 4, 1, 9 };
            var res = day.PlayRound(6, input);
            Assert.Equal(result, res.cups);
            Assert.Equal(8, res.startCup);
        }

        [Fact]
        public void MovePart04Test()
        {
            int[] input = new int[] { 9, 2, 5, 8, 4, 1, 3, 6, 7 };
            int[] result = new int[] { 9, 3, 6, 7, 2, 5, 8, 4, 1 };
            var res = day.PlayRound(5, input);
            Assert.Equal(result, res.cups);
            Assert.Equal(8, res.startCup);
        }

        [Fact]
        public void Part01Test01()
        {
            string input = "389125467";
            Assert.Equal("67384529", day.SolvePart1(input));
        }

        [Fact]
        public void Part02InputTest1()
        {
            string input = "1234";
            int[] result = new int[] { 0, 2, 3, 4, 1 };
            Assert.Equal(result, day.ParseStatePart02(input, 4));
        }

        [Fact]
        public void Part02InputTest2()
        {
            string input = "1234";
            int[] result = new int[] { 0, 2, 3, 4, 5, 6, 1 };
            Assert.Equal(result, day.ParseStatePart02(input, 6));
        }

        [Fact]
        public void Part02InputTest3()
        {
            string input = "23154";
            // 2  3  1
            //
            int[] result = new int[] { 0, 5, 3, 1, 6, 4, 7, 2 };
            Assert.Equal(result, day.ParseStatePart02(input, 7));
        }

        [Fact]
        public void Part02InputTest4()
        {
            string input = "23154";
            int[] result = new int[] { 0, 5, 3, 1, 2, 4 };
            Assert.Equal(result, day.ParseStatePart02(input, 5));
        }

        [Fact]
        public void Part02RetestPart1()
        {
            string input = "318946572";
            int[] result1 = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int[] result2 = new int[] { 0, 5, 8, 7, 3, 2, 4, 9, 6, 1 };
            Assert.Equal(result2, day.PlayGame(input,9,100));
        }

        [Fact]
        public void Part02SampleTest()
        {
            string input = "389125467";            
            Assert.Equal("149245887792", day.SolvePart2(input));
        }
    }
}