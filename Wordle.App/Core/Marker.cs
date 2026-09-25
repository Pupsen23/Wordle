namespace Core;

public static class Marker
{
    public enum CharStatus
    {
        INCORRECT,
        PRESENT,
        CORRECT
    };
    public static CharStatus[] GetMarked(GameSession gameSession, string guessWord)
    {
        return GetMerged(GetCorrect(guessWord, gameSession.CorrectWord), GetPresent(guessWord, gameSession.CorrectWord));
    }
    private static Dictionary<char, int> GetCharsCount(string guessWord)
    {
        Dictionary<char, int> charCount = [];

        for (int i = 0; i < guessWord.Length; i++)
        {
            if (!charCount.ContainsKey(guessWord[i]))
                charCount.Add(guessWord[i], 0);

            charCount[guessWord[i]]++;
        }

        return charCount;
    }
    private static CharStatus[] GetCorrect(string guessWord, string correctWord)
    {
        CharStatus[] charStatus = new CharStatus[correctWord.Length];

        for (int i = 0; i < correctWord.Length; i++)
        {
            if (correctWord[i] == guessWord[i])
                charStatus[i] = CharStatus.CORRECT;
        }

        return charStatus;
    }
    private static CharStatus[] GetPresent(string guessWord, string correctWord)
    {
        CharStatus[] charStatus = new CharStatus[correctWord.Length];
        Dictionary<char, int> charCount = GetCharsCount(guessWord);

        for (int i = 0; i < correctWord.Length; i++)
        {
            if (correctWord[i] == guessWord[i])
                continue;

            if (correctWord.Contains(guessWord[i]) && charCount[guessWord[i]] != 0)
            {
                charStatus[i] = CharStatus.PRESENT;
                charCount[guessWord[i]]--;
            }
        }

        return charStatus;
    }
    private static CharStatus[] GetMerged(CharStatus[] correctCharStatuses, CharStatus[] presentCharStatuses)
    {
        CharStatus[] charStatus = new CharStatus[correctCharStatuses.Length];

        for (int i = 0; i < correctCharStatuses.Length; i++)
        {
            if (correctCharStatuses[i] == CharStatus.CORRECT)
            {
                charStatus[i] = CharStatus.CORRECT;
                continue;
            }
            
            charStatus[i] = presentCharStatuses[i];
        }

        return charStatus;
    }
}