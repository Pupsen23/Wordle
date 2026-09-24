namespace Wordle.Tests.Mandatory.Mr2;

/// <summary>Обязательные тесты: раскраска букв.</summary>
public class LetterMatchingTest
{
    [Fact(
        DisplayName = "Базовый случай: загадано \"озеро\", ввод \"арбуз\" -> ❌🟡❌❌🟡",
        Skip = "MR2: реализуй тест и удали эту строку")]
    public void BasicCase()
    {
        Assert.Fail("Тест не реализован");
    }

    [Fact(
        DisplayName = "Полное совпадение: загадано \"озеро\", ввод \"озеро\" -> ✅✅✅✅✅",
        Skip = "MR2: реализуй тест и удали эту строку")]
    public void ExactMatch()
    {
        Assert.Fail("Тест не реализован");
    }

    [Fact(
        DisplayName = "Повторяющиеся буквы: загадано \"сорок\", ввод \"оооом\" -> 🟡❌❌✅❌",
        Skip = "MR2: реализуй тест и удали эту строку")]
    public void RepeatedLettersAreNotDoubleCounted()
    {
        Assert.Fail("Тест не реализован");
    }

    [Fact(
        DisplayName = "Ни одна буква не подошла: все позиции ❌",
        Skip = "MR2: реализуй тест и удали эту строку")]
    public void NoMatchingLetters()
    {
        Assert.Fail("Тест не реализован");
    }
}