namespace Wordle.Core
{
    public static class GeoGuesser
    {
        public static GameSession.GuessStatus ApplyGuess(GameSession gameSession, string guessWord)
        {
            if (!gameSession.CheckStatus())
                throw new Exception("Cannot ApplyGuess() on GameSession object with status not IN_PROGRESS.");

            if (!gameSession.Attempts.CheckValues())
            {
                gameSession.Status = GameSession.GameStatus.LOSE;
                return GameSession.GuessStatus.NO_ATTEMPTS;
            }

            if (gameSession.CorrectWord.Equals(guessWord))
            {
                gameSession.Status = GameSession.GameStatus.WIN;
                return GameSession.GuessStatus.CORRECT;
            }

            gameSession.Attempts.Value++;
            gameSession.History.Add(guessWord);

            return GameSession.GuessStatus.INCORRECT;
        }
    }
}