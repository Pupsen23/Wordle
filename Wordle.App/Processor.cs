namespace Wordle.App
{
    public class Processor
    {
        private static Processor? _instance;
        public static Processor Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Processor();

                return _instance;
            }
        }
        public bool ApplyGuess(GameSession gameSession, )
        {
            
        }
    }
}