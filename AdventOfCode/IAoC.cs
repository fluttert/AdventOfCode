namespace AdventOfCode
{
    /// <summary>
    /// Interface for Advent of Code, where each challenge has 2 parts
    /// </summary>
    public interface IAoC
    {
        /// <summary>
        /// Solves part 1 of an Advent of Code challenge day
        /// </summary>
        /// <param name="input">string input</param>
        /// <returns>string (usually a number)</returns>
        string SolvePart1(string input);

        /// <summary>
        /// Solves part 2 of an Advent of Code challenge day
        /// </summary>
        /// <param name="input">string input</param>
        /// <returns>string (usually a number)</returns>
        string SolvePart2(string input);

        /// <summary>
        /// Gets the challenge input associated with the day
        /// </summary>
        /// <returns></returns>
        string GetInput();
    }
}