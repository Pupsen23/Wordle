namespace Wordle.Core;

public class GuessResult
{
    public enum GuessErrorStatus
    {
        InvalidWordLength = 0,
        HasInvalidSymbols = 1
    }
    public bool Result { get; }
    public GuessErrorStatus? ErrorStatus { get; }
    public string Word { get; }
    public GuessResult(bool result, string word)
    {
        Result = result;
        Word = word;
    }
    public GuessResult(bool result, string word, GuessErrorStatus? errorStatus)
    {
        Result = result;
        Word = word;
        ErrorStatus = errorStatus;
    }
}