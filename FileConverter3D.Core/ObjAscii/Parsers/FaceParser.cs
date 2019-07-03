namespace FileConverter3D.ObjAscii
{
    public class FaceParser : ObjAsciiParserBase
    {
        public override string DataSignature => "F";

        public override IValue Parse(string parsable) 
            => ParseFaceLine(parsable, DataSignature);
    }
}
