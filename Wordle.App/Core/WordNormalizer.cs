namespace Core;

public static class WordNormalizer
{
    public static bool CheckWordLength(IEnumerable<string> words, int wordLength)
    {
        for (int i = 0; i < words.Count(); i++)
        {
            if (!CheckWordLength(words.ElementAt(i), wordLength))
                return false;
        }

        return true;
    }
    public static bool CheckWordLength(string word, int wordLength) { return word.Length == wordLength; }
    public static bool CheckWordLength(string word, WordDictionary wordDictionary)
    {
        return CheckWordLength(word, wordDictionary.Words.ElementAt(0).Length);
    }
    public static bool CheckEmpty(IEnumerable<string> words) { return words.Count() == 0; }
    public static bool CheckCorrectWord(string correctWord, WordDictionary wordDictionary)
    {
        return wordDictionary.Words.Contains(correctWord);
    }
    public static bool CheckGuessWord(string guessWord, WordDictionary wordDictionary)
    {
        return CheckWordLength(guessWord, wordDictionary);
    }
}