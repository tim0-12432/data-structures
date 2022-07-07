using System.Collections;

namespace Datastructures;

public class BTree<T> where T : IComparable, new()
{
    public class Node : IEnumerable
    {
        private readonly uint nodeDegree;
        
        private List<T> nodeList = new List<T>();

        private List<Node> references = new List<Node>();
        
        public Node Parent { get; set; }

        public Node(uint nodeDegree, T data)
        {
            this.nodeDegree = nodeDegree;
            nodeList.Add(data);
        }

        public T GetNodeData(uint index)
        {
            return nodeList[(int)index];
        }
        
        public Node GetReference(uint index)
        {
            return references[(int)index];
        }
        
        public void SetNodeData(uint index, T data)
        {
            nodeList[(int)index] = data;
        }
        
        public void SetReference(uint index, Node reference)
        {
            references[(int)index] = reference;
        }

        public T this[uint index]
        {
            get => GetNodeData(index);
            set => SetNodeData(index, value);
        }

        public bool Validate()
        {
            if (nodeList.Count > 2 * nodeDegree - 1 || nodeList.Count < nodeDegree - 1)
                return false;
            if (references.Count > 2 * nodeDegree)
                return false;
            if (references.Count != nodeList.Count + 1)
                return false;
            return true;
        }

        public bool ShouldSplit()
        {
            if (nodeList.Count >= 2 * nodeDegree - 1)
                return true;
            return false;
        }

        public int GetNodeIndex(T data)
        {
            T result = nodeList.Find(d => d.Equals(data));
            if (result == null)
                return -1;
            return nodeList.IndexOf(result);
        }

        public int GetReferenceIndex(T data)
        {
            if (GetNodeIndex(data) == -1)
            {
                if (nodeList[0].CompareTo(data) > 0)
                    return 0;
                T before;
                for (int i = 0; i < nodeList.Count; i++)
                {
                    if (nodeList[i].CompareTo(data) < 0)
                        before = nodeList[i];
                    else if (nodeList[i].CompareTo(data) > 0)
                        return i - 1;
                }

                return nodeList.Count;
            }
            return -1;
        }

        public IEnumerator GetEnumerator()
        {
            return nodeList.GetEnumerator();
        }

        public void Split()
        {
            if (ShouldSplit())
            {
                int midIdx = nodeList.Count / 2;
            }
        }
    }

    private readonly uint nodeDegree = 2;

    private Node root = null;
    
    public uint Height { get; private set; } = 0;
    
    public BTree(uint degree)
    {
        nodeDegree = degree;
    }

    public T Search(T key)
    {
        Node current = root;
        while (current != null)
        {
            int listIndex = current.GetNodeIndex(key);
            if (listIndex != -1)
                return current.GetNodeData(Convert.ToUInt32(listIndex));
            current = current.GetReference(Convert.ToUInt32(current.GetReferenceIndex(key)));
        }

        return new T();
    }

    public void Insert(T data)
    {
        
    }

    public T Delete(T key)
    {
        
    }
}