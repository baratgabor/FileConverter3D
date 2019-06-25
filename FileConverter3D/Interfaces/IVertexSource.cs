using System.Collections.Generic;

namespace FileConverter3D
{
    public interface IVertexSource
    {
        IEnumerable<Vertex> Vertices { get; }
    }
}
