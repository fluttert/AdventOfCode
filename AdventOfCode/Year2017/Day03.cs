namespace AdventOfCode.Year2017
{
    public class Day03 : IAoC
    {
        /// <summary>
        /// Manhattan distance to the center of a spiral-number-sequence
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string SolvePart1(string input)
        {
            int candidate = int.Parse(input);
            int manhattanDistance = 0;
            if (candidate == 1) { return manhattanDistance.ToString(); }

            // determine side, as each side*side is 1*1, 3*3, 5*5, 7*7, etc
            int stepsFromCenter = 0;
            int side = 1;
            while ((side * side) < candidate)
            {
                stepsFromCenter++;
                side += 2;
            }

            // shortest way in cross-form, seen from the center.
            int[] minimumsteps = new int[] {
                (side * side) - 1* stepsFromCenter,
                (side * side) - 3* stepsFromCenter,
                (side * side) - 5 *stepsFromCenter,
                (side * side) - 7* stepsFromCenter
            };

            // Go from the stepsFromCenter(closest) towards the diagonals (furthers away)
            for (int i = 0; i <= stepsFromCenter; i++)
            {
                foreach (int minstep in minimumsteps)
                {
                    if (minstep + i == candidate || minstep - i == candidate)
                    {
                        manhattanDistance = stepsFromCenter + i;
                        break;
                    }
                }
                if (manhattanDistance != 0) { break; }
            }
            return manhattanDistance.ToString();
        }

        /// <summary>
        /// Find when there is a number bigger then the input in a custom spiral
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string SolvePart2(string input)
        {
            // init
            int result = 0;
            int dimension = 501;
            int offset = (dimension / 2) + 1;
            int candidate = int.Parse(input);

            // create a grid, the last one had a minimum stepsFromCenter of 303, and this thing should grow very fast
            // estimated guess is that the required spiral will not be bigger then 501*501
            int[][] grid = new int[dimension][];
            for (int i = 0; i < dimension; i++) { grid[i] = new int[dimension]; }
            grid[offset][offset] = 1;

            // some code to get the sprial in the right direction
            int curX = offset, curY = offset;
            Direction heading = Direction.EAST;
            while (result == 0)
            {
                Direction nextHeading = heading;
                if (heading == Direction.EAST)
                {
                    curX++;
                    if (grid[curX][curY + 1] == 0) { nextHeading = Direction.NORTH; }
                }
                if (heading == Direction.WEST)
                {
                    curX--;
                    if (grid[curX][curY - 1] == 0) { nextHeading = Direction.SOUTH; }
                }
                if (heading == Direction.NORTH)
                {
                    curY++;
                    if (grid[curX - 1][curY] == 0) { nextHeading = Direction.WEST; }
                }
                if (heading == Direction.SOUTH)
                {
                    curY--;
                    if (grid[curX + 1][curY] == 0) { nextHeading = Direction.EAST; }
                }

                // captain, should be change course?
                heading = nextHeading;

                // get me the sum of adjecent squares
                int sum = 0;
                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        sum += grid[curX + i][curY + j];
                    }
                }

                // solution check
                grid[curX][curY] = sum;
                if (sum > candidate)
                {
                    result = sum;
                }
            }

            return result.ToString();
        }

        private enum Direction
        {
            EAST,
            NORTH,
            WEST,
            SOUTH
        }

        public string GetInput() => @"368078";
    }
}