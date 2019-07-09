using System.Threading.Tasks;

namespace FileConverter3D.Console
{
    interface ICommandAsync
    {
        string OperationName { get; }
        (bool result, string failReason) CanExecute();
        Task ExecuteAsync();
    }
}