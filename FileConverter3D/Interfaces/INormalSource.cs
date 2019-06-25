using System.Collections.Generic;

namespace FileConverter3D
{
    public interface INormalSource
    {
        IEnumerable<Normal> Normals { get; }
    }
}
