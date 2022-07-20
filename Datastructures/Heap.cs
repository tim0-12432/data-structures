using System.Collections;
using System.Text;

namespace Datastructures;

/// <summary>
/// Binary heap.
/// </summary>
/// <typeparam name="T">Type must be comparable</typeparam>
public abstract class Heap<T> : IEnumerable<T> where T : IComparable, new()
{
    protected List<T> Items = new List<T>();
    protected uint CountOfItems;

    /// <summary>
    /// Constructor.
    /// </summary>
    public Heap()
    {
        CountOfItems = 0;
    }

    /// <summary>
    /// Amount of elements in the heap.
    /// </summary>
    /// <returns></returns>
    public uint Size() => CountOfItems;

    /// <summary>
    /// Check if heap is empty.
    /// </summary>
    /// <returns>Boolean</returns>
    public bool IsEmpty() => CountOfItems == 0;

    /// <summary>
    /// Insert a element to the heap.
    /// </summary>
    /// <param name="data">New element</param>
    public void Insert(T data)
    {
        Items.Insert(0, data);
        CountOfItems++;
        CreateHeap();
    }

    /// <summary>
    /// Extract the root element from the heap.
    /// </summary>
    /// <returns>The old root element</returns>
    public T ExtractRoot()
    {
        T root = Items[0];
        Items.RemoveAt(0);
        CountOfItems--;
        Heapify(0);
        return root;
    }
    
    private void CreateHeap()
    {
        for (int i = (int)CountOfItems / 2; i >= 0; i--)
            Heapify(0);
    }

    protected abstract bool Compare(T a, T b);

    private void Heapify(int rootIndex)
    {
        int indexToChange = rootIndex;
        int leftChildIndex = rootIndex * 2 + 1;
        if (leftChildIndex < CountOfItems && Compare(Items[leftChildIndex], Items[indexToChange]))
            indexToChange = leftChildIndex;

        int rightChildIndex = rootIndex * 2 + 2;
        if (rightChildIndex < CountOfItems && Compare(Items[rightChildIndex], Items[indexToChange]))
            indexToChange = rightChildIndex;

        if (indexToChange != rootIndex)
        {
            (Items[indexToChange], Items[rootIndex]) = (Items[rootIndex], Items[indexToChange]);
            Heapify(indexToChange);
        }
    }
    
    /// <summary>
    /// Enumerator of the heap.
    /// </summary>
    /// <returns>The enumerator</returns>
    public IEnumerator<T> GetEnumerator()
    {
        return Items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    
    /// <summary>
    /// A string representing the heap.
    /// </summary>
    /// <returns>String representation</returns>
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder($"{this.GetType().Name.Split('`')[0]}=[");
        foreach (T data in Items)
            builder.Append($"{data},");
        if (!'['.Equals(builder[^1]))
            builder.Remove(builder.Length - 1, 1);
        builder.Append(']');
        return builder.ToString();
    }
}