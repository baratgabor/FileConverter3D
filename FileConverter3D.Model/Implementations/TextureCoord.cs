using System;
using System.Runtime.InteropServices;

namespace FileConverter3D
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct TextureCoord : IValue, IVector2
    {
        public float X { get; }

        public float Y { get; }

        public TextureCoord(float x, float y)
        {
            X = x;
            Y = y;
        }

        public void Accept(IValueVisitor visitor)
            => visitor.Visit(this);
    }
}
