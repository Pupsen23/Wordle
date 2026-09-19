using System.Collections.ObjectModel;

namespace Wordle.App
{
    public class WordDict
    {
        private List<string> _words;
        private uint _wordLength;
        public ReadOnlyCollection<string> Words
        {
            get { return _words.AsReadOnly(); }
            set
            {
                if (value.Count == 0)
                    throw new Exception("Words length must be > 0.");

                uint wordLength = (uint) value.ElementAt(0).Length;

                for (int i = 1; i < value.Count; i++)
                {
                    if (CheckWordLength(value.ElementAt(i), wordLength))
                        throw new Exception($"Not consistent word length, index: {i}.");
                }

                _words = value.ToList();
                _wordLength = wordLength;
            }
        }
        public uint WordLength { get { return _wordLength; } }
        public int Length { get { return _words.Count; } }
        public WordDict(IEnumerable<string> words)
        {
            Words = words.ToList().AsReadOnly();
        }
        private bool CheckWordLength(string word, uint length) { return word.Length == length; }
        private bool CheckIndex(int index) { return index > 0 && index < Length; }
        public bool AddWord(string word)
        {
            if (!CheckWordLength(word, _wordLength))
                return false;
            
            _words.Add(word);
            return true;
        }
        public bool RemoveWord(int index)
        {
            if (!CheckIndex(index) || _words.Count - 1 == 0)
                return false;
            
            _words.RemoveAt(index);
            return true;
        }
        public string PickWord(int index)
        {
            if (!CheckIndex(index))
                throw new IndexOutOfRangeException($"Index must be beetween {1} - {Length - 1}, got {index}");
            
            return _words[index];
        }
    }
}