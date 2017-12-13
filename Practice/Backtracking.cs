using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice
{
    public class Backtracking
    {
        #region Combination Sum http://www.practice.geeksforgeeks.org/problem-page.php?pid=1264
        public static void CombinationSumMain()
        {
            int[] array = new int[] { 2, 4, 6, 8 };
            int sum = 8;
            List<List<int>> foundCombinations = new List<List<int>>();
            findCombinationSum(array, sum, new List<int>(), 0 , foundCombinations, 0);
            foreach (var list in foundCombinations)
            {
                foreach (var item in list)
                {
                    Console.Write(item + " ");
                }
                Console.WriteLine("");
            }
        } 

        static void findCombinationSum(int[] array, int sum, List<int> currentNumbers, int tempSum, List<List<int>> foundCombinations, int x)
        {
            Console.Write("tried: ");
            foreach (var item in currentNumbers)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
            if (tempSum == sum)
            {
                List<int> temp = new List<int>();
                foreach (var item in currentNumbers)
                {
                    temp.Add(item);
                }

                foundCombinations.Add(temp);
            }
            //else if (tempSum > sum)
            //{
            //    tempSum = tempSum - currentNumbers[currentNumbers.Count - 1];
            //    currentNumbers.RemoveAt(currentNumbers.Count - 1);
            //}
            else if (tempSum < sum)
            {
                if (x == array.Count())
                    return;
                // add x
                tempSum = tempSum + array[x];
                currentNumbers.Add(array[x]);

                // find by x
                findCombinationSum(array, sum, currentNumbers, tempSum, foundCombinations, x);

                //remove last
                tempSum = tempSum - currentNumbers[currentNumbers.Count - 1];
                currentNumbers.RemoveAt(currentNumbers.Count - 1);

                findCombinationSum(array, sum, currentNumbers, tempSum, foundCombinations, x + 1);
            }
        }

        #endregion
    }
}
