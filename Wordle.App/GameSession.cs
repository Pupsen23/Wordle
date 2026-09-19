using System.Collections.ObjectModel;

namespace Wordle.App
{
    public class GameSession
    {
        private string _correctWord;
        private uint _maxAttempts;
        private uint _attempts;
        private string _status;
        private List<string> _history;
        /// <summary>
        /// Отслеживает изменения CorrectWord и MaxAttempts
        /// </summary>
        private bool _isChanged;
        public string CorrectWord 
        {
            get { return _correctWord; }
            set
            {
                _correctWord = value;
                _isChanged = true;
            }
        }
        public uint MaxAttempts
        {
            get { return _maxAttempts; }
            set
            {
                if (value == 0)
                    throw new Exception("MaxAttempts value must be > 0.");

                _maxAttempts = value;
                _isChanged = true;
            }
        }
        public uint Attempts { get { return _attempts; } }
        public string Status { get { return _status; } }
        public ReadOnlyCollection<string> History { get { return _history.AsReadOnly(); } }
        public bool IsChanged { get { return _isChanged; } }
        public GameSession(string correctWord, uint maxAttempts)
        {
            CorrectWord = correctWord;
            MaxAttempts = maxAttempts;
            Reset();
        }
        private bool CheckAttempts() { return Attempts < MaxAttempts; }
        /// <summary>
        /// По задумке: поменялись CorrectWord или MaxAttempts - нужно выполнить Reset()
        /// </summary>
        public void Reset()
        {
            _attempts = 0;
            _status = Statuses.IN_PROGRESS;
            _history = [];
            _isChanged = false;
        }
        /// <summary>
        /// Угадал? true : false; если попытки кончились - null
        /// </summary>
        /// <param name="guessWord"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool? Guess(string guessWord)
        {
            if (IsChanged)
                throw new Exception($"Is not playable, IsChanged = true, must be resetted ('Reset()')");

            if (!CheckAttempts())
            {
                _status = Statuses.LOSE;
                return null;
            }

            _attempts++;
            _history.Add(guessWord);

            if (guessWord.Equals(CorrectWord))
            {
                _status = Statuses.WIN;
                return true;
            }

            return false;
        }
    }
}