using System;
using System.Collections.Generic;
using System.Globalization;

namespace FileConverter3D.Core
{
    //TODO: Heavy use of Split(); implementations are too allocation heavy.
    public abstract class ObjAsciiParserBase : IValueParser<string>
    {
        private readonly char[] SplitSpace = new[] { ' ' };
        private readonly char[] SplitSlash = new[] { '/' };

        private ArgumentException TooFewElements => new ArgumentException("Insufficient number of elements while parsing vector.");
        private ArgumentException InvalidSig => new ArgumentException("Invalid signature in parsable string.");
        private ArgumentException FloatParseFail => new ArgumentException("One or more values in string line cannot be parsed to float.");

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
            var segments = parsable.Split(SplitSpace, StringSplitOptions.RemoveEmptyEntries);

            if (segments.Length < 4)
                throw TooFewElements;

            if (!string.Equals(segments[0], signatureCheck, StringComparison.OrdinalIgnoreCase))
                throw InvalidSig;

            var parseSuccess =
                float.TryParse(segments[1], NumberStyles.Float, CultureInfo.InvariantCulture, out var x)
                & float.TryParse(segments[2], NumberStyles.Float, CultureInfo.InvariantCulture, out var y)
                & float.TryParse(segments[3], NumberStyles.Float, CultureInfo.InvariantCulture, out var z);

            if (!parseSuccess)
                throw FloatParseFail;

            return new Vector3(x, y, z);
        }

        protected Vector2 ParseVector2Line(string parsable, string signatureCheck)
        {
            var segments = parsable.Split(SplitSpace, StringSplitOptions.RemoveEmptyEntries);

            if (segments.Length < 3)
                throw TooFewElements;

            if (!string.Equals(segments[0], signatureCheck, StringComparison.OrdinalIgnoreCase))
                throw InvalidSig;

            var parseSuccess =
                float.TryParse(segments[1], NumberStyles.Float, CultureInfo.InvariantCulture, out var x)
                & float.TryParse(segments[2], NumberStyles.Float, CultureInfo.InvariantCulture, out var y);

            if (!parseSuccess)
                throw FloatParseFail;

            return new Vector2(x, y);
        }

        protected Face ParseFaceLine(string parsable, string signatureCheck)
        {
            var segments = parsable.Split(SplitSpace, StringSplitOptions.RemoveEmptyEntries);

            if (!string.Equals(segments[0], signatureCheck, StringComparison.OrdinalIgnoreCase))
                throw InvalidSig;

            if (segments.Length < 4)
                throw new ArgumentException($"Error parsing face: Insufficient number of vertices. A valid face string must consist of minimum 3 vertex segments.");

            var face = new Face();
            for (int i = 1; i < segments.Length; i++)
            {
                if (!TryParseFaceVertex(segments[i], out var faceVertex))
                    throw new ArgumentException($"Error parsing face: Cannot parse face vertex #{i}.");

                face.AddFaceVertex(faceVertex);
            }

            return face;
        }

        private bool TryParseFaceVertex(string vertexString, out FaceVertex faceVertex)
        {
            faceVertex = default;
            var segments = vertexString.Split(SplitSlash, StringSplitOptions.None);

            int vi = ParseOne(segments[0], out var success);
            if (!success) return false;

            int? tci = null;
            if (segments.Length > 1 && segments[1].Length > 0)
            {
                tci = ParseOne(segments[1], out success);
                if (!success) return false;
            }

            int? ni = null;
            if (segments.Length > 2 && segments[2].Length > 0)
            {
                ni = ParseOne(segments[2], out success);
                if (!success) return false;
            }

            // -1 converts indexes to 0 based instead of 1 based
            faceVertex = new FaceVertex(vi - 1, tci - 1, ni - 1);
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
