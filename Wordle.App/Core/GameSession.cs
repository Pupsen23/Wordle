namespace Wordle.Core;

public class GameSession
{
    public enum GameStatus
    {
        InProgress = 0,
        Win = 1,
        Lose = 2
    }
    public GameStatus Status { get; set; } = GameStatus.InProgress;
    public List<GuessResult> History { get; } = [];
    public WordDictionary WordDictionary { get; }
    public string CorrectWord { get; }
    public GameAttempt Attempts { get; }
    public GameSession(WordDictionary wordDictionary, string correctWord, int maxAttempts)
    {
        WordDictionary= wordDictionary;
        CorrectWord = correctWord;
        Attempts = new GameAttempt(maxAttempts);
    }
    public bool CheckStatus() { return Status.Equals(GameStatus.InProgress); }
}