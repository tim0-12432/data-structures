using System.Collections;
using System.Text;

namespace Datastructures;

/// <summary>
/// Queue implementation based on a list.
/// </summary>
/// <typeparam name="T">Type</typeparam>
public class Queue<T> : IEnumerable<T> where T : new()
{
    private LinkedList<T> queue = new LinkedList<T>();

    /// <summary>
    /// Adds a element to the end of the queue.
    /// </summary>
    /// <param name="data">Element</param>
    public void Enqueue(T data)
    {
        queue.AddLast(data);
    }

    /// <summary>
    /// Get the length of the queue.
    /// </summary>
    /// <returns>The length</returns>
    public int Length() => queue.Count;

    /// <summary>
    /// Checks if queue is empty.
    /// </summary>
    /// <returns>Is empty or not</returns>
    public bool IsEmpty() => queue.Count == 0;

    /// <summary>
    /// Get the next element.
    /// </summary>
    /// <returns>The next element</returns>
    public T Peek()
    {
        return queue.First == null ? new T() : queue.First.Value;
    }

    /// <summary>
    /// Delete the first element in the queue.
    /// </summary>
    /// <returns>First element</returns>
    public T Dequeue()
    {
        T element = queue.First == null ? new T() : queue.First.Value;
        queue.RemoveFirst();
        return element;
    }

    /// <summary>
    /// Get the enumerator of the queue.
    /// </summary>
    /// <returns>Enumerator</returns>
    public IEnumerator<T> GetEnumerator()
    {
        return queue.GetEnumerator();
    }

    /// <summary>
    /// Return a string representation of the queue.
    /// </summary>
    /// <returns>A string representation</returns>
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder("Queue=[");
        foreach (T elem in queue)
        {
            builder.Append($"{elem},");
        }

        builder.Remove(builder.Length - 1, 1);
        return builder.Append("]").ToString();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}