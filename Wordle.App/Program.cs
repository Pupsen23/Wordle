using Wordle.Cli;
using Wordle.Core;
using Wordle.Cli.StringCollection;
using Wordle.Core.Words;

namespace Wordle;

public class Program
{
    public static void Main(string[] args)
    {
        Config config = ArgumentParser.GetConfig(args, out List<ArgumentException> argumentParseExceptions);

        if (config.ShowHelp)
        {
            foreach (string message in Help.All)
                Console.WriteLine(message);

            return;
        }

        if (config.ShowArgumentParseExceptions)
        {
            foreach (ArgumentException exception in argumentParseExceptions)
                Console.WriteLine(exception.Message);
        }
        
        WordDictionary wordDictionary;
        List<string> rawWords = config.RawWords.ToList();

        if (config.Word != null)
            rawWords.Add(new Word(config.Word).Value);

        wordDictionary = new WordDictionary(rawWords);
            
        ConsoleGame consoleGame = new ConsoleGame(Console.In, Console.Out, config, wordDictionary);
        consoleGame.Run();
    }
}