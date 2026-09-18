using System.Collections.ObjectModel;

namespace Wordle.App
{
    public class GameSession
    {
        public static readonly string STATUS_IN_PROGRESS = "IN_PROGRESS";
        public static readonly string STATUS_WIN = "WIN";
        public static readonly string STATUS_LOSE = "LOSE";
        private string _word = "";
        private uint _attemptsMax;
        private uint _attempts;
        private string _status = "";
        private List<string> _history = [];
        public string Word 
        {
            get { return _word; }
            set { ReStart(); }
        }
        public uint AttemptsMax
        {
            get { return _attemptsMax; }
            set
            {
                if (value == 0)
                    throw new Exception("AttemptsMax value = 0");

                ReStart();
            }
        }
        public uint Attempts { get { return _attempts; } }
        public string Status { get { return _status; } }
        public ReadOnlyCollection<string> History { get { return _history.AsReadOnly(); } }
        public GameSession(string word, uint attemptsMax)
        {
            _word = word;
            _attemptsMax = attemptsMax;
        }
        private void ReStart()
        {
            _status = STATUS_IN_PROGRESS;
            _attempts = 0;

            _history.Clear();
        }
        public bool Guess(string word)
        {
            if (!IsValid())
                throw new Exception($"Attempts ({Attempts}) >= AttemptsMax ({AttemptsMax})");

            if (word.Equals(Word))
            {
                _status = STATUS_WIN;

                return true;
            }

            _history.Add(word);
            return false;
        }
        public bool IsValid()
        {
            return Status == STATUS_IN_PROGRESS && Attempts < AttemptsMax;
        }
    }
}