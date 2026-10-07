using Wordle.Core;
using Wordle.Cli;
using System.Text;
using Wordle.Core.Words;

namespace Wordle.Tests.Mandatory.Mr3;

/// <summary>Обязательные тесты: меню и режим автопроверки.</summary>
public class MenuTest
{
    [Fact(DisplayName = "Некорректный пункт меню не роняет программу")]
    public void InvalidMenuChoiceDoesNotCrash()
    {
        Config config = new Config();
        StringReader stringReader = new StringReader("k\nq\n");
        ConsoleGame consoleGame = new ConsoleGame(stringReader, Console.Out, config, new WordDictionary(config.RawWords));
        Exception? exception = null;

        try { consoleGame.Run(); }
        catch(Exception ex) { exception = ex; }

        Assert.Null(exception);
    }

    [Fact(DisplayName = "Можно сыграть несколько партий подряд без перезапуска")]
    public void SeveralGamesInARow()
    {
        SecretWord secretWord = new SecretWord("озеро");
        Config config = new Config() { MaxAttempts = 1, Word = secretWord };
        StringBuilder stringBuilder = new StringBuilder();
        StringReader stringReader = new StringReader("1\nозеро\n1\nозеро\nq\n");
        StringWriter stringWriter = new StringWriter(stringBuilder);
        ConsoleGame consoleGame = new ConsoleGame(stringReader, stringWriter, config, new WordDictionary(config.RawWords));

        consoleGame.Run();
        bool result = stringBuilder.ToString().ToArray().Count("Игра завершена") == 2;

        Assert.True(result);
    }

    [Fact(DisplayName = "Детерминированный режим даёт предсказуемый вывод для автопроверки")]
    public void DeterministicModeProducesPredictableOutput()
    {
        Config config = new Config() { MaxAttempts = 1, Word = new SecretWord("озеро") };
        WordDictionary wordDictionary = new WordDictionary(config.RawWords);
        string result1;
        string result2;
        string inputString = "1\nозеро\nq\n";
        StringBuilder outputString = new StringBuilder();
        StringReader stringReader;
        StringWriter stringWriter;

        {
            stringReader = new StringReader(inputString);
            stringWriter = new StringWriter(outputString);
            ConsoleGame consoleGame = new ConsoleGame(stringReader, stringWriter, config, wordDictionary);
            consoleGame.Run();
            result1 = outputString.ToString();
            outputString.Clear();
        }
        config = new Config() { MaxAttempts = 1, Word = new SecretWord("озеро") };
        {
            stringReader = new StringReader(inputString);
            stringWriter = new StringWriter(outputString);
            ConsoleGame consoleGame = new ConsoleGame(stringReader, stringWriter, config, wordDictionary);
            consoleGame.Run();
            result2 = outputString.ToString();
            outputString.Clear();
        }

        Assert.Equal(result1, result2);
        Assert.Contains("Игра завершена, победа!", result1);
    }
}