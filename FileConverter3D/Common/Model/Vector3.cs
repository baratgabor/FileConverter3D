using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FileConverter3D
{
    public struct Vector3
    {
        public static Vector3 Zero => new Vector3(0, 0, 0);

        public readonly float X;
        public readonly float Y;
        public readonly float Z;

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static bool TryParse(string vectorString, out Vector3 vector3)
        {
            if (string.IsNullOrWhiteSpace(vectorString))
                goto Fail;

            var mg = Regex.Match(vectorString, @"(-?\d+(?:\.\d+)?)[ ,\,,\;](-?\d+(?:\.\d+)?)[ ,\,,\;](-?\d+(?:\.\d+)?)").Groups;

            var allParsed =
                float.TryParse(mg[1].Value, out var x) &
                float.TryParse(mg[2].Value, out var y) &
                float.TryParse(mg[3].Value, out var z);

            if (!allParsed)
                goto Fail;

            vector3 = new Vector3(x, y, z);
            return true;

            Fail:
            vector3 = Zero;
            return false;
        }
    }
}
