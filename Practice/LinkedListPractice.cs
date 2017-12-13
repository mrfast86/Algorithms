using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice
{
    public static class LinkedListPractice
    {
        #region Sort a list of 0s 1s and 2s

        public static void LinkedListSort012Main()
        {
            LinkedList<int> l = new LinkedList<int>();
            l.AddLast(1);
            l.AddLast(0);
            l.AddLast(2);
            l.AddLast(1);
            l.AddLast(0);

            LinkedListNode<int> n = sortLinkedList012(l.First);

            foreach (int value in l)
            {
                Console.WriteLine(value);
            }
        }

        private static LinkedListNode<int> sortLinkedList012(LinkedListNode<int> root)
        {
            int zeroes = 0;
            int ones = 0;
            int twos = 0;

            LinkedListNode<int> temp = root;

            while (temp != null)
            {
                switch (temp.Value)
                {
                    case 1:
                        ones++;
                        temp = temp.Next;
                        break;
                    case 2:
                        twos++;
                        temp = temp.Next;
                        break;
                    case 0:
                        zeroes++;
                        temp = temp.Next;
                        break;
                    default:
                        temp = temp.Next;
                        break;
                }
            }

            temp = root;
            while (ones > 0 || twos > 0 || zeroes > 0)
            {
                if (zeroes > 0)
                {
                    temp.Value = 0;
                    zeroes--;
                    temp = temp.Next;
                    continue;
                }

                if (ones > 0)
                {
                    temp.Value = 1;
                    ones--;
                    temp = temp.Next;
                    continue;
                }

                if (twos > 0)
                {
                    temp.Value = 2;
                    twos--;
                    temp = temp.Next;
                    continue;
                }
            }

            return root;
        }

        #endregion
    }
}
