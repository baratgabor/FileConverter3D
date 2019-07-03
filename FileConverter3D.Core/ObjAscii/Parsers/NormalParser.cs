namespace FileConverter3D.ObjAscii
{
    public class NormalParser : ObjAsciiParserBase
    {
        public override string DataSignature => "VN";

        public override IValue Parse(string parsable)
        {
            var vec = ParseVector3Line(parsable, DataSignature);
            return new Normal(vec.X, vec.Y, vec.Z);
        }
    }
}
