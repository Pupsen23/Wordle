using System.Collections.ObjectModel;

namespace Wordle.App
{
    public class WordDict
    {
        private static WordDict? _instance;
        public static WordDict Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new WordDict(0);

                return _instance;
            }
        }
        private string[] _words = 
        [
            "абрис",
            "авеню",
            "агава",
            "агент",
            "адрес",
            "азарт",
            "актер",
            "аллея",
            "алмаз",
            "ампер",
            "ангел",
            "анонс",
            "арбуз",
            "архив",
            "аскет",
            "атлас",
            "афиша",
            "багаж",
            "барон",
            "басня",
            "бекон",
            "берег",
            "бетон",
            "билет",
            "бисер",
            "бланк",
            "блеск",
            "блоха",
            "бокал",
            "бомба",
            "борец",
            "ботва",
            "брань",
            "брешь",
            "бронх",
            "брюки",
            "бугор",
            "будка",
            "буква",
            "букет",
            "булка",
            "буран",
            "буфер",
            "буфет",
            "вагон",
            "валет",
            "ванна",
            "вафля",
            "ведро",
            "веник",
        ];
        private Random _randomizer;
        public ReadOnlyCollection<string> Words { get { return _words.AsReadOnly(); } }
        private WordDict(int seed)
        {
            _randomizer = new Random(seed);
        }
        public static void Init(int seed)
        {
            _instance = new WordDict(seed);
        }
        public string Pick()
        {
            return _words[_randomizer.Next(_words.Length)];
        }
    }
}