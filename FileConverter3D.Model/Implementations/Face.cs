using System.Collections.Generic;
using System.Linq;

namespace FileConverter3D
{
    /// <summary>
    /// Represents a face in a 3D mesh.
    /// </summary>
    public class Face : IValue
    {
        public IList<FaceVertex> Vertices => _vertices;
        public bool IsTriangle => Vertices.Count == 3;
        public bool IsQuad => Vertices.Count == 4;

        protected List<FaceVertex> _vertices = new List<FaceVertex>();

        public Face() { }

        public Face(params FaceVertex[] faceVertices)
            => _vertices.AddRange(faceVertices);

        public void AddFaceVertex(FaceVertex vertex)
            => _vertices.Add(vertex);

        public void Accept(IValueVisitor visitor)
            => visitor.Visit(this);

        public override bool Equals(object obj)
        {
            if (!(obj is Face other))
                return false;

            if (_vertices.Count != other._vertices.Count)
                return false;

            for (int i = 0; i < _vertices.Count; i++)
                if (_vertices[i] != other._vertices[i])
                    return false;

            return true;
        }

        public override string ToString()
            => Vertices.Aggregate("", (str, next) => str += $"[V:{next.VertexIndex} VT:{next.TextureCoordIndex} VN:{next.NormalIndex}] ");
    }
}
