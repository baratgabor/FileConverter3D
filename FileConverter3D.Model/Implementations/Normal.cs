using System;
using System.Runtime.InteropServices;

namespace FileConverter3D
{
    /// <summary>
    /// Represent a normal in a 3D mesh.
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Normal : IValue, IVector3
    {
        public float X { get; }

        public float Y { get; }

        public float Z { get; }

        public Normal(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public void Accept(IValueVisitor visitor)
            => visitor.Visit(this);
    }
}
