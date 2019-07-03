using System.IO;

namespace FileConverter3D
{
    /// <summary>
    /// Exposes file content at a given path as a Stream.
    /// </summary>
    public interface IFileStreamer
    {
        Stream GetStream(string filePath);
    }
}