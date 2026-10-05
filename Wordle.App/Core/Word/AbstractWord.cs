namespace Wordle.Core.Words;

public abstract class AbstractWord
{
    protected readonly string _value;
    public int Length { get; }
    /// <exception cref="ArgumentException"></exception>
    protected AbstractWord(string? value)
    {
        string normalizedValue;
        bool normalizeResult = StringNormalizer.TryNormalize(value, out normalizedValue);

        if (!normalizeResult)
            throw new ArgumentException($"String parameter 'value' cannot be null or empty (received: '{value}').");

        StringNormalizer.StringErrorStatus? stringErrorStatus = StringNormalizer.Check(normalizedValue);
            
        if (stringErrorStatus.Equals(StringNormalizer.StringErrorStatus.InvalidLength))
            throw new ArgumentException($"String parameter 'value' length cannot be lower than or equal to 1 after normalization (received (normalized): '{normalizedValue}', length: {normalizedValue.Length}).");
        else if (stringErrorStatus.Equals(StringNormalizer.StringErrorStatus.InvalidStructure))
            throw new ArgumentException($"String parameter 'value' cannot be one repeated symbol like 'aaaaa' after normalization (received (normalized): '{normalizedValue}').");
        else if (stringErrorStatus.Equals(StringNormalizer.StringErrorStatus.HasForbiddenSymbols))
            throw new ArgumentException($"String parameter 'value' cannot contain symbols but letters (received (normalized): '{normalizedValue}').");

        _value = normalizedValue;
        Length = normalizedValue.Length;
    }
    protected AbstractWord(AbstractWord abstractWord)
    {
        _value = abstractWord._value;
        Length = abstractWord.Length;
    }
    public bool CompareTo(AbstractWord word) { return _value.Equals(word._value); }
    /// <exception cref="IndexOutOfRangeException"></exception>
    public bool CompareAt(char letter, int index) 
    {
        if (index >= _value.Length || index < 0)
            throw new IndexOutOfRangeException("Integer parameter 'index' is out of range of inner string.");

        return _value[index].Equals(letter);
    }
    public bool CompareLength(AbstractWord word) { return Length == word.Length; }
    public bool Contains(char letter) { return _value.Contains(letter); }
    public int Count(char letter) { return _value.Count(letter); }
}