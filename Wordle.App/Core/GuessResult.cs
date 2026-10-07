using Wordle.Core.Words;

namespace Wordle.Core;

public class GuessResult
{
    public enum GuessErrorStatus
    {
        InvalidLength,
        NotInWordDictionary
    }
    public Word Word { get; }
    public bool? Result { get; }
    public GuessErrorStatus? ErrorStatus { get; }
    public GuessResult(Word word, bool result)
    {
        Word = word;
        Result = result;
    }
    public GuessResult(Word word, GuessErrorStatus errorStatus)
    {
        Word = word;
        ErrorStatus = errorStatus;
    }
}