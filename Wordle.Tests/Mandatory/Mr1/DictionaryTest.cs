namespace Wordle.Tests.Mandatory.Mr1;

/// <summary>Обязательные тесты: словарь.</summary>
public class DictionaryTest
{
    [Fact(
        DisplayName = "Словарь содержит не меньше 50 слов",
        Skip = "MR1: реализуй тест и удали эту строку")]
    public void DictionaryContainsAtLeastFiftyWords()
    {
        Assert.Fail();
    }

    [Fact(
        DisplayName = "Все слова словаря состоят ровно из 5 букв",
        Skip = "MR1: реализуй тест и удали эту строку")]
    public void AllWordsAreExactlyFiveLettersLong()
    {
        Assert.Fail("Тест не реализован");
    }

    [Fact(
        DisplayName = "Пустой словарь приводит к ошибке, а не к запуску игры без слова",
        Skip = "MR1: реализуй тест и удали эту строку")]
    public void EmptyDictionaryIsRejected()
    {
        Assert.Fail("Тест не реализован");
    }
}