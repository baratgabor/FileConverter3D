using System;
using System.Threading.Tasks;

namespace FileConverter3D.Console
{
    class RelayCommandConsoleConcurrent : ICommandAsync
    {
        public string OperationName { get; }
        private readonly Func<(bool result, string failReason)> _canExecute;
        private readonly Action _execute;
        private bool _executeSynced; // Hack. But wouldn't make sense to refactor for something this trivial.

        public RelayCommandConsoleConcurrent(string operationName, Func<(bool result, string failReason)> canExecute, Action execute, bool executeSynced = false)
        {
            OperationName = operationName;
            _canExecute = canExecute;
            _execute = execute;
            _executeSynced = executeSynced;
        }

        public (bool result, string failReason) CanExecute()
            => _canExecute();

        public async Task ExecuteAsync()
        {
            System.Console.Write($"Executing operation '{OperationName}' ... ");

            if (_executeSynced)
                _execute();
            else
            {
                await Task.Run(() =>
                {
                    try
                    {
                        _execute();
                    }
                    catch
                    {
                        System.Console.WriteLine("Failed.");
                        throw;
                    }
                });
            }

            System.Console.WriteLine("Success.");
        }
    }
}