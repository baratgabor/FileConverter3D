using System.Collections.Generic;

namespace FileConverter3D
{
    /// <summary>
    /// Represents a single face in a mesh.
    /// </summary>
    public class Face : IValue
    {
        public List<FaceVertex> Vertices = new List<FaceVertex>();
        public bool IsTriangle => Vertices.Count == 3;
        public bool IsQuad => Vertices.Count == 4;

        public Face() { }

        public Face(params FaceVertex[] faceVertices)
            => Vertices.AddRange(faceVertices);

        public void AddFaceVertex(FaceVertex vertex)
            => Vertices.Add(vertex);

        public void Accept(IValueVisitor visitor)
            => visitor.Visit(this);

        public override bool Equals(object obj)
        {
            if (!(obj is Face other))
                return false;

            if (Vertices.Count != other.Vertices.Count)
                return false;

            for (int i = 0; i < Vertices.Count; i++)
                if (Vertices[i] != other.Vertices[i])
                    return false;

            return true;
        }
    }
}
