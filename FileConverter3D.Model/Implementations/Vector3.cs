using System.Text.RegularExpressions;

namespace FileConverter3D
{
    /// <summary>
    /// Generalized 3D vector data structure. Implicitly converts into specialized equivalent types used in Model.
    /// </summary>
    public struct Vector3 : IVector3
    {
        public static Vector3 Zero => new Vector3(0, 0, 0);

        public float X { get; }
        public float Y { get; }
        public float Z { get; }

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static implicit operator Vertex(Vector3 v)
            => new Vertex(v.X, v.Y, v.Z);

        public static implicit operator Normal(Vector3 v)
            => new Normal(v.X, v.Y, v.Z);

        public static Vector3 operator -(Vector3 first, Vector3 second)
            => new Vector3(first.X - second.X, first.Y - second.Y, first.Z - second.Z);

        public static Vector3 operator +(Vector3 first, Vector3 second)
            => new Vector3(first.X + second.X, first.Y + second.Y, first.Z + second.Z);

        public static Vector3 operator *(Vector3 first, float scalar)
            => new Vector3(first.X * scalar, first.Y * scalar, first.Z * scalar);
    }
}
