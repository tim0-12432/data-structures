using System.Collections;
using System.Text;

namespace Datastructures;

/// <summary>
/// A singly linked list.
/// </summary>
/// <typeparam name="T">Generic type</typeparam>
public class SingleLinkedList<T> : IEnumerable<T>
{
    /// <summary>
    /// A element of the singly linked list.
    /// </summary>
    /// <typeparam name="TS">Type of the list</typeparam>
    public class ListElement<TS>
    {
        /// <summary>
        /// Successor of this element.
        /// </summary>
        public ListElement<TS> Successor { get; set; } = null;
        public TS Data { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="data">Data of the element</param>
        public ListElement(TS data)
        {
            Data = data;
        }
    }

    /// <summary>
    /// List iterator.
    /// </summary>
    /// <typeparam name="TU">Type of the list</typeparam>
    public class ListEnumerator<TU> : IEnumerator<TU>
    {
        private ListElement<TU> _first = null;
        private ListElement<TU> _current = null;

        public TU Current { get; private set; }

        public ListEnumerator(ListElement<TU> first)
        {
            _first = first;
        }

        public bool MoveNext()
        {
            if (_current == null)
            {
                _current = _first;
            }
            else
            {
                if (_current.Successor == null)
                    return false;
                _current = _current.Successor;
            }

            Current = _current.Data;
            return true;
        }

        public void Reset()
        {
            _current = null;
        }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            // nothing to clear up
        }
    }

    private ListElement<T> _first = null;
    
    /// <summary>
    /// List length.
    /// </summary>
    public int Length { get; private set; } = 0;

    /// <summary>
    /// Append data at the end of the list.
    /// </summary>
    /// <param name="data">Data</param>
    public void Append(T data)
    {
        ListElement<T> element = new ListElement<T>(data);
        ListElement<T> current = _first;
        if (current == null) {
            _first = element;
        }
        else
        {
            while (current.Successor != null)
                current = current.Successor;
            current.Successor = element;
        }
        Length++;
    }
    /// <summary>
    /// Append element at the end of the list.
    /// </summary>
    /// <param name="element">Element</param>
    public void Append(ListElement<T> element)
    {
        Append(element.Data);
    }
    
    /// <summary>
    /// Insert data at the start of the list.
    /// </summary>
    /// <param name="data">Data</param>
    public void Prepend(T data) {
        ListElement<T> element = new ListElement<T>(data);
        if (_first == null) {
            _first = element;
        }
        else {
            element.Successor = _first;
            _first = element;
        }
        Length++;
    }
    /// <summary>
    /// Insert element at the start of the list.
    /// </summary>
    /// <param name="element">Element</param>
    public void Prepend(ListElement<T> element)
    {
        Prepend(element.Data);
    }
    
    /// <summary>
    /// Insert data after another element.
    /// </summary>
    /// <param name="data">Data of the other element</param>
    /// <param name="element">Data to insert</param>
    public void InsertAfter(T data, T element) {
        ListElement<T> current = _first;
        while (current != null && !current.Data.Equals(data))
            current = current.Successor;
        if (current == null)
            throw new ArgumentException("Element with given data does not exist in list!");
        ListElement<T> insertion = new ListElement<T>(element);
        insertion.Successor = current.Successor;
        current.Successor = insertion;
        Length++;
    }
    /// <summary>
    /// Insert data after another element.
    /// </summary>
    /// <param name="data">Data of the other element</param>
    /// <param name="element">Element to insert</param>
    public void InsertAfter(T data, ListElement<T> element) {
        InsertAfter(data, element.Data);
    }
    
    /// <summary>
    /// Delete a element with given data.
    /// </summary>
    /// <param name="data">Data to search for</param>
    /// <returns>Data</returns>
    /// <exception cref="ArgumentException">Data does not exist</exception>
    public T Delete(T data) {
        ListElement<T> current = _first;
        ListElement<T> beforeCurrent = null;
        while (current != null && !current.Data.Equals(data))
        {
            beforeCurrent = current;
            current = current.Successor;
        }
        if (current == null)
            throw new ArgumentException("Element with given data does not exist in list!");

        if (beforeCurrent == null)
            _first = current.Successor;
        else
            beforeCurrent.Successor = current.Successor;

        Length--;
        return current.Data;
    }
    /// <summary>
    /// Delete a element with given data.
    /// </summary>
    /// <param name="element">Element to search for</param>
    /// <returns>Data</returns>
    /// <exception cref="ArgumentException">Data does not exist</exception>
    public T Delete(ListElement<T> element)
    {
        return Delete(element.Data);
    }
    
    /// <summary>
    /// Clear the list.
    /// </summary>
    public void ClearAll()
    {
        _first = null;
        Length = 0;
    }
    
    /// <summary>
    /// Check if data exists in list.
    /// </summary>
    /// <param name="data">Data</param>
    /// <returns>Amount of occurrences</returns>
    public int Contains(T data) {
        int amount = 0;
        ListElement<T> current = _first;
        while (current != null) {
            if (current.Data.Equals(data))
                amount++;
            current = current.Successor;
        }
        return amount;
    }
    /// <summary>
    /// Check if data exists in list.
    /// </summary>
    /// <param name="element">Element</param>
    /// <returns>Amount of occurrences</returns>
    public int Contains(ListElement<T> element)
    {
        return Contains(element.Data);
    }
    
    /// <summary>
    /// Search for a element with given data.
    /// </summary>
    /// <param name="data">Data</param>
    /// <returns>ListElement with data</returns>
    /// <exception cref="ArgumentException">Data does not exist</exception>
    public ListElement<T> Search(T data) {
        ListElement<T> current = _first;
        while (current != null && !current.Data.Equals(data))
            current = current.Successor;
        if (current == null)
            throw new ArgumentException("Element with given data does not exist in list!");
        return current;
    }
    
    /// <summary>
    /// String representation of the list.
    /// </summary>
    /// <returns>String representation</returns>
    public override String ToString() {
        ListElement<T> current = _first;
        StringBuilder result = new StringBuilder("List=[" + (current != null ? current.Data : ""));
        if (current != null)
            current = current.Successor;
        while (current != null) {
            result.Append("," + current.Data);
            current = current.Successor;
        }
        return result + "]";
    }

    /// <summary>
    /// Enumerator.
    /// </summary>
    /// <returns>List enumerator</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>
    /// Enumerator.
    /// </summary>
    /// <returns>List enumerator</returns>
    public IEnumerator<T> GetEnumerator()
    {
        return new ListEnumerator<T>(_first);
    }
}