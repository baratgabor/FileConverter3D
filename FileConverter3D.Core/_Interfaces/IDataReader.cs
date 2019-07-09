using System.Collections.Generic;
using System.IO;

namespace FileConverter3D.Core
{
    public interface IDataReader<TData> : IStateInfoProvider
    {
        IEnumerable<TData> Read(Stream stream);
    }
}