using System.IO;

namespace FileConverter3D.Core
{
    /// <summary>
    /// Exposes file content at a given path as a Stream.
    /// </summary>
    public interface IFileStreamer
    {
        Stream GetStream(string filePath);
    }
}