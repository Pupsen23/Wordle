namespace Wordle.Cli;

public class CommandDictionary
{
    /*private Dictionary<char, ICommand> _keyValue = [];
    private Dictionary<string, ICommand> _nameValue = [];
    public IEnumerable<ICommand> AllClickable { get { return _keyValue.Values; } }
    public IEnumerable<ICommand> All { get { return _nameValue.Values; } }
    public CommandDictionary() {}
    public bool Add(ICommand command)
    {
        if (_nameValue.ContainsKey(command.Name))
            return false;
        
        _nameValue.Add(command.Name, command);
        
        if (command.Key != null)
        {
            char key = (char) command.Key;

            if (!_keyValue.ContainsKey(key))
                _keyValue.Add(key, command);
        }

        return true;
    }
    public bool TryGetCommand(char key, out ICommand? command) { return _keyValue.TryGetValue(key, out command); }
    public bool TryGetCommand(string name, out ICommand? command) { return _nameValue.TryGetValue(name, out command); }*/
}