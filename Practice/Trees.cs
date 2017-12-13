using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice
{
    public static class Trees
    {
        #region Lowest common ancestor http://www.geeksforgeeks.org/lowest-common-ancestor-binary-tree-set-1/
        public static void LCA()
        {
            // Let us create the Binary Tree shown in above diagram.
            Node root = new Node(1);
            root.Left = new Node(2);
            root.Right = new Node(3);
            root.Left.Left = new Node(4);
            root.Left.Right = new Node(5);
            root.Right.Left = new Node(6);
            root.Right.Right = new Node(7);
            Console.WriteLine(findLCA(root, 4, 5));
            Console.WriteLine(findLCA(root, 4, 6));
            Console.WriteLine(findLCA(root, 3, 4));
            Console.WriteLine(findLCA(root, 2, 4));
        } 

        static int findLCA(Node root, int a, int b)
        {
            if (root.Value == a || root.Value == b)
                return root.Value;

            bool Left = isInSubtree(root.Left, a);
            bool Right = isInSubtree(root.Right, b);

            if ((Left && Right) || (!Left && !Right))
                return root.Value;

            if (Left)
                return findLCA(root.Left, a, b);

            if (Right)
                return findLCA(root.Right, a, b);

            return 0;
        }
        
        static bool isInSubtree(Node root, int a)
        {
            if (root == null)
                return false;

            if (root.Value == a)
                return true;

            return isInSubtree(root.Left, a) || isInSubtree(root.Right, a);
        }

        public static void LCA2()
        {
            // Let us create the Binary Tree shown in above diagram.
            Node root = new Node(1);
            root.Left = new Node(2);
            root.Right = new Node(3);
            root.Left.Left = new Node(4);
            root.Left.Right = new Node(5);
            root.Right.Left = new Node(6);
            root.Right.Right = new Node(7);
            Console.WriteLine(findLCAByPath(root, 4, 5));
            Console.WriteLine(findLCAByPath(root, 4, 6));
            Console.WriteLine(findLCAByPath(root, 3, 4));
            Console.WriteLine(findLCAByPath(root, 2, 4));
        }

        private static int findLCAByPath(Node root, int v1, int v2)
        {
            List<int> path1 = new List<int>();
            findPathStack(root, v1, path1);
            List<int> path2 = new List<int>();
            findPathStack(root, v2, path2);
            
            int returnValue = -1;
            for (int i = 0; i < path1.Count; i ++)
            {
                if (path1[i] == path2[i])
                    returnValue = path1[i];
            }

            return returnValue;
        }

        static bool findPathStack(Node node, int v, List<int> stack)
        {
            if (node == null)
            {
                return false;
            }

            stack.Add(node.Value);
            if (node.Value == v)
                return true;
            
            bool Left = findPathStack(node.Left, v, stack);
            if (Left)
                return true;

            bool Right = findPathStack(node.Right, v, stack);
            if (Right)
                return true;

            stack.RemoveAt(stack.Count-1);

            return false;
        }

        #endregion

        #region http://www.geeksforgeeks.org/print-Left-view-binary-tree/

        public static void PrintLeftView()
        {
            Node root = new Node(12);
            root.Left = new Node(10);
            root.Right = new Node(30);
            root.Right.Left = new Node(25);
            root.Right.Right = new Node(40);

            int i = 0;
            LeftView(root, 1, ref i);
        }

        private static void LeftView(Node root, int level, ref int maxPrintedLevel)
        {
            if (root == null)
                return;

            if (maxPrintedLevel < level)
            { 
                Console.WriteLine(root.Value);
                maxPrintedLevel = level;
            }

            LeftView(root.Left, level + 1, ref maxPrintedLevel);
            LeftView(root.Right, level + 1, ref maxPrintedLevel);
        }

        #endregion

        #region Sum of two nodes N http://www.geeksforgeeks.org/find-a-pair-with-given-sum-in-bst/

        public static void findPairMain()
        {
            Node root =  new Node(15);
            root.Left = new Node(10);
            root.Right = new Node(20);
            root.Left.Left = new Node(8);
            root.Left.Right = new Node(12);
            root.Right.Left = new Node(16);
            root.Right.Right = new Node(25);

            int target = 27;

            findPair(root, target);
        }

        private static void findPair(Node root, int target)
        {
            Stack<Node> inOrder = new Stack<Node>();
            Stack<Node> revOrder = new Stack<Node>();
            inOrder.Push(root);
            revOrder.Push(root);
            Node lastPopped = new Node(-1);
            Node lastPopped2 = new Node(-1);
            int v1 = 0;
            int v2 = 0;
            bool f1 = false;
            bool f2 = false;

            while (inOrder.Count() > 0 || revOrder.Count() > 0)
            {
                while (inOrder.Count() > 0 && !f1)
                {
                    Node temp = inOrder.Peek();
                    if (temp.Left != null && lastPopped != temp.Left && lastPopped != temp.Right)
                    {
                        inOrder.Push(temp.Left);
                    }
                    else if (temp.Right != null && lastPopped != temp.Right)
                    {
                        v1 = temp.Value;
                        f1 = true;
                        inOrder.Push(temp.Right);
                    }
                    else
                    {
                        if (lastPopped != temp.Right)
                        {
                            v1 = temp.Value;
                            f1 = true;
                            lastPopped = inOrder.Pop();
                            break;
                        }
                        lastPopped = inOrder.Pop();
                    }
                }

                while (revOrder.Count() > 0 && !f2)
                {
                    Node temp = revOrder.Peek();
                    if (temp.Right != null && lastPopped2 != temp.Right && lastPopped2 != temp.Left)
                    {
                        revOrder.Push(temp.Right);
                    }
                    else if (temp.Left != null && lastPopped2 != temp.Left)
                    {
                        v2 = temp.Value;
                        f2 = true;
                        revOrder.Push(temp.Left);
                    }
                    else
                    {
                        if (lastPopped2 != temp.Left)
                        {
                            v2 = temp.Value;
                            f2 = true;
                            lastPopped2 = revOrder.Pop();
                            break;
                        }

                        lastPopped2 = revOrder.Pop();
                    }
                }

                if (v1 == v2)
                {
                    Console.WriteLine("not found");
                    break;
                }
                else if (v1 + v2 == target)
                    Console.WriteLine(v1 + "," + v2);
                else if (v1 + v2 < target)
                {
                    f1 = false;
                }
                else if (v1 + v2 > target)
                {
                    f2 = false;
                } 
            }
        }

        #endregion

        #region Find median in a stream
        //http://practice.geeksforgeeks.org/problem-page.php?pid=1731

        //public static void FindMedianMain()
        //{
        //    int[] a = new int[] { 5, 15, 1, 3 };
        //    MinHeap
        //}

        #endregion

        #region Print all leaf node paths

        public static void PrintAllLeafPathsMain()
        {
            Node n = new Node(4);
            n.Left = new Node(5);
            n.Left.Left = new Node(2);
            n.Left.Right = new Node(6);
            n.Right = new Node(8);
            n.Right.Left = new Node(1);

            printPathsRecur(n, new List<int>());

            //          4
            //      5       8
            //  2     6    1
        }

        public static void printPathsRecur(Node node, List<int> path) 
        {
          if (node==null) 
            return;
 
          /* append this node to the path array */
          path.Add(node.Value);
 
          /* it's a leaf, so print the path that led to here  */
          if (node.Left==null && node.Right== null) 
          {
            //printArray(path, pathLen);
           }
          else
          {
            /* otherwise try both subtrees */
            printPathsRecur(node.Left, path);
                path.RemoveAt(path.Count - 1);
            printPathsRecur(node.Right, path);
                path.RemoveAt(path.Count - 1);
            }
        }

        #endregion

        #region Maximum sum
        // http://practice.geeksforgeeks.org/problems/max-level-sum-in-binary-tree/1 

        public static void MaximumSumMain()
        {
            Node n = Node.InitializeTree();
            int max = 0;
            int h = getTreeHeight(n);
            for (int i = 0; i < h; i++)
            {
                int sumOfLevel = getSumOfLevel(n, i);
                if (sumOfLevel > max)
                    max = sumOfLevel;
            }

            Console.WriteLine(max);
        }

        private static int getSumOfLevel(Node n, int i)
        {
            if (n == null)
                return 0;

            if (i == 0)
                return n.Value;

            return getSumOfLevel(n.Left, i - 1) + getSumOfLevel(n.Right, i - 1);
        }

        private static int getTreeHeight(Node n)
        {
            if (n == null)
                return 0;

            if (n.Left == null && n.Right == null)
                return 1;

            return Math.Max(getTreeHeight(n.Left) + 1, getTreeHeight(n.Right) + 1);
        }
        #endregion

    }
}
