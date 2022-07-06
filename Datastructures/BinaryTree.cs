using System.Collections;
using System.Text;

namespace Datastructures;

/// <summary>
/// Binary search tree.
/// </summary>
/// <typeparam name="T">Type must be comparable</typeparam>
public class BinaryTree<T> : IEnumerable<T> where T : IComparable, new()
{
    /// <summary>
    /// A single tree node.
    /// </summary>
    public class Node
    {
        /// <summary>
        /// Data to contain.
        /// </summary>
        public T Data { get; private set; }
        
        /// <summary>
        /// Parent of the node.
        /// </summary>
        public Node Parent { get; set; }
        
        /// <summary>
        /// Left child of the node.
        /// </summary>
        public Node LeftChild { get; set; }
        
        /// <summary>
        /// Right child of the node.
        /// </summary>
        public Node RightChild { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="data">The data to contain</param>
        public Node(T data)
        {
            Data = data;
            LeftChild = Parent = RightChild = null;
        }

        /// <summary>
        /// Checks if node is left child.
        /// </summary>
        /// <returns>Is left child</returns>
        public bool IsLeftChild()
        {
            if (Parent != null && Parent.LeftChild != null)
                return Parent.LeftChild.Data.Equals(Data);
            return false;
        }
        
        /// <summary>
        /// Checks if node is right child.
        /// </summary>
        /// <returns>Is right child</returns>
        public bool IsRightChild()
        {
            if (Parent != null && Parent.RightChild != null)
                return Parent.RightChild.Data.Equals(Data);
            return false;
        }

        /// <summary>
        /// Get the sibling of a node.
        /// </summary>
        /// <returns>The sibling</returns>
        public Node GetSibling()
        {
            if (Parent != null)
            {
                if (IsLeftChild())
                    return Parent.RightChild;
                return Parent.LeftChild;
            }

            return null;
        }
    }
    
    /// <summary>
    /// Height of the tree;
    /// </summary>
    public int Height { get; protected set; }

