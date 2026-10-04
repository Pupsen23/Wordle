namespace Wordle.Core.Words;

public class Word : AbstractWord
{
    public string Value { get { return _value; } }
    public Word(string? value) : base(value) {}
    public Word(AbstractWord abstractWord) : base(abstractWord) {}
}