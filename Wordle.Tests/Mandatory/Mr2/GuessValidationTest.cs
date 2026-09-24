namespace Wordle.Tests.Mandatory.Mr2;

/// <summary>Обязательные тесты: валидация ввода.</summary>
public class GuessValidationTest
{
    [Theory(
        DisplayName = "Слово не из 5 букв отклоняется",
        Skip = "MR2: реализуй тест и удали эту строку")]
    [InlineData("дом")]
    [InlineData("домики")]
    [InlineData("")]
    public void WordOfWrongLengthIsRejected(string guess)
    {
        Assert.Fail("Тест не реализован");
    }

    [Theory(
        DisplayName = "Ввод с не-буквами отклоняется",
        Skip = "MR2: реализуй тест и удали эту строку")]
    [InlineData("дом12")]
    [InlineData("дом!!")]
    [InlineData("до ма")]
    public void NonLetterInputIsRejected(string guess)
    {
        Assert.Fail("Тест не реализован");
    }

    [Fact(
        DisplayName = "Слово, которого нет в словаре, отклоняется",
        Skip = "MR2: реализуй тест и удали эту строку")]
    public void WordOutsideDictionaryIsRejected()
    {
        Assert.Fail("Тест не реализован");
    }

    [Fact(
        DisplayName = "Некорректный ввод не тратит попытку",
        Skip = "MR2: реализуй тест и удали эту строку")]
    public void InvalidInputDoesNotConsumeAttempt()
    {
        Assert.Fail("Тест не реализован");
    }

    [Fact(
        DisplayName = "Ввод не зависит от регистра: \"ОЗЕРО\" и \"озеро\" обрабатываются одинаково",
        Skip = "MR2: реализуй тест и удали эту строку")]
    public void InputIsCaseInsensitive()
    {
        Assert.Fail("Тест не реализован");
    }
}