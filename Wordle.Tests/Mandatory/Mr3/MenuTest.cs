namespace Wordle.Tests.Mandatory.Mr3;

/// <summary>Обязательные тесты: меню и режим автопроверки.</summary>
public class MenuTest
{
    [Fact(
        DisplayName = "Некорректный пункт меню не роняет программу",
        Skip = "MR3: реализуй тест и удали эту строку")]
    public void InvalidMenuChoiceDoesNotCrash()
    {
        Assert.Fail("Тест не реализован");
    }

    [Fact(
        DisplayName = "Можно сыграть несколько партий подряд без перезапуска",
        Skip = "MR3: реализуй тест и удали эту строку")]
    public void SeveralGamesInARow()
    {
        Assert.Fail("Тест не реализован");
    }

    [Fact(
        DisplayName = "Детерминированный режим даёт предсказуемый вывод для автопроверки",
        Skip = "MR3: реализуй тест и удали эту строку")]
    public void DeterministicModeProducesPredictableOutput()
    {
        Assert.Fail("Тест не реализован");
    }
}