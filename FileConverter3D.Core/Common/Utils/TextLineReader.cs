using System.Collections.Generic;
using System.IO;

namespace FileConverter3D.Common
{
    /// <summary>
    /// Reads string lines from a file stream, and returns them trimmed.
    /// </summary>
    public class TextLineReader : IDataReader<string>
    {
        public virtual IEnumerable<string> Read(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                    if (!string.IsNullOrWhiteSpace(line))
                        yield return line.Trim();
            }
        }
    }
}
