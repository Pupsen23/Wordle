using System.Collections.ObjectModel;

namespace Wordle.App
{
    public class Data
    {
        private static Data? _instance;
        public static Data Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Data();
                
                return _instance;
            }
        }
        private List<string> _words =
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
        public ReadOnlyCollection<string> Words { get { return _words.AsReadOnly(); } }
        public int Seed { get; set; } = 123;
    }
}