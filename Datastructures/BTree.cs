using System.Collections;

namespace Datastructures;

public class BTree<T> where T : IComparable, new()
{
    public class Node : IEnumerable
    {
        private readonly uint nodeDegree;
        
        public List<T> Values { get; set; } = new List<T>();

        public List<Node> Children { get; set; } = new List<Node>();

        public bool IsLeaf { get; set; } = false;
        
        public Node Parent { get; set; }

        public Node(uint nodeDegree, T data)
        {
            this.nodeDegree = nodeDegree;
            Values.Add(data);
        }
        
        public Node(uint nodeDegree)
        {
            this.nodeDegree = nodeDegree;
        }

        public bool Validate()
        {
            if (Values.Count > 2 * nodeDegree - 1 || Values.Count < nodeDegree - 1)
                return false;
            if (Children.Count != Values.Count + 1)
                return false;
            return true;
        }

        public bool ShouldSplit()
        {
            if (Values.Count >= 2 * nodeDegree - 1)
                return true;
            return false;
        }

        public IEnumerator GetEnumerator()
        {
            return Values.GetEnumerator();
        }
    }

    private readonly uint nodeDegree = 2;

    private Node root = null;
    
    public uint Size { get; private set; }

    public uint Height { get; private set; } = 0;
    
    public BTree(uint degree)
    {
        nodeDegree = degree;
    }
    
    public BTree()
    {
    }

    public bool IsEmpty() => Size == 0; 
    

    public T Search(T value)
    {
        if (root != null)
            return new T();

        return Find(root, value);
    }

    private T Find(Node root, T k)
    {
        List<T> values = root.Values;

        int i = values.Count - 1;

        if (k.CompareTo(values[i]) > 0)
            i++;
        else
        {
            while (i > 0 && k.CompareTo(values[i]) < 0)
                i--;

            if (k.CompareTo(values[i]) > 0)
                i++;
        }

        if (i < values.Count && k.CompareTo(values[i]) == 0)
            return root.Values[i];
        if (root.IsLeaf)
            return new T();
        return Find(root.Children[i], k);
    }

    public void Insert(T data)
    {
        if (root != null && root.ShouldSplit())
        {
            Node newRoot = new Node(nodeDegree, data);
            newRoot.IsLeaf = false;
            newRoot.Children.Add(root);

            SplitChild(newRoot, 0);

            Height++;

            InsertHelper(newRoot, data);
        }
        else if (root == null)
        {
            root = new Node(nodeDegree, data);
            InsertHelper(root, data);
        }
        else
        {
            root = new Node(nodeDegree, data);
            root.IsLeaf = true;
            
            Height++;
        }

        Size++;
    }

    private void InsertHelper(Node node, T data)
    {
        if (node != null && node.IsLeaf)
        {
            int i = node.Values.Count - 1;

            if (data.CompareTo(node.Values[i]) > 0)
                node.Values.Insert(i + 1, data);
            else
            {
                while (i > 0 && data.CompareTo(node.Values[i]) < 0)
                    i--;

                if (data.CompareTo(node.Values[i]) > 0)
                    i++;
                
                node.Values.Insert(i, data);
            }
        }
        else
        {
            int i = node.Values.Count - 1;
            
            while (i > 0 && data.CompareTo(node.Values[i]) < 0)
                i--;

            i++;

            if (node.Children[i].ShouldSplit())
            {
                SplitChild(node.Children[i], Convert.ToUInt32(i));
                if (data.CompareTo(node.Values[i]) > 0)
                    i++;
            }
            
            InsertHelper(node.Children[i], data);
        }
    }

    private void SplitChild(Node x, uint i)
    {
        Node y = x.Children[(int)i];
        T medianValue = y.Values[((int)nodeDegree - 1)];

        Node z = new Node(nodeDegree);
        z.IsLeaf = y.IsLeaf;

        uint firstValueAfterMiddle = nodeDegree;
        for (uint j = firstValueAfterMiddle; j < nodeDegree * 2 - 1; j++)
            z.Values.Add(y.Values[(int)j]);

        if (!y.IsLeaf)
        {
            uint firstChildOfSecondHalf = nodeDegree;
            for (uint j = firstChildOfSecondHalf; j < nodeDegree * 2; j++)
                z.Children.Add(y.Children[(int)j]);
        }

        y.Values = y.Values.GetRange(0, (int)nodeDegree - 1);
        if (!y.IsLeaf) y.Children = y.Children.GetRange(0, (int)nodeDegree);

        x.Children.Insert((int)i + 1, z);

        x.Values.Insert((int)i, medianValue);
    }

    public void Delete(T key)
    {
        if (root == null) return;

        Delete(root, key);

        if (root.Values.Count == 0)
        {
            root = root.Children[0];
            Height--;
        }
    }

    private void Delete(Node x, T k)
    {
        int i = x.Values.Count - 1;
        
        if (k.CompareTo(x.Values[i]) > 0)
            i++;
        else
        {
            while (i > 0 && k.CompareTo(x.Values[i]) < 0)
                i--;
        }

        bool valueIsInX = i < x.Values.Count && x.Values[i].Equals(k);
        
        if (valueIsInX && x.IsLeaf) {
            x.Values.Insert(i, new T());
            return;
        }
        if (valueIsInX && !x.IsLeaf)
        {
            Node y = x.Children[i];
            Node z = x.Children[i + 1];

            if (y.Values.Count >= nodeDegree)
            {
                x.Values[i] = y.Values[^1];

                y.Values = y.Values.GetRange(0, y.Values.Count - 1);
            }
            else if (z.Values.Count >= nodeDegree)
            {
                x.Values[i] = z.Values[0];

                z.Values.Insert(0, new T());
            }
            else
            {
                foreach (T value in z.Values)
                    y.Values.Add(value);

                x.Values.Insert(i, new T());

                x.Children.Insert(i + 1, new Node(nodeDegree));
            }
        }

        if (x.IsLeaf) return;

        if (x.Children[i].Values.Count == nodeDegree - 1)
        {
            bool childHasRightSibling = i < x.Children.Count - 1;
              bool rightSiblingHasValuesToGive = x.Children[i + 1].Values.Count >= nodeDegree;
              if (childHasRightSibling && rightSiblingHasValuesToGive)
              {
                  x.Children[i].Values.Add(x.Values[i]);

                T firstValueOfRightSibling = x.Children[i + 1].Values[0];
                if (firstValueOfRightSibling == null)
                    throw new ArgumentException();
                x.Values[i] = firstValueOfRightSibling;
              }

              bool childHasLeftSibling = i > 0;
              bool leftSiblingHasValuesToGive = x.Children[i - 1].Values.Count >= nodeDegree;

              if (childHasLeftSibling && leftSiblingHasValuesToGive)
              {
                  x.Children[i].Values.Add(x.Values[i]);

                    T lastValueOfLeftSibling = x.Children[i + 1].Values[0];
                    x.Children[i + 1].Values.RemoveAt(0);
                    if (lastValueOfLeftSibling == null) throw new ArgumentException();
                    x.Values[i] = lastValueOfLeftSibling;
              }

              if (childHasRightSibling)
              {
                  Node child = x.Children[i];
                  Node rightSibling = x.Children[i + 1];

                    child.Values.Add(x.Values[i]);

                    foreach (T value in rightSibling.Values)
                        child.Values.Add(value);

                    x.Values.Insert(i, new T());

                    x.Children.Insert(i + 1, new Node(nodeDegree));
              }
              else
              {
                  Node child = x.Children[i];
                  Node leftSibling = x.Children[i - 1];

                  child.Values.Add(x.Values[i]);

                foreach (T value in leftSibling.Values)
                    child.Values.Add(value);

                x.Values.Insert(i, new T());

                x.Children.Insert(i - 1, new Node(nodeDegree));
              }
        }

        Delete(x.Children[i], k);
    }
}