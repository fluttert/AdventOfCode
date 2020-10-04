using System;
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
        /// <returns>duplicate of the original list (deepcopy)</returns>
        public static List<T> Duplicate<T>(List<T> original)
        {
            var output = new List<T>();
            for (int i = 0; i < original.Count; i++)
            {
                output.Add(original[i]);
            }
            return output;
        }

        /// <summary>
        /// Duplicate an array, using copy mechanism
        /// </summary>
        /// <typeparam name="T">Type of the array</typeparam>
        /// <param name="original">The original array to be copied</param>
        /// <returns>duplicate of original array (deepcopy)</returns>
        public static T[] Duplicate<T>(T[] original) {
            var duplicate = new T[original.Length];
            Array.Copy(original, duplicate, original.Length);
            return duplicate;
        }

        /// <summary>
        /// Duplicate an array and add an element, using copy mechanism
        /// </summary>
        /// <typeparam name="T">Type of the array</typeparam>
        /// <param name="original">The original array to be copied</param>
        /// <returns>duplicate of original array (deepcopy)</returns>
        public static T[] DuplicateAndAddOneElement<T>(T[] original, T newElement)
        {
            var duplicate = new T[original.Length + 1];
            Array.Copy(original, duplicate, original.Length);
            duplicate[^1] = newElement;
            return duplicate;
        }
    }
}