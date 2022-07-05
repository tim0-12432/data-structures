namespace Datastructures;

public class RedBlackTree<T> : BinaryTree<T> where T : IComparable, new()
{
    public enum Color { RED, BLACK }
    
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
            Color = Color.RED;
        }
    }
    
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="root">Root node</param>
    public RedBlackTree(T root)
    {
        RBNode node = new RBNode(root);
        node.Color = Color.BLACK;
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
        (root as RBNode).Color = Color.BLACK;
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
        if ((root as RBNode).Color != Color.BLACK && root != null)
            return false;
        RBNode current = Minimum(root) as RBNode;
        while (current != null)
        {
            if (current.Color != Color.RED && current.Color != Color.BLACK)
                return false;
            current = Successor(current) as RBNode;
        }

        return true;
    }
}