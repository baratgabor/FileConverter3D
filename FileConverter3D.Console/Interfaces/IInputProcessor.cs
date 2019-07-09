namespace FileConverter3D.Console
{
    interface IInputProcessor
    {
        string OptionName { get; }
        int ArgumentCount { get; }
        string HelpText { get; }
        ICommandAsync Process(string[] args);
    }
}