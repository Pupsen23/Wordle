namespace Wordle.Core;

public class WordDictionary
{
    private List<string> _words;
    public ReadOnlyCollection<string> Words
    {
        get { return _words.AsReadOnly(); }
        set
        {
            if (WordNormalizer.CheckEmpty(value))
                throw new ArgumentException($"Words array length must be > 0, received (length): {value.Count()}");

            _words = value.ToList();

            for (int i = 0; i < value.Count; i++)
                _words[i] = _words[i].Trim().ToLowerInvariant();

            for (int i = 0; i < _words.Count; i++)
            {
                if (!WordNormalizer.CheckWordLength(_words[i], _words[0].Length))
                    throw new ArgumentException($"Not consistent word length, index: {i}");

                if (!WordNormalizer.CheckWordSymbols(_words[i]))
                    throw new ArgumentException($"Not allowed symbol in words element, index: {i}");
            }
        }
    }
    public int Length { get { return _words.Count; } }
    public WordDictionary(IEnumerable<string> words) { Words = words.ToList().AsReadOnly(); }
}