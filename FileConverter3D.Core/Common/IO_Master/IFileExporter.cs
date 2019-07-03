namespace FileConverter3D
{
    public interface IFileExporter
    {
        void Export(string filePath, IModel model);
    }
}
