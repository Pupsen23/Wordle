namespace Wordle.Core;

public class GuessResult
{
    public enum GuessStatus
    {
        GameIsFinished = 0,
        NoAttempts = 1,
        InvalidWordLength = 2,
        Incorrect = 3,
        Correct = 4
    }
    public bool Result { get; }
    public GuessStatus Status { get; }
    public string Word { get; }
    public GuessResult(bool result, GuessStatus status, string word) {}
}