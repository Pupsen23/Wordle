using Wordle.Cli;
using Wordle.Core;

namespace Wordle;

public class Program
{
    public static void Main(string[] args)
    {
        Config config = ArgumentParser.GetConfig(args, out List<ArgumentException> argumentParseExceptions);
        foreach (ArgumentException exception in argumentParseExceptions)
            Console.WriteLine(exception.Message);
        ConsoleGame consoleGame = new ConsoleGame(Console.In, Console.Out, config, new WordDictionary(Config.Words));

        if (config.IsDetermined != null)
        {
            if ((bool) config.IsDetermined)
                consoleGame.PlayOnce();
            else
                consoleGame.Run();
        }
    }
}