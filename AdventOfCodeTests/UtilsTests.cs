using Xunit;

namespace AdventOfCodeTests
{
    public class UtilsTests
    {
        [Fact]
        public void ArrayDifferences()
        {
            var numbers = new int[] { 7, 6, 5, 4, 3, 2, 1 };
            var subset = new int[] { 6, 3, 1 };
            var diff = new int[] { 7, 5, 4, 2 };
            Assert.Equal(diff, AdventOfCode.Utils.Utils.ArrayDifference(numbers, subset));

            numbers = new int[] { 7, 6, 5, 4, 3, 2, 1 };
            subset = new int[] { 7, 6, 5 };
            diff = new int[] { 4, 3, 2, 1 };
            Assert.Equal(diff, AdventOfCode.Utils.Utils.ArrayDifference(numbers, subset));

            numbers = new int[] { 7, 6, 5, 4, 3, 2, 1 };
            subset = new int[] { 3, 2, 1 };
            diff = new int[] { 7, 6, 5, 4 };
            Assert.Equal(diff, AdventOfCode.Utils.Utils.ArrayDifference(numbers, subset));
        }
    }
}