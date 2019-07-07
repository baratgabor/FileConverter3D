using System.Threading.Tasks;

namespace FileConverter3D.Console
{
    class CompositeCommand : ICommandAsync
    {
        private ICommandAsync[] _commands;

        public CompositeCommand(params ICommandAsync[] commands) => _commands = commands;

        public (bool result, string failReason) CanExecute()
        {
            foreach (var c in _commands)
            {
                var canExec = c.CanExecute();
                if (!canExec.result)
                    return (false, canExec.failReason);
            }

            return (true, "");
        }

        public async Task ExecuteAsync()
        {
            foreach (var c in _commands)
                await c.ExecuteAsync();
        }
    }
}