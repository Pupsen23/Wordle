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
        CharStatus[] charStatus = new CharStatus[gameSession.CorrectWord.Length];
        MarkCorrect(charStatus, guessWord, gameSession.CorrectWord);
        MarkPresent(charStatus, guessWord, gameSession.CorrectWord);
        return charStatus;
    }
    private static void MarkCorrect(CharStatus[] charStatus, string guessWord, string correctWord)
    {
        for (int i = 0; i < correctWord.Length; i++)
        {
            if (correctWord[i] == guessWord[i])
                charStatus[i] = CharStatus.Correct;
        }
    }
    private static void MarkPresent(CharStatus[] charStatus, string guessWord, string correctWord)
    {
        int correctMarks = charStatus.Count(CharStatus.Correct);

        for (int i = 0; i < correctWord.Length; i++)
        {
            if (correctWord[i] == guessWord[i])
                continue;

            if (correctWord.Contains(guessWord[i]) && correctWord.Count(guessWord[i]) != correctMarks)
                charStatus[i] = CharStatus.Present;
        }
    }
}