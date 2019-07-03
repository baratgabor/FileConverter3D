using System.IO;

namespace FileConverter3D.Core.Common
{
    /// <summary>
    /// Simple adapter to File.Open() in reading mode.
    /// </summary>
    public class ReadingStream : IFileStreamer
    {
        public Stream GetStream(string filePath) 
            => File.Open(filePath, FileMode.Open);
    }
}
