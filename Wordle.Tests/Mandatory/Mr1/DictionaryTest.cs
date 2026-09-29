using Wordle.Core;

namespace Wordle.Tests.Mandatory.Mr1;

/// <summary>Обязательные тесты: словарь.</summary>
public class DictionaryTest
{
    [Fact(DisplayName = "Словарь содержит не меньше 50 слов")]
    public void DictionaryContainsAtLeastFiftyWords()
    {
        const int minWords = 50;

        WordDictionary wordDictionary = new WordDictionary(Config.Words);

        Assert.InRange(wordDictionary.Length, minWords, int.MaxValue);
    }

    [Fact(DisplayName = "Все слова словаря состоят ровно из 5 букв")]
    public void AllWordsAreExactlyFiveLettersLong()
    {
        WordDictionary wordDictionary = new WordDictionary(Config.Words);

        Assert.All(wordDictionary.Words, word => Assert.Matches("^[а-я]{5}$", word));
    }

    [Fact(DisplayName = "Пустой словарь приводит к ошибке, а не к запуску игры без слова")]
    public void EmptyDictionaryIsRejected()
    {
        List<string> words = new List<string>();

        Assert.Throws<ArgumentException>(() => new WordDictionary(words));
    }
}