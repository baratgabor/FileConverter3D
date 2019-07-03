namespace FileConverter3D.Core
{
    public interface IFileImporter
    {
        IModel Import(string filePath);
    }
}
