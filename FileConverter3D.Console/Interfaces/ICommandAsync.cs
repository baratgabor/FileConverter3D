using System.Threading.Tasks;

namespace FileConverter3D.Console
{
    interface ICommandAsync
    {
        (bool result, string failReason) CanExecute();
        Task ExecuteAsync();
    }
}