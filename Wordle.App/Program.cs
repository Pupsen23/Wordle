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
    /*public static void Main(string[] args)
    {
        var config = ArgumentParser.GetConfig(args);

        Console.WriteLine("args >");

        foreach (string arg in args)
            Console.WriteLine(arg);

        Console.WriteLine("config >");
        Console.WriteLine(config.Seed);
        Console.WriteLine(config.MaxAttempts);
        Console.WriteLine(config.CorrectWord);
    }*/
}