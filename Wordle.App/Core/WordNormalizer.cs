namespace Wordle.Core;

public static class WordNormalizer
{
    public enum WordErrorStatus
    {
        InvalidLength,
        InvalidStructure,
        HasForbiddenSymbols,
    }
    public static bool CheckLength(string word1, string word2) { return word1.Length == word2.Length; }
    public static bool CheckLength(string word, int wordLength) { return word.Length == wordLength; }
    public static bool CheckLength(string word) { return word.Length > 1; }
    public static bool CheckForbiddenSymbols(string word)
    {
        foreach (char symbol in word)
        {
            if (!char.IsLetter(symbol))
                return false;
        }

        return true;
    }
    public static bool CheckStructure(string word) { return word.Count(word[0]) != word.Length; }
    public static string Normalize(string word) { return word.Trim().ToLowerInvariant(); }
}