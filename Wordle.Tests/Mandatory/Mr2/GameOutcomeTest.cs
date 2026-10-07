using Wordle.Core;
using Wordle.Core.Words;

namespace Wordle.Tests.Mandatory.Mr2;

/// <summary>Обязательные тесты: завершение партии.</summary>
public class GameOutcomeTest
{
    [Fact(DisplayName = "Угаданное слово переводит сессию в статус WIN")]
    public void CorrectGuessWinsTheGame()
    {
        Word correctWord = new Word("мышка");
        WordDictionary wordDictionary = new WordDictionary(new Config().RawWords);
        GameSession gameSession = WordleEngine.StartGame(wordDictionary, correctWord, 5)!;

        WordleEngine.ApplyGuess(gameSession, correctWord);

        Assert.Equal(GameSession.GameStatus.Win, gameSession.Status);
    }

    [Fact(DisplayName = "После 6 неудачных попыток сессия переходит в статус LOSE")]
    public void SixFailedAttemptsLoseTheGame()
    {
        Word correctWord = new Word("мышка");
        Word guessWord = new Word("озеро");
        int maxAttempts = 6;
        WordDictionary wordDictionary = new WordDictionary(new Config().RawWords);
        GameSession gameSession = WordleEngine.StartGame(wordDictionary, correctWord, maxAttempts)!;
        
        for (int i = 0; i <= maxAttempts; i++)
            WordleEngine.ApplyGuess(gameSession, guessWord);

        Assert.Equal(GameSession.GameStatus.Lose, gameSession.Status);
    }

    [Fact(DisplayName = "При поражении показывается загаданное слово")]
    public void AnswerIsRevealedOnLoss()
    {
        Word correctWord = new Word("мышка");
        Word guessWord = new Word("чмоня");
        int maxAttempts = 2;
        WordDictionary wordDictionary = new WordDictionary(new Config().RawWords);
        GameSession gameSession = WordleEngine.StartGame(wordDictionary, correctWord, maxAttempts)!;

        WordleEngine.ApplyGuess(gameSession, guessWord);
        WordleEngine.ApplyGuess(gameSession, guessWord);
        WordleEngine.ApplyGuess(gameSession, guessWord);

        Assert.Equal(correctWord.Value, gameSession.Word.GetRevealed().Value);
    }

    [Fact(DisplayName = "Завершённая партия больше не принимает попытки")]
    public void FinishedGameRejectsFurtherGuesses()
    {
        Word correctWord = new Word("мышка");
        Word guessWord = new Word("озеро");
        int maxAttempts = 2;
        WordDictionary wordDictionary = new WordDictionary(new Config().RawWords);
        GameSession gameSession = WordleEngine.StartGame(wordDictionary, correctWord, maxAttempts)!;

        GuessResult? guessResult = WordleEngine.ApplyGuess(gameSession, guessWord);
        guessResult = WordleEngine.ApplyGuess(gameSession, guessWord);
        guessResult = WordleEngine.ApplyGuess(gameSession, guessWord);

        Assert.Null(guessResult);
    }
}