using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FileConverter3D.StlBinary
{
    /// <summary>
    /// Extracts model data to create STL triangles.
    /// </summary>
    public class TriangleExtractor : IModelReader<StlTriangle>
    {
        protected ITriangulator _triangulator;

        public TriangleExtractor(ITriangulator triangulator)
            => _triangulator = triangulator;

        public IEnumerable<StlTriangle> Read(IModel model)
        {
            var vertexList = new List<Vertex>();
            foreach (var faceVertices in model.GetFaceVertices(model.Faces, vertexList))
                foreach (var (a, b, c) in _triangulator.Triangulate(faceVertices))
                    yield return new StlTriangle(
                        normal: CalculateFaceNormal(a, b, c),
                        a: a,
                        b: b,
                        c: c
                    );
        }

        /// <summary>
        /// Calculates a simple face normal that is orthogonal to the face.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected Vertex CalculateFaceNormal(Vertex a, Vertex b, Vertex c)
        {
            var u = b - a;
            var v = c - a;

            return u.Cross(v);
        }
    }
}