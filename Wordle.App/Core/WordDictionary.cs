using Wordle.Core.Words;

namespace Wordle.Core;

public class WordDictionary
{
    private List<Word> _words = [];
    public ReadOnlyCollection<Word> Words { get { return _words.AsReadOnly(); } }
    public int Length { get { return _words.Count; } }
    public WordDictionary() {}
    /// <exception cref="ArgumentException"></exception>
    public WordDictionary(IEnumerable<string> rawWords, bool checkConsistence = false)
    {
        bool setResult = SetWords(rawWords, checkConsistence);

        if (checkConsistence && !setResult)
            throw new ArgumentException("WordDictionary constructor: parameter 'rawWords' cannot be empty and elements length must be consistent.");
        else if (!setResult)
            throw new ArgumentException("WordDictionary constructor: parameter 'rawWords' cannot be empty.");
    }
    /// <exception cref="ArgumentException"></exception>
    public bool SetWords(IEnumerable<string> rawWords, bool checkConsistence = false)
    {
        if (rawWords.Count() == 0)
            return false;

        string firstRawWord = rawWords.ElementAt(0);
        List<Word> tempWords = [];

        foreach (string rawWord in rawWords)
        {   
            if (checkConsistence && rawWord.Length != firstRawWord.Length)
                return false;
            
            // может вызвать ошибку
            tempWords.Add(new Word(rawWord));
        }

        _words = tempWords;
        
        return true;
    }
    public bool SetWords(IEnumerable<Word> words, bool checkConsistence = false)
    {
        if (words.Count() == 0)
            return false;

        if (checkConsistence)
        {
            Word firstWord = words.ElementAt(0);

            foreach (Word word in words)
            {   
                if (word.Length != firstWord.Length)
                    return false;
            }
        }
        
        _words = words.ToList(); // копия или ссылка?
        
        return true;
    }
}