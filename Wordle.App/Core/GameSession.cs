namespace Wordle.Core;

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
    public GameStatus Status { get; set; } = GameStatus.IN_PROGRESS;
    public List<string> History { get; } = [];
    public GameSession(string correctWord, int maxAttempts)
    {
        CorrectWord = correctWord;
        Attempts = new Attempt(maxAttempts);
    }
    public static GuessStatus ApplyGuess(GameSession gameSession, string guessWord)
    {
        if (!WordNormalizer.CheckGuessWord(guessWord, gameSession.CorrectWord))
            throw new ArgumentException($"GuessWord parameter length must be equal to GameSession.CorrectWord.Length (current: {gameSession.CorrectWord.Length}), " +
                $"received: {guessWord} ({guessWord.Length}).");

        if (!gameSession.CheckStatus())
            throw new Exception("Cannot ApplyGuess() on GameSession object with status not IN_PROGRESS.");

        if (!gameSession.Attempts.CheckValues())
        {
            gameSession.Status = GameStatus.LOSE;
            return GuessStatus.NO_ATTEMPTS;
        }

        if (gameSession.CorrectWord.Equals(guessWord))
        {
            gameSession.Status = GameStatus.WIN;
            return GuessStatus.CORRECT;
        }

        gameSession.Attempts.Value++;
        gameSession.History.Add(guessWord);

        return GuessStatus.INCORRECT;
    }
    public bool CheckStatus() { return Status.Equals(GameStatus.IN_PROGRESS); }
}