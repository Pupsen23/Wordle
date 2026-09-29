namespace Wordle.Core;

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
    public WordRandomizer() { Seed = GetRandomSeed(); }
    public WordRandomizer(int seed) { Seed = seed; }
    public static int GetRandomSeed() { return (int) DateTime.Now.Ticks - DateTime.Now.Nanosecond * DateTime.Now.Microsecond; }
    public int GetRandomIndex(WordDictionary wordDictionary) { return _random.Next(wordDictionary.Length); }
    public string GetRandomWord(WordDictionary wordDictionary) { return wordDictionary.Words[GetRandomIndex(wordDictionary)]; }
}