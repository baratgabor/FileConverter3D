using System.Collections.Generic;
using System.IO;

namespace FileConverter3D
{
    interface IDataReader<TData>
    {
        IEnumerable<TData> Read(Stream stream);
    }
}