using System.Collections.ObjectModel;

namespace Wordle.App
{
    public class WordDict
    {
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
        public WordDict(int seed)
        {
            _randomizer = new Random(seed);
        }
        /*public string Pick(int index)
        {
            if (index < 0 || index >= _words.Length)
                return "";

            return _words[index]; 
        }*/
        public string Pick()
        {
            return _words[_randomizer.Next(_words.Length)];
        }
    }
}