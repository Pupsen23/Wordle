using Wordle.Core.Words;

namespace Wordle.Core;

public static class Marker
{
    public enum CharStatus
    {
        Incorrect = 0,
        Present = 1,
        Correct = 2
    };
    public static CharStatus[]? GetMarked(GameSession gameSession, Word guessWord)
    {
        if (!guessWord.CompareLength(gameSession.Word))
            return null;

        CharStatus[] charStatusResult = new CharStatus[guessWord.Length];
        CharStatus[] charStatusMarked;
        CharStatus[] charStatusPresent;

        {
            Dictionary<char, int> charCount = GetCharCount(gameSession, guessWord);
            charStatusMarked = GetCorrectMarked(gameSession, guessWord, charCount);
            charStatusPresent = GetPresentMarked(gameSession, guessWord, charCount);
        }

        for (int i = 0; i < charStatusResult.Length; i++)
        {
            if (charStatusMarked[i].Equals(CharStatus.Correct))
                charStatusResult[i] = charStatusMarked[i];
            else
                charStatusResult[i] = charStatusPresent[i];
        }

        return charStatusResult;
    }
    private static Dictionary<char, int> GetCharCount(GameSession gameSession, Word guessWord)
    {
        Dictionary<char, int> charCount = []; // символ из guessWord : кол-во в GameSession.Word

        foreach (char symbol in guessWord.Value)
        {
            if (!charCount.ContainsKey(symbol))
                charCount.Add(symbol, gameSession.Word.Count(symbol));
        }

        return charCount;
    }
    // 🟡✅❌
    // отбор
    // около
    // ✅❌❌❌❌
    // о - 2-1
    // к - 0
    // л - 0
    private static CharStatus[] GetCorrectMarked(GameSession gameSession, Word guessWord, Dictionary<char, int> charCount)
    {
        CharStatus[] charStatus = new CharStatus[guessWord.Length];

        for (int i = 0; i < guessWord.Length; i++)
        {
            if (charCount[guessWord.Value[i]] != 0 && gameSession.Word.CompareAt(guessWord.Value[i], i))
            {
                charStatus[i] = CharStatus.Correct;
                charCount[guessWord.Value[i]]--;
            }
        }

        return charStatus;
    }
    // 🟡✅❌
    // отбор
    // около
    // ✅❌🟡❌🟡
    // о - 1-1
    // к - 0
    // л - 0
    private static CharStatus[] GetPresentMarked(GameSession gameSession, Word guessWord, Dictionary<char, int> charCount)
    {
        CharStatus[] charStatus = new CharStatus[guessWord.Length];

        for (int i = 0; i < guessWord.Length; i++)
        {
            if (!gameSession.Word.CompareAt(guessWord.Value[i], i) && charCount[guessWord.Value[i]] != 0)
            {
                charStatus[i] = CharStatus.Present;
                charCount[guessWord.Value[i]]--;
            }
        }

        return charStatus;
    }
}