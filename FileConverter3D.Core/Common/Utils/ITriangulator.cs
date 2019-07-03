using System.Collections.Generic;

namespace FileConverter3D
{
    public interface ITriangulator
    {
        IEnumerable<(FaceVertex a, FaceVertex b, FaceVertex c)> Triangulate(ICollection<FaceVertex> faceVertices);
        IEnumerable<(Vertex a, Vertex b, Vertex c)> Triangulate(ICollection<Vertex> faceVertices);
    }
}