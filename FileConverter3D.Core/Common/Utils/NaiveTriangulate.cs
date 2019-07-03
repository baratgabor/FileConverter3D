using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace FileConverter3D.Common
{
    /// <summary>
    /// Naive triangulation strategy.
    /// </summary>
    public class NaiveTriangulate : ITriangulator
    {
        /// <summary>
        /// Triangulates super-3 vertex faces the simplest way possible. 3 vertex faces are returned unchanged.
        /// Assumes that faces are convex and vertices are ordered counter-clockwise (or clockwise).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<(FaceVertex a, FaceVertex b, FaceVertex c)> Triangulate(ICollection<FaceVertex> faceVertices)
            => Triangulate<FaceVertex>(faceVertices);

        /// <summary>
        /// Triangulates super-3 vertex faces the simplest way possible. 3 vertex faces are returned unchanged.
        /// Assumes that faces are convex and vertices are ordered counter-clockwise (or clockwise).
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<(Vertex a, Vertex b, Vertex c)> Triangulate(ICollection<Vertex> faceVertices)
            => Triangulate<Vertex>(faceVertices);

        /// <summary>
        /// Internal type-agnostic triangulation logic.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected IEnumerable<(T a, T b, T c)> Triangulate<T>(ICollection<T> faceVertices)
        {
            var vertCount = faceVertices.Count;

            if (vertCount < 3)
                throw new ArgumentException($"Invalid face with vertex count of {vertCount}. A valid face must contain at least 3 vertices.");

            // Sliding window of three, with increment of two
            for (int i = 0; i < vertCount - 1; i += 2)
            {
                yield return (
                    a: faceVertices.ElementAt(i),
                    b: faceVertices.ElementAt(i + 1),

                    // Notice modulo: Index C overflows to index 0 at last step
                    c: faceVertices.ElementAt((i + 2) % vertCount)
                );
            }
        }
    }
}