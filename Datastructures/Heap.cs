using System.Collections;
using System.Text;

namespace Datastructures;

/// <summary>
/// Binary heap.
/// </summary>
/// <typeparam name="T">Type must be comparable</typeparam>
public abstract class Heap<T> : IEnumerable<T> where T : IComparable, new()
{
    /// <summary>
    /// Exception class for wrong type of heap used.
    /// </summary>
    protected class NotDeterminableException : Exception
    {
        private static string MESSAGE = "Wrong type of heap for this action. Attribute cannot be determined!";
        
        /// <summary>
        /// Constructor.
        /// </summary>
        public NotDeterminableException() : base(MESSAGE)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Message</param>
        public NotDeterminableException(string? message) : base(message)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="innerException">Exception raised</param>
        public NotDeterminableException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
    
    /// <summary>
    /// A single heap node.
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

    protected uint elementCount = 0;
    
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="root">Root node</param>
    public Heap(T root)
    {
        Node node = new Node(root);
        this.root = node;
        Height = 1;
        elementCount = 1;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    public Heap()
    {
        Height = 0;
    }

    protected abstract void Heapify(Node node);

    protected void CreateHeap()
    {
        Node current = root;
        uint i = 0;
        while (i <= elementCount / 2 && current != null)
        {
            Heapify(current);
            if (current.IsLeftChild())
                current = current.GetSibling();
            else if (current.IsRightChild())
                current = current.Parent.GetSibling().LeftChild;
            else
            {
                if (current.Parent != null && current.Parent.Parent != null)
                    current = current.Parent.GetSibling().LeftChild.LeftChild;
                else
                    current = current.LeftChild;
            }
            i++;
        }
    }

    public void Insert(T data)
    {
        Node newElem = new Node(data);
        if (root == null)
            root = newElem;
        else
        {
            Node current = root;
            while (current != null && current.LeftChild != null)
                current = current.LeftChild;
            current.LeftChild = newElem;
        }
        elementCount++;
        CreateHeap();
    }

    public T Delete(T data)
    {
        List<T> list = ((T[]) this).ToList();
        list.Remove(data);
        MaxHeap<T> result = (MaxHeap<T>) list.ToArray();
        this.root = result.root;
        return data;
    }

    public abstract T Minimum();

    public abstract T Maximum();

    /// <summary>
    /// Traversing in Inorder-sequence.
    /// </summary>
    /// <returns>A string representation</returns>
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder($"{this.GetType().Name.Split('`')[0]}=[");
        foreach (T data in this)
            builder.Append($"{data},");
        if (!'['.Equals(builder[^1]))
            builder.Remove(builder.Length - 1, 1);
        builder.Append(']');
        return builder.ToString();
    }


    public IEnumerator<T> GetEnumerator()
    {
        T[] list = (T[])this;
        foreach (T elem in list)
            yield return elem;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private static void GetIndexOfNode(T[] arr, Node node, int height)
    {
        arr[height] = node.Data;
        if (node.LeftChild != null)
            GetIndexOfNode(arr, node.LeftChild, (height + 1) * 2 - 1);
        if (node.RightChild != null)
            GetIndexOfNode(arr, node.RightChild, (height + 1) * 2);
    }

    private static void GetNodeOfIndex(List<T> arr, ref Node node, int height, ref uint count)
    {
        node = new Node(arr[height]);
        count++;
        int leftIdx = (height + 1) * 2 - 1;
        if (arr.Count >= leftIdx + 1)
        {
            Node left = node.LeftChild;
            GetNodeOfIndex(arr, ref left, leftIdx, ref count);
            node.LeftChild = left;
        }
        int rightIdx = (height + 1) * 2;
        if (arr.Count >= rightIdx + 1)
        {
            Node right = node.RightChild;
            GetNodeOfIndex(arr, ref right, rightIdx, ref count);
            node.RightChild = right;
        }
    }
    
    public static explicit operator T[](Heap<T> heap)
    {
        T[] result = new T[Convert.ToInt32(heap.elementCount)];
        for (int i = 0; i < result.Length; i++)
            result[i] = new T();
        GetIndexOfNode(result, heap.root, 0);
        return result;
    }
    
    public static explicit operator Heap<T>(T[] heap)
    {
        List<T> list = heap.ToList();
        Heap<T> result = new MaxHeap<T>();
        uint resultCount = 0;
        GetNodeOfIndex(list, ref result.root, 0, ref resultCount);
        result.elementCount = resultCount;
        // result.CreateHeap();
        return result;
    }
}