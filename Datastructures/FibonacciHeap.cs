using System.Text;

namespace Datastructures;

/// <summary>
/// Fibonacci heap.
/// </summary>
/// <typeparam name="T">Type must be comparable</typeparam>
public class FibonacciHeap<T> where T : IComparable, new()
{
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
        /// Degree stores the node's amount of children.
        /// </summary>
        public uint Degree { get; set; }
        
        /// <summary>
        /// Flag storing if the node lost a child.
        /// </summary>
        public bool Mark { get; set; }
        
        /// <summary>
        /// Parent of the node.
        /// </summary>
        public Node Parent { get; set; }
        
        /// <summary>
        /// Left sibling of the node.
        /// </summary>
        public Node LeftSibling { get; set; }
        
        /// <summary>
        /// Right sibling of the node.
        /// </summary>
        public Node RightSibling { get; set; }
        
        /// <summary>
        /// Children of the node.
        /// </summary>
        public List<Node> Children { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="data">The data to contain</param>
        public Node(T data)
        {
            Data = data;
            LeftSibling = Parent = RightSibling = null;
            Degree = 0;
            Mark = false;
            Children = new List<Node>();
        }

        /// <summary>
        /// Should not be used externally!
        /// </summary>
        /// <param name="data">New data</param>
        public void DecreaseDataTo(T data)
        {
            if (data.CompareTo(Data) < 0)
                Data = data;
        }
    }

    private List<Node> root;
    private Node minimum;

    /// <summary>
    /// Amount of elements.
    /// </summary>
    public uint Size { get; private set; }
    
    /// <summary>
    /// Head node.
    /// </summary>
    public Node Head { get; private set; }
    
    /// <summary>
    /// Constructor.
    /// </summary>
    public FibonacciHeap()
    {
        Head = minimum = null;
        Size = 0;
        root = new List<Node>();
    }

    /// <summary>
    /// Checks if heap is empty.
    /// </summary>
    /// <returns>Boolean flag</returns>
    public bool IsEmpty() => Size == 0;

    /// <summary>
    /// Minimum of the fibonacci heap.
    /// </summary>
    /// <returns>Smallest value</returns>
    public T Minimum() => minimum == null ? new T() : minimum.Data;

    /// <summary>
    /// Insert a new node in the heap.
    /// </summary>
    /// <param name="data">Data for the new node</param>
    public void Insert(T data)
    {
        Node newElem = new Node(data);
        newElem.LeftSibling = newElem;
        newElem.RightSibling = newElem;

        if (Head != null)
        {
            Head.LeftSibling.RightSibling = newElem;
            newElem.LeftSibling = Head.LeftSibling;
            newElem.RightSibling = Head;
            Head.LeftSibling = newElem;
        }

        Head = newElem;
        root.Add(newElem);

        Size++;

        if (minimum == null)
            minimum = newElem;
        else if (data.CompareTo(minimum.Data) < 0)
            minimum = newElem;
    }

    /// <summary>
    /// Removes the smallest node from the root.
    /// </summary>
    /// <returns>The old minimum</returns>
    public T ExtractMin()
    {
        Node smallest = minimum;
        if (minimum != null)
        {
            foreach (Node child in smallest.Children)
            {
                Insert(child.Data);
            }

            root.Remove(smallest);
            smallest.RightSibling.LeftSibling = smallest.LeftSibling;
            smallest.LeftSibling.RightSibling = smallest.RightSibling;
            if (smallest == smallest.RightSibling)
                minimum = Head = null;
            else
            {
                minimum = Head = smallest.RightSibling;
                Consolidate();
            }

            Size--;
        }

        return smallest.Data;
    }

    private void Consolidate()
    {
        int dMin = Convert.ToInt32(Math.Ceiling(Math.Log2(Size + 1)));
        List<List<Node>> newForest = new List<List<Node>>(dMin);
        for (int i = 0; i < dMin; i++)
            newForest.Add(new List<Node>());

        Node cur = Head;

        while (cur != null)
        {
            Node nextCur = cur.RightSibling;
            cur.Parent = null;
            cur.RightSibling = null;
            cur.LeftSibling = null;

            uint index = cur.Degree;

            newForest[(int)index].Add(cur);
            cur = nextCur;
        }

        for (int i = 0; i < dMin; i++)
        {
            if (newForest[i] != null)
            {
                int numOfDegreeTrees = newForest[i].Count;
                while (numOfDegreeTrees >= 2)
                {
                    Node x = newForest[i][0];
                    newForest[i].RemoveAt(0);
                    Node y = newForest[i][0];
                    newForest[i].RemoveAt(0);

                    Node tree = x.Data.CompareTo(y.Data) < 0 ? FibHeapLink(x, y) : FibHeapLink(y, x);
                    
                    newForest[i + 1].Add(tree);
                    numOfDegreeTrees -= 2;
                }
            }
        }

        cur = null;
        Head = null;
        root = new List<Node>();

        for (int i = newForest.Count - 1; i >= 0; i--)
        {
            List<Node> trees = newForest[i];
            if (trees.Count == 0) continue;

            Node tree = trees[0];
            root.Add(tree);
            if (cur == null)
            {
                cur = tree;
                Head = cur;
            }
            else
            {
                cur.RightSibling = tree;
                tree.LeftSibling = cur;
                cur = cur.RightSibling;
            }
        }

        minimum = Head;
        foreach (Node tree in root)
        {
            if (minimum.Data.CompareTo(tree.Data) > 0)
                minimum = tree;
        }
    }

