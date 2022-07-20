using System.Text;

namespace Datastructures;

/// <summary>
/// A hashtable.
/// </summary>
/// <typeparam name="T">Type</typeparam>
public class Hashtable<T> where T : new()
{
    private readonly int _maxTableSize = 50;
    private readonly LinkedList<T>[] _table;

    /// <summary>
    /// Constructor.
    /// </summary>
    public Hashtable()
    {
        _table = new LinkedList<T>[_maxTableSize];
        for (int i = 0; i < _maxTableSize; i++)
        {
            _table[i] = new LinkedList<T>();
        }
    }
    
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="m">Maximum table size</param>
    public Hashtable(uint m)
    {
        _maxTableSize = (int)m;
        _table = new LinkedList<T>[m];
        for (int i = 0; i < m; i++)
        {
            _table[i] = new LinkedList<T>();
        }
    }

    /// <summary>
    /// Insert a element.
    /// </summary>
    /// <param name="data">Data of the element</param>
    public void Insert(T data)
    {
        int index = GetHash(data);
        _table[index].AddFirst(data);
    }

    /// <summary>
    /// Delete data from the hashtable.
    /// </summary>
    /// <param name="data">Data to search for</param>
    /// <returns>The removed data</returns>
    public T Delete(T data)
    {
        int index = GetHash(data);
        LinkedListNode<T> element = _table[index].Find(data);
        _table[index].Remove(data);
        return element == null ? new T() : element.Value;
    }

    /// <summary>
    /// Search for an element in the hashtable.
    /// </summary>
    /// <param name="data">Data to search for</param>
    /// <returns>Found data</returns>
    public T Search(T data)
    {
        int index = GetHash(data);
        LinkedListNode<T> element = _table[index].Find(data);
        return element == null ? new T() : element.Value;
    }

    /// <summary>
    /// String representation of the hashtable.
    /// </summary>
    /// <returns>The string representation</returns>
    public override string ToString()
    {
        StringBuilder builder = new StringBuilder("Hashtable=[");
        foreach (LinkedList<T> cell in _table)
        {
            builder.Append('[');
            foreach (T elem in cell)
            {
                builder.Append($"{elem},");
            }
            if (!'['.Equals(builder[^1]))
                builder.Remove(builder.Length - 1, 1);
            builder.Append("],");
        }
        builder.Remove(builder.Length - 1, 1);
        return builder.Append(']').ToString();
    }

    private int GetHash(T data)
    {
        double number;
        if (data is string)
            number = BitConverter.ToDouble(Encoding.ASCII.GetBytes(data as string), 0);
        else if (data is int || data is float || data is double)
            number = Convert.ToDouble(data);
        else
            number = data.GetHashCode();

        return Convert.ToInt32(Math.Floor(8 * (number * (Math.Sqrt(5) - 1) / 2) % 701)) % _maxTableSize;
    }
}