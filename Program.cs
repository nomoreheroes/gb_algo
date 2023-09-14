Console.WriteLine("Начнем");
lesson4.Tree tree = new lesson4.Tree();
for(int i=0;i<100;i++) tree.insert(new Random().Next(1,101));
tree.Print();
Console.WriteLine(tree.Contain(35));
namespace lesson4
{
    public enum colors
    {
        RED,
        BLACK
    }

    class Tree
    {
        public Node root;

        public void Print()
        {
            this.Print(root);
        }

        private void Print(Node node)
        {
           if (node.left!= null) Print(node.left);
           Console.WriteLine(node.value);
           if (node.right != null) Print(node.right);
        }

        public class Node
        {
            public int value;
            public Node left;
            public Node right;

            public colors color = colors.BLACK;

            public Node(int value): this(value,null,null){}
            public Node(int value, Node left, Node right) {
                this.value = value;
                this.left = left;
                this.right = right;
            }
        }

        Node rotateRight(Node node)
        {
            Node x = node.left;
            node.left = x.right;
            x.right = node;
            x.color = node.color;
            node.color = colors.RED;
            return x;
        }

        Node rotateLeft(Node node)
        {
            Node x = node.right;
            node.right = x.left;
            x.left = node;
            x.color = node.color;
            node.color = colors.RED;
            return x;
        }

        void flipColors(Node node)
        {
            foreach(Node x in new Node[]{node,node.left,node.right})
            {
                if (x.color == colors.RED)
                    x.color = colors.BLACK;
                else x.color = colors.RED;
            }
        }

        public bool Contain(int value)
        {
            if (root == null)
                return false;
            return Contain(value,root);
        }

        private bool Contain(int value, Node node)
        {
            if (node.value == value)
                return true;
            if(value < node.value)
                return Contain(value, node.left);
            return Contain(value, node.right);
        }

        public void insert(int value)
        {
            this.root = insert(this.root,value);
            this.root.color = colors.BLACK;
        }

        public Node insert(Node node, int value)
        {
            if (node == null)
            {
                return new Node(value);
            }
            if (node.left != null && node.right != null)
            {
                if (node.left.color == colors.RED && node.right.color == colors.RED)
                {
                    this.flipColors(node);
                }
            }

            if (value < node.value)
            {
                node.left = insert(node.left,value);
            } else {
                node.right = insert(node.right,value);
            }

            if(node.right != null && node.left != null)
            {
                if (node.right.color == colors.RED && node.left.color == colors.BLACK)
                    node = rotateLeft(node);
            }

            if (node.left != null && node.left.left != null)
            {
                if (node.left.color == colors.RED && node.left.left.color == colors.RED)
                    node = rotateRight(node);
            }
            
            return node;
        }
    }
}    