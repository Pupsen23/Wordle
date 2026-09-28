namespace Wordle.Core;

public static class Marker
{
    public enum CharStatus
    {
        Incorrect = 0,
        Present = 1,
        Correct = 2
    };
    public static CharStatus[] GetMarked(GameSession gameSession, string guessWord)
    {
        CharStatus[] charStatusCorrect = GetCorrect(guessWord, gameSession.CorrectWord);
        CharStatus[] charStatusPresent = GetPresent(guessWord, gameSession.CorrectWord, charStatusCorrect);
        return GetMerged(charStatusCorrect, charStatusPresent);
    }
    private static CharStatus[] GetCorrect(string guessWord, string correctWord)
    {
        CharStatus[] charStatus = new CharStatus[correctWord.Length];

        for (int i = 0; i < correctWord.Length; i++)
        {
            if (correctWord[i] == guessWord[i])
                charStatus[i] = CharStatus.Correct;
        }

        return charStatus;
    }
    private static CharStatus[] GetPresent(string guessWord, string correctWord, CharStatus[] charStatusCorrect)
    {
        CharStatus[] charStatusPresent = new CharStatus[correctWord.Length];

        for (int i = 0; i < correctWord.Length; i++)
        {
            if (correctWord[i] == guessWord[i])
                continue;

            if (correctWord.Contains(guessWord[i]) && correctWord.Count(guessWord[i]) != charStatusCorrect.Count(CharStatus.Correct))
                charStatusPresent[i] = CharStatus.Present;
        }

        return charStatusPresent;
    }
    private static CharStatus[] GetMerged(CharStatus[] correctCharStatus, CharStatus[] presentCharStatus)
    {
        CharStatus[] charStatus = new CharStatus[correctCharStatus.Length];

        for (int i = 0; i < correctCharStatus.Length; i++)
        {
            if (correctCharStatus[i] == CharStatus.Correct)
            {
                charStatus[i] = CharStatus.Correct;
                continue;
            }
            
            charStatus[i] = presentCharStatus[i];
        }

        return charStatus;
    }
}