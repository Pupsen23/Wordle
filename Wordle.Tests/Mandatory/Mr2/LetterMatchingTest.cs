using Wordle.Core;
using Wordle.Core.Words;

namespace Wordle.Tests.Mandatory.Mr2;

/// <summary>Обязательные тесты: раскраска букв.</summary>
public class LetterMatchingTest
{
    [Fact(DisplayName = "Базовый случай: загадано \"озеро\", ввод \"арбуз\" -> ❌🟡❌❌🟡")]
    public void BasicCase()
    {
        SecretWord word = new SecretWord("озеро");
        Word guessWord = new Word("арбуз");
        WordDictionary wordDictionary = new WordDictionary(new Config().RawWords);
        GameSession gameSession = WordleEngine.StartGame(wordDictionary, word, 5)!;
        Marker.CharStatus[] expected = [ Marker.CharStatus.Incorrect, Marker.CharStatus.Present, Marker.CharStatus.Incorrect, Marker.CharStatus.Incorrect, Marker.CharStatus.Present ];
        
        Marker.CharStatus[] actual = Marker.GetMarked(gameSession, guessWord)!;

        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Полное совпадение: загадано \"озеро\", ввод \"озеро\" -> ✅✅✅✅✅")]
    public void ExactMatch()
    {
        Word word = new Word("озеро");
        WordDictionary wordDictionary = new WordDictionary(new Config().RawWords);
        GameSession gameSession = WordleEngine.StartGame(wordDictionary, word, 5)!;
        Marker.CharStatus[] expected =
        [ Marker.CharStatus.Correct, Marker.CharStatus.Correct, Marker.CharStatus.Correct, Marker.CharStatus.Correct, Marker.CharStatus.Correct ];
        
        Marker.CharStatus[] actual = Marker.GetMarked(gameSession, word)!;

        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Повторяющиеся буквы: загадано \"сорок\", ввод \"оооом\" -> 🟡❌❌✅❌")]
    public void RepeatedLettersAreNotDoubleCounted()
    {
        Word correctWord = new Word("сорок");
        Word guessWord = new Word("оооом");
        WordDictionary wordDictionary = new WordDictionary(new Config().RawWords);
        GameSession gameSession = WordleEngine.StartGame(wordDictionary, correctWord, 5)!;
        Marker.CharStatus[] expected =
        [ Marker.CharStatus.Incorrect, Marker.CharStatus.Correct, Marker.CharStatus.Incorrect, Marker.CharStatus.Correct, Marker.CharStatus.Incorrect ];
        
        Marker.CharStatus[] actual = Marker.GetMarked(gameSession, guessWord)!;

        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Ни одна буква не подошла: все позиции ❌")]
    public void NoMatchingLetters()
    {
        Word correctWord = new Word("тапки");
        Word guessWord = new Word("чмоня");
        WordDictionary wordDictionary = new WordDictionary(new Config().RawWords);
        GameSession gameSession = WordleEngine.StartGame(wordDictionary, correctWord, 5)!;
        Marker.CharStatus[] expected =
        [ Marker.CharStatus.Incorrect, Marker.CharStatus.Incorrect, Marker.CharStatus.Incorrect, Marker.CharStatus.Incorrect, Marker.CharStatus.Incorrect ];
        
        Marker.CharStatus[] actual = Marker.GetMarked(gameSession, guessWord)!;

        Assert.Equal(expected, actual);
    }
}