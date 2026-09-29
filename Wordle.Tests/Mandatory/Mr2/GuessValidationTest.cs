using Wordle.Core;

namespace Wordle.Tests.Mandatory.Mr2;

/// <summary>Обязательные тесты: валидация ввода.</summary>
public class GuessValidationTest
{
    [Theory(DisplayName = "Слово не из 5 букв отклоняется")]
    [InlineData("дом")]
    [InlineData("домики")]
    [InlineData("")]
    public void WordOfWrongLengthIsRejected(string guess)
    {
        WordDictionary wordDictionary = new WordDictionary(DefaultConfig.Words);
        GameSession gameSession = WordleEngine.StartGame(wordDictionary);

        GuessResult guessResult = WordleEngine.ApplyGuess(gameSession, guess)!;

        Assert.Equal(GuessResult.GuessErrorStatus.InvalidWordLength, guessResult.ErrorStatus);
    }

    [Theory(DisplayName = "Ввод с не-буквами отклоняется")]
    [InlineData("дом12")]
    [InlineData("дом!!")]
    [InlineData("до ма")]
    public void NonLetterInputIsRejected(string guess)
    {
        WordDictionary wordDictionary = new WordDictionary(DefaultConfig.Words);
        GameSession gameSession = WordleEngine.StartGame(wordDictionary);

        GuessResult guessResult = WordleEngine.ApplyGuess(gameSession, guess)!;

        Assert.Equal(GuessResult.GuessErrorStatus.HasInvalidSymbols, guessResult.ErrorStatus);
    }

    [Fact(DisplayName = "Слово, которого нет в словаре, отклоняется")]
    public void WordOutsideDictionaryIsRejected()
    {
        string guess = "чмоня";
        WordDictionary wordDictionary = new WordDictionary(DefaultConfig.Words);
        GameSession gameSession = WordleEngine.StartGame(wordDictionary);

        GuessResult guessResult = WordleEngine.ApplyGuess(gameSession, guess)!;

        Assert.Null(guessResult.ErrorStatus);
        Assert.False(guessResult.Result);
    }

    [Fact(DisplayName = "Некорректный ввод не тратит попытку")]
    public void InvalidInputDoesNotConsumeAttempt()
    {
        string guess = "дом12";
        WordDictionary wordDictionary = new WordDictionary(DefaultConfig.Words);
        GameSession gameSession = WordleEngine.StartGame(wordDictionary);
        int attempts = gameSession.Attempts.Value;

        GuessResult guessResult = WordleEngine.ApplyGuess(gameSession, guess)!;

        Assert.Equal(attempts, gameSession.Attempts.Value);
    }

    [Fact(DisplayName = "Ввод не зависит от регистра: \"ОЗЕРО\" и \"озеро\" обрабатываются одинаково")]
    public void InputIsCaseInsensitive()
    {
        string guess1 = "ОЗЕРО";
        string guess2 = "озеро";
        WordDictionary wordDictionary = new WordDictionary(DefaultConfig.Words);
        GameSession gameSession = WordleEngine.StartGame(wordDictionary);

        GuessResult guessResult1 = WordleEngine.ApplyGuess(gameSession, guess1)!;
        GuessResult guessResult2 = WordleEngine.ApplyGuess(gameSession, guess2)!;

        Assert.Equal(guessResult1.Word, guessResult2.Word);
    }
}