using System.Collections.Generic;
using System.Linq;

namespace FileConverter3D
{
    /// <summary>
    /// Checks if a point is inside the specified 3D mesh.
    /// Assumes a closed mesh.
    /// </summary>
    class PointIsInsideMesh : IModelCalculation<bool>
    {
        private const double EPSILON = 0.0000001;
        private readonly ITriangulator _triangulator;
        private readonly Vector3 _point;

        // TODO: Consider using List, since num of elements should be low, so hashing overhead might make HashSet slower.
        private readonly HashSet<Vector3> _intersectionPoints = new HashSet<Vector3>();

        public PointIsInsideMesh(ITriangulator triangulator, Vector3 point)
        {
            _triangulator = triangulator;
            _point = point;
        }

        public bool Calculate(IModel model)
        {
            // Counts ray-triangle intersections with an arbitrary ray direction.
            // If intersection count is odd, the point is inside.
            // Can return incorrect value in certain edge cases (when the ray casted touches a single triangle).

            var intersectNum = 0;
            var rayDir = _point + new Vector3(0.1f, 0, 0);
            var vertexList = new List<Vertex>();

            //TODO: Slow implementation. Room for improvement with broad-phase prefiltering, e.g. AABB-based.
            foreach (var faceVertices in model.GetFaceVertices(model.Faces, vertexList))
                foreach (var tri in _triangulator.Triangulate(vertexList))
                    if (RayIntersectsTriangle(_point, rayDir, tri, out var intersectPoint))
                        // Count only new intersection points, since intersection point can touch the edge of two triangles, or a vertex of multiple triangles.
                        if (_intersectionPoints.Add(intersectPoint))
                            intersectNum++;

            _intersectionPoints.Clear();

            return intersectNum % 2 == 1;
        }

        // Algorithm: https://en.wikipedia.org/wiki/M%C3%B6ller%E2%80%93Trumbore_intersection_algorithm
        public bool RayIntersectsTriangle(
            Vector3 rayOrigin,
            Vector3 rayVector,
            (Vertex a, Vertex b, Vertex c) triangle,
            out Vector3 intersectPoint)
        {
            Vertex edge1 = triangle.b - triangle.a;
            Vertex edge2 = triangle.c - triangle.a;
            double a, f, u, v;
            var h = rayVector.Cross(edge2);
            a = edge1.Dot(h);
            if (a > -EPSILON && a < EPSILON)
            {
                intersectPoint = Vector3.Zero;
                return false;    // This ray is parallel to this triangle.
            }
            f = 1.0 / a;
            var s = rayOrigin - triangle.a;
            u = f * (s.Dot(h));
            if (u < 0.0 || u > 1.0)
            {
                intersectPoint = Vector3.Zero;
                return false;
            }
            var q = s.Cross(edge1);
            v = f * rayVector.Dot(q);
            if (v < 0.0 || u + v > 1.0)
            {
                intersectPoint = Vector3.Zero;
                return false;
            }
            // At this stage we can compute t to find out where the intersection point is on the line.
            double t = f * edge2.Dot(q);
            if (t > EPSILON) // ray intersection
            {
                intersectPoint = rayOrigin + (rayVector * (float)t);
                return true;
            }
            else // This means that there is a line intersection but not a ray intersection.
            {
                intersectPoint = Vector3.Zero;
                return false;
            }
        }
    }
}
