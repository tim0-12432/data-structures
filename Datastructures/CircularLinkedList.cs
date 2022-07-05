using System.Text;

namespace Datastructures;

/// <summary>
/// A circular double linked list.
/// </summary>
/// <typeparam name="T">Generic type</typeparam>
public class CircularLinkedList<T> : DoubleLinkedList<T>
{
    /// <summary>
    /// Append data at the end of the list.
    /// </summary>
    /// <param name="data">Data</param>
    public new void Append(T data)
    {
        base.Append(data);
        _first.Predecessor = _last;
        _last.Successor = _first;
    }
    
    /// <summary>
    /// Insert data at the start of the list.
    /// </summary>
    /// <param name="data">Data</param>
    public new void Prepend(T data)
    {
        base.Prepend(data);
        _first.Predecessor = _last;
        _last.Successor = _first;
    }
    
    /// <summary>
    /// Insert data after another element.
    /// </summary>
    /// <param name="data">Data of the other element</param>
    /// <param name="element">Data to insert</param>
    /// <exception cref="ArgumentException">Data does not exist</exception>
    public new void InsertAfter(T data, T element)
    {
        InsertAfter(data, element, _first);
    }
    
    /// <summary>
    /// Insert data after another element.
    /// </summary>
    /// <param name="data">Data of the other element</param>
    /// <param name="element">Data to insert</param>
    /// <param name="start">Element to start searching for the other element</param>
    /// <exception cref="ArgumentException">Data does not exist</exception>
    public void InsertAfter(T data, T element, ListElement<T> start)
    {
        ListElement<T> current = start;
        if (!current.Data.Equals(data))
        {
            current = current.Successor;
            while (current != null && current != start && !current.Data.Equals(data))
                current = current.Successor;
            if (current == null || current == start)
                throw new ArgumentException("Element with given data does not exist in list!");
        }
        ListElement<T> insertion = new ListElement<T>(element);
        insertion.Successor = current.Successor;
        current.Successor.Predecessor = insertion;
        insertion.Predecessor = current;
        current.Successor = insertion;
        _first.Predecessor = _last;
        _last.Successor = _first;
        Length++;
    }
    
    /// <summary>
    /// Delete a element with given data.
    /// </summary>
    /// <param name="data">Data to search for</param>
    /// <returns>Data</returns>
    /// <exception cref="ArgumentException">Data does not exist</exception>
    public new T Delete(T data)
    {
        return Delete(data, _first);
    }

    /// <summary>
    /// Delete a element with given data.
    /// </summary>
    /// <param name="data">Data to search for</param>
    /// <param name="start">Element to start searching for the data element</param>
    /// <returns>Data</returns>
    /// <exception cref="ArgumentException">Data does not exist</exception>
    public T Delete(T data, ListElement<T> start)
    {
        ListElement<T> current = start;
        if (current != null && !current.Data.Equals(data))
        {
            current = current.Successor;
            while (current != start && !current.Data.Equals(data))
                current = current.Successor;
        }
        if (current == null || !current.Data.Equals(data))
            throw new ArgumentException("Element with given data does not exist in list!");

        if (current.Predecessor == _last)
            _first = current.Successor;
        else
            current.Predecessor.Successor = current.Successor;

        if (current.Successor == _first)
            _last = current.Predecessor;
        else
            current.Successor.Predecessor = current.Predecessor;

        Length--;
        _first.Predecessor = _last;
        _last.Successor = _first;
        return current.Data;
    }
    
    /// <summary>
    /// Search for a element with given data.
    /// </summary>
    /// <param name="data">Data</param>
    /// <returns>ListElement with data</returns>
    /// <exception cref="ArgumentException">Data does not exist</exception>
    public new ListElement<T> Search(T data)
    {
        return Search(data, _first);
    }
    
    /// <summary>
    /// Search for a element with given data.
    /// </summary>
    /// <param name="data">Data</param>
    /// <param name="start">Element to start searching for the data element</param>
    /// <returns>ListElement with data</returns>
    /// <exception cref="ArgumentException">Data does not exist</exception>
    public ListElement<T> Search(T data, ListElement<T> start)
    {
        ListElement<T> current = start;
        if (current != null && !current.Data.Equals(data))
        {
            current = current.Successor;
            while (current != start && !current.Data.Equals(data))
                current = current.Successor;
        }
        if (current == null || !current.Data.Equals(data))
            throw new ArgumentException("Element with given data does not exist in list!");
        return current;
    }
    
    /// <summary>
    /// Check if data exists in list.
    /// </summary>
    /// <param name="data">Data</param>
    /// <returns>Amount of occurrences</returns>
    public new int Contains(T data)
    {
        int amount = 0;
        if (_first.Data.Equals(data))
            amount++;
        ListElement<T> current = _first.Successor;
        while (current != null && current != _first) {
            if (current.Data.Equals(data))
                amount++;
            current = current.Successor;
        }
        return amount;
    }

    /// <summary>
    /// Enumerator.
    /// </summary>
    /// <returns>List enumerator</returns>
    public new IEnumerator<T> GetEnumerator()
    {
        ListElement<T> current = _first;
        if (current != null)
        {
            yield return current.Data;
            current = current.Successor;
            while (current != _first)
            {
                yield return current.Data;
                current = current.Successor;
            }
        }
    }

    /// <summary>
    /// String representation of the list.
    /// </summary>
    /// <returns>String representation</returns>
    public override string ToString()
    {
        ListElement<T> current = _first;
        StringBuilder result = new StringBuilder("List=[" + (current != null ? current.Data : ""));
        if (current != null)
            current = current.Successor;
        while (current != null && current != _first ) {
            result.Append("," + current.Data);
            current = current.Successor;
        }
        return result + "]";
    }
}