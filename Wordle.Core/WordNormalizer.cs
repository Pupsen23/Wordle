namespace Wordle.Core
{
    public static class WordNormalizer
    {
        public static bool CheckWordLength(IEnumerable<string> words, int wordLength)
        {
            for (int i = 0; i < words.Count(); i++)
            {
                if (wordLength != words.ElementAt(i).Length)
                    return false;
            }

            return true;
        }
        public static bool CheckEmpty(IEnumerable<string> words) { return words.Count() == 0; }
    }
}