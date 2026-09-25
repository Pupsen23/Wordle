using System.Collections.ObjectModel;

namespace Core;

public class WordDictionary
{
    private List<string> _words;
    public ReadOnlyCollection<string> Words
    {
        get { return _words.AsReadOnly(); }
        set
        {
            if (WordNormalizer.CheckEmpty(value))
                throw new ArgumentException($"Words array length must be > 0, received (length): {_words.Count()}");

            if (!WordNormalizer.CheckWordLength(value, value.ElementAt(0).Length))
                throw new ArgumentException($"Not consistent word length.");

            _words = value.ToList();
        }
    }
    public int Length { get { return _words.Count; } }
    public WordDictionary(IEnumerable<string> words) { Words = words.ToList().AsReadOnly(); }
}