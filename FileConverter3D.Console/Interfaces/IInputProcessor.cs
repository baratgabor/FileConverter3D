namespace FileConverter3D.Console
{
    interface IInputProcessor
    {
        string OptionName { get; }
        int ArgumentCount { get; }

        ICommandAsync Process(string[] args);
    }
}