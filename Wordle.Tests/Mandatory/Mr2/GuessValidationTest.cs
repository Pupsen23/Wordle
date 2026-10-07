using Wordle.Core;
using Wordle.Core.Words;

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
        WordDictionary wordDictionary = new WordDictionary(new Config().RawWords);
        GameSession gameSession = WordleEngine.StartGame(wordDictionary, 5);

        if (string.IsNullOrEmpty(guess))
            Assert.Throws<ArgumentException>(() => new Word(guess));
        else
            Assert.Equal(GuessResult.GuessErrorStatus.InvalidLength, WordleEngine.ApplyGuess(gameSession, new Word(guess))!.ErrorStatus);
    }

    [Theory(DisplayName = "Ввод с не-буквами отклоняется")]
    [InlineData("дом12")]
    [InlineData("дом!!")]
    [InlineData("до ма")]
    public void NonLetterInputIsRejected(string guess)
    {
        Assert.Throws<ArgumentException>(() => new Word(guess));
    }

    [Fact(DisplayName = "Слово, которого нет в словаре, отклоняется")]
    public void WordOutsideDictionaryIsRejected()
    {
        Word guessWord = new Word("чмоня");
        WordDictionary wordDictionary = new WordDictionary(new Config().RawWords);
        GameSession gameSession = WordleEngine.StartGame(wordDictionary, 5);

        GuessResult guessResult = WordleEngine.ApplyGuess(gameSession, guessWord)!;

        Assert.Equal(guessResult.ErrorStatus, GuessResult.GuessErrorStatus.NotInWordDictionary);
    }

    [Fact(DisplayName = "Некорректный ввод не тратит попытку")]
    public void InvalidInputDoesNotConsumeAttempt()
    {
        Assert.Throws<ArgumentException>(() => new Word("дом12"));
    }

    [Fact(DisplayName = "Ввод не зависит от регистра: \"ОЗЕРО\" и \"озеро\" обрабатываются одинаково")]
    public void InputIsCaseInsensitive()
    {
        Word guessWord1 = new Word("ОЗЕРО");
        Word guessWord2 = new Word("озеро");
        WordDictionary wordDictionary = new WordDictionary(new Config().RawWords);
        GameSession gameSession = WordleEngine.StartGame(wordDictionary, 5);

        GuessResult guessResult1 = WordleEngine.ApplyGuess(gameSession, guessWord1)!;
        GuessResult guessResult2 = WordleEngine.ApplyGuess(gameSession, guessWord2)!;

        Assert.Equal(guessWord1.Value, guessWord2.Value);
        Assert.Equal(guessResult1.Word.Value, guessResult2.Word.Value);
    }
}