using Wordle.Core.Words;

namespace Wordle.Core;

public class GuessResult
{
    public Word Word { get; }
    public bool Result { get; }
    public GuessResult(Word word, bool result)
    {
        Word = word;
        Result = result;
    }
}