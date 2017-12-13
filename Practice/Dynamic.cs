#region License
// -------------------------------------------------------------------------------------------------------
// <copyright file="Dynamic.cs" company="Gary Jonas Computing Ltd.">
//     Copyright (c) 2011 - 2016 Gary Jonas Computing Ltd. All Rights Reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------------
#endregion
namespace Practice
{
    using System;
    using System.Collections.Generic;

    public class Dynamic
    {
        public static void KnapsackDuplicates()
        {
            int W = 100;
            int[] val = { 1, 30 };
            int[] wt = { 1, 50 };
            int n = 2;

            // dp[i] is going to store maximum value
            // with knapsack capacity i.
            int[] dp = new int[W+1];

            // Fill dp[] using above recursive formula
            for (int i = 0; i <= W; i++)
                for (int j = 0; j < n; j++)
                    if (wt[j] <= i)
                        dp[i] = Math.Max(dp[i], dp[i - wt[j]] + val[j]);
            
        }

        public static void CoinChangeMain()
        {
            int[] a = { 5, 10, 25 };
            Console.WriteLine(CoinChange(a, 25, 3, new List<int>()));
        }

        static int CoinChange(int[] c, int sum, int i, List<int> path)
        {
            if (sum == 0)
            {
                foreach (var item in path)
                {
                    Console.Write(item + " ");
                }

                Console.WriteLine("");
                return 1;
            }

            if (sum < 0) return 0;
            if (i <= 0 && sum >= 1) return 0;

            path.Add(c[i - 1]);
            int include = CoinChange(c, sum - c[i - 1], i, path);
            path.RemoveAt(path.Count - 1);
            int exclude = CoinChange(c, sum, i - 1, path);
            return exclude+include;
        }
    }
}