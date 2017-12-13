#region License
// -------------------------------------------------------------------------------------------------------
// <copyright file="Sorting.cs" company="Gary Jonas Computing Ltd.">
//     Copyright (c) 2011 - 2016 Gary Jonas Computing Ltd. All Rights Reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------------
#endregion
namespace Practice
{
    using System;

    public static class Sorting
    {
        public static void QuickSort()
        {
            int[] ar = { 5, 8, 1, 3, 7, 9, 2 };

            quickSortPartition(ar, 0, ar.Length - 1);
            printArray(ar, 0, ar.Length - 1);
        }

        static void quickSortPartition(int[] ar, int s, int e)
        {
            if (s >= e)
                return;

            // pick median
            int med = (s + e) / 2;
            int i = s;
            int j = e;

            while (i < j)
            {
                while (ar[i] < ar[med])
                    i++;

                while (ar[med] < ar[j])
                    j--;

                if (ar[i] > ar[j])
                {
                    swap(ar, i, j);

                    if (j == med)
                        med = i;
                    else if (i == med)
                        med = j;
                }
            }

            quickSortPartition(ar, s, med - 1);
            //printArray(ar, s, med - 1);
            quickSortPartition(ar, med + 1, e);
            //printArray(ar, med + 1, e);
        }

        static void swap(int[] ar, int i, int j)
        {
            int temp = ar[i];
            ar[i] = ar[j];
            ar[j] = temp;
        }

        static void printArray(int[] ar, int from, int to)
        {
            if (to - from < 1) return;
            for (int i = from; i <= to; i++)
            {
                Console.Write(" " + ar[i]);
            }

            Console.WriteLine("");
        }
    }
}