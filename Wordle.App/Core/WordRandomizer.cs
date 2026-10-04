namespace Wordle.Core;

public class WordRandomizer
{
    private Random _random;
    private int _seed = GetRandomSeed();
    public int Seed { get { return _seed; } }
    public WordRandomizer() { _random = new Random(_seed); }
    public WordRandomizer(int seed)
    {
        _seed = seed;
        _random = new Random(seed);
    }
    public static int GetRandomSeed() { return (int) DateTime.Now.Ticks - DateTime.Now.Nanosecond * DateTime.Now.Microsecond; }
    public int GetRandomIndex(WordDictionary wordDictionary) { return _random.Next(wordDictionary.Length); }
    public string GetRandomWord(WordDictionary wordDictionary) { return wordDictionary.Words[GetRandomIndex(wordDictionary)]; }
    public int SetSeed(int seed)
    {
        int prevSeed = _seed;
        _seed = seed;
        _random = new Random(seed);

        return prevSeed;
    }
}