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
    private void ShowMarked(Word guessWord)
    {
        if (_gameSession == null)
            return;

        // 🟡✅❌
        var guessWordMarked = Marker.GetMarked(_gameSession, guessWord);
        string marks = "";

        foreach (var mark in guessWordMarked!) // прощаю, длина уже проверена (должно быть)
        {
            if (mark.Equals(Marker.CharStatus.Correct))
                marks += "✅";
            else if (mark.Equals(Marker.CharStatus.Incorrect))
                marks += "❌";
            else if (mark.Equals(Marker.CharStatus.Present))
                marks += "🟡";
        }

        _textWriter.WriteLine(marks);
    }
    public void RunGameLoop()
    {
        if (_gameSession == null) // в текущей реализации не может быть
            return;

        string input;
        bool normalizeResult;
        StringNormalizer.StringErrorStatus? inputCheckResult;
        Word guessWord;
        GuessResult? guessResult;

        while (true)
        {
            _textWriter.Write($"Попыток осталось: {_gameSession.GetRemainingAttempts()}/{_gameSession.MaxAttempts}\n" +
                                $"Длина слова: {_gameSession.Word.Length}\n" +
                                $"Ввод ('q' - выход): ");

            normalizeResult = StringNormalizer.TryNormalize(_textReader.ReadLine(), out input);

            if (!normalizeResult)
            {
                _textWriter.WriteLine("Некорректный ввод (пустой)!");
                continue;
            }
            
            if (input.Equals("q"))
            {
                _textWriter.Write("Игра завершена досрочно!\n" +
                                    $"Загаданное слово: '{_gameSession.Word.GetRevealed().Value}'\n");
                return;
            }

            inputCheckResult = StringNormalizer.Check(input);

            if (inputCheckResult != null)
            {
                if (inputCheckResult == StringNormalizer.StringErrorStatus.InvalidLength)
                    _textWriter.WriteLine("Некорректный ввод (несоответствующая длина)!");
                else if (inputCheckResult == StringNormalizer.StringErrorStatus.InvalidStructure)
                    _textWriter.WriteLine("Некорректный ввод (несоответствующая структура)!");
                else if (inputCheckResult == StringNormalizer.StringErrorStatus.HasForbiddenSymbols)
                    _textWriter.WriteLine("Некорректный ввод (запрещенные символы)!");

                continue;
            }
            
            guessWord = new Word(input);
            guessResult = WordleEngine.ApplyGuess(_gameSession, guessWord);

            if (guessResult == null) // но этого не может быть сейчас
            {
                _textWriter.WriteLine("Игра уже завершена, произошла ошибка!" +
                                        $"Загаданное слово: '{_gameSession.Word.GetRevealed().Value}'\n");
                return;
            }

            if (guessResult.ErrorStatus != null)
            {
                if (guessResult.ErrorStatus == GuessResult.GuessErrorStatus.InvalidLength)
                    _textWriter.WriteLine($"Некорректный ввод (длина слова не соответствует {_gameSession.Word.Length})!");
                else if (guessResult.ErrorStatus == GuessResult.GuessErrorStatus.NotInWordDictionary)
                    _textWriter.WriteLine("Некорректный ввод (неизвестное слово)!");

                continue;
            }
            
            ShowMarked(guessWord);

            if (guessResult.Result != null && (bool) guessResult.Result)
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
            _gameSession = WordleEngine.StartGame(_wordDictionary, _config.Word, _config.MaxAttempts);
            _config.Word = null; // забыто
        }
        else
            _gameSession = WordleEngine.StartGame(_wordDictionary, _config.MaxAttempts);

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

        _textWriter.Write($"Итог: {_gameSession.Status}\n" +
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
    public void Run()
    {
        string input;
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
            StringNormalizer.TryNormalize(_textReader.ReadLine(), out input);

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