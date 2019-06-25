using System.Collections.Generic;

namespace FileConverter3D
{
    public class Face : IValue
    {
        public List<(int v, int? vt, int? vn)> FaceVertices;

        public Face()
        {}

        public Face(params (int v, int? vt, int? vn)[] vertices)
        {
            FaceVertices = new List<(int v, int? vt, int? vn)>(vertices);
        }

        public void AddFaceVertex((int v, int? vt, int? vn) vertex)
        {
            if (FaceVertices == null)
                FaceVertices = new List<(int v, int? vt, int? vn)>();

            FaceVertices.Add(vertex);
        }

        public void Accept(IValueVisitor visitor)
            => visitor.Visit(this);

        public override bool Equals(object obj)
        {
            if (!(obj is Face other))
                return false;

            if (FaceVertices.Count != other.FaceVertices.Count)
                return false;

            for (int i = 0; i < FaceVertices.Count; i++)
                if (FaceVertices[i] != other.FaceVertices[i])
                    return false;

            return true;
        }
    }
}
