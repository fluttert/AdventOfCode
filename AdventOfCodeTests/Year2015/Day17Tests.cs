using AdventOfCode.Year2015;
using System;
using System.Collections.Generic;
using Xunit;

namespace AdventOfCodeTests
{
    public class Day17Tests
    {
        private readonly Day17 day = new Day17();

        [Fact]
        public void Part01()
        {
            string input = @"20
15
10
5
5";         
            Assert.Equal("4", day.CombineContainers(input,25).Item1);
           
        }

       



        [Fact]
        public void Part02()
        {
            string input = @"20
15
10
5
5";
            Assert.Equal("3", day.CombineContainers(input, 25).Item2);
        }
    }
}