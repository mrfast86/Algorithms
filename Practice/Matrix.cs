#region License
// -------------------------------------------------------------------------------------------------------
// <copyright file="Matrix.cs" company="Gary Jonas Computing Ltd.">
//     Copyright (c) 2011 - 2016 Gary Jonas Computing Ltd. All Rights Reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------------
#endregion
namespace Practice
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class Matrix
    {
        #region http://www.practice.geeksforgeeks.org/problem-page.php?pid=1623
        public static void FloodFillAlgorithmMain()
        {
            int[,] m = new int[3, 4] { { 0, 1, 1, 0 }, { 1, 1, 1, 1 }, { 0, 1, 2, 3 } };
            int k = 5;
            int row = 0;
            int col = 1;
            fillFlood(m, k, row, col);
            for (int i = 0; i < m.GetLength(0); i++)
            {
                for (int j = 0; j < m.GetLength(1); j++)
                {
                    Console.Write(m[i, j] + " ");
                }
            }
        }

        static void fillFlood(int[,] m, int k, int row, int col)
        {
            Queue<Tuple<int, int>> q = new Queue<Tuple<int, int>>();
            q.Enqueue(new Tuple<int, int>(row, col));
            int check = m[row, col];
            while (q.Count > 0)
            {
                Tuple<int, int> t = q.Dequeue();
                m[t.Item1, t.Item2] = k;
                if (t.Item1 < m.GetLength(0) - 1 && m[t.Item1 + 1, t.Item2] == check)
                    q.Enqueue(new Tuple<int, int>(t.Item1 + 1, t.Item2));
                if (t.Item2 < m.GetLength(1) - 1 && m[t.Item1, t.Item2 + 1] == check)
                    q.Enqueue(new Tuple<int, int>(t.Item1, t.Item2 + 1));
                if (t.Item1 > 0 && m[t.Item1 - 1, t.Item2] == check)
                    q.Enqueue(new Tuple<int, int>(t.Item1 - 1, t.Item2));
                if (t.Item2 > 0 && m[t.Item1, t.Item2 - 1] == check)
                    q.Enqueue(new Tuple<int, int>(t.Item1, t.Item2 - 1));
            }
        }
        #endregion
    }
}