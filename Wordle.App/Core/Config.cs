using Wordle.Core.Words;

namespace Wordle.Core;

public class Config
{
    private readonly string[] _rawWords =
    [
        "арбуз",
        "банан",
        "ветер",
        "город",
        "книга",
        "кошка",
        "мышка",
        "школа",
        "весна",
        "осень",
        "берег",
        "трава",
        "песок",
        "дверь",
        "стена",
        "крыша",
        "крыло",
        "замок",
        "число",
        "буква",
        "слово",
        "фраза",
        "текст",
        "песня",
        "танец",
        "кубик",
        "мячик",
        "шайба",
        "спорт",
        "бегун",
        "метла",
        "ведро",
        "чашка",
        "ложка",
        "вилка",
        "плита",
        "щетка",
        "паста",
        "шапка",
        "сорок",
        "носки",
        "обувь",
        "сапог",
        "озеро",
        "тапки",
        "сумка",
        "рубль",
        "рынок",
        "товар",
        "касса"
    ];
    public ReadOnlyCollection<string> RawWords { get { return _rawWords.AsReadOnly(); } }
    public bool ShowArgumentParseExceptions { get; set; } = false;
    public bool ShowHelp { get; set; } = false;
    public int MaxAttempts { get; set; } = 5;
    public int? Seed { get; set; }
    public SecretWord? Word { get; set; }
    public Config() {}
    public Config(int maxAttempts, int seed, SecretWord word, IEnumerable<string> rawWords)
    {
        MaxAttempts = maxAttempts;
        Seed = seed;
        Word = word;
        _rawWords = rawWords.ToArray();
    }
}