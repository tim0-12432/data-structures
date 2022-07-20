namespace Datastructures;

/// <summary>
/// Binary max-heap.
/// </summary>
/// <typeparam name="T">Type must be comparable</typeparam>
public class MaxHeap<T> : Heap<T> where T : IComparable, new()
{
    protected override void Heapify(Node node)
    {
        Node max = node;
        Node left = node.LeftChild;
        Node right = node.RightChild;
        if (left != null && left.Data.CompareTo(node.Data) > 0)
            max = left;
        if (right != null && right.Data.CompareTo(max.Data) > 0)
            max = right;
        if (!max.Equals(node))
        {
            Node parent = node.Parent;
            Node leftTree = node.LeftChild;
            Node rightTree = node.RightChild;
            if (parent != null)
            {
                if (node.IsLeftChild())
                    parent.LeftChild = max;
                else
                    parent.RightChild = max;
            }
            node.Parent = max;
            node.RightChild = max.RightChild;
            if (max.RightChild != null)
            {
                node.RightChild.Parent = node;
            }
            node.LeftChild = max.LeftChild;
            if (max.LeftChild != null)
            {
                node.LeftChild.Parent = node;
            }
            max.LeftChild = leftTree;
            max.RightChild = rightTree;
            Heapify(max);
        }
    }

    public override T Minimum()
    {
        throw new NotDeterminableException("Minimum is hard to spot! Use a Min-Heap to get the minimum easily!");
    }

    public override T Maximum()
    {
        return root.Data;
    }
}