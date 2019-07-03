namespace FileConverter3D.Core
{
    public interface IFileExporter
    {
        void Export(string filePath, IModel model);
    }
}
