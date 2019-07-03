using System;

namespace FileConverter3D
{
    // Implementation based on: https://github.com/BSVino/MathForGameDevelopers/blob/matrix-trs/math/matrix.cpp
    class Matrix4x4
    {
        protected const float M_PI = 3.14159265358979323846f;
        protected float[,] m = new float[4,4];

        public void SetTranslation(IVector3 vecPos)
        {
	        m[3,0] = vecPos.X;
	        m[3,1] = vecPos.Y;
	        m[3,2] = vecPos.Z;
        }

        public void SetRotation(float flAngle, IVector3 v)
        {
            float x = v.X;
            float y = v.Y;
            float z = v.Z;

            float c = (float)Math.Cos(flAngle * M_PI / 180f);
            float s = (float)Math.Sin(flAngle * M_PI / 180f);
            float t = 1 - c;

            m[0,0] = x * x * t + c;
            m[1,0] = x * y * t - z * s;
            m[2,0] = x * z * t + y * s;

            m[0,1] = y * x * t + z * s;
            m[1,1] = y * y * t + c;
            m[2,1] = y * z * t - x * s;

            m[0,2] = z * x * t - y * s;
            m[1,2] = z * y * t + x * s;
            m[2,2] = z * z * t + c;
        }

        public void SetScale(IVector3 vecScale)
        {
            m[0,0] = vecScale.X;
            m[1,1] = vecScale.Y;
            m[2,2] = vecScale.Z;
        }

        public Matrix4x4 AddTranslation(IVector3 v)
        {
            var t = new Matrix4x4();
            t.SetTranslation(v);
            var r = new Matrix4x4();
            r = this * t;

	        return r;
        }

        public Matrix4x4 AddScale(IVector3 vecScale)
        {
            var s = new Matrix4x4();
            s.SetScale(vecScale);
            var r = new Matrix4x4();
	        r = this * s;

	        return r;
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
