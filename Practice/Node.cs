namespace Practice
{
    public class Node
    {
        public int Value;
        public Node Left;
        public Node Right;

        public Node(int value)
        {
            this.Value = value;
        }

        public static Node InitializeTree()
        {
            //      1
            //    /   \
            //   2      3
            // /  \      \
            //4    5      8
            //          /   \
            //         6     7
            Node n = new Practice.Node(1);
            n.Left = new Practice.Node(2);
            n.Right = new Practice.Node(3);
            n.Left.Left = new Practice.Node(4);
            n.Right.Right = new Practice.Node(8);
            n.Right.Right.Right = new Practice.Node(7);
            n.Right.Right.Left = new Practice.Node(6);
            n.Left.Right = new Practice.Node(5);

            return n;
        }
    }
}