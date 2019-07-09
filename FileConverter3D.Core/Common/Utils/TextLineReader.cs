using System.Collections.Generic;
using System.IO;

namespace FileConverter3D.Core.Common
{
    /// <summary>
    /// Reads string lines from a file stream, and returns them trimmed.
    /// </summary>
    public class TextLineReader : IDataReader<string>
    {
        public string StateInfo => _lineNumber == 0 ? string.Empty : $"At line number {_lineNumber}, content: '{_line}'";

        protected int _lineNumber;
        protected string _line;

        public virtual IEnumerable<string> Read(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                while ((_line = reader.ReadLine()) != null)
                {
                    _lineNumber++;
                    if (!string.IsNullOrWhiteSpace(_line))
                        yield return _line.Trim();
                }
            }
        }
    }
}
