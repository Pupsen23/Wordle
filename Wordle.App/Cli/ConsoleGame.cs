using Wordle.Core;
using Wordle.Core.Words;

namespace Wordle.Cli;

public class ConsoleGame
{
    private readonly TextReader _textReader;
    private readonly TextWriter _textWriter;
    private readonly Config _config;
    private readonly WordDictionary _wordDictionary;
    private GameSession? _gameSession;
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
    public void RunGameLoop()
    {
        if (_gameSession == null)
            return;

        string? input;
        Word guessWord;

        while (true)
        {
            _textWriter.Write($"Попыток осталось: {_gameSession.GetRemainingAttempts()}/{_gameSession.MaxAttempts}\n" +
                                $"Длина слова: {_gameSession.Word.Length}\n" +
                                $"Ввод ('q' - выход): ");

            input = _textReader.ReadLine();
            
            if (!string.IsNullOrEmpty(input) && input.Equals("q"))
            {
                _textWriter.Write("Игра завершена досрочно!\n" +
                                    $"Загаданное слово: '{_gameSession.Word.GetRevealed().Value}'\n");
                return;
            }

            // это временно
            try { guessWord = new Word(input); }
            catch(ArgumentException ex)
            {
                _textWriter.WriteLine(ex.Message);
                continue;
            }

            /*if (guessResult.WordErrorStatus != null)
            {
                if (guessResult.WordErrorStatus == StringNormalizer.StringErrorStatus.InvalidLength)
                    _textWriter.WriteLine($"Некорректный ввод (длина слова не соответствует {gameSession.Word.Length})!");
                else if (guessResult.WordErrorStatus == StringNormalizer.StringErrorStatus.InvalidStructure)
                    _textWriter.WriteLine("Некорректная структура ввода!");
                else if (guessResult.WordErrorStatus == StringNormalizer.StringErrorStatus.HasForbiddenSymbols)
                    _textWriter.WriteLine($"Некорректный ввод (запрещенные символы)!");

                continue;
            }*/

            GuessResult? guessResult = WordleEngine.ApplyGuess(_gameSession, guessWord);

            if (guessResult == null) // но этого не может быть        может быть второе условие
            {
                if (!_gameSession.CheckStatus())
                {
                    _textWriter.WriteLine("Игра уже завершена, произошла ошибка!" +
                                            $"Загаданное слово: '{_gameSession.Word.GetRevealed().Value}'\n");
                    return;
                }
                else
                {
                    _textWriter.WriteLine($"Некорректный ввод (длина слова не соответствует {_gameSession.Word.Length})!");
                    continue;
                }
            }

            // 🟡✅❌
            var guessWordMarked = Marker.GetMarked(_gameSession, guessWord);
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
            
            if (guessResult.Result)
            {
                _textWriter.WriteLine("Игра завершена, победа!");
                return;
            }
            else if (_gameSession.Status == GameSession.GameStatus.Lose)
            {
                _textWriter.Write("Игра завершена, попыток не осталось!\n" +
                                    $"Загаданное слово: '{_gameSession.Word.GetRevealed().Value}'\n");
                return;
            }
            else
                _textWriter.WriteLine("Неверная догадка!");
        }
    }
    public void PlayOnce()
    {
        if (_config.Word != null) // если было подано конкретное слово
        {
            if (_config.MaxAttempts != null)
                _gameSession = WordleEngine.StartGame(_config.Word, (int) _config.MaxAttempts);
            else
                _gameSession = WordleEngine.StartGame(_config.Word);
            
            _config.Word = null;
        }
        else
        {
            if (_config.MaxAttempts != null)
                _gameSession = WordleEngine.StartGame(_wordDictionary, (int) _config.MaxAttempts);
            else
                _gameSession = WordleEngine.StartGame(_wordDictionary);
        }

        RunGameLoop();
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
    public void ShowLastGame()
    {
        if (_gameSession == null)
        {
            _textWriter.WriteLine("Последняя игра не найдена!");
            return;
        }

        _textWriter.Write($"Итог: {_gameSession.Status}\n" + // эта строчка временна (2 - победа)
                            $"Загаданное слово: '{_gameSession.Word.GetRevealed().Value}'\n" +
                            $"Потрачено попыток: {_gameSession.Attempts}/{_gameSession.MaxAttempts}\n" +
                            $"История попыток (с последней): \n");
        
        if (_gameSession.History.Count == 0)
        {
            _textWriter.WriteLine("Пусто...");
            return;
        }

        for (int i = _gameSession.History.Count - 1; i >= 0; i--)
            _textWriter.WriteLine($"{i + 1}) {_gameSession.History[i].Value}");
    }
    // как выводить секретное слово если оно блять секретное?
    public void Run()
    {
        string? input;
        string[] options =
        [
            "1) Играть",
            "2) Обновить сид",
            "3) Посмотреть последнюю игру",
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
                    ShowLastGame();
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