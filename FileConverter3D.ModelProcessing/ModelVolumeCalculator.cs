using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace FileConverter3D
{
    class ModelVolumeCalculator : IModelCalculation<float>
    {
        ITriangulator _triangulator;

        public ModelVolumeCalculator(ITriangulator triangulator)
            => _triangulator = triangulator;

        public float Calculate(IModel model)
        {
            var vol = 0f;
            var vertexList = new List<Vertex>();

            foreach (var vertices in model.GetFaceVertices(model.Faces, vertexList))
                foreach (var tri in _triangulator.Triangulate(vertices))
                    vol += SignedVolumeOfTriangle(tri);

            return Math.Abs(vol);
        }

        // Source: https://stackoverflow.com/a/1568551/2906385
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float SignedVolumeOfTriangle((Vertex a, Vertex b, Vertex c) tri)
        {
            var v321 = tri.c.X * tri.b.Y * tri.a.Z;
            var v231 = tri.b.X * tri.c.Y * tri.a.Z;
            var v312 = tri.c.X * tri.a.Y * tri.b.Z;
            var v132 = tri.a.X * tri.c.Y * tri.b.Z;
            var v213 = tri.b.X * tri.a.Y * tri.c.Z;
            var v123 = tri.a.X * tri.b.Y * tri.c.Z;
            return (1.0f / 6.0f) * (-v321 + v231 + v312 - v132 - v213 + v123);
        }
    }
}
