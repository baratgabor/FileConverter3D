using System.Collections.Generic;

namespace FileConverter3D
{
    interface ITriangulator
    {
        IEnumerable<(FaceVertex a, FaceVertex b, FaceVertex c)> Triangulate(ICollection<FaceVertex> faceVertices);
        IEnumerable<(Vertex a, Vertex b, Vertex c)> Triangulate(ICollection<Vertex> faceVertices);
    }
}