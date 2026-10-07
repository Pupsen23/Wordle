using Wordle.Core.Words;

namespace Wordle.Core;

public static class WordleEngine
{
    public static WordRandomizer WordRandomizer { get; } = new WordRandomizer();
    public static GameSession StartGame(WordDictionary wordDictionary, int maxAttempts)
    {
        return new GameSession(new SecretWord(WordRandomizer.GetRandomWord(wordDictionary)), maxAttempts, wordDictionary);
    }
    public static GameSession StartGame(WordDictionary wordDictionary, AbstractWord word, int maxAttempts)
    {
        return new GameSession(new SecretWord(word), maxAttempts, wordDictionary);
    }
    public static GuessResult? ApplyGuess(GameSession gameSession, Word guessWord)
    {   
        if (!gameSession.CheckStatus())
            return null;

        if (!guessWord.CompareLength(gameSession.Word))
            return new GuessResult(guessWord, GuessResult.GuessErrorStatus.InvalidLength);
        else if (!gameSession.WordDictionary.Contains(guessWord))
            return new GuessResult(guessWord, GuessResult.GuessErrorStatus.NotInWordDictionary);

        GuessResult guessResult;
        gameSession.Attempts++;
        gameSession.HistAdd(guessWord);

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