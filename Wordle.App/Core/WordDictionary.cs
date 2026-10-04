namespace Wordle.Core;

public class WordDictionary
{
    private List<string> _words = [];
    public ReadOnlyCollection<string> Words { get { return _words.AsReadOnly(); } }
    public int Length { get { return _words.Count; } }
    public WordDictionary() {}
    public WordDictionary(IEnumerable<string> words, bool checkConsistence = false)
    {
        bool setResult = SetWords(words, out WordNormalizer.WordErrorStatus? wordErrorStatus, checkConsistence);

        if (checkConsistence && !setResult)
            throw new ArgumentException("Words container cannot be empty and elements length must be consistent.");
        else if (!setResult)
            throw new ArgumentException("Words container cannot be empty.");

        if (wordErrorStatus == WordNormalizer.WordErrorStatus.InvalidLength)
            throw new ArgumentException($"Words container elements length must be > 0.");
        else if (wordErrorStatus == WordNormalizer.WordErrorStatus.InvalidStructure)
            throw new ArgumentException($"Words container elements cannot be one repeated letter like 'aaaaa'.");
        else if (wordErrorStatus == WordNormalizer.WordErrorStatus.HasForbiddenSymbols)
            throw new ArgumentException($"Words container elements cannot contain symbols but letters.");
    }
    public bool SetWords(IEnumerable<string> words, out WordNormalizer.WordErrorStatus? wordErrorStatus, bool checkConsistence = false)
    {
        wordErrorStatus = null;

        if (words.Count() == 0)
            return false;

        List<string> tempWords = words.ToList();

        for (int i = 0; i < tempWords.Count; i++)
        {
            tempWords[i] = WordNormalizer.Normalize(tempWords[i]);

            if (checkConsistence && !WordNormalizer.CheckLength(tempWords[i], tempWords[0]))
                return false;

            if (!WordNormalizer.CheckLength(tempWords[i]))
            {
                wordErrorStatus = WordNormalizer.WordErrorStatus.InvalidLength;
                return false;
            }
            
            if (!WordNormalizer.CheckStructure(tempWords[i]))
            {
                wordErrorStatus = WordNormalizer.WordErrorStatus.InvalidStructure;
                return false;
            }

            if (!WordNormalizer.CheckForbiddenSymbols(tempWords[i]))
            {
                wordErrorStatus = WordNormalizer.WordErrorStatus.HasForbiddenSymbols;
                return false;
            }
        }

        _words = tempWords;
        
        return true;
    }
}