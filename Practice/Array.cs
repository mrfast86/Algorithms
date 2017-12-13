using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice
{
    using System.Data;

    public static class Array
    {
        #region Get Max Product
        public static void GetMaxProduct()
        {
            //http://www.geeksforgeeks.org/find-maximum-product-of-a-triplet-in-array/
            int[] arr = new int[] { 1, -4, 3, -6, 7, 0 };
            int n = arr.Count();

            int max = maxProduct(arr, n);

            if (max == -1)
                Console.WriteLine("No Triplet Exists");
            else
                Console.WriteLine("Maximum product is " + max);
        }

        private static int maxProduct(int[] arr, int n)
        {
            int max1 = 0;
            int max2 = 0;
            int max3 = 0;
            int min1 = 0;
            int min2 = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                // update max 1, biggest
                if (arr[i] > max1)
                {
                    max3 = max2;
                    max2 = max1;
                    max1 = arr[i];
                }
                else if (arr[i] > max2)
                {
                    max3 = max2;
                    max2 = arr[i];
                }
                else if (arr[i] > max3)
                {
                    max3 = arr[i];
                }
                else if (arr[i] < min1)
                {
                    min2 = min1;
                    min1 = arr[i];
                }
                else if (arr[i] < min2)
                {
                    min2 = arr[i];
                }
            }

            return Math.Max(min1 * min2 * max1, max1 * max2 * max3);
        }
        #endregion

        #region Stock problem - max one stock hold, must sell before another buy.  unlim transactions

        public static void StackProblemMain()
        {
            int[] s = { 3, 2, 4, 1, 5 };
            Console.WriteLine(findProfit(s));
        }

        static int findProfit(int[] s)
        {
            int min = s[0]; // set first as min
            int max = 0;
            int profit = 0;

            for (int i = 1; i < s.Length; i++)
            {
                if (s[i] < max)
                {
                    // sell
                    if (max > min)
                    {
                        profit += max - min;
                        min = s[i];
                        max = 0;
                    }
                }
                else if (s[i] < min)
                {
                    min = s[i];
                }
                else if (s[i] > max)
                {
                    max = s[i];
                    if (i == s.Length-1 && max > min)
                    {
                        profit += max - min;
                        min = s[i];
                        max = 0;
                    }
                }
            }

            return profit;
        }

        #endregion

        #region Prison Break
        // hackerrank test question

        public static void PrisonBreakMain()
        {
            int[] rArray = {2,3};
            int[] cArray = { 2,3 };

            Console.WriteLine(prison(4, 4, rArray, cArray));
        }

        static long prison(int rows, int cols, int[] rArray, int[] cArray)
        {
            // Construct hashsets for horizontal/vertical bars
            bool[] horBars = new bool[rows+2];
            bool[] verBars = new bool[cols+2];

            // Scofield removes bars!! (set removed to true)
            removeBars(horBars, rArray);
            removeBars(verBars, cArray);

            int maxHorDiff = getMaxContinuous(horBars);
            int maxVerDiff = getMaxContinuous(verBars);

            return maxHorDiff * maxVerDiff;
        }

        static void removeBars(bool[] bars, int[] toRemove)
        {
            foreach (int r in toRemove)
            {
                bars[r] = true;
            }
        }

        static int getMaxContinuous(bool[] bars)
        {
            int maxDiff = 1;
            int tempDiff = 1;
            for (int i = 0; i < bars.Length; i++)
            {
                if (bars[i])
                {
                    tempDiff++;
                    if (tempDiff > maxDiff) maxDiff = tempDiff;
                }
                else
                {
                    tempDiff = 1;
                }
            }

            return maxDiff;
        }

        #endregion

        public static void AreEqualMain()
        {
            int[] a = { 1, 2, 5, 4, 0 };
            int[] b = { 2, 4, 5, 0, 1 };

            Console.WriteLine(areEqual(a, b));
        }

        private static bool areEqual(int[] a, int[] b)
        {
            Dictionary<int, int> intCount = new Dictionary<int, int>();
            foreach (int i in a)
            {
                if (intCount.ContainsKey(i))
                    intCount[i] += 1;
                else
                    intCount.Add(i, 1);
            }

            foreach (int i in b)
            {
                if (!intCount.ContainsKey(i))
                    return false;

                intCount[i] -= 1;
            }

            foreach (var i in intCount)
            {
                if (i.Value != 0)
                    return false;
            }

            return true;
        }

        public static void TwoSumMain()
        {
            int[] a = new int[]{ 2, 7, 8, 9, 15 };
            int target = 9;
            int[] result = TwoSum(a, target);
            if (result != null && result.Any())
                Console.WriteLine(result[0] + ", " + result[1]);
        }

        private static int[] TwoSum(int[] a, int target)
        {
            if (a == null || a.Length < 2)
                return null;

            Dictionary<int, int> valueToIndexDictionary = new Dictionary<int, int>();
            int index = -1;
            for (int i = 0; i < a.Length; i++)
            {
                if (valueToIndexDictionary.TryGetValue(a[i], out index))
                {
                    return new int[] { a[i], a[index] };
                }
                else
                {
                    valueToIndexDictionary.Add(target - a[i], i);
                }
            }

            return null;
        }
    }
}
