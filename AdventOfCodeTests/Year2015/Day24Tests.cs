using AdventOfCode.Year2015;
using Xunit;

namespace AdventOfCodeTests
{
    public class Day24Tests
    {
        private readonly Day24 day = new Day24();

        [Fact]
        public void Part01()
        {
            string input = @"1
2
3
4
5
7
8
9
10
11";

            Assert.Equal("99", day.SolvePart1(input));
        }

        [Fact]
        public void MakeSmallestSets()
        {
            var lists = day.MakeSmallestSets(new int[] { 1, 2, 3 }, 6);
            Assert.Single(lists);

            lists = day.MakeSmallestSets(new int[] { 1, 2, 3 }, 3);
            Assert.Single(lists);

            lists = day.MakeSmallestSets(new int[] { 1, 2, 3 }, 7);
            Assert.Empty(lists);

            lists = day.MakeSmallestSets(new int[] { 1, 2, 3, 4, 5 }, 6);
            Assert.Equal(2, lists.Count);

            lists = day.MakeSmallestSets(new int[] { 1, 2, 3, 4, 5 }, 5);
            Assert.Single(lists);

            lists = day.MakeSmallestSets(new int[] { 1, 2, 3, 4, 5, 6 }, 7);
            Assert.Equal(3, lists.Count);
        }

        [Fact]
        public void ArrayDifferences() { 
            var numbers = new int[]{ 7,6,5,4,3,2,1};
            var subset = new int[] { 6, 3, 1 };
            var diff = new int[] { 7, 5, 4, 2 };
            Assert.Equal(diff, day.ArrayDifference(numbers, subset));
        }

        [Fact]
        public void Sets()
        {
            var lists = day.MakeSets(new int[] { 1, 2, 3 }, 6);
            Assert.Single(lists);

            lists = day.MakeSets(new int[] { 1, 2, 3 }, 3);
            Assert.Equal(2, lists.Count);

            lists = day.MakeSets(new int[] { 1, 2, 3 }, 7);
            Assert.Empty(lists);

            lists = day.MakeSets(new int[] { 1, 2, 3, 4, 5 }, 6);
            Assert.Equal(3, lists.Count);

            lists = day.MakeSets(new int[] { 1, 2, 3, 4, 5 }, 5);
            Assert.Equal(3, lists.Count);

            lists = day.MakeSets(new int[] { 1, 2, 3, 4, 5, 6 }, 6);
            Assert.Equal(4, lists.Count);

            lists = day.MakeSets(new int[] { 1, 2, 3, 4, 5, 6,7 }, 7);
            Assert.Equal(5, lists.Count);
        }

        [Fact]
        public void Part02()
        {
            // no tests
        }
    }
}