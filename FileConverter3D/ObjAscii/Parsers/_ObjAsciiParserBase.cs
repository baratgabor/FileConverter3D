using System;
using System.Collections.Generic;
using System.Globalization;

namespace FileConverter3D
{
    abstract class ObjAsciiParserBase : IValueParser<string>
    {
        public abstract string DataSignature { get; }

        /// <summary>
        /// Fast can parse check. Checks only if line signature is correct.
        /// </summary>
        public bool CanParse(string parsable)
            => !string.IsNullOrEmpty(parsable) 
            && parsable.StartsWith(DataSignature, StringComparison.OrdinalIgnoreCase) 
            && parsable[DataSignature.Length] == ' ';

        public abstract IValue Parse(string parsable);

        public IEnumerable<IValue> Parse(IEnumerable<string> parsables)
        {
            foreach (var p in parsables)
                yield return Parse(p);
        }

        protected Vector3 ParseVector3Line(string parsable, string signatureCheck)
        {
            var segments = parsable.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (segments.Length < 4)
                throw new ArgumentException("Insufficient number of elements.");

            if (!string.Equals(segments[0], signatureCheck, StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("Invalid signature in parsable string line.");

            var parseSuccess =
                float.TryParse(segments[1], NumberStyles.Float, CultureInfo.InvariantCulture, out var x)
                & float.TryParse(segments[2], NumberStyles.Float, CultureInfo.InvariantCulture, out var y)
                & float.TryParse(segments[3], NumberStyles.Float, CultureInfo.InvariantCulture, out var z);

            if (!parseSuccess)
                throw new ArgumentException("One or more values in string line cannot be parsed to float.");

            return new Vector3(x, y, z);
        }

        protected Vector2 ParseVector2Line(string parsable, string signatureCheck)
        {
            var segments = parsable.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (segments.Length < 3)
                throw new ArgumentException("Insufficient number of elements.");

            if (!string.Equals(segments[0], signatureCheck, StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("Invalid signature in parsable string line.");

            var parseSuccess =
                float.TryParse(segments[1], NumberStyles.Float, CultureInfo.InvariantCulture, out var x)
                & float.TryParse(segments[2], NumberStyles.Float, CultureInfo.InvariantCulture, out var y);

            if (!parseSuccess)
                throw new ArgumentException("One or more values in string line cannot be parsed to float.");

            return new Vector2(x, y);
        }

        protected Face ParseFaceLine(string parsable, string signatureCheck)
        {
            var segments = parsable.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (!string.Equals(segments[0], signatureCheck, StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("Invalid signature in parsable string line.");

            if (segments.Length < 4)
                throw new ArgumentException($"Insufficient number of vertices. A valid face string must consist of minimum 3 vertex segments.");

            var face = new Face();
            for (int i = 1; i < segments.Length; i++)
            {
                if (!TryParseFaceVertex(segments[i], out var faceVertex))
                    throw new ArgumentException($"Cannot parse face vertex {i}.");

                face.AddFaceVertex(faceVertex);
            }

            return face;
        }

        private bool TryParseFaceVertex(string vertexString, out FaceVertex faceVertex)
        {
            faceVertex = new FaceVertex(0, null, null);
            var segments = vertexString.Split(new[] { '/' }, StringSplitOptions.None);

            faceVertex.VertexIndex = ParseOne(segments[0], out var success);
            if (!success) return false;

            if (segments.Length > 1 && segments[1].Length > 0)
            {
                faceVertex.TextureCoordIndex = ParseOne(segments[1], out success);
                if (!success) return false;
            }

            if (segments.Length > 2 && segments[2].Length > 0)
            {
                faceVertex.NormalIndex = ParseOne(segments[2], out success);
                if (!success) return false;
            }

            return true;

            int ParseOne(string segment, out bool parseSuccess)
            {
                if (int.TryParse(segment, NumberStyles.Float, CultureInfo.InvariantCulture, out var parsedInt))
                {
                    parseSuccess = true;
                    return parsedInt;
                }
                else
                {
                    parseSuccess = false;
                    return default;
                }
            }
        }
    }
}
