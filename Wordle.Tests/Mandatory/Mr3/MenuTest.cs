using Wordle.Core;
using Wordle.Cli;
using System.Text;

namespace Wordle.Tests.Mandatory.Mr3;

/// <summary>Обязательные тесты: меню и режим автопроверки.</summary>
public class MenuTest
{
    [Fact(DisplayName = "Некорректный пункт меню не роняет программу")]
    public void InvalidMenuChoiceDoesNotCrash()
    {
        Config config = new Config();
        StringReader stringReader = new StringReader("5\nq\n");
        ConsoleGame consoleGame = new ConsoleGame(stringReader, Console.Out, config);
        Exception? exception = null;

        try { consoleGame.Run(); }
        catch(Exception ex) { exception = ex; }

        Assert.Null(exception);
    }

    [Fact(DisplayName = "Можно сыграть несколько партий подряд без перезапуска")]
    public void SeveralGamesInARow()
    {
        Config config = new Config(1, 123);
        StringBuilder stringBuilder = new StringBuilder();
        StringReader stringReader = new StringReader("1\nчмоня\n1\nчмоня\nq\n");
        StringWriter stringWriter = new StringWriter(stringBuilder);
        ConsoleGame consoleGame = new ConsoleGame(stringReader, stringWriter, config);

        consoleGame.Run();
        bool result = stringBuilder.ToString().ToArray().Count("Игра завершена") == 2;

        Assert.True(result);
    }

    [Fact(DisplayName = "Детерминированный режим даёт предсказуемый вывод для автопроверки")]
    public void DeterministicModeProducesPredictableOutput()
    {
        Config config = new Config(1, "озеро");
        string result1;
        string result2;
        string inputString = "1\nозеро\n1\nозеро\nq\n";
        StringBuilder outputString = new StringBuilder();
        StringReader stringReader;
        StringWriter stringWriter = new StringWriter(outputString);

        {
            stringReader = new StringReader(inputString);
            ConsoleGame consoleGame = new ConsoleGame(stringReader, stringWriter, config);
            consoleGame.Run();
            result1 = outputString.ToString();
            outputString.Clear();
        }
        {
            stringReader = new StringReader(inputString);
            ConsoleGame consoleGame = new ConsoleGame(stringReader, stringWriter, config);
            consoleGame.Run();
            result2 = outputString.ToString();
            outputString.Clear();
        }

        Assert.Equal(result1, result2);
        Assert.Contains("Игра завершена, победа!", result1);
    }
}