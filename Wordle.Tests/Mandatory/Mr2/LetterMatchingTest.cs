using Wordle.Core;

namespace Wordle.Tests.Mandatory.Mr2;

/// <summary>Обязательные тесты: раскраска букв.</summary>
public class LetterMatchingTest
{
    [Fact(DisplayName = "Базовый случай: загадано \"озеро\", ввод \"арбуз\" -> ❌🟡❌❌🟡")]
    public void BasicCase()
    {
        string correctWord = "озеро";
        string guessWord = "арбуз";
        WordDictionary wordDictionary = new WordDictionary(Config.RawWords);
        GameSession gameSession = WordleEngine.StartGame(wordDictionary, correctWord)!;
        Marker.CharStatus[] expected = [ Marker.CharStatus.Incorrect, Marker.CharStatus.Present, Marker.CharStatus.Incorrect, Marker.CharStatus.Incorrect, Marker.CharStatus.Present ];
        
        Marker.CharStatus[] actual = Marker.GetMarked(gameSession, guessWord);

        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Полное совпадение: загадано \"озеро\", ввод \"озеро\" -> ✅✅✅✅✅")]
    public void ExactMatch()
    {
        string correctWord = "озеро";
        string guessWord = "озеро";
        WordDictionary wordDictionary = new WordDictionary(Config.RawWords);
        GameSession gameSession = WordleEngine.StartGame(wordDictionary, correctWord)!;
        Marker.CharStatus[] expected =
        [ Marker.CharStatus.Correct, Marker.CharStatus.Correct, Marker.CharStatus.Correct, Marker.CharStatus.Correct, Marker.CharStatus.Correct ];
        
        Marker.CharStatus[] actual = Marker.GetMarked(gameSession, guessWord);

        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Повторяющиеся буквы: загадано \"сорок\", ввод \"оооом\" -> 🟡❌❌✅❌")]
    public void RepeatedLettersAreNotDoubleCounted()
    {
        string correctWord = "сорок";
        string guessWord = "оооом";
        WordDictionary wordDictionary = new WordDictionary(Config.RawWords);
        GameSession gameSession = WordleEngine.StartGame(wordDictionary, correctWord)!;
        Marker.CharStatus[] expected =
        [ Marker.CharStatus.Incorrect, Marker.CharStatus.Correct, Marker.CharStatus.Incorrect, Marker.CharStatus.Correct, Marker.CharStatus.Incorrect ];
        
        Marker.CharStatus[] actual = Marker.GetMarked(gameSession, guessWord);

        Assert.Equal(expected, actual);
    }

    [Fact(DisplayName = "Ни одна буква не подошла: все позиции ❌")]
    public void NoMatchingLetters()
    {
        string correctWord = "тапки";
        string guessWord = "чмоня";
        WordDictionary wordDictionary = new WordDictionary(Config.RawWords);
        GameSession gameSession = WordleEngine.StartGame(wordDictionary, correctWord)!;
        Marker.CharStatus[] expected =
        [ Marker.CharStatus.Incorrect, Marker.CharStatus.Incorrect, Marker.CharStatus.Incorrect, Marker.CharStatus.Incorrect, Marker.CharStatus.Incorrect ];
        
        Marker.CharStatus[] actual = Marker.GetMarked(gameSession, guessWord);

        Assert.Equal(expected, actual);
    }
}