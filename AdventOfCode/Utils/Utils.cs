using System.Collections.Generic;

namespace AdventOfCode.Utils
{
    public class Utils
    {
        /// <summary>
        /// Duplicate a list, with no reference to the original (aka DeepCopy)
        /// </summary>
        /// <typeparam name="T">Type of List</typeparam>
        /// <param name="original">original list</param>
        /// <returns>duplicate of the original list</returns>
        public static List<T> Duplicate<T>(List<T> original)
        {
            var output = new List<T>();
            for (int i = 0; i < original.Count; i++)
            {
                output.Add(original[i]);
            }
            return output;
        }
    }
}