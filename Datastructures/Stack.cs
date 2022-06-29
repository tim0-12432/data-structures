using System.Collections;
using System.Text;

namespace Datastructures;

/// <summary>
/// Stack.
/// </summary>
/// <typeparam name="T">Generic type</typeparam>
public class Stack<T> : IEnumerable<T>
{
    /// <summary>
    /// A element of this stack.
    /// </summary>
    public class StackElement
    {
        /// <summary>
        /// Next element in stack.
        /// </summary>
        public StackElement Next { get; set; } = null;
        
        /// <summary>
        /// Data of this element.
        /// </summary>
        public T Data { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="data">Data of this element</param>
        public StackElement(T data)
        {
            Data = data;
        }
    }
    
    private StackElement topOfStack = null;

    /// <summary>
    /// Height of this stack.
    /// </summary>
    /// <returns>The height</returns>
    public int Height { get; private set; } = 0;

    /// <summary>
    /// Insert data on top of the stack.
    /// </summary>
    /// <param name="data">Data</param>
    /// <exception cref="StackOverflowException">Stack is filled</exception>
    public void Push(T data)
    {
        StackElement neu = new StackElement(data);
        if (topOfStack != null)
        {
            neu.Next = topOfStack;
        }
        topOfStack = neu;
        Height++;
    }

    /// <summary>
    /// Delete data from top of the stack.
    /// </summary>
    /// <returns>The removed data</returns>
    /// <exception cref="InvalidOperationException">Stack is empty</exception>
    public T Pop()
    {
        StackElement elem = topOfStack;
        if (elem == null)
            throw new InvalidOperationException("Cannot pop from empty Stack!");
        topOfStack = elem.Next;
        Height--;
        return elem.Data;
    }

    /// <summary>
    /// Checks if stack is empty.
    /// </summary>
    /// <returns>Is empty</returns>
    public bool IsEmpty() => topOfStack == null;

    /// <summary>
    /// Get the element on top of the stack.
    /// </summary>
    /// <returns>Data</returns>
    /// <exception cref="InvalidOperationException">Stack is empty</exception>
    public T Peek()
    {
        if (topOfStack == null)
            throw new InvalidOperationException("Stack is empty!");
        return topOfStack.Data;
    }
    
    /// <summary>
    /// Reverse the stack.
    /// </summary>
    public void Reverse()
    {
        StackElement elem = topOfStack;
        if (elem != null && elem.Next != null)
        {
            StackElement before = null;
            while (elem != null)
            {
                StackElement next = elem.Next;
                elem.Next = before;
                before = elem;
                elem = next;
            }

            topOfStack = before;
        }
    }

    /// <summary>
    /// Get the enumerator of the stack.
    /// </summary>
    /// <returns>Enumerator</returns>
    public IEnumerator<T> GetEnumerator()
    {
        StackElement current = topOfStack;
        while (current != null)
        {
            yield return current.Data;
            current = current.Next;
        }
    }

    /// <summary>
    /// String representation of the stack.
    /// </summary>
    /// <returns>String representation</returns>
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder("Stack=[");
        StackElement current = topOfStack;
        while (current != null)
        {
            builder.Append($"{current.Data},");
            current = current.Next;
        }
        builder.Remove(builder.Length - 1, 1);
        return builder.Append("]").ToString();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}