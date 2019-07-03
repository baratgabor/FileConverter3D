using System.Collections.Generic;
using System.IO;

namespace FileConverter3D.ObjAscii
{
    /// <summary>
    /// Filters out not supported lines early to improve parsing performance.
    /// Requires manual update if support is implemented for additional line types.
    /// </summary>
    public class LineFilterProxy : IDataReader<string>
    {
        protected IDataReader<string> _proxied;

        public LineFilterProxy(IDataReader<string> proxied)
            => _proxied = proxied;

        public IEnumerable<string> Read(Stream stream)
        {
            // Ignore all lines that don't start with 'v' or 'f'
            foreach (var line in _proxied.Read(stream))
                if (line[0] == 'v' || line[0] == 'f')
                    yield return line;
        }
    }
}
