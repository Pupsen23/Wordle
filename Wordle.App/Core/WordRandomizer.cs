namespace Core;

public class WordRandomizer
{
    private int _seed;
    private Random _random;
    public int Seed 
    {
        get { return _seed; }
        set
        {
            _seed = value;
            _random = new Random(value);
        }
    }
    public WordRandomizer() { Seed = (int) DateTime.Now.Ticks - DateTime.Now.Nanosecond * DateTime.Now.Microsecond; }
    public WordRandomizer(int seed) { Seed = seed; }
    public int GetRandomIndex(WordDictionary wordDictionary) { return _random.Next(wordDictionary.Length); }
    public string GetRandomWord(WordDictionary wordDictionary) { return wordDictionary.Words[GetRandomIndex(wordDictionary)]; }
}