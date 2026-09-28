namespace Wordle.Core;

public class WordleEngine
{
    public int MaxAttempts { get; set; } = 5;
    public WordRandomizer WordRandomizer { get; } = new WordRandomizer();
    public WordleEngine() {}
    public GameSession StartGame(WordDictionary wordDictionary)
    {
        return new GameSession(wordDictionary, WordRandomizer.GetRandomWord(wordDictionary), MaxAttempts);
    }
    public GameSession? StartGame(WordDictionary wordDictionary, string correctWord)
    {
        if (WordNormalizer.CheckCorrectWord(correctWord, wordDictionary))
            return new GameSession(wordDictionary, correctWord, MaxAttempts);
        
        return null;
    }
    public static GuessResult ApplyGuess(GameSession gameSession, string guessWord)
    {
        if (!gameSession.CheckStatus())
            return new GuessResult(false, GuessResult.GuessStatus.GameIsFinished, guessWord);

        if (!WordNormalizer.CheckGuessWord(guessWord, gameSession.CorrectWord))
            return new GuessResult(false, GuessResult.GuessStatus.InvalidWordLength, guessWord);

        GuessResult guessResult;
        gameSession.Attempts.Value++;

        if (gameSession.CorrectWord.Equals(guessWord))
        {
            guessResult = new GuessResult(true, GuessResult.GuessStatus.Correct, guessWord);
            gameSession.Status = GameSession.GameStatus.Win;    
        }
        else if (!gameSession.Attempts.CheckValues())
        {
            guessResult = new GuessResult(false, GuessResult.GuessStatus.NoAttempts, guessWord);
            gameSession.Status = GameSession.GameStatus.Lose;    
        }
        else
            guessResult = new GuessResult(false, GuessResult.GuessStatus.Incorrect, guessWord);

        gameSession.History.Add(guessResult);

        return guessResult;
    }
}