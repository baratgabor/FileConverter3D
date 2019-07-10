using System;

namespace FileConverter3D
{
    /// <summary>
    /// Represents a face vertex in a face.
    /// </summary>
    public readonly struct FaceVertex : IEquatable<FaceVertex>
    {
        //TODO: Consider binding textcoord and normal to vertices instead. Though, it seems technically in e.g. OBJ different faces CAN assign different normals to the same shared vertex.
        public int VertexIndex { get; }
        public int? TextureCoordIndex { get; }
        public int? NormalIndex { get; }

        public FaceVertex(int vertexIndex, int? textureCoordIndex = null, int? normalIndex = null)
        {
            VertexIndex = vertexIndex;
            TextureCoordIndex = textureCoordIndex;
            NormalIndex = normalIndex;
        }

        public static bool operator ==(FaceVertex first, FaceVertex second)
            => 
            first.VertexIndex == second.VertexIndex &&
            first.TextureCoordIndex == second.TextureCoordIndex &&
            first.NormalIndex == second.NormalIndex;

        public static bool operator !=(FaceVertex first, FaceVertex second)
            => !(first == second);

        public override bool Equals(object obj)
            => obj is FaceVertex other && this == other;

        public bool Equals(FaceVertex other)
           => this == other;

        public override int GetHashCode()
        {
            int hash = 23;
            hash = hash * 31 + VertexIndex;
            hash = hash * 31 + (TextureCoordIndex ?? -1);
            hash = hash * 31 + (NormalIndex ?? -2);

            return hash;
        }
    }
}
