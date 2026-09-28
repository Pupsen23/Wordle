namespace Wordle.Cli;

public interface ICommand
{
    public string Name { get; set; }
    public string Info { get; set; }
    public char? Key { get; set; }
    public void Execute(AppInfo appInfo);
}