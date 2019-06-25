using System.IO;

namespace FileConverter3D.Common
{
    /// <summary>
    /// Simple adapter to File.Open() in reading mode.
    /// </summary>
    class ReadingStream : IFileStreamer
    {
        public Stream GetStream(string filePath) 
            => File.Open(filePath, FileMode.Open);
    }
}
