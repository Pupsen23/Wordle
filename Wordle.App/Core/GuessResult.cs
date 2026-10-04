namespace Wordle.Core;

public class GuessResult
{
    public string Word { get; }
    public bool? Result { get; }
    public WordNormalizer.WordErrorStatus? WordErrorStatus { get; }
    public GuessResult(string word, bool result)
    {
        Word = word;
        Result = result;
    }
    public GuessResult(string word, WordNormalizer.WordErrorStatus wordErrorStatus)
    {
        Word = word;
        WordErrorStatus = wordErrorStatus;
    }
}