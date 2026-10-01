namespace Wordle.Core;

public static class WordNormalizer
{
    public static bool CheckWordLength(string word, int wordLength) { return word.Length == wordLength; }
    public static bool CheckEmpty(IEnumerable<string> words) { return words.Count() == 0; }
    public static bool CheckWordSymbols(string word)
    {
        foreach (char symbol in word)
        {
            if (!char.IsLetter(symbol))
                return false;
        }

        return true;
    }
}