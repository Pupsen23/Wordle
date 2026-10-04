namespace Wordle.Core;

public class GameSession
{
    public enum GameStatus
    {
        InProgress,
        Win,
        Lose
    }
    private int _attempts = 0;
    private GameStatus _status = GameStatus.InProgress;
    private List<string> _history = [];
    public SecretWord Word { get; }
    public int MaxAttempts { get; }
    public int Attempts
    {
        get { return _attempts; }
        set
        {
            if (!CheckStatus())
                throw new ArgumentException("GameSession Attempts value cannot be set when game is finished (not InProgress).");
            else if (value > MaxAttempts)
                throw new ArgumentException($"GameSession Attempts value (received: {value}) must be <= MaxAttempts value (current: {MaxAttempts}).");
            
            _attempts = value;
        }
    }
    public GameStatus Status
    {
        get { return _status; }
        set
        {
            if (!CheckStatus())
                throw new ArgumentException("GameSession Status value cannot be set when game is finished (not InProgress).");
            
            _status = value;
        }
    }
    public ReadOnlyCollection<string> History { get { return _history.AsReadOnly(); } }
    public GameSession(string word, int maxAttempts)
    {
        if (maxAttempts <= 0)
            throw new ArgumentException("GameSession MaxAttemptps value must be > 0");
        
        Word = new SecretWord(word);
        MaxAttempts = maxAttempts;
    }
    public bool CheckAttempts() { return Attempts < MaxAttempts; }
    public bool CheckStatus() { return Status.Equals(GameStatus.InProgress); } // может CheckFinished?
    public int GetRemainingAttempts() { return MaxAttempts - Attempts; }
}