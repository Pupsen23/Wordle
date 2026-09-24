using System.Collections.ObjectModel;

namespace Wordle.Core
{
    public class GameSession
    {
        public enum GameStatus
        {
            IN_PROGRESS = 0,
            WIN = 0,
            LOSE = 0
        }
        public enum GuessStatus
        {
            NO_ATTEMPTS = 0,
            INCORRECT = 1,
            CORRECT = 2
        }
        private string _correctWord;
        private int _maxAttempts;
        private int _attempts;
        private GameStatus _status;
        private List<string> _history;
        // Отслеживает изменения CorrectWord и MaxAttempts
        public string CorrectWord { get { return _correctWord; } }
        public int MaxAttempts { get { return _maxAttempts; } }
        public int Attempts { get { return _attempts; } }
        public GameStatus Status { get { return _status; } }
        public ReadOnlyCollection<string> History { get { return _history.AsReadOnly(); } }
        public GameSession(string correctWord, int maxAttempts)
        {
            if (maxAttempts < 1)
                throw new Exception("Max Attempts must be > 0");

            _correctWord = correctWord;
            _maxAttempts = maxAttempts;
            _attempts = 0;
            _status = GameStatus.IN_PROGRESS;
            _history = [];

        }
        private bool CheckAttempts() { return Attempts < MaxAttempts; }
        // По задумке: поменялись CorrectWord или MaxAttempts - нужно выполнить Reset()
        public GuessStatus Guess(string guessWord)
        {
            if (!Status.Equals(GameStatus.IN_PROGRESS))
                throw new Exception("GameSession status is not 'IN_PROGRESS'");

            if (!CheckAttempts())
            {
                _status = GameStatus.LOSE;
                return GuessStatus.NO_ATTEMPTS;
            }

            _attempts++;
            _history.Add(guessWord);

            if (guessWord.Equals(CorrectWord))
            {
                _status = GameStatus.WIN;
                return GuessStatus.CORRECT;
            }

            return GuessStatus.INCORRECT;
        }
    }
}