namespace AdventOfCode.Year2017
{
    public class Day17 : IAoC
    {
        public string SolvePart1(string input)
        {
            int stepsForward = int.Parse(input);
            int[] buffer = new int[2018];
            buffer[1] = 1;
            int length = 2, position = 1;
            while (length < 2018)
            {
                position = (position + stepsForward) % length;

                // move all stuff further
                for (int i = length; i > position; i--)
                {
                    buffer[i] = buffer[i - 1];
                }
                buffer[position + 1] = length;

                position++;
                length++;
            }
            return buffer[((position + 1) % length)].ToString();
        }

        public string SolvePart2(string input)
        {
            int bufferLength = 50000000;
            int stepsForward = int.Parse(input);
            int length = 2, position = 1, result = 1;
            while (length < bufferLength)
            {
                position = (position + stepsForward) % length;
                if (position == 0) { result = length; }
                position++;
                length++;
            }

            return result.ToString();
        }

        public string GetInput() => "376";
    }
}