using System.Collections.Generic;

namespace FileConverter3D
{
    public interface IVertexSource
    {
        IList<Vertex> Vertices { get; }
    }
}
