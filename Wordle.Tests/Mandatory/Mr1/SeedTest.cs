using Wordle.Core;

namespace Wordle.Tests.Mandatory.Mr1;

/// <summary>Обязательные тесты: одинаковый seed обязан давать одинаковое загаданное слово.</summary>
public class SeedTest
{
    [Fact(DisplayName = "Одинаковый seed даёт одинаковое загаданное слово")]
    public void SameSeedProducesSameAnswer()
    {
        int seed = 123;
        WordDictionary wordDictionary = new WordDictionary(DefaultConfig.Words);
        WordRandomizer wordRandomizer1 = new WordRandomizer(seed);
        WordRandomizer wordRandomizer2 = new WordRandomizer(seed);

        string randomWord1 = wordRandomizer1.GetRandomWord(wordDictionary);
        string randomWord2 = wordRandomizer2.GetRandomWord(wordDictionary);

        Assert.Equal(randomWord1, randomWord2);
    }

    [Fact(DisplayName = "Разные seed'ы дают разные слова хотя бы иногда")]
    public void DifferentSeedsProduceDifferentAnswers()
    {
        int seed1 = 123;
        int seed2 = 456;
        WordDictionary wordDictionary = new WordDictionary(DefaultConfig.Words);
        WordRandomizer wordRandomizer1 = new WordRandomizer(seed1);
        WordRandomizer wordRandomizer2 = new WordRandomizer(seed2);

        string randomWord1 = wordRandomizer1.GetRandomWord(wordDictionary);
        string randomWord2 = wordRandomizer2.GetRandomWord(wordDictionary);

        Assert.NotEqual(randomWord1, randomWord2);
    }
}