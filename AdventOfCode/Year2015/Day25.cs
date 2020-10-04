namespace AdventOfCode.Year2015
{
    // Challenge can be found on: https://adventofcode.com/2015/day/25
    public class Day25 : IAoC
    {
        public string SolvePart1(string input)
        {
            int dimension = 7_000;
            var grid = new long[dimension][];
            for (int i = 0; i < grid.Length; i++)
            {
                grid[i] = new long[dimension];
            }

            // populate
            int startRow = 0, x = startRow, y = 0;
            long nextNumber = 20151125;
            while (startRow < dimension)
            {
                grid[x][y] = nextNumber;
                x--; y++;

                nextNumber = (nextNumber * 252533L) % 33554393L;

                // reset! next row
                if (x < 0)
                {
                    startRow++;
                    x = startRow;
                    y = 0;
                }
            }
            return grid[2980][3074].ToString();
        }

        public string SolvePart2(string input)
        {
            return "";
        }

        public string GetInput()
        {
            return "To continue, please consult the code grid in the manual. Enter the code at row 2981, column 3075.";
        }

        // testing out how to create the grid
        public long[][] CreateGrid(long dimension, long startNumber = 1)
        {
            var grid = new long[dimension][];
            for (int i = 0; i < grid.Length; i++)
            {
                grid[i] = new long[dimension];
            }

            // populate
            int startRow = 0, x = startRow, y = 0;
            long nextNumber = startNumber;
            while (startRow < dimension)
            {
                grid[x][y] = nextNumber;
                x--; y++;
                nextNumber++;

                // reset! next row
                if (x < 0)
                {
                    startRow++;
                    x = startRow;
                    y = 0;
                }
            }
            return grid;
        }
    }
}