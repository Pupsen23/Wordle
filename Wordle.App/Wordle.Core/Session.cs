namespace Wordle.Core
{
    public static class Session
    {
        private static GameSession _gameSession = new GameSession(WordRandomizer.GetRandomWord(WordDictionary), Config.MaxAttempts);
        public static WordDictionary WordDictionary { get; } = new WordDictionary(Config.Words);
        public static WordRandomizer WordRandomizer { get; } = new WordRandomizer();
        public static GameSession GameSession { get { return _gameSession; } }
        public static GameSession.GuessStatus? ApplyGuess(GameSession gameSession, string guessWord)
        {
            if (!WordNormalizer.CheckGuessWord(guessWord, WordDictionary))
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
        public static void InitGameSession(string correctWord, int maxAttempts)
        {
            if (WordNormalizer.CheckCorrectWord(correctWord, WordDictionary))
                throw new ArgumentException($"Word '{correctWord} is not in the Session.WordDictionary'");

            _gameSession = new GameSession(correctWord, maxAttempts);
        }
    }
}