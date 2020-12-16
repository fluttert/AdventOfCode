using AdventOfCode.Year2020;
using System;
using Xunit;

namespace AdventOfCodeTests.Year2020
{
    public class Day11Tests
    {
        private readonly Day11 day = new Day11();

        [Fact]
        public void Part01Test1()
        {
            string input = @"L.LL.LL.LL
LLLLLLL.LL
L.L.L..L..
LLLL.LL.LL
L.LL.LL.LL
L.LLLLL.LL
..L.L.....
LLLLLLLLLL
L.LLLLLL.L
L.LLLLL.LL";
            Assert.Equal("37", day.SolvePart1(input));
        }

        [Fact]
        public void Part02Test1()
        {
            string input = @"L.LL.LL.LL
LLLLLLL.LL
L.L.L..L..
LLLL.LL.LL
L.LL.LL.LL
L.LLLLL.LL
..L.L.....
LLLLLLLLLL
L.LLLLLL.L
L.LLLLL.LL";
            Assert.Equal("26", day.SolvePart2(input));
        }

        [Fact]
        public void Part02Test2()
        {
            string input = @"L.LL.LL.LL
LLLLLLL.LL
L.L.L..L..
LLLL.LL.LL
L.LL.LL.LL
L.LLLLL.LL
..L.L.....
LLLLLLLLLL
L.LLLLLL.L
L.LLLLL.LL";
            string output = @"#.##.##.##
#######.##
#.#.#..#..
####.##.##
#.##.##.##
#.#####.##
..#.#.....
##########
#.######.#
#.#####.##";
            var actualOutput = string.Join(Environment.NewLine, day.UpdateMap(input.Split(Environment.NewLine), false));

            Assert.Equal(output, actualOutput);
        }

        [Fact]
        public void Part02Test3()
        {
            string input = @"#.##.##.##
#######.##
#.#.#..#..
####.##.##
#.##.##.##
#.#####.##
..#.#.....
##########
#.######.#
#.#####.##";
            string output = @"#.LL.LL.L#
#LLLLLL.LL
L.L.L..L..
LLLL.LL.LL
L.LL.LL.LL
L.LLLLL.LL
..L.L.....
LLLLLLLLL#
#.LLLLLL.L
#.LLLLL.L#";
            var actualOutput = string.Join(Environment.NewLine, day.UpdateMap(input.Split(Environment.NewLine), false));

            Assert.Equal(output, actualOutput);
        }


        [Fact]
        public void Part02Test4()
        {
            string input = @"#.LL.LL.L#
#LLLLLL.LL
L.L.L..L..
LLLL.LL.LL
L.LL.LL.LL
L.LLLLL.LL
..L.L.....
LLLLLLLLL#
#.LLLLLL.L
#.LLLLL.L#";
            string output = @"#.L#.##.L#
#L#####.LL
L.#.#..#..
##L#.##.##
#.##.#L.##
#.#####.#L
..#.#.....
LLL####LL#
#.L#####.L
#.L####.L#";
            var actualOutput = string.Join(Environment.NewLine, day.UpdateMap(input.Split(Environment.NewLine), false));

            Assert.Equal(output, actualOutput);
        }
    }
}