using Wordle.Core;

namespace Wordle.Cli;

public class AppInfo
{
    public bool IsRunning { get; set; } = true;
    public WordRandomizer WordRandomizer { get; set; }
    public WordDictionary WordDictionary { get; set; }
    public GameSession GameSession { get; set; }
    public CommandDictionary CommandDictionary { get; set; }
    public AppInfo(WordRandomizer wordRandomizer, WordDictionary wordDictionary, GameSession gameSession, CommandDictionary commandDictionary)
    {
        WordRandomizer = wordRandomizer;
        WordDictionary = wordDictionary;
        GameSession = gameSession;
        CommandDictionary = commandDictionary;
    }
}