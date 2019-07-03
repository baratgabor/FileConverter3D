using System;
using System.Collections.Generic;
using System.Text;

namespace FileConverter3D.ObjAscii
{
    public class CombinedValueSerializer : IValueSerializer<IValue, string>
    {
        private StringBuilder _sb = new StringBuilder();

        public bool CanSerialize(IValue value)
            => value is Vertex 
            || value is Normal 
            || value is TextureCoord 
            || value is Face;

        public string Serialize(IValue value)
        {
            // At this point I was tired of abstractions, so I fell back to a good old switch ;)
            // But it may be prudent to promote serialization of each type to class level, however simple they are.
            switch (value)
            {
                case Vertex v:
                    return $"v {v.X} {v.Y} {v.Z}";

                case Normal vn:
                    return $"vn {vn.X} {vn.Y} {vn.Z}";

                case TextureCoord vt:
                    return $"vt {vt.X} {vt.Y}";

                case Face f:
                    return $"f {BuildFaceVertList(f)}";

                default:
                    throw new InvalidOperationException($"Cannot serialize type '{value.GetType().Name}'.");
            }

            string BuildFaceVertList(Face f)
            {
                _sb.Clear();

                foreach (var vert in f.Vertices)
                {
                    bool hasTextCoord = vert.TextureCoordIndex != null;
                    bool hasNormal = vert.NormalIndex != null;

                    if (!hasTextCoord && !hasNormal)
                        _sb.Append($"{vert.VertexIndex + 1} ");

                    else if (!hasTextCoord && hasNormal)
                        _sb.Append($"{vert.VertexIndex + 1}//{vert.NormalIndex + 1} ");

                    else if (hasTextCoord && !hasNormal)
                        _sb.Append($"{vert.VertexIndex + 1}/{vert.TextureCoordIndex + 1} ");

                    else if (hasTextCoord && hasNormal)
                        _sb.Append($"{vert.VertexIndex + 1}/{vert.TextureCoordIndex + 1}/{vert.NormalIndex + 1} ");
                }

                // Remove last space
                _sb.Length -= 1;

                return _sb.ToString();
            }
        }

        public IEnumerable<string> Serialize(IEnumerable<IValue> values)
        {
            foreach (var value in values)
                yield return Serialize(value);
        }
    }
}
