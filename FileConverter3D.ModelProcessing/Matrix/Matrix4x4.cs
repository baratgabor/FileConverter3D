using System;

namespace FileConverter3D
{
    // Core implementation based on: https://github.com/BSVino/MathForGameDevelopers/blob/matrix-trs/math/matrix.cpp
    // Euler rotation based on: https://www.learnopencv.com/rotation-matrix-to-euler-angles/
    // Matrix inversion based on: https://www.scratchapixel.com/code.php?id=22&origin=/lessons/mathematics-physics-for-computer-graphics/geometry
    public class Matrix4x4 : ITransformMatrix
    {
        protected internal float[,] m = new float[4,4];

        public Matrix4x4()
        {
            // Set to identity
            m[0, 0] = 1;
            m[1, 1] = 1;
            m[2, 2] = 1;
            m[3, 3] = 1;
        }

        public virtual void SetTranslation(IVector3 vecPos)
        {
	        m[3,0] = vecPos.X;
	        m[3,1] = vecPos.Y;
	        m[3,2] = vecPos.Z;
        }

        public virtual void SetRotation(IVector3 eulerDegs)
        {
            // Convert degrees to radians
            var rads = new Vector3(
                x: eulerDegs.X * (float)(Math.PI / 180), 
                y: eulerDegs.Y * (float)(Math.PI / 180), 
                z: eulerDegs.Z * (float)(Math.PI / 180)
            );

            var rX = new Matrix4x4()
            {
                m = new float[,]{
                       { 1, 0, 0, 0 },
                       { 0, (float)Math.Cos(rads.X), (float)-Math.Sin(rads.X), 0 },
                       { 0, (float)Math.Sin(rads.X), (float)Math.Cos(rads.X), 0 },
                       {0, 0, 0, 1 }
                }
            };

            var rY = new Matrix4x4()
            {
                m = new float[,]{
                       { (float)Math.Cos(rads.Y), 0, (float)Math.Sin(rads.Y), 0 },
                       { 0, 1, 0, 0 },
                       { (float)-Math.Sin(rads.Y), 0, (float)Math.Cos(rads.Y), 0 },
                       { 0, 0, 0, 1 }
                }
            };

            var rZ = new Matrix4x4()
            {
                m = new float[,]{
                       { (float)Math.Cos(rads.Z), (float)-Math.Sin(rads.Z), 0, 0 },
                       { (float)Math.Sin(rads.Z), (float)Math.Cos(rads.Z), 0, 0 },
                       { 0, 0, 1, 0 },
                       { 0, 0, 0, 1 }
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


        public ITransformMatrix Invert()
        {
            int i, j, k;
            var s = new Matrix4x4();
            var t = new Matrix4x4 { m = m.Clone() as float[,] };

            // Forward elimination
            for (i = 0; i < 3; i++)
            {
                int pivot = i;

                var pivotsize = t.m[i,i];

                if (pivotsize < 0)
                    pivotsize = -pivotsize;

                for (j = i + 1; j < 4; j++)
                {
                    var tmp = t.m[j,i];

                    if (tmp < 0)
                        tmp = -tmp;

                    if (tmp > pivotsize)
                    {
                        pivot = j;
                        pivotsize = tmp;
                    }
                }

                if (pivotsize == 0)
                {
                    // Cannot invert singular matrix
                    return new Matrix4x4();
                }

                if (pivot != i)
                {
                    for (j = 0; j < 4; j++)
                    {
                        var tmp = t.m[i, j];
                        t.m[i,j] = t.m[pivot, j];
                        t.m[pivot, j] = tmp;

                        tmp = s.m[i, j];
                        s.m[i, j] = s.m[pivot, j];
                        s.m[pivot, j] = tmp;
                    }
                }

                for (j = i + 1; j < 4; j++)
                {
                    var f = t.m[j,i] / t.m[i,i];

                    for (k = 0; k < 4; k++)
                    {
                        t.m[j,k] -= f * t.m[i,k];
                        s.m[j,k] -= f * s.m[i,k];
                    }
                }
            }

            // Backward substitution
            for (i = 3; i >= 0; --i)
            {
                float f;

                if ((f = t.m[i,i]) == 0)
                {
                    // Cannot invert singular matrix
                    return new Matrix4x4();
                }

                for (j = 0; j < 4; j++)
                {
                    t.m[i, j] /= f;
                    s.m[i, j] /= f;
                }

                for (j = 0; j < i; j++)
                {
                    f = t.m[j, i];

                    for (k = 0; k < 4; k++)
                    {
                        t.m[j, k] -= f * t.m[i, k];
                        s.m[j, k] -= f * s.m[i, k];
                    }
                }
            }

            return s;
        }

        public ITransformMatrix Transpose()
        {
            int h = 4, w = 4;
            Matrix4x4 result = new Matrix4x4();

            for (int i = 0; i < w; i++)
                for (int j = 0; j < h; j++)
                    result.m[j, i] = m[i, j];

            return result;
        }

        public ITransformMatrix Mul(ITransformMatrix otherMatrix)
        {
            if (!(otherMatrix is Matrix4x4 other4x4))
                throw new ArgumentException($"Multiplication is only supported with matching type of '{nameof(Matrix4x4)}'.");

            return this * other4x4;
        }

        public Vector3 Mul(IVector3 vector)
            => this * vector;
    }
}
