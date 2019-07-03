namespace FileConverter3D.Core.ObjAscii
{
    public class VertexParser : ObjAsciiParserBase
    {
        public override string DataSignature => "V";

        public override IValue Parse(string parsable)
        {
            var vec = ParseVector3Line(parsable, DataSignature);
            return new Vertex(vec.X, vec.Y, vec.Z);
        }
    }
}
