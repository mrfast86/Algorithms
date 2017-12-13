using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice
{
    public static class Strings
    {
        #region Virus question
        // Previous amazon question - personal
        public static void FindNthVirus()
        {
            //a -> ab, b -> ba per t=1
            //ab , t=3, n = 5
            //ab => abba => abbabaab => abbabaabbaababba
            // if  n < 2^t
            // we know it's on the left side, decrement t
            // if n > 2^t
            // we know it's on right side, decrement t and n by 2^t-1
            // so if n=1, t=1
            // if t = 1, 
            //        a
            //    a       b
            //  a  b      b  a
            // a b  b a  b a  a b
            // a b b a b a a b b a a b a b b a
            Console.WriteLine(findElement('a', 4, 15));
        }

        static char findElement(char c, int t, int n)
        {
            if (t == 1)
            {
                if (n == 1)
                {
                    if (c == 'a')
                        return 'a';
                    if (c == 'b')
                        return 'b';
                }
                if (n == 2)
                {
                    if (c == 'a')
                        return 'a';
                    if (c == 'b')
                        return 'a';
                }

                return '0';
            }

            if (n <= (Math.Pow(2, t)) / 2)
            {
                // go left, keep n
                if (c == 'a')
                    return findElement('a', t - 1, n);
                else if (c == 'b')
                    return findElement('b', t - 1, n);
            }
            else if (n > (2 ^ t) / 2)
            {
                // go right, decrement n by (2^t)/2
                if (c == 'a')
                    return findElement('b', t - 1, (n - ((int)(Math.Pow(2, t)) / 2)));
                if (c == 'b')
                    return findElement('a', t - 1, (n - ((int)(Math.Pow(2, t)) / 2)));
            }

            return '0';
        }
        #endregion

        #region Longest running parenthesis 
        //http://www.practice.geeksforgeeks.org/problem-page.php?pid=1247

        public static void LogestRunningParenthesisMain()
        {
            string input = ")()())";
            int value = longestParenthesis(input);
            Console.WriteLine(value);
        }

        static int longestParenthesis(string input)
        {
            char[] c = input.ToCharArray();
            Stack<char> s = new Stack<char>();
            int i = 0;
            int retValue = 0;
            while (i < c.Length)
            {
                switch (c[i])
                {
                    case '(':
                        s.Push('(');
                        break;
                    case ')':
                        if (s.Count > 0 && s.Peek() == '(')
                        {
                            retValue += 2;
                            s.Pop();
                        }
                        break;
                }

                i++;
            }

            return retValue;
        }

        #endregion

        #region Word Break - Part 2
        //http://www.practice.geeksforgeeks.org/problem-page.php?pid=1278
        public static void WordBreakPart2Main()
        {
            string s = "snakesandladder";
            string[] dict = { "snakes", "and", "ladder" };
            string[] dict2 = { "snake", "snakes", "and", "sand", "ladder" };

            bool valid = wordBreakNaive(s, dict);
            Console.WriteLine(valid);

            List<string>[] matched = wordBreakDynamic(s, dict2);
            foreach (var m in matched)
            {
                if (m == null) continue;
                foreach (var w in m)
                {
                    Console.WriteLine(w);
                }
            }
        }

        static bool wordBreakNaive(string s, string[] dict, int start = 0, int wordIndex = 0)
        {
            // This is the naive recursive approach to determine if putting break will validate string
            if (wordIndex == dict.Count())
                return true;

            int end = dict[wordIndex].Length;
            if (start + end >= s.Length + 1)
                return false;

            if (s.Substring(start, end) == dict[wordIndex])
                return wordBreakNaive(s, dict, start + end, wordIndex + 1);

            return false;
        }

        static List<string>[] wordBreakDynamic(string s, string[] dict)
        {
            List<string>[] matched = new List<string>[s.Length + 1];
            matched[0] = new List<string>();
            for (int i = 0; i < s.Length; i++)
            {
                if (matched[i] == null)
                    continue;

                for (int j = 0; j < dict.Count(); j++)
                {
                    string wordToCheck = dict[j];
                    int end = wordToCheck.Length;
                    if (i + end > s.Length + 1)
                    {
                        break;
                    }

                    if (s.Substring(i, end) == wordToCheck)
                    {
                        if (matched[i + end] == null)
                            matched[i + end] = new List<string>();
                        matched[i + end].Add(wordToCheck);
                    }
                }
            }

            return matched;
        }

        #endregion

        #region Word Ladder Program

        //http://www.geeksforgeeks.org/word-ladder-length-of-shortest-chain-to-reach-a-target-word/
        // This will be my own version to print the path
        public static void WordLadderMain()
        {
            HashSet<string> dict = new HashSet<string>(){ "POON", "PLEE", "SAME", "POIE", "PLEA", "PLIE", "POIN" };
            string start = "TOON";
            string target = "PLEA";

            findWordLadder(dict, start, target);
        }

        static void findWordLadder(HashSet<string> dict, string start, string target)
        {

            MultiNode node = new MultiNode(start);
            buildTree(dict, start, node, "");

            getHeights(node);
            MultiNode temp = new MultiNode(string.Empty);

            while (node.Children.Count != 0)
            {
                int minHeight = 9999999;
                Console.Write(node.Value + " ");
                for (int i = 0; i < node.Children.Count; i++)
                {
                    if (node.Children[i].Height <= minHeight)
                    {
                        minHeight = node.Children[i].Height;
                        temp = node.Children[i];
                    }
                }

                node = temp;
            }
        }

        static int getHeights(MultiNode n)
        {
            if (n.Children.Count == 0) return 1;
            else
            {
                int maxHeight = 0;
                foreach (var t in n.Children)
                {
                    int height = getHeights(t);
                    if (height > maxHeight)
                        maxHeight = height;
                }

                n.Height = maxHeight + 1;

                return n.Height;
            }
        }

        static void buildTree(HashSet<string> dict, string currentWord, MultiNode root, string prevWord)
        {
            HashSet<string> visited = new HashSet<string>();
            foreach (string w in dict)
            {
                if (isAdjascent(currentWord, w) && prevWord != w)
                {
                    MultiNode tempNode = new MultiNode(w);
                    root.Children.Add(tempNode);
                    visited.Add(w);
                    buildTree(dict, w, tempNode, currentWord);
                }
            }
        }

        static bool isAdjascent(string w1, string w2)
        {
            int counter = 0;
            for (int i = 0; i < w1.Length; i++)
            {
                if (w1[i] != w2[i])
                {
                    if (counter > 0) return false;
                    else counter++;
                }
            }

            return counter == 1;
        }

        class MultiNode
        {
            public string Value;

            public List<MultiNode> Children = new List<MultiNode>();

            public int Height;

            public MultiNode(string value)
            {
                this.Value = value;
            }
        }

        #endregion

        #region Longest K unique characters substring
        // http://www.practice.geeksforgeeks.org/problem-page.php?pid=1643
        public static void LongestUniqueCharsMain()
        {
            string a = "aabbcc";
            int s = longestUniqueChars(a, 2);
            Console.WriteLine(s);
        }

        static int longestUniqueChars(string s, int k)
        {
            if (s.Length == 0)
                return 0;

            if (s.Length == 1)
                return 1;

            char[] c = s.ToCharArray();
            int max = 0;
            int i = 0;
            int j = 0;
            HashSet<char> h = new HashSet<char>();
            h.Add(c[i]);
            int tempMax = 0;
            while (j < c.Count())
            {
                if (h.Contains(c[j]))
                {
                    tempMax++;
                    if (tempMax > max)
                        max = tempMax;
                    j++;
                }
                else if (!h.Contains(c[j]))
                {
                    h.Add(c[j]);
                    if (h.Count <= k)
                    {
                        tempMax++;
                        if (tempMax > max)
                            max = tempMax;
                        j++;
                    }
                    else
                    {
                        bool valid = false;

                        while (!valid)
                        {
                            char tempChar = c[i];
                            while (c[i] == tempChar)
                            {
                                i++;
                                tempMax--;
                            }

                            valid = checkValid(c, i, j, k);
                        }

                        h.Remove(c[i - 1]);
                        tempMax++;
                        if (tempMax > max)
                            max = tempMax;
                        j++;
                    }
                }
            }

            return max;
        }

        static bool checkValid(char[] c, int i, int j, int k)
        {
            HashSet<char> h = new HashSet<char>();
            while (i <= j)
            {
                if (!h.Contains(c[i]))
                    h.Add(c[i]);
                i++;
            }

            return h.Count <= k;
        }

        #endregion

        #region Remove “b” and “ac” from a given string
        //http://practice.geeksforgeeks.org/problem-page.php?pid=297

        public static void RemoveStringsmain()
        {
            string a = "adaaabbaabcd";
            RemoveStrings(a);
        }

        static void RemoveStrings(string main)
        {
            // rather this seems easy as you should end up with aaac if you pass in aababc, which means 1 pass
            char[] c = main.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 'a')
                    if (i + 1 < c.Length && c[i + 1] == 'c')
                    {
                        i++;
                        continue;
                    }

                if (c[i] == 'b') continue;

                Console.Write(c[i]);
            }
        }

        #endregion

        #region Print permutation
        public static void PrintPermutationMain()
        {
            string s = "abc";
            printPermutation(0, string.Empty, s, s.Length);
        }

        static void printPermutation(int i, string temp, string s, int strLen)
        {
            if (i == strLen)
            {
                Console.WriteLine(temp);
                return;
            }

            char[] cArray = s.ToCharArray();
            for (int index = 0; index < cArray.Length; index++)
            {
                char c = cArray[index];
                temp += c;
                string sFiltered = s.Remove(index, 1);
                printPermutation(i + 1, temp, sFiltered, strLen);
                temp = temp.Remove(temp.Length - 1);
            }
        } 
        #endregion
        
        #region Count palindrome
        public static void CountPalindromeMain()
        {
            string s = "abccb";
            int a = countPalindromes(s) + s.Length;
            Console.WriteLine(a);
        }

        static int countPalindromes(string s)
        {
            return countHelper(s, new HashSet<string>(), 0, s.Length - 1);
        }

        static int countHelper(string s, HashSet<string> visited, int i, int j)
        {
            if (string.IsNullOrEmpty(s) || i >= j)
                return 0;

            int counter = 0;

            if (isPalindrome(s.Substring(i, j + 1 - i)) && !visited.Contains(i + "@" + j))
            {
                counter++;
                visited.Add(i + "@" + j);
            }

            counter += countHelper(s, visited, i + 1, j) + countHelper(s, visited, i, j - 1);

            return counter;
        }

        static bool isPalindrome(string s)
        {
            int i = 0;
            int j = s.Length - 1;
            while (i < j)
            {
                if (s[i] != s[j])
                    return false;

                i++;
                j--;
            }

            return true;
        } 
        #endregion

        #region Word Ladder

        public static void WordLadderMainMyVersion()
        {
            string s = string.Empty;
            HashSet<string> list = new HashSet<string>{ "DOG", "CAT", "COG", "CAG", "CIT" };
            string start = "DOG";
            string end = "CAT";
            Node found = findPath(list, start, end);
            while (found != null)
            {
                s = found.Value + "," + s;
                found = found.Parent;
            }

            s = s.TrimEnd(',');

            Console.WriteLine(s);
        }

        class Node
        {
            public string Value;
            public Node Parent;
            public Node(string value, Node parent)
            {
                Value = value;
                Parent = parent;
            }
        }

        static Node findPath(HashSet<string> list, string start, string end)
        {
            Queue<Node> q = new Queue<Node>();
            q.Enqueue(new Node(start, null));
            list.Remove(start);

            while (q.Any())
            {
                Node temp = q.Dequeue();
                if (temp.Value == end)
                    return temp;

                foreach (Node item in getChildren(temp, list))
                {
                    q.Enqueue(item); 
                }
            }

            return null;
        }

        static List<Node> getChildren(Node temp, HashSet<string> list)
        {
            List<Node> retList = new List<Node>();
            for (int j = 0; j < list.Count; j++)
            {
                int i = 0;
                int counter = 0;
                if (list.ElementAt(j).Length != temp.Value.Length)
                    continue;
                while (i < list.ElementAt(j).Length)
                {
                    if (list.ElementAt(j)[i] != temp.Value[i])
                    {
                        counter++;
                        if (counter > 1)
                            break;
                    }

                    i++;
                }

                if (counter == 1)
                {
                    retList.Add(new Node(list.ElementAt(j), temp));
                    list.Remove(list.ElementAt(j));
                }
            }

            return retList;
        }

        #endregion

        #region Reverse words

        public static void ReverseWordsMain()
        {
            //Console.WriteLine(reverseWords("i.like.this.program.very.much"));
            Console.WriteLine(reverseWordsByReversingTwice("i.like.this.program.very.much"));
        }
        private static string reverseWords(string s)
        {
            string[] c = s.Split('.');
            int i = 0;
            int j = c.Length - 1;
            while (i < j)
            {
                var temp = c[i];
                c[i] = c[j];
                c[j] = temp;
                i++;
                j--;
            }

            return String.Join(".", c);
        }

        private static string reverseWordsByReversingTwice(string b)
        {
            // i.like.this
            // rev internal words - i.ekil.siht
            // rev entire this.like.i
            char[] s = b.ToCharArray();
            int i = 0;
            int j = 0;
            int nextIdx = 0;
            while (nextIdx < s.Length)
            {
                while (j < s.Length - 1 && s[j + 1] != '.')
                    j++;

                nextIdx = j + 1;

                while (i < j)
                {
                    swap(ref s[i], ref s[j]);
                    i++;
                    j--;
                }

                i = nextIdx + 1;
                j = nextIdx + 1;

            }

            i = 0;
            j = s.Length - 1;
            while (i < j)
            {
                swap(ref s[i], ref s[j]);
                i++;
                j--;
            }

            return new string(s);
        }

        private static void swap(ref char a, ref char b)
        {
            char temp = a;
            a = b;
            b = temp;
        }

        #endregion

        public static void PracticeMain()
        {
            
        }
    }
}
