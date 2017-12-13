using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice
{
    public static class TriePractice
    {
        public static void MainTest()
        {
            TrieNodeUsingDictionary n = new TrieNodeUsingDictionary();
            n.Insert("CAT");
            n.Insert("CAG");
            n.Insert("DOG");
            n.Insert("DOT");
            n.Insert("DOooo");
            n.Insert("Dott");

            string searchString = "Do";
            n = n.Search(searchString);

            TrieNodeUsingDictionary.PrintNodes(n, searchString);
        }

        private static void PrintNodes(TrieNode n, string searchString)
        {
            if (n.Leaf)
                Console.WriteLine(searchString);

            for (int i=0; i<n.Nodes.Length; i++)
            {
                if (n.Nodes[i] == null)
                    continue;

                PrintNodes(n.Nodes[i], searchString + TrieNode.GetChar(i));
            }
        }
    }

    public class TrieNode
    {
        public TrieNode[] Nodes = new TrieNode[26];
        public bool Leaf;
        public TrieNode()
        {
        }

        public TrieNode Search(string s)
        {
            TrieNode currentNode = this;
            char[] cArray = s.ToCharArray();
            for (int i=0; i<cArray.Length; i++)
            {
                currentNode = currentNode.Nodes[getIndex(cArray[i])];
                if (currentNode == null)
                    return null;
            }

            return currentNode;
        }

        public void Insert(string s)
        {
            if (string.IsNullOrEmpty(s)) return;

            char[] charArray = s.ToCharArray();
            TrieNode n = this;
            for (int i=0; i<charArray.Length; i++)
            {
                TrieNode temp = n.Nodes[getIndex(charArray[i])];
                if (temp == null)
                {
                    temp = new TrieNode();
                    n.Nodes[getIndex(charArray[i])] = temp;
                }

                n = temp;
            }

            n.Leaf = true;
        }

        private int getIndex(char c)
        {
            return c % 32 - 1;
        }

        public static char GetChar(int i)
        {
            return (char)('A' + i);
        }
    }

    public class TrieNodeUsingDictionary
    {
        public Dictionary<char, TrieNodeUsingDictionary> Nodes;
        public bool Leaf;
        public TrieNodeUsingDictionary()
        {
            Nodes = new Dictionary<char, TrieNodeUsingDictionary>();
        }

        public TrieNodeUsingDictionary Search(string s)
        {
            char[] cArray = s.ToCharArray();
            TrieNodeUsingDictionary n = this;
            foreach (char c in cArray)
            {
                n.Nodes.TryGetValue(c, out n);
                if (n == null)
                    return null;
            }

            return n;
        }

        public void Insert(string s)
        {
            char[] cArray = s.ToCharArray();
            TrieNodeUsingDictionary n = this;
            foreach (char c in cArray)
            {
                TrieNodeUsingDictionary temp;
                n.Nodes.TryGetValue(c, out temp);

                if (temp == null)
                {
                    temp = new TrieNodeUsingDictionary();
                    n.Nodes.Add(c, temp);
                }

                n = temp;
            }

            n.Leaf = true;
        }

        public static void PrintNodes(TrieNodeUsingDictionary n, string searchString)
        {
            if (n.Leaf)
                Console.WriteLine(searchString);

            foreach (KeyValuePair<char, TrieNodeUsingDictionary> t in n.Nodes)
            {
                TrieNodeUsingDictionary.PrintNodes(t.Value, searchString + t.Key);
            }
        }
    }
}
