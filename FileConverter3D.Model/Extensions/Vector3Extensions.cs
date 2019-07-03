using System;

namespace FileConverter3D
{
    public static class Vector3Extensions
    {
        public static Vector3 Cross(this IVector3 first, IVector3 second)
            => new Vector3(
                x: (first.Y * second.Z) - (first.Z * second.Y),
                y: (first.Z * second.X) - (first.X * second.Z),
                z: (first.X * second.Y) - (first.Y * second.X)
            );

        public static float Dot(this IVector3 first, IVector3 second)
            => (first.X * second.X) + (first.Y * second.Y) + (first.Z * second.Z);

        public static float Magnitude(this IVector3 vector)
            => (float)Math.Sqrt(MagnitudeSquared(vector));

        public static float MagnitudeSquared(this IVector3 vector)
            => vector.X * vector.X + vector.Y * vector.Y + vector.Z * vector.Z;

        public static Vector3 Normalize(this IVector3 vector)
            => vector.Div(vector.Magnitude());

        public static Vector3 Div(this IVector3 vector, float scalar)
            => new Vector3(vector.X / scalar, vector.Y / scalar, vector.Z / scalar);

        public static Vector3 Mul(this IVector3 vector, float scalar)
            => new Vector3(vector.X * scalar, vector.Y * scalar, vector.Z * scalar);
    }
}
