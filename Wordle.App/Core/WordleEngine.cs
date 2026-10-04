namespace Wordle.Core;

public static class WordleEngine
{
    public static WordRandomizer WordRandomizer { get; } = new WordRandomizer();
    public static GameSession StartGame(WordDictionary wordDictionary, int maxAttempts = 5)
    {
        return new GameSession(WordRandomizer.GetRandomWord(wordDictionary), maxAttempts);
    }
    public static GameSession StartGame(string word, int maxAttempts = 5)
    {
        return new GameSession(word, maxAttempts);
    }
    public static GuessResult? ApplyGuess(GameSession gameSession, string guessWord)
    {
        guessWord = WordNormalizer.Normalize(guessWord);
        
        if (!gameSession.CheckStatus())
            return null;

        else if (!WordNormalizer.CheckLength(guessWord, gameSession.Word.Length))
            return new GuessResult(guessWord, WordNormalizer.WordErrorStatus.InvalidLength);
        else if (!WordNormalizer.CheckStructure(guessWord))
            return new GuessResult(guessWord, WordNormalizer.WordErrorStatus.InvalidStructure);
        else if (!WordNormalizer.CheckForbiddenSymbols(guessWord))
            return new GuessResult(guessWord, WordNormalizer.WordErrorStatus.HasForbiddenSymbols);

        GuessResult guessResult;
        gameSession.Attempts++;

        if (gameSession.Word.CompareTo(guessWord))
        {
            guessResult = new GuessResult(guessWord, true);
            gameSession.Status = GameSession.GameStatus.Win;    
        }
        else if (!gameSession.CheckAttempts())
        {
            guessResult = new GuessResult(guessWord, false);
            gameSession.Status = GameSession.GameStatus.Lose;    
        }
        else
            guessResult = new GuessResult(guessWord, false);

        return guessResult;
    }
}