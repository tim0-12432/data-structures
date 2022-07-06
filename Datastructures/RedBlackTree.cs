namespace Datastructures;

public class RedBlackTree<T> : BinaryTree<T> where T : IComparable, new()
{
    public enum Color { Red, Black }
    
    public class RBNode : Node
    {
        /// <summary>
        /// Parent of the node.
        /// </summary>
        public Color Color { get; set; }
        
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="data">The data to contain</param>
        public RBNode(T data) : base(data)
        {
            Color = Color.Red;
        }
    }
    
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="root">Root node</param>
    public RedBlackTree(T root)
    {
        RBNode node = new RBNode(root);
        node.Color = Color.Black;
        this.root = node;
        Height = 1;
    }
    
    /// <summary>
    /// Constructor.
    /// </summary>
    public RedBlackTree() : base() {}
    
    /// <summary>
    /// Insert a new node in the tree.
    /// </summary>
    /// <param name="data">Data for the new node</param>
    public new void Insert(T data)
    {
        RBNode newElement = new RBNode(data);
        Insert(newElement);
        (root as RBNode).Color = Color.Black;
        if (newElement.Parent != null
            && newElement.Parent.Parent != null
            && newElement.Parent.Parent.LeftChild != null
            && newElement.Parent.Parent.RightChild != null
            && (newElement.Parent as RBNode).Color == Color.Red)
        {
            if ((newElement.Parent.Parent.LeftChild as RBNode).Color == Color.Red && (newElement.Parent.Parent.RightChild as RBNode).Color == Color.Red)
            {
                if (newElement.Parent.IsLeftChild())
                {
                    (newElement.Parent.Parent.RightChild as RBNode).Color = Color.Black;
                }
                else if (newElement.Parent.IsRightChild())
                {
                    (newElement.Parent.Parent.LeftChild as RBNode).Color = Color.Black;
                }

                (newElement.Parent as RBNode).Color = Color.Black;
                (newElement.Parent.Parent as RBNode).Color = Color.Red;
            }
            if (newElement.Parent.IsLeftChild() && (newElement.Parent.Parent.RightChild as RBNode).Color == Color.Black
                || newElement.Parent.IsRightChild() && (newElement.Parent.Parent.LeftChild as RBNode).Color == Color.Black)
            {
                if (newElement.IsLeftChild())
                    RightRotate(newElement.Parent);
                else if (newElement.IsRightChild())
                    LeftRotate(newElement.Parent);
            }
        }
        (root as RBNode).Color = Color.Black;
    }

    /// <summary>
    /// Delete a node from the tree.
    /// </summary>
    /// <param name="element">Node to remove</param>
    /// <returns>The removed data</returns>
    public T Delete(RBNode element)
    {
        return base.Delete(element);
    }

    /// <summary>
    /// Search for some data in the tree.
    /// </summary>
    /// <param name="data">Data to search for</param>
    /// <returns>The node containing the data</returns>
    public RBNode Search(T data)
    {
        Node found = base.Search(data);
        if (found.Data.Equals(default(T)))
            return new RBNode(new T());
        RBNode node = found as RBNode;
        if (node == null)
            throw new ArgumentException("Node is not from a Red-Black-Tree!");
        return node;
    }

    /// <summary>
    /// Validate the RB-Tree rules.
    /// </summary>
    /// <returns>Validation result</returns>
    public bool Validate()
    {
        if ((root as RBNode).Color != Color.Black && root != null)
            return false;

        if (GetBlackHeight(root as RBNode, 0) == -1)
            return false;

        return true;
    }

    private int GetBlackHeight(RBNode node, int blackHeight)
    {
        RBNode left = node.LeftChild as RBNode;
        RBNode right = node.RightChild as RBNode;
        if (left == null || right == null)
            return blackHeight;
        if (node.Color == Color.Red && (left.Color != Color.Black || right.Color != Color.Black))
            return -1;

        int currBlackHeight = blackHeight;
        if (node.Color == Color.Black)
            currBlackHeight++;
        int lTree = GetBlackHeight(left, currBlackHeight);
        int rTree = GetBlackHeight(right, currBlackHeight);
        if (lTree != rTree)
            return -1;
        return lTree;
    }
}