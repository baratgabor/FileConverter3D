using System.Collections.Generic;

namespace FileConverter3D
{
    public interface INormalSource
    {
        IList<Normal> Normals { get; }
    }
}
