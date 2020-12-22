using AdventOfCode.Year2020;
using Xunit;

namespace AdventOfCodeTests.Year2020
{
    public class Day10Tests
    {
        private readonly Day10 day = new Day10();

        [Fact]
        public void Part01Test1()
        {
            string input = @"16
10
15
5
1
11
7
19
6
12
4";
            Assert.Equal("35", day.SolvePart1(input));
        }

        [Fact]
        public void Part01Test2()
        {
            string input = @"28
33
18
42
31
14
46
20
48
47
24
23
49
45
19
38
39
11
1
32
25
35
8
17
7
9
4
2
34
10
3";
            Assert.Equal("220", day.SolvePart1(input));
        }

        [Fact]
        public void Part02Test1()
        {
            string input = @"16
10
15
5
1
11
7
19
6
12
4";
            Assert.Equal("8", day.SolvePart2(input));
        }

        [Fact]
        public void Part02Test2()
        {
            string input = @"28
33
18
42
31
14
46
20
48
47
24
23
49
45
19
38
39
11
1
32
25
35
8
17
7
9
4
2
34
10
3";
            Assert.Equal("19208", day.SolvePart2(input));
        }
    }
}