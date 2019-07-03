using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FileConverter3D
{
    public class ModelSurfaceAreaCalculator : IModelCalculation<float>
    {
        protected ITriangulator _triangulator;

        public ModelSurfaceAreaCalculator(ITriangulator triangulator)
            => _triangulator = triangulator;

        /// <summary>
        /// Calculates the area of the specified model.
        /// Assumes flat convex faces and clockwise or counter-clockwise sorted vertices.
        /// </summary>
        public float Calculate(IModel model)
        {
            var surface = 0f;
            var vertexList = new List<Vertex>();

            foreach (var faceVertices in model.GetFaceVertices(model.Faces, vertexList))
                foreach (var tri in _triangulator.Triangulate(faceVertices))
                    surface += GetTriArea(tri);

            return surface;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected float GetTriArea((Vertex A, Vertex B, Vertex C) tri)
        {
            var u = tri.B - tri.A;
            var v = tri.C - tri.A;

            return 0.5f * u.Cross(v).Magnitude();
        }
    }
}
