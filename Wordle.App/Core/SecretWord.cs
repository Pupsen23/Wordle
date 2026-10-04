namespace Wordle.Core;

public class SecretWord
{
    private readonly string _value;
    public int Length { get; }
    public SecretWord(string value)
    {
        if (!WordNormalizer.CheckLength(value))
            throw new ArgumentException($"SecretWord value cannot be null or empty (received: '{value}').");
        else if (!WordNormalizer.CheckStructure(value))
            throw new ArgumentException($"SecretWord value cannot be one repeated letter like 'aaaaa' (received: '{value}').");
        else if (!WordNormalizer.CheckForbiddenSymbols(value))
            throw new ArgumentException($"SecretWord value cannot contain symbols but letters (received: '{value}').");

        value = WordNormalizer.Normalize(value);
        _value = value;
        Length = value.Length;
    }
    public bool CompareTo(string word) { return _value.Equals(word); }
    public bool CompareAt(char wordChar, int index) 
    {
        if (index >= _value.Length || index < 0)
            throw new IndexOutOfRangeException("CompareAt index parameter is out of range of SecretWord's string value.");

        return _value[index].Equals(wordChar);
    }
    public bool Contains(char wordChar) { return _value.Contains(wordChar); }
    public int Count(char wordChar) { return _value.Count(wordChar); }
}