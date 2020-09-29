using AdventOfCode.Year2015;
using System;
using Xunit;

namespace AdventOfCodeTests
{
    public class Day23Tests
    {
        private readonly Day23 day = new Day23();

        [Fact]
        public void Part01()
        {
            string input = @"inc a
jio a, +2
tpl a
inc a";

            Assert.Equal("2", day.Computer(input.Split(Environment.NewLine)).registerA.ToString());
        }

        [Fact]
        public void Part02()
        {
           // no tests
        }
    }
}