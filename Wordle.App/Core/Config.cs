using Wordle.Core.Words;

namespace Wordle.Core;

public class Config
{
    private static List<string> _rawWords =
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
    public static ReadOnlyCollection<string> RawWords { get { return _rawWords.AsReadOnly(); } }
    public bool? IsDetermined { get; set; }
    public int? MaxAttempts { get; set; }
    public int? Seed { get; set; }
    public SecretWord? Word { get; set; }
    public Config() {}
    public Config(bool isDetermined, int maxAttempts, int seed, SecretWord word)
    {
        IsDetermined = isDetermined;
        MaxAttempts = maxAttempts;
        Seed = seed;
        Word = word;
    }
}