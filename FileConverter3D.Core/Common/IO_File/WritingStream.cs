using System.IO;

namespace FileConverter3D.Common
{
    /// <summary>
    /// Simple adapter to File.Open() in file creation mode.
    /// </summary>
    public class WritingStream : IFileStreamer
    {
        public Stream GetStream(string filePath)
            => File.Open(filePath, FileMode.CreateNew);
    }
}