    protected Node root = null;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="root">Root node</param>
    public BinaryTree(T root)
    {
        Node node = new Node(root);
        this.root = node;
        Height = 1;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    public BinaryTree()
    {
        Height = 0;
    }

    /// <summary>
    /// Insert a new node in the tree.
    /// </summary>
    /// <param name="data">Data for the new node</param>
    public void Insert(T data)
    {
        Node newElement = new Node(data);
        Insert(newElement);
    }

    /// <summary>
    /// Insert a new node in the tree.
    /// </summary>
    /// <param name="node">New node to insert</param>
    public void Insert(Node node)
    {
        Node current = root;
        Node element = null;
        int newHeight = 1;
        while (current != null)
        {
            element = current;
            if (node.Data.CompareTo(current.Data) == -1)
                current = current.LeftChild;
            else
                current = current.RightChild;
            newHeight++;
        }
        node.Parent = element;
        if (element == null)
        {
            root = node;
            newHeight = 1;
        }
        else
        {
            if (node.Data.CompareTo(element.Data) == -1)
                element.LeftChild = node;
            else
                element.RightChild = node;
        }

        if (newHeight > Height)
            Height = newHeight;
    }

    /// <summary>
    /// Delete a node from the tree.
    /// </summary>
    /// <param name="element">Node to remove</param>
    /// <returns>The removed data</returns>
    public T Delete(Node element)
    {
        return DeleteNode(element).Item1;
    }

    protected (T, Node) DeleteNode(Node element)
    {
        Node parent = element.Parent;
        Node transplantation = null;
        if (element.LeftChild == null && element.RightChild == null)
            transplantation = null;
        else if (element.LeftChild != null && element.RightChild == null)
            transplantation = element.LeftChild;
        else if (element.RightChild != null && element.LeftChild == null)
            transplantation = element.RightChild;
        else
        {
            Node min = Minimum(element.RightChild);
            min.LeftChild = element.LeftChild;
            transplantation = min;
        }

        if (parent == null)
            root = transplantation;
        else if (element.Equals(parent.LeftChild))
            parent.LeftChild = transplantation;
        else
            parent.RightChild = transplantation;
        if (parent != null)
            transplantation.Parent = parent;
        return (element.Data, transplantation);
    }

    /// <summary>
    /// Minimum in the tree.
    /// </summary>
    /// <param name="subtree">Start from a certain node</param>
    /// <returns>The smallest node</returns>
    public Node Minimum(Node? subtree)
    {
        Node current = subtree ?? root;
        if (current == null)
            return new Node(new T());
        while (current.LeftChild != null)
        {
            current = current.LeftChild;
        }
        return current;
    }

    /// <summary>
    /// Maximum in the tree.
    /// </summary>
    /// <param name="subtree">Start from a certain node</param>
    /// <returns>The greatest node</returns>
    public Node Maximum(Node? subtree)
    {
        Node current = subtree ?? root;
        if (current == null)
            return new Node(new T());
        while (current.RightChild != null)
        {
            current = current.RightChild;
        }
        return current;
    }

    /// <summary>
    /// Search for some data in the tree.
    /// </summary>
    /// <param name="data">Data to search for</param>
    /// <returns>The node containing the data</returns>
    public Node Search(T data)
    {
        Node current = root;
        while (current != null && !data.Equals(current.Data))
        {
            if (data.CompareTo(current.Data) == -1)
                current = current.LeftChild;
            else
                current = current.RightChild;
        }
        return current ?? new Node(new T());
    }

    /// <summary>
    /// Find the predecessor of a node.
    /// </summary>
    /// <param name="node">Node</param>
    /// <returns>The predecessor of the node</returns>
    public Node Predecessor(Node node)
    {
        if (node.LeftChild != null)
            return Maximum(node.LeftChild);
        Node parent = node.Parent;
        while (parent != null && node == parent.LeftChild)
        {
            node = parent;
            parent = parent.Parent;
        }

        return parent;
    }

    /// <summary>
    /// Find the successor of a node.
    /// </summary>
    /// <param name="node">Node</param>
    /// <returns>The successor of the node</returns>
    public Node Successor(Node node)
    {
        if (node.RightChild != null)
            return Minimum(node.RightChild);
        Node parent = node.Parent;
        while (parent != null && node == parent.RightChild)
        {
            node = parent;
            parent = parent.Parent;
        }

        return parent;
    }

    /// <summary>
    /// Enumerator using the Inorder-sequence.
    /// </summary>
    /// <returns>The enumerator.</returns>
    public IEnumerator<T> GetEnumerator()
    {
        Node current = Minimum(root);
        while (current != null)
        {
            yield return current.Data;
            current = Successor(current);
        }
    }

    /// <summary>
    /// Traversing in Inorder-sequence.
    /// </summary>
    /// <returns>A string representation</returns>
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder("BinaryTree=[");
        foreach (T data in this)
            builder.Append($"{data},");
        if (!'['.Equals(builder[^1]))
            builder.Remove(builder.Length - 1, 1);
        builder.Append("],");
        return builder.ToString();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    
    protected void LeftRotate(Node node)
    {
        Node leftTree = node.Parent;
        Node parent = node.Parent.Parent;
        if (node.Parent.IsLeftChild())
            node.Parent.Parent.LeftChild = node;
        else if (node.Parent.IsRightChild())
            node.Parent.Parent.RightChild = node;
        node.Parent = parent;
        leftTree.RightChild = node.LeftChild;
        node.LeftChild = leftTree;
    }
    
    protected void RightRotate(Node node)
    {
        Node rightTree = node.Parent;
        Node parent = node.Parent.Parent;
        if (node.Parent.IsLeftChild())
            node.Parent.Parent.LeftChild = node;
        else if (node.Parent.IsRightChild())
            node.Parent.Parent.RightChild = node;
        node.Parent = parent;
        rightTree.LeftChild = node.RightChild;
        node.RightChild = rightTree;
    }
}