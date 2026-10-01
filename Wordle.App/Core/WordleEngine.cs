namespace Wordle.Core;

public static class WordleEngine
{
    public static int MaxAttempts { get; set; } = 5;
    public static WordRandomizer WordRandomizer { get; } = new WordRandomizer();
    public static GameSession StartGame(WordDictionary wordDictionary)
    {
        return new GameSession(wordDictionary, WordRandomizer.GetRandomWord(wordDictionary), MaxAttempts);
    }
    public static GameSession StartGame(WordDictionary wordDictionary, string correctWord)
    {
        return new GameSession(wordDictionary, correctWord, MaxAttempts);
    }
    public static GuessResult? ApplyGuess(GameSession gameSession, string guessWord)
    {
        guessWord = guessWord.Trim().ToLowerInvariant();
        
        if (!gameSession.CheckStatus())
            return null;

        if (!WordNormalizer.CheckWordLength(guessWord, gameSession.CorrectWord.Length))
            return new GuessResult(false, guessWord, GuessResult.GuessErrorStatus.InvalidWordLength);

        if (!WordNormalizer.CheckWordSymbols(guessWord))
            return new GuessResult(false, guessWord, GuessResult.GuessErrorStatus.HasInvalidSymbols);

        GuessResult guessResult;
        gameSession.Attempts.Value++;

        if (gameSession.CorrectWord.Equals(guessWord))
        {
            guessResult = new GuessResult(true, guessWord);
            gameSession.Status = GameSession.GameStatus.Win;    
        }
        else if (!gameSession.Attempts.CheckValues())
        {
            guessResult = new GuessResult(false, guessWord);
            gameSession.Status = GameSession.GameStatus.Lose;    
        }
        else
            guessResult = new GuessResult(false, guessWord);

        gameSession.History.Add(guessResult);

        return guessResult;
    }
}