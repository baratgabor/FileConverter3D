using System.Collections.Generic;
using System.IO;

namespace FileConverter3D
{
    public interface IValueReader<TDataUnit>
    {
        IEnumerable<TDataUnit> Read(Stream stream);
    }
}
