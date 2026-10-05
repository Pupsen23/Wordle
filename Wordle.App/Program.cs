using Wordle.Cli;
using Wordle.Core;
using Wordle.Cli.Messages;

namespace Wordle;

public class Program
{
    public static void Main(string[] args)
    {
        Config config = ArgumentParser.GetConfig(args, out List<ArgumentException> argumentParseExceptions);

        if (config.ShowHelp) // временно
        {
            Console.WriteLine(Help.Determined);
            Console.WriteLine(Help.ShowArgumentParseExceptions);
            Console.WriteLine(Help.MaxAttempts);
            Console.WriteLine(Help.Seed);
            Console.WriteLine(Help.Word);
            Console.WriteLine(Help.InstantGuessWord);
            return;
        }

        if (config.ShowArgumentParseExceptions) // временно
        {
            foreach (ArgumentException exception in argumentParseExceptions)
                Console.WriteLine(exception.Message);
        }

        ConsoleGame consoleGame = new ConsoleGame(Console.In, Console.Out, config, new WordDictionary(Config.RawWords));

        if (config.IsDetermined)
            consoleGame.RunOnce();
        else
            consoleGame.Run();
    }
}