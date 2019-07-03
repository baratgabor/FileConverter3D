using System;
using System.Runtime.InteropServices;

namespace FileConverter3D
{
    /// <summary>
    /// Represent a vertex in a 3D mesh.
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Vertex : IValue, IVector3
    {
        public float X { get; }

        public float Y { get; }

        public float Z { get; }

        public Vertex(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public void Accept(IValueVisitor visitor)
            => visitor.Visit(this);

        public static Vertex operator -(Vertex first, Vertex second) 
            => new Vertex(first.X - second.X, first.Y - second.Y, first.Z - second.Z);

        public static Vertex operator +(Vertex first, Vertex second)
            => new Vertex(first.X + second.X, first.Y + second.Y, first.Z + second.Z);
    }
}
