using System;
using System.Threading.Tasks;

namespace FileConverter3D.Console
{
    class RelayCommandConsoleConcurrent : ICommandAsync
    {
        private readonly string _operationName;
        private readonly Func<(bool result, string failReason)> _canExecute;
        private readonly Action _execute;
        private bool _executeSynced; // Hack. But wouldn't make sense to refactor for something this trivial.

        public RelayCommandConsoleConcurrent(string operationName, Func<(bool result, string failReason)> canExecute, Action execute, bool executeSynced = false)
        {
            _operationName = operationName;
            _canExecute = canExecute;
            _execute = execute;
            _executeSynced = executeSynced;
        }

        public (bool result, string failReason) CanExecute()
            => _canExecute();

        public async Task ExecuteAsync()
        {
            System.Console.Write($"Executing operation '{_operationName}' ... ");

            try
            {
                if (_executeSynced)
                    _execute();
                else
                    await Task.Run(_execute);
            }
            catch (Exception e)
            {
                var message = e is AggregateException ae ? ae.InnerException.Message : e.Message;
                System.Console.WriteLine("Failed. Reason:" + Environment.NewLine + message);
                throw;
            }

            System.Console.WriteLine("Success.");
        }
    }
}