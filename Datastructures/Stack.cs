using System.Collections;
using System.Text;

namespace Datastructures;

/// <summary>
/// Stack.
/// </summary>
/// <typeparam name="T">Generic type</typeparam>
public class Stack<T> : IEnumerable<T>
{
    private static readonly uint MAX_STACK = 1000;
    private T[] stack;
    private uint topOfStack;

    /// <summary>
    /// Constructor.
    /// </summary>
    public Stack()
    {
        stack = new T[MAX_STACK];
        topOfStack = 0;
    }

    /// <summary>
    /// Height of this stack.
    /// </summary>
    /// <returns>The height</returns>
    public int Height() => (int)topOfStack;

    /// <summary>
    /// Insert data on top of the stack.
    /// </summary>
    /// <param name="data">Data</param>
    /// <exception cref="StackOverflowException">Stack is filled</exception>
    public void Push(T data)
    {
        if (topOfStack > MAX_STACK)
            throw new StackOverflowException("Stack is completely filled!");
        stack[topOfStack] = data;
        topOfStack++;
    }

    /// <summary>
    /// Delete data from top of the stack.
    /// </summary>
    /// <returns>The removed data</returns>
    /// <exception cref="InvalidOperationException">Stack is empty</exception>
    public T Pop()
    {
        if (topOfStack == 0)
            throw new InvalidOperationException("Stack is empty!");
        return stack[--topOfStack];
    }

    /// <summary>
    /// Checks if stack is empty.
    /// </summary>
    /// <returns>Is empty</returns>
    public bool IsEmpty() => topOfStack == 0;

    /// <summary>
    /// Get the element on top of the stack.
    /// </summary>
    /// <returns>Data</returns>
    /// <exception cref="InvalidOperationException">Stack is empty</exception>
    public T Peek()
    {
        if (topOfStack == 0)
            throw new InvalidOperationException("Stack is empty!");
        return stack[topOfStack - 1];
    }

    /// <summary>
    /// Get the enumerator of the stack.
    /// </summary>
    /// <returns>Enumerator</returns>
    public IEnumerator<T> GetEnumerator()
    {
        for (int i = (int)topOfStack - 1; i >= 0; i--)
            yield return stack[i];
    }

    /// <summary>
    /// String representation of the stack.
    /// </summary>
    /// <returns>String representation</returns>
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder("Stack=[");
        for (int i = 0; i < topOfStack; i++)
            builder.Append($"{stack[i]},");
        builder.Remove(builder.Length - 1, 1);
        return builder.Append("]").ToString();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}