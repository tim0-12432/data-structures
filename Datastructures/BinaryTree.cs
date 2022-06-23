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
    /// <typeparam name="T">Same type as the tree</typeparam>
    public class Node<T>
    {
        /// <summary>
        /// Data to contain.
        /// </summary>
        public T Data { get; private set; }
        
        /// <summary>
        /// Parent of the node.
        /// </summary>
        public Node<T> Parent { get; set; }
        
        /// <summary>
        /// Left child of the node.
        /// </summary>
        public Node<T> LeftChild { get; set; }
        
        /// <summary>
        /// Right child of the node.
        /// </summary>
        public Node<T> RightChild { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="data">The data to contain</param>
        public Node(T data)
        {
            Data = data;
            LeftChild = Parent = RightChild = null;
        }
    }
    
    /// <summary>
    /// Height of the tree;
    /// </summary>
    public int Height { get; private set; }

    private Node<T> root = null;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="root">Root node.</param>
    public BinaryTree(T root)
    {
        Node<T> node = new Node<T>(root);
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
        Node<T> newElement = new Node<T>(data);
        Node<T> current = root;
        Node<T> element = null;
        int newHeight = 1;
        while (current != null)
        {
            element = current;
            if (newElement.Data.CompareTo(current.Data) == -1)
                current = current.LeftChild;
            else
                current = current.RightChild;
            newHeight++;
        }
        newElement.Parent = element;
        if (element == null)
        {
            root = newElement;
            newHeight = 1;
        }
        else
        {
            if (newElement.Data.CompareTo(element.Data) == -1)
                element.LeftChild = newElement;
            else
                element.RightChild = newElement;
        }

        if (newHeight > Height)
            Height = newHeight;
    }

    /// <summary>
    /// Delete a node from the tree.
    /// </summary>
    /// <param name="element">Node to remove</param>
    /// <returns>The removed data</returns>
    public T Delete(Node<T> element)
    {
        Node<T> parent = element.Parent;
        Node<T> transplantation = null;
        if (element.LeftChild == null && element.RightChild == null)
            transplantation = null;
        else if (element.LeftChild != null && element.RightChild == null)
            transplantation = element.LeftChild;
        else if (element.RightChild != null && element.LeftChild == null)
            transplantation = element.RightChild;
        else
        {
            Node<T> min = Minimum(element.RightChild);
            min.LeftChild = element.LeftChild;
            transplantation = min;
        }

        if (parent == null)
            root = transplantation;
        else if (element.Equals(parent.LeftChild))
            parent.LeftChild = transplantation;
        else
            parent.RightChild = transplantation;
        return element.Data;
    }

    /// <summary>
    /// Minimum in the tree.
    /// </summary>
    /// <param name="subtree">Start from a certain node</param>
    /// <returns>The smallest node</returns>
    public Node<T> Minimum(Node<T>? subtree)
    {
        Node<T> current = subtree ?? root;
        if (current == null)
            return new Node<T>(new T());
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
    public Node<T> Maximum(Node<T>? subtree)
    {
        Node<T> current = subtree ?? root;
        if (current == null)
            return new Node<T>(new T());
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
    public Node<T> Search(T data)
    {
        Node<T> current = root;
        while (current != null && !data.Equals(current.Data))
        {
            if (data.CompareTo(current.Data) == -1)
                current = current.LeftChild;
            else
                current = current.RightChild;
        }
        return current ?? new Node<T>(new T());
    }

    /// <summary>
    /// Find the predecessor of a node.
    /// </summary>
    /// <param name="node">Node</param>
    /// <returns>The predecessor of the node</returns>
    public Node<T> Predecessor(Node<T> node)
    {
        if (node.LeftChild != null)
            return Maximum(node.LeftChild);
        Node<T> parent = node.Parent;
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
    public Node<T> Successor(Node<T> node)
    {
        if (node.RightChild != null)
            return Minimum(node.RightChild);
        Node<T> parent = node.Parent;
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
        Node<T> current = Minimum(root);
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
}