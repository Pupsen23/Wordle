namespace Wordle.Core.Words;

public class SecretWord : AbstractWord
{
    public SecretWord(string? value) : base(value) {}
    public SecretWord(AbstractWord abstractWord) : base(abstractWord) {}
    public Word GetRevealed() { return new Word(_value); }
}