using System;
using System.Threading.Tasks;

namespace FileConverter3D.Console
{
    class RelayCommandConsoleConcurrent : ICommandAsync
    {
        private readonly string _operationName;
        private readonly Func<(bool result, string failReason)> _canExecute;
        private readonly Action _execute;

        public RelayCommandConsoleConcurrent(string operationName, Func<(bool result, string failReason)> canExecute, Action execute)
        {
            _operationName = operationName;
            _canExecute = canExecute;
            _execute = execute;
        }

        public (bool result, string failReason) CanExecute()
            => _canExecute();

        public async Task ExecuteAsync()
        {
            System.Console.Write($"Executing operation '{_operationName}' ... ");

            try { await Task.Run(_execute); }
            catch (AggregateException ae) {
                System.Console.WriteLine("Failed. Reason:" + Environment.NewLine + ae.InnerException.Message);
                throw;
            }

            System.Console.WriteLine("Success.");
        }
    }
}