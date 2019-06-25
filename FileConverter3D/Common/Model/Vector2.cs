using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FileConverter3D
{
    public struct Vector2
    {
        public static Vector2 Zero => new Vector2(0, 0);

        public float X { get; private set; }
        public float Y { get; private set; }

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static bool TryParse(string vectorString, out Vector2 vector2)
        {
            if (string.IsNullOrWhiteSpace(vectorString))
                goto Fail;

            var mg = Regex.Match(vectorString, @"(-?\d+(?:\.\d+)?)[ ,\,,\;](-?\d+(?:\.\d+)?)").Groups;

            var allParsed =
                float.TryParse(mg[1].Value, out var x) &
                float.TryParse(mg[2].Value, out var y);

            if (!allParsed)
                goto Fail;

            vector2 = new Vector2(x, y);
            return true;

        Fail:
            vector2 = Vector2.Zero;
            return false;
        }
    }
}
