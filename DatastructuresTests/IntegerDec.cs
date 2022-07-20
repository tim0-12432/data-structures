namespace DatastructuresTests;

public class IntegerDec : IDecreasable, IComparable, IConvertible
{
    private int _value;

    public IntegerDec()
    {
        _value = 0;
    }

    public IntegerDec(int num)
    {
        _value = num;
    }

    public static implicit operator int(IntegerDec num)
    {
        return num._value;
    }

    public static implicit operator IntegerDec(int num)
    {
        return new IntegerDec(num);
    }
    
    public static bool operator ==(IntegerDec a, int b)
    {
        return a._value == b;
    }

    public static bool operator !=(IntegerDec a, int b)
    {
        return a._value != b;
    }
    
    public static bool operator ==(IntegerDec a, IntegerDec b)
    {
        return a._value == b._value;
    }

    public static bool operator !=(IntegerDec a, IntegerDec b)
    {
        return a._value != b._value;
    }

    public IDecreasable Decrease()
    {
        _value--;
        return this;
    }

    public int CompareTo(object? obj)
    {
        int num = Convert.ToInt32(obj);
        if (num == _value) return 0;
        return num > _value ? -1 : 1;
    }

    public override string ToString()
    {
        return "" + _value;
    }

    public override bool Equals(object? obj)
    {
        int num = Convert.ToInt32(obj);
        return num == _value;
    }

    public TypeCode GetTypeCode()
    {
        throw new NotImplementedException();
    }

    public bool ToBoolean(IFormatProvider? provider)
    {
        return Convert.ToBoolean(_value);
    }

    public byte ToByte(IFormatProvider? provider)
    {
        return Convert.ToByte(_value);
    }

    public char ToChar(IFormatProvider? provider)
    {
        return Convert.ToChar(_value);
    }

    public DateTime ToDateTime(IFormatProvider? provider)
    {
        return Convert.ToDateTime(_value);
    }

    public decimal ToDecimal(IFormatProvider? provider)
    {
        return Convert.ToDecimal(_value);
    }

    public double ToDouble(IFormatProvider? provider)
    {
        return Convert.ToDouble(_value);
    }

    public short ToInt16(IFormatProvider? provider)
    {
        return Convert.ToInt16(_value);
    }

    public int ToInt32(IFormatProvider? provider)
    {
        return _value;
    }

    public long ToInt64(IFormatProvider? provider)
    {
        return Convert.ToInt64(_value);
    }

    public sbyte ToSByte(IFormatProvider? provider)
    {
        return Convert.ToSByte(_value);
    }

    public float ToSingle(IFormatProvider? provider)
    {
        return Convert.ToSingle(_value);
    }

    public string ToString(IFormatProvider? provider)
    {
        return this.ToString();
    }

    public object ToType(Type conversionType, IFormatProvider? provider)
    {
        throw new NotImplementedException();
    }

    public ushort ToUInt16(IFormatProvider? provider)
    {
        return Convert.ToUInt16(_value);
    }

    public uint ToUInt32(IFormatProvider? provider)
    {
        return Convert.ToUInt32(_value);
    }

    public ulong ToUInt64(IFormatProvider? provider)
    {
        return Convert.ToUInt64(_value);
    }
}