namespace Wordle.Cli.StringCollection;

public static class Arguments
{
    public static readonly ImmutableArray<string> All =
    [
        ShowArgumentParseExceptions,
        MaxAttempts,
        Seed,
        Word,
    ];
    public const string ShowArgumentParseExceptions = "-s";
    public const string ShowHelp = "--help";
    public const string MaxAttempts = "--max-attempts";
    public const string Seed = $"--seed";
    public const string Word = $"--word";
}