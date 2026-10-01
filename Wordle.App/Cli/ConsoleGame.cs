using Wordle.Core;

namespace Wordle.Cli;

public class ConsoleGame
{
    private readonly TextReader _textReader;
    private readonly TextWriter _textWriter;
    private readonly Config _config;
    public ConsoleGame(TextReader consoleIn, TextWriter consoleOut, Config config)
    {
        _textReader = consoleIn;
        _textWriter = consoleOut;
        _config = config;

        WordleEngine.MaxAttempts = _config.MaxAttempts;
        WordleEngine.WordRandomizer.Seed = _config.Seed;
    }
    public void RunGameLoop(GameSession gameSession)
    {
        string? guessWord;

        while (true)
        {
            _textWriter.Write($"Попыток осталось: {gameSession.Attempts.GetRemaining()}/{gameSession.Attempts.MaxValue}\n" +
                                $"Длина слова: {gameSession.CorrectWord.Length}\n" +
                                $"Сид: {_config.Seed}\n");

            _textWriter.Write("Ввод ('q' - выход): ");
            guessWord = _textReader.ReadLine();

            if (string.IsNullOrEmpty(guessWord))
            {
                _textWriter.WriteLine("Некорректный ввод (пустой)!");
                continue;
            }

            if (guessWord.Equals("q"))
            {
                _textWriter.Write("Игра завершена досрочно!\n" +
                                    $"Загаданное слово: '{gameSession.CorrectWord}'\n");
                return;
            }

            GuessResult? guessResult = WordleEngine.ApplyGuess(gameSession, guessWord);

            if (guessResult == null) // но этого не может быть
            {
                _textWriter.WriteLine("Игра уже завершена, произошла ошибка!" +
                                        $"Загаданное слово: '{gameSession.CorrectWord}'\n");
                return;
            }

            if (guessResult.ErrorStatus == GuessResult.GuessErrorStatus.InvalidWordLength)
            {
                _textWriter.WriteLine($"Некорректный ввод (длина слова не соответствует {gameSession.CorrectWord.Length})!");
                continue;
            }
            else if (guessResult.ErrorStatus == GuessResult.GuessErrorStatus.HasInvalidSymbols)
            {
                _textWriter.WriteLine($"Некорректный ввод (запрещенные символы)!");
                continue;
            }

            // 🟡✅❌
            if (guessResult.Result)
            {
                _textWriter.WriteLine("Игра завершена, победа!");
                return;
            }
            else
            {
                var guessWordMarked = Marker.GetMarked(gameSession, guessWord);
                string marks = "";
                _textWriter.WriteLine("Неверная догадка!");

                foreach (var mark in guessWordMarked)
                {
                    if (mark.Equals(Marker.CharStatus.Correct))
                        marks += "✅";
                    else if (mark.Equals(Marker.CharStatus.Incorrect))
                        marks += "❌";
                    else if (mark.Equals(Marker.CharStatus.Present))
                        marks += "🟡";
                }

                _textWriter.WriteLine(marks);

                if (gameSession.Status == GameSession.GameStatus.Lose)
                {
                    _textWriter.Write("Игра завершена, попыток не осталось!\n" +
                                        $"Загаданное слово: '{gameSession.CorrectWord}'\n");
                    return;
                }
            }
        }
    }
    public void PlayOnce()
    {
        GameSession gameSession;

        if (string.IsNullOrEmpty(_config.CorrectWord))
            gameSession = WordleEngine.StartGame(new WordDictionary(Config.Words));
        else
            gameSession = WordleEngine.StartGame(new WordDictionary(Config.Words), _config.CorrectWord);

        RunGameLoop(gameSession);
    }
    public void UpdateSeed()
    {
        WordleEngine.WordRandomizer.Seed = WordRandomizer.GetRandomSeed();
        _config.Seed = WordleEngine.WordRandomizer.Seed;
        _textWriter.WriteLine($"Сид обновлен, новое значение: {_config.Seed}");
    }
    public void ShowConfig()
    {
        _textWriter.Write($"Максимальное кол-во попыток: {_config.MaxAttempts}\n" +
                            $"Сид: {_config.Seed}\n");
    }
    public void ShowDebugInfo()
    {
        ShowConfig();
        _textWriter.Write($"Последнее загаданное слово: '{_config.CorrectWord}'\n" +
                            $"Строка вызова: '{Environment.CommandLine}'\n");
    }
    public void Run()
    {
        string? inputKey;
        string[] options =
        [
            "1) Играть",
            "2) Обновить сид",
            "3) Посмотреть конфиг",
            "4) Выход",
            "!) Посмотреть отладочную информацию"
        ];

        while (true)
        {
            _textWriter.WriteLine("Добро пожаловать в Wordle!");
            foreach (string option in options) _textWriter.WriteLine(option);
            _textWriter.Write("Ввод: ");
            inputKey = _textReader.ReadLine()?.Trim().ToLowerInvariant();

            switch (inputKey)
            {
                case "1":
                    PlayOnce();
                    break;
                case "2":
                    UpdateSeed();
                    break;
                case "3":
                    ShowConfig();
                    break;
                case "!":
                    ShowDebugInfo();
                    break;
                case "4":
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