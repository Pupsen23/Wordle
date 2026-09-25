namespace Core;

public class Attempt
{
    private int _value;
    private int _maxValue;
    public int Value
    {
        get { return _value; }
        set
        {
            if (value > MaxValue || value < 0)
                throw new ArgumentException($"Value proprty of Attempt must be <= MaxValue and >= 0, received: {value}");
            
            _value = value;
        }
    }
    public int MaxValue
    {
        get { return _maxValue; }
        set
        {
            if (value < 1)
                throw new ArgumentException($"MaxValue proprty of Attempt must be >= 1, received: {value}");
            
            _maxValue = value;
        }
    }
    public Attempt(int maxValue)
    {
        MaxValue = maxValue;
        Value = 0;
    }
    public bool CheckValues() { return Value < MaxValue; }
}