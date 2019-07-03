using System;
using System.Runtime.InteropServices;

namespace FileConverter3D.Core.StlBinary
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct StlTriangle : IValue
    {
        public Vertex Normal;
        public Vertex A;
        public Vertex B;
        public Vertex C;
        public ushort AttribByteCount;

        public StlTriangle(Vertex normal, Vertex a, Vertex b, Vertex c, ushort attribByteCount = default)
        {
            Normal = normal;
            A = a;
            B = b;
            C = c;
            AttribByteCount = attribByteCount;
        }

        public void Accept(IValueVisitor visitor)
        {
            visitor.Visit(Normal);
            visitor.Visit(A);
            visitor.Visit(B);
            visitor.Visit(C);
        }
    }
}