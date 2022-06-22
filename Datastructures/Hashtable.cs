using System.Text;

namespace Datastructures;

public class Hashtable<T> where T : new()
{
    private static readonly int MAX_TABLE_SIZE = 50;
    private LinkedList<T>[] table = new LinkedList<T>[MAX_TABLE_SIZE];

    public Hashtable()
    {
        for (int i = 0; i < MAX_TABLE_SIZE; i++)
        {
            table[i] = new LinkedList<T>();
        }
    }

    public void Insert(T data)
    {
        int index = GetHash(data);
        table[index].AddFirst(data);
    }

    public T Delete(T data)
    {
        int index = GetHash(data);
        LinkedListNode<T> element = table[index].Find(data);
        table[index].Remove(data);
        return element == null ? new T() : element.Value;
    }

    public T Search(T data)
    {
        int index = GetHash(data);
        LinkedListNode<T> element = table[index].Find(data);
        return element == null ? new T() : element.Value;
    }

    public override string ToString()
    {
        StringBuilder builder = new StringBuilder("Hashtable=[");
        foreach (LinkedList<T> cell in table)
        {
            builder.Append("[");
            foreach (T elem in cell)
            {
                builder.Append($"{elem},");
            }
            if (!'['.Equals(builder[^1]))
                builder.Remove(builder.Length - 1, 1);
            builder.Append("],");
        }
        builder.Remove(builder.Length - 1, 1);
        return builder.Append("]").ToString();
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

        return Convert.ToInt32(Math.Floor(8 * (number * (Math.Sqrt(5) - 1) / 2) % 701)) % MAX_TABLE_SIZE;
    }
}