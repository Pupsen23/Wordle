using System.Net;
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

            _textWriter.Write("Догадка: ");
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

            if (guessResult == null) break; // fdhfdhdfjhdfjh

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
                _textWriter.WriteLine("Неверная догадка!");

                foreach (var mark in guessWordMarked)
                {
                    if (mark.Equals(Marker.CharStatus.Correct))
                        _textWriter.Write("✅");
                    else if (mark.Equals(Marker.CharStatus.Incorrect))
                        _textWriter.Write("❌");
                    else if (mark.Equals(Marker.CharStatus.Present))
                        _textWriter.Write("🟡");
                }

                _textWriter.Write('\n');

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

        _config.Seed = WordleEngine.WordRandomizer.Seed;

        RunGameLoop(gameSession);
    }
    public void Run()
    {
        string? input;

        while (true)
        {
            _textWriter.Write("Добро пожаловать в Wordle!\n" +
                            "Играем? (да/нет): ");
            input = _textReader.ReadLine()?.Trim().ToLowerInvariant();

            switch (input)
            {
                case "да":
                    PlayOnce();
                    break;
                case "нет":
                    _textWriter.WriteLine("Выход...");
                    return;
                default:
                    _textWriter.WriteLine("Некорректный ввод!");
                    break;
            }
        }
    }
}