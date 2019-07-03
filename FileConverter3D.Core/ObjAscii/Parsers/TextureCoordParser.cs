namespace FileConverter3D.Core.ObjAscii
{
    public class TextureCoordParser : ObjAsciiParserBase
    {
        public override string DataSignature => "VT";

        public override IValue Parse(string parsable)
        {
            var vec = ParseVector2Line(parsable, DataSignature);
            return new TextureCoord(vec.X, vec.Y);
        }
    }
}