    private Node FibHeapLink(Node x, Node y)
    {
        y.Parent = x;
        if (y.RightSibling != null)
            y.RightSibling.LeftSibling = y.LeftSibling;
        if (y.LeftSibling != null)
            y.LeftSibling.RightSibling = y.RightSibling;
        if (x.Children.Count > 0)
        {
            y.LeftSibling = x.Children[^1];
            x.Children[^1].RightSibling = y; 
            y.RightSibling = x.Children[0];
            x.Children[0] = y;
        }

        x.LeftSibling = x.RightSibling = null;
        x.Children.Add(y);
        x.Degree++;
        y.Mark = false;
        return x;
    }

    /// <summary>
    /// Decreases the priority of a node.
    /// </summary>
    /// <param name="x">Node to be decreased</param>
    /// <param name="k">New value</param>
    /// <exception cref="ArgumentException">Value must be lower than x's priority</exception>
    public void DecreaseKey(Node x, T k)
    {
        if (k.CompareTo(x.Data) > 0)
            throw new ArgumentException("k must not be greater than x!");
        x.DecreaseDataTo(k);
        Node parent = x.Parent;
        if (parent != null && parent.Data.CompareTo(x.Data) > 0)
        {
            Cut(x, parent);
            CascadingCut(parent);
        }

        if (minimum.Data.CompareTo(x.Data) > 0)
            minimum = x;
    }

    private void Cut(Node x, Node y)
    {
        y.Children.Remove(x);
        if (x.RightSibling != null)
            x.RightSibling.LeftSibling = x.LeftSibling;
        if (x.LeftSibling != null)
            x.LeftSibling.RightSibling = x.RightSibling;
        y.Degree--;
        Insert(x.Data);
    }

    private void CascadingCut(Node x)
    {
        Node y = x.Parent;
        if (y != null)
        {
            if (y.Mark == false)
                y.Mark = true;
            else
            {
                Cut(x, y);
                CascadingCut(y);
            }
        }
    }

    /// <summary>
    /// Remove a node from the fibonacci heap.
    /// </summary>
    /// <param name="node">Node to be removed</param>
    /// <returns>The value of the removed element</returns>
    public T Delete(Node node)
    {
        T val = node.Data;
        DecreaseKey(node, minimum.Data);
        ExtractMin();
        return val;
    }

    /// <summary>
    /// Union two fibonacci heaps.
    /// </summary>
    /// <param name="other">Other fibonacci heap</param>
    public void Union(FibonacciHeap<T> other)
    {
        List<Node> newRoot = root;
        foreach (Node r in other.root)
            newRoot.Add(r);

        Node cur = other.Head;
        Node curLeft = cur.LeftSibling;
        Node headRight = Head.RightSibling;

        Head.RightSibling = cur;
        headRight.LeftSibling = curLeft;
        cur.LeftSibling = cur;
        curLeft.RightSibling = headRight;

        minimum = (Minimum().CompareTo(other.Minimum()) > 0 ? other.minimum : minimum);

        Size += other.Size;

        root = newRoot;
    }

    /// <summary>
    /// Traverses elements to a list.
    /// </summary>
    /// <returns>List of data</returns>
    public T[] GetElements()
    {
        return GetElements(root).ToArray();
    }

    private List<T> GetElements(List<Node> children)
    {
        List<T> newList = new List<T>();
        foreach (Node node in children)
        {
            newList.Add(node.Data);
            if (node.Children.Count > 0)
            {
                List<T> items = GetElements(node.Children);
                foreach (T item in items)
                {
                    newList.Add(item);
                }
            }
        }

        return newList;
    }
    
    /// <summary>
    /// String representing all elements in the fibonacci heap.
    /// </summary>
    /// <returns>A string representation</returns>
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder($"{this.GetType().Name.Split('`')[0]}=[");
        T[] elements = GetElements();
        foreach (T data in elements)
            builder.Append($"{data.ToString()},");
        if (!'['.Equals(builder[^1]))
            builder.Remove(builder.Length - 1, 1);
        builder.Append(']');
        return builder.ToString();
    }
}