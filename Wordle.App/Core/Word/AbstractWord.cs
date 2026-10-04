namespace Wordle.Core.Words;

public abstract class AbstractWord
{
    protected readonly string _value;
    public int Length { get; }
    /// <exception cref="ArgumentException"></exception>
    protected AbstractWord(string? value)
    {
        StringNormalizer.StringErrorStatus? stringErrorStatus = StringNormalizer.Check(value);

        if (stringErrorStatus.Equals(StringNormalizer.StringErrorStatus.IsNullOrEmpty)) // далее value не null, поэтому все прощаю
            throw new ArgumentException($"AbstractWord's constructor: string parameter 'value' cannot be null or empty (received: '{value}').");
        else if (stringErrorStatus.Equals(StringNormalizer.StringErrorStatus.InvalidLength))
            throw new ArgumentException($"AbstractWord's constructor: string parameter 'value' length cannot be lower than or equal to 1 (received: '{value}', length: {value!.Length}).");
        else if (stringErrorStatus.Equals(StringNormalizer.StringErrorStatus.InvalidStructure))
            throw new ArgumentException($"AbstractWord's constructor: string parameter 'value' cannot be one repeated symbol like 'aaaaa' (received: '{value}').");
        else if (stringErrorStatus.Equals(StringNormalizer.StringErrorStatus.HasForbiddenSymbols))
            throw new ArgumentException($"AbstractWord's constructor: string parameter 'value' cannot contain symbols but letters (received: '{value}').");

        value = StringNormalizer.Normalize(value!);
        _value = value;
        Length = value.Length;
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
            throw new IndexOutOfRangeException("AbstractWord's method CompareAt: integer parameter 'index' is out of range of inner string.");

        return _value[index].Equals(letter);
    }
    public bool CompareLength(AbstractWord word) { return Length == word.Length; }
    public bool Contains(char letter) { return _value.Contains(letter); }
    public int Count(char letter) { return _value.Count(letter); }
}