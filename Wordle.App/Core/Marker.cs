namespace Wordle.Core;

public static class Marker
{
    public enum CharStatus
    {
        Incorrect = 0,
        Present = 1,
        Correct = 2
    };
    public static CharStatus[]? GetMarked(GameSession gameSession, string guessWord)
    {
        if (!WordNormalizer.CheckLength(guessWord, gameSession.Word.Length))
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
    private static Dictionary<char, int> GetCharCount(GameSession gameSession, string guessWord)
    {
        Dictionary<char, int> charCount = []; // символ из guessWord : кол-во в GameSession.Word

        foreach (char symbol in guessWord)
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
    private static CharStatus[] GetCorrectMarked(GameSession gameSession, string guessWord, Dictionary<char, int> charCount)
    {
        CharStatus[] charStatus = new CharStatus[guessWord.Length];

        for (int i = 0; i < guessWord.Length; i++)
        {
            if (charCount[guessWord[i]] != 0 && gameSession.Word.CompareAt(guessWord[i], i))
            {
                charStatus[i] = CharStatus.Correct;
                charCount[guessWord[i]]--;
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
    private static CharStatus[] GetPresentMarked(GameSession gameSession, string guessWord, Dictionary<char, int> charCount)
    {
        CharStatus[] charStatus = new CharStatus[guessWord.Length];

        for (int i = 0; i < guessWord.Length; i++)
        {
            if (!gameSession.Word.CompareAt(guessWord[i], i) && charCount[guessWord[i]] != 0)
            {
                charStatus[i] = CharStatus.Present;
                charCount[guessWord[i]]--;
            }
        }

        return charStatus;
    }
}