using Wordle.Core;

namespace Wordle.Cli;

public class ConsoleGame
{
    private readonly TextReader _textReader;
    private readonly TextWriter _textWriter;
    private readonly Config _config;
    private readonly WordDictionary _wordDictionary;
    public ConsoleGame(TextReader consoleIn, TextWriter consoleOut, Config config, WordDictionary wordDictionary)
    {
        _textReader = consoleIn;
        _textWriter = consoleOut;
        _config = config;
        _wordDictionary = wordDictionary;

        if (_config.Seed != null)
            WordleEngine.WordRandomizer.SetSeed((int) _config.Seed);
        else
            _config.Seed = WordleEngine.WordRandomizer.Seed;
    }
    public void RunGameLoop(GameSession gameSession)
    {
        string? guessWord;

        while (true)
        {
            _textWriter.Write($"Попыток осталось: {gameSession.GetRemainingAttempts()}/{gameSession.MaxAttempts}\n" +
                                $"Длина слова: {gameSession.Word.Length}\n" +
                                $"Ввод ('q' - выход): ");

            guessWord = _textReader.ReadLine();

            if (string.IsNullOrEmpty(guessWord))
            {
                _textWriter.WriteLine("Некорректный ввод (пустой)!");
                continue;
            }

            if (guessWord.Equals("q"))
            {
                _textWriter.Write("Игра завершена досрочно!\n" +
                                    $"Загаданное слово: '{true}'\n");
                return;
            }

            GuessResult? guessResult = WordleEngine.ApplyGuess(gameSession, guessWord);

            if (guessResult == null) // но этого не может быть
            {
                _textWriter.WriteLine("Игра уже завершена, произошла ошибка!" +
                                        $"Загаданное слово: '{true}'\n");
                return;
            }

            if (guessResult.WordErrorStatus != null)
            {
                if (guessResult.WordErrorStatus == WordNormalizer.WordErrorStatus.InvalidLength)
                    _textWriter.WriteLine($"Некорректный ввод (длина слова не соответствует {gameSession.Word.Length})!");
                else if (guessResult.WordErrorStatus == WordNormalizer.WordErrorStatus.InvalidStructure)
                    _textWriter.WriteLine("Некорректная структура ввода!");
                else if (guessResult.WordErrorStatus == WordNormalizer.WordErrorStatus.HasForbiddenSymbols)
                    _textWriter.WriteLine($"Некорректный ввод (запрещенные символы)!");

                continue;
            }

            // 🟡✅❌
            var guessWordMarked = Marker.GetMarked(gameSession, guessWord);
            string marks = "";

            foreach (var mark in guessWordMarked!) // прощаю, длина уже проверена
            {
                if (mark.Equals(Marker.CharStatus.Correct))
                    marks += "✅";
                else if (mark.Equals(Marker.CharStatus.Incorrect))
                    marks += "❌";
                else if (mark.Equals(Marker.CharStatus.Present))
                    marks += "🟡";
            }

            _textWriter.WriteLine(marks);

            if (guessResult.Result != null && (bool) guessResult.Result)
            {
                _textWriter.WriteLine("Игра завершена, победа!");
                return;
            }
            else if (gameSession.Status == GameSession.GameStatus.Lose)
            {
                _textWriter.Write("Игра завершена, попыток не осталось!\n" +
                                    $"Загаданное слово: '{true}'\n");
                return;
            }
            else
                _textWriter.WriteLine("Неверная догадка!");
        }
    }
    public void PlayOnce()
    {
        GameSession gameSession;

        if (!string.IsNullOrEmpty(_config.Word)) // если было подано конкретное слово
        {
            if (_config.MaxAttempts != null)
                gameSession = WordleEngine.StartGame(_config.Word, (int) _config.MaxAttempts);
            else
                gameSession = WordleEngine.StartGame(_config.Word);
            
            _config.Word = null;
        }
        else
        {
            if (_config.MaxAttempts != null)
                gameSession = WordleEngine.StartGame(_wordDictionary, (int) _config.MaxAttempts);
            else
                gameSession = WordleEngine.StartGame(_wordDictionary);
        }

        RunGameLoop(gameSession);
    }
    public void UpdateSeed()
    {
        int currentSeed = WordRandomizer.GetRandomSeed();
        int prevSeed = WordleEngine.WordRandomizer.SetSeed(currentSeed);
        _config.Seed = currentSeed;
        _textWriter.WriteLine($"Сид обновлен: {prevSeed} -> {currentSeed}");
    }
    public void ShowConfig()
    {
        _textWriter.Write($"Максимальное кол-во попыток: {_config.MaxAttempts}\n" +
                            $"Сид: {_config.Seed}\n");
    }
    // как выводить секретное слово если оно блять секретное?
    public void Run()
    {
        string? input;
        string[] options =
        [
            "1) Играть",
            "2) Обновить сид",
            "3) Посмотреть историю",
            "4) Посмотреть конфиг",
            "5) Выход"
        ];

        while (true)
        {
            _textWriter.WriteLine("Добро пожаловать в Wordle!");
            foreach (string option in options) _textWriter.WriteLine(option);
            _textWriter.Write("Ввод: ");
            input = _textReader.ReadLine()?.Trim().ToLowerInvariant();

            switch (input)
            {
                case "1":
                    PlayOnce();
                    break;
                case "2":
                    UpdateSeed();
                    break;
                case "3":

                    break;
                case "4":
                    ShowConfig();
                    break;
                case "5":
                case "q":
                    _textWriter.WriteLine("Выход...");
                    return;
                default:
                    _textWriter.WriteLine("Некорректный ввод!");
                    break;
            }
        }
    }
}