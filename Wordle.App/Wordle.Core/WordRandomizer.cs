namespace Wordle.Core
{
    public class WordRandomizer
    {
        private Random _random;
        public int Seed { get; }
        public WordRandomizer(int seed)
        {
            _random = new Random(seed);
        }
        public int GetRandomIndex(WordDictionary wordDictionary) { return _random.Next(wordDictionary.Length); }
        public string GetRandomWord(WordDictionary wordDictionary) { return wordDictionary.Words[GetRandomIndex(wordDictionary)]; }
    }
}