using System;

namespace FileConverter3D
{
    // Implementation based on: https://github.com/BSVino/MathForGameDevelopers/blob/matrix-trs/math/matrix.cpp
    // Euler rotation based on: https://www.learnopencv.com/rotation-matrix-to-euler-angles/
    class Matrix4x4 : IMatrix
    {
        protected const float M_PI = 3.14159265358979323846f;
        protected internal float[,] m = new float[4,4];

        public virtual void SetTranslation(IVector3 vecPos)
        {
	        m[3,0] = vecPos.X;
	        m[3,1] = vecPos.Y;
	        m[3,2] = vecPos.Z;
        }

        public virtual void SetRotation(IVector3 eulers)
        {
            var rX = new Matrix4x4()
            {
                m = new float[,]{
                       { 1, 0, 0 },
                       { 0, (float)Math.Cos(eulers.X), (float)-Math.Sin(eulers.X) },
                       { 0, (float)Math.Sin(eulers.X), (float)Math.Cos(eulers.X) }
                }
            };

            var rY = new Matrix4x4()
            {
                m = new float[,]{
                       { (float)Math.Cos(eulers.Y), 0, (float)Math.Sin(eulers.Y) },
                       { 0, 1, 0 },
                       { (float)-Math.Sin(eulers.Y), 0, (float)Math.Cos(eulers.Y) }
                }
            };

            var rZ = new Matrix4x4()
            {
                m = new float[,]{
                       { (float)Math.Cos(eulers.Z), (float)-Math.Sin(eulers.Z), 0 },
                       { (float)Math.Sin(eulers.Z), (float)Math.Cos(eulers.Z), 0 },
                       { 0, 0, 1 }
                }
            };

            // Replace inner matrix state with combined rotation matrix state
            m = (rX * rY * rZ).m;
        }

        public virtual void SetScale(IVector3 vecScale)
        {
            m[0,0] = vecScale.X;
            m[1,1] = vecScale.Y;
            m[2,2] = vecScale.Z;
        }

        public static Matrix4x4 operator*(Matrix4x4 f, Matrix4x4 s)
        {
            Matrix4x4 r = new Matrix4x4();

	        for (int i = 0; i< 4; i++)
		        for (int j = 0; j< 4; j++)
			        r.m[i,j] = f.m[0,j]*s.m[i,0] + f.m[1,j]*s.m[i,1] + f.m[2,j]*s.m[i,2] + f.m[3,j]*s.m[i,3];

	        return r;
        }

        public static Vector3 operator *(Matrix4x4 m, IVector3 v)
            => new Vector3(
                x: m.m[0, 0] * v.X + m.m[1, 0] * v.Y + m.m[2, 0] * v.Z + m.m[3, 0],
                y: m.m[0, 1] * v.X + m.m[1, 1] * v.Y + m.m[2, 1] * v.Z + m.m[3, 1],
                z: m.m[0, 2] * v.X + m.m[1, 2] * v.Y + m.m[2, 2] * v.Z + m.m[3, 2]
            );
    }
}
