using Wordle.Core.Words;

namespace Wordle.Core;

public class WordDictionary
{
    private List<Word> _words = [];
    public ReadOnlyCollection<Word> Words { get { return _words.AsReadOnly(); } }
    public int Length { get { return _words.Count; } }
    /// <exception cref="ArgumentException"></exception>
    public WordDictionary(IEnumerable<string> rawWords, bool checkConsistence = false)
    {
        bool setResult = SetWords(rawWords, checkConsistence);

        if (checkConsistence && !setResult)
            throw new ArgumentException("Parameter 'rawWords' cannot be empty and elements length must be consistent.");
        else if (!setResult)
            throw new ArgumentException("Parameter 'rawWords' cannot be empty.");
    }
    public WordDictionary(IEnumerable<Word> words, bool checkConsistence = false)
    {
        bool setResult = SetWords(words, checkConsistence);

        if (checkConsistence && !setResult)
            throw new ArgumentException("Parameter 'rawWords' cannot be empty and elements length must be consistent.");
        else if (!setResult)
            throw new ArgumentException("Parameter 'rawWords' cannot be empty.");
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

        Word firstWord = words.ElementAt(0);
        List<Word> tempWords = [];

        foreach (Word word in words)
        {   
            if (checkConsistence && !word.CompareLength(firstWord))
                return false;
            
            tempWords.Add(new Word(word));
        }

        _words = tempWords;
        
        return true;
    }
    public bool Contains(Word word)
    {
        foreach (Word innerWord in Words)
        {
            if (innerWord.CompareTo(word))
                return true;
        }

        return false;
    }
}