namespace FileConverter3D
{
    public interface IFileImporter
    {
        IModel Import(string filePath);
    }
}
