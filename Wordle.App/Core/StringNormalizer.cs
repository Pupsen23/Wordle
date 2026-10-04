namespace Wordle.Core;

public static class StringNormalizer
{
    public enum StringErrorStatus
    {
        IsNullOrEmpty = 0,
        InvalidLength = 1,
        InvalidStructure = 2,
        HasForbiddenSymbols = 3,
    }
    private static bool CheckLength(string str) { return str.Length > 1; } // 2
    private static bool CheckForbiddenSymbols(string str) // 3
    {   
        foreach (char symbol in str)
        {
            if (!char.IsLetter(symbol))
                return false;
        }

        return true;
    }
    private static bool CheckStructure(string str) { return str.Count(str[0]) != str.Length; } // 4
    public static StringErrorStatus? Check(string? str) // 1 + 2-3-4
    {
        if (string.IsNullOrEmpty(str))
            return StringErrorStatus.IsNullOrEmpty;
        else if (!CheckLength(str))
            return StringErrorStatus.InvalidLength;
        else if (!CheckStructure(str))
            return StringErrorStatus.InvalidStructure;
        else if (!CheckForbiddenSymbols(str))
            return StringErrorStatus.HasForbiddenSymbols;
        
        return null;
    }
    public static string Normalize(string str) { return str.Trim().ToLowerInvariant(); }
}