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
        /// Duplicate a hashset, with no reference to the original (aka DeepCopy)
        /// </summary>
        /// <typeparam name="T">Type of HashSet</typeparam>
        /// <param name="original">original HashSet</param>
        /// <returns>duplicate of the original HashSet (deepcopy)</returns>
        public static HashSet<T> Duplicate<T>(HashSet<T> original)
        {
            var output = new HashSet<T>();
            foreach (T item in original) { 
                output.Add(item);
            }
            return output;
        }


        /// <summary>
        /// Duplicate a Dictionary, with no reference to the original (aka DeepCopy)
        /// </summary>
        /// <typeparam name="TKey">Type of Dictionary Keys</typeparam>
        /// <typeparam name="TValue">Type of Dictionary Values</typeparam>
        /// <param name="original">original dictionary</param>
        /// <returns>duplicate of the original Dictionary (deepcopy)</returns>
        public static Dictionary<TKey,TValue> Duplicate<TKey,TValue>(Dictionary<TKey,TValue> original)
        {
            var output = new Dictionary<TKey, TValue>();
            foreach (var item in original)
            {
                output.Add(item.Key,item.Value);
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


        /// <summary>
        /// Return difference an Array and a subset of that array
        /// </summary>
        /// <remarks>Array and Subset needs to be in the same order</remarks>
        /// <typeparam name="T"></typeparam>
        /// <param name="completeArray"></param>
        /// <param name="subset"></param>
        /// <returns></returns>
        public static T[] ArrayDifference<T>(T[] completeArray, T[] subset)
        {
            T[] diff = new T[completeArray.Length - subset.Length];
            int i = 0, j = 0, k = 0;
            while (i < completeArray.Length && j < subset.Length)
            {
                // skip if the same
                if (completeArray[i].Equals( subset[j])) { i++; j++; }
                else { diff[k] = completeArray[i]; i++; k++; }
            }
            // add any remaining numbers if whole subset was already added
            while (i < completeArray.Length)
            {
                diff[k] = completeArray[i]; k++; i++;
            }
            return diff;
        }

        /// <summary>Classic GCD, using Euclidian algorithm</summary>
        /// <param name="a">First number</param>
        /// <param name="b">Second number</param>
        /// <returns>Long: biggest common divisor, or 0 (zero) on negative numbers and 0</returns>
        /// <remarks>https://en.wikipedia.org/wiki/Euclidean_algorithm</remarks>
        public static long GreatestCommonDivisor(long a, long b)
        {
            // do a zero check and negativity check
            if (a <= 0 || b <= 0) { return 0; }

            // force a = biggest, b = smallest
            if (b > a) { long t = a; a = b; b = t; }

            // Divide the remainders, until the first factor is found. This first factor will be the biggest factor
            while (b != 0)
            {
                long temp = a % b;  // Keep substracting b from a, untill the remainder is smaller then b
                a = b;              // b is now the biggest factor, so it becomes a
                b = temp;           // the smaller factor will be b (or 0 when a common divider is found)
            }
            return a;
        }

        public static long LeastCommonMultiple(long a, long b) {
            // do a zero check and negativity check
            if (a <= 0 || b <= 0) { return 0; }
            // force a = biggest, b = smallest
            if (b > a) { long t = a; a = b; b = t; }
            return b * (a / GreatestCommonDivisor(a, b));

        }
    }
}