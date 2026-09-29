namespace Wordle.Core;

public class Config
{
    private static List<string> _words =
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
    public static ReadOnlyCollection<string> Words { get { return _words.AsReadOnly(); } }
    public int Seed { get; set; } = WordRandomizer.GetRandomSeed();
    public int MaxAttempts { get; set; } = 5;
    public string? CorrectWord { get; set; }
    public Config() {}
    public Config(int seed, int maxAttempts, string correctWord)
    {
        Seed = seed;
        MaxAttempts = maxAttempts;
        CorrectWord = correctWord;
    }
}