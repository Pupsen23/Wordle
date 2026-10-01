using Wordle.Cli;
using Wordle.Core;

namespace Wordle;

public class Program
{
    public static void Main(string[] args)
    {
        Config config = ArgumentParser.GetConfig(args);
        ConsoleGame consoleGame = new ConsoleGame(Console.In, Console.Out, config);
        consoleGame.Run();
    }
}