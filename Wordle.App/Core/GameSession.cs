namespace Core;

public class GameSession
{
    public enum GameStatus
    {
        IN_PROGRESS = 0,
        WIN = 0,
        LOSE = 0
    }
    public enum GuessStatus
    {
        NO_ATTEMPTS = 0,
        INCORRECT = 1,
        CORRECT = 2
    }
    public string CorrectWord { get; }
    public Attempt Attempts { get; }
    public GameStatus Status { get; set; }
    public List<string> History { get; }
    public GameSession(string correctWord, int maxAttempts)
    {
        CorrectWord = correctWord;
        Attempts = new Attempt(maxAttempts);
        Status = GameStatus.IN_PROGRESS;
        History = [];
    }
    public bool CheckStatus() { return Status.Equals(GameStatus.IN_PROGRESS); }
}