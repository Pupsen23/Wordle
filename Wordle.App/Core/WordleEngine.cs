using Wordle.Core.Words;

namespace Wordle.Core;

public static class WordleEngine
{
    public static WordRandomizer WordRandomizer { get; } = new WordRandomizer();
    public static GameSession StartGame(WordDictionary wordDictionary, int maxAttempts = 5)
    {
        return new GameSession(new SecretWord(WordRandomizer.GetRandomWord(wordDictionary)), maxAttempts);
    }
    public static GameSession StartGame(AbstractWord word, int maxAttempts = 5)
    {
        return new GameSession(new SecretWord(word), maxAttempts);
    }
    public static GuessResult? ApplyGuess(GameSession gameSession, Word guessWord)
    {   
        if (!gameSession.CheckStatus() || !guessWord.CompareLength(gameSession.Word))
            return null;

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