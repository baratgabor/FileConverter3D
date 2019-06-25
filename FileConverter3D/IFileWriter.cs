using System.Collections.Generic;
using System.IO;

namespace FileConverter3D
{
    interface IFileWriter<T>
    {
        void Write(IEnumerable<T> data, Stream destination);
    }
}
