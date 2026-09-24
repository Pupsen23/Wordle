namespace Wordle.Tests.Mandatory.Mr1;

/// <summary>Обязательные тесты: одинаковый seed обязан давать одинаковое загаданное слово.</summary>
public class SeedTest
{
    [Fact(
        DisplayName = "Одинаковый seed даёт одинаковое загаданное слово",
        Skip = "MR1: реализуй тест и удали эту строку")]
    public void SameSeedProducesSameAnswer()
    {
        Assert.Fail("Тест не реализован");
    }

    [Fact(
        DisplayName = "Разные seed'ы дают разные слова хотя бы иногда",
        Skip = "MR1: реализуй тест и удали эту строку")]
    public void DifferentSeedsProduceDifferentAnswers()
    {
        Assert.Fail("Тест не реализован");
    }
}