namespace FileConverter3D.Console
{
    class OverwriteSwitch : IInputProcessor
    {
        public string OptionName { get; set; } = "overwritemode";

        public int ArgumentCount { get; set; } = 0;

        private ConverterState _state;

        public OverwriteSwitch(ConverterState state) => _state = state;

        public ICommandAsync Process(string[] args)
            => new RelayCommandConsoleConcurrent(
                "activate overwrite mode",
                () => (true, ""),
                () => _state.OverwriteMode = true,
                executeSynced: true);
    }
}