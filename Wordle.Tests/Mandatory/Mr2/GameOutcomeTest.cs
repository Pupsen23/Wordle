namespace Wordle.Tests.Mandatory.Mr2;

/// <summary>Обязательные тесты: завершение партии.</summary>
public class GameOutcomeTest
{
    [Fact(
        DisplayName = "Угаданное слово переводит сессию в статус WIN",
        Skip = "MR2: реализуй тест и удали эту строку")]
    public void CorrectGuessWinsTheGame()
    {
        Assert.Fail("Тест не реализован");
    }

    [Fact(
        DisplayName = "После 6 неудачных попыток сессия переходит в статус LOSE",
        Skip = "MR2: реализуй тест и удали эту строку")]
    public void SixFailedAttemptsLoseTheGame()
    {
        Assert.Fail("Тест не реализован");
    }

    [Fact(
        DisplayName = "При поражении показывается загаданное слово",
        Skip = "MR2: реализуй тест и удали эту строку")]
    public void AnswerIsRevealedOnLoss()
    {
        Assert.Fail("Тест не реализован");
    }

    [Fact(
        DisplayName = "Завершённая партия больше не принимает попытки",
        Skip = "MR2: реализуй тест и удали эту строку")]
    public void FinishedGameRejectsFurtherGuesses()
    {
        Assert.Fail("Тест не реализован");
    }
}