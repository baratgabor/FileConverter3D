using System;
using System.Collections.Generic;
using System.Linq;

namespace FileConverter3D.StlBinary
{
    /// <summary>
    /// Extracts model data to create STL triangles.
    /// </summary>
    class TriangleExtractor : Common.IModelReader<StlTriangle>
    {
        public IEnumerable<StlTriangle> Read(IModel model)
        {
            foreach (var face in model.Faces)
            {
                foreach (var (A, B, C) in NaiveTriangulate(face, model))
                {
                    yield return new StlTriangle(
                        normal: CalculateFaceNormal(A, B, C),
                        a: A,
                        b: B,
                        c: C
                    );
                }
            }
        }

        /// <summary>
        /// Triangulates super-3 vertex faces the simplest way possible. 3 vertex faces are returned unchanged.
        /// Assumes that faces are convex and flat.
        /// </summary>
        protected IEnumerable<(Vertex A, Vertex B, Vertex C)> NaiveTriangulate(Face face, IModel model)
        {
            var vertCount = face.FaceVertices.Count;

            if (vertCount < 3)
                throw new ArgumentException($"Invalid face with vertex count of {vertCount}. A valid face must contain at least 3 vertices.");

            // Sliding window of three, with increment of two
            for (int i = 0; i < vertCount - 1; i += 2)
            {
                //TODO: Single responsibility; this function does vertex lookup too besides the triangulation.
                yield return (
                    A: model.Vertices.ElementAt(face.FaceVertices[i].v - 1),
                    B: model.Vertices.ElementAt(face.FaceVertices[i + 1].v - 1),

                    // Notice modulo: Index C overflows to index 0 at last step
                    C: model.Vertices.ElementAt(face.FaceVertices[i + 2 % vertCount].v - 1)
                );
            }
        }

        /// <summary>
        /// Calculates a simple face normal that is orthogonal to the face.
        /// </summary>
        protected Vertex CalculateFaceNormal(Vertex a, Vertex b, Vertex c)
        {
            var u = new Vector3(b.x - a.x, b.y - a.y, b.z - a.z); // a - b
            var v = new Vector3(c.x - a.x, c.y - a.y, c.z - a.z); // c - a

            // Cross product
            return new Vertex(
                x: (u.Y * v.Z) - (u.Z * v.Y),
                y: (u.Z * v.X) - (u.X * v.Z),
                z: (u.X * v.Y) - (u.Y * v.X)
            );
        }
    }
}