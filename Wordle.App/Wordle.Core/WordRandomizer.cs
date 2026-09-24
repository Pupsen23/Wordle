namespace Wordle.Core
{
    public class WordRandomizer
    {
        private Random _random;
        private int _seed;
        public int Seed { get { return _seed; } }
        public WordRandomizer(int seed)
        {
            _random = new Random(seed);
        }
        public int GetRandomIndex(WordDictionary wordDictionary) { return _random.Next(wordDictionary.Length); }
        public string GetRandomWord(WordDictionary wordDictionary) { return wordDictionary.Words[GetRandomIndex(wordDictionary)]; }
    }
}