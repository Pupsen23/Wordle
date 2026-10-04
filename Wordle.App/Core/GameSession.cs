using Wordle.Core.Words;

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
    private List<Word> _history = [];
    public SecretWord Word { get; }
    public int MaxAttempts { get; }
    /// <exception cref="InvalidOperationException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public int Attempts
    {
        get { return _attempts; }
        set
        {
            if (!CheckStatus())
                throw new InvalidOperationException("GameSession property Attempts: value cannot be set when game is finished (not InProgress).");
            else if (value > MaxAttempts)
                throw new ArgumentException($"GameSession property Attempts: value must be lower than or equal to MaxAttempts ({MaxAttempts}) (received: {value}).");
            
            _attempts = value;
        }
    }
    /// <exception cref="InvalidOperationException"></exception>
    public GameStatus Status
    {
        get { return _status; }
        set
        {
            if (!CheckStatus())
                throw new InvalidOperationException("GameSession's property Status: value cannot be set when game is finished (not InProgress).");
            
            _status = value;
        }
    }
    public ReadOnlyCollection<Word> History { get { return _history.AsReadOnly(); } }
    /// <exception cref="ArgumentException"></exception>
    public GameSession(SecretWord word, int maxAttempts)
    {
        if (maxAttempts <= 0)
            throw new ArgumentException($"GameSession's constructor: integer parameter 'MaxAttemptps' must be greater than 0 (received: {maxAttempts}).");
        
        Word = word;
        MaxAttempts = maxAttempts;
    }
    public bool CheckAttempts() { return Attempts < MaxAttempts; }
    public bool CheckStatus() { return Status.Equals(GameStatus.InProgress); } // может CheckFinished?
    public int GetRemainingAttempts() { return MaxAttempts - Attempts; }
    public void HistAdd(Word word) { _history.Add(word); }
}