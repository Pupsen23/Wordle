namespace Wordle.Cli.StringCollection;

public class Help
{
    public static readonly ImmutableArray<string> All =
    [
        ShowArgumentParseExceptions,
        MaxAttempts,
        Seed,
        Word,
    ];
    public const string ShowArgumentParseExceptions = $"{Arguments.ShowArgumentParseExceptions} - показать ошибки парсера аргументов";
    public const string ShowHelp = $"{Arguments.ShowHelp} - ?";
    public const string MaxAttempts = $"{Arguments.MaxAttempts} - установить максимальное кол-во попыток";
    public const string Seed = $"{Arguments.Seed} - установить начальное значение сида";
    public const string Word = $"{Arguments.Word} - установить угадываемое слово";
}