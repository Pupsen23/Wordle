namespace Wordle.Cli.Messages;

public static class Help
{
    public const string Determined = $"{ArgumentParser.DeterminedArgName} - запустить в детерминированном режиме, необходим аргумент '{ArgumentParser.InstantGuessWord}'";
    public const string ShowArgumentParseExceptions = $"{ArgumentParser.ShowArgumentParseExceptionsArgName} - показать ошибки парсера аргументов";
    public const string MaxAttempts = $"{ArgumentParser.MaxAttemptsArgName} - установить максимальное кол-во попыток (будет проигнорировано в детерминированном режиме)";
    public const string Seed = $"{ArgumentParser.SeedArgName} - установить начальное значение сида";
    public const string Word = $"{ArgumentParser.WordArgName} - установить угадываемое слово";
    public const string InstantGuessWord = $"{ArgumentParser.InstantGuessWord} - установить мгновенную догадку";
}