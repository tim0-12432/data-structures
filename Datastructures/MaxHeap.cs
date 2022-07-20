namespace Datastructures;

/// <summary>
/// Binary max-heap.
/// </summary>
/// <typeparam name="T">Type must be comparable</typeparam>
public class MaxHeap<T> : Heap<T> where T : IComparable, new()
{
    protected override bool Compare(T a, T b) => a.CompareTo(b) > 0;

    /// <summary>
    /// Get the greatest element in the heap.
    /// </summary>
    /// <returns>The maximum</returns>
    public T Maximum() => Items[0];
}