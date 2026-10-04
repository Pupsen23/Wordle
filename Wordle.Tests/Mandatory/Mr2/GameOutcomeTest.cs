using Wordle.Core;

namespace Wordle.Tests.Mandatory.Mr2;

/// <summary>Обязательные тесты: завершение партии.</summary>
public class GameOutcomeTest
{
    [Fact(DisplayName = "Угаданное слово переводит сессию в статус WIN")]
    public void CorrectGuessWinsTheGame()
    {
        string correctWord = "мышка";
        WordDictionary wordDictionary = new WordDictionary(Config.RawWords);
        GameSession gameSession = WordleEngine.StartGame(wordDictionary, correctWord)!;

        WordleEngine.ApplyGuess(gameSession, correctWord);

        Assert.Equal(GameSession.GameStatus.Win, gameSession.Status);
    }

    [Fact(DisplayName = "После 6 неудачных попыток сессия переходит в статус LOSE")]
    public void SixFailedAttemptsLoseTheGame()
    {
        string correctWord = "мышка";
        WordleEngine.MaxAttempts = 6;
        WordDictionary wordDictionary = new WordDictionary(Config.RawWords);
        GameSession gameSession = WordleEngine.StartGame(wordDictionary, correctWord)!;

        WordleEngine.ApplyGuess(gameSession, "нигга");
        WordleEngine.ApplyGuess(gameSession, "чмоня");
        WordleEngine.ApplyGuess(gameSession, "пидор");
        WordleEngine.ApplyGuess(gameSession, "говно");
        WordleEngine.ApplyGuess(gameSession, "фунгу");
        WordleEngine.ApplyGuess(gameSession, "сперм");

        Assert.Equal(GameSession.GameStatus.Lose, gameSession.Status);
    }

    [Fact(DisplayName = "При поражении показывается загаданное слово")]
    public void AnswerIsRevealedOnLoss()
    {
        string correctWord = "мышка";
        WordleEngine.MaxAttempts = 2;
        WordDictionary wordDictionary = new WordDictionary(Config.RawWords);
        GameSession gameSession = WordleEngine.StartGame(wordDictionary, correctWord)!;

        GuessResult? guessResult = WordleEngine.ApplyGuess(gameSession, "чмоня");
        guessResult = WordleEngine.ApplyGuess(gameSession, "чмоня");
        guessResult = WordleEngine.ApplyGuess(gameSession, "чмоня");

        Assert.Equal(correctWord, gameSession.CorrectWord);
    }

    [Fact(DisplayName = "Завершённая партия больше не принимает попытки")]
    public void FinishedGameRejectsFurtherGuesses()
    {
        string correctWord = "мышка";
        WordleEngine.MaxAttempts = 2;
        WordDictionary wordDictionary = new WordDictionary(Config.RawWords);
        GameSession gameSession = WordleEngine.StartGame(wordDictionary, correctWord)!;

        GuessResult? guessResult = WordleEngine.ApplyGuess(gameSession, "чмоня");
        guessResult = WordleEngine.ApplyGuess(gameSession, "чмоня");
        guessResult = WordleEngine.ApplyGuess(gameSession, "чмоня");

        Assert.Null(guessResult);
    }
}