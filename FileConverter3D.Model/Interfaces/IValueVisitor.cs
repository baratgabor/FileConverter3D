namespace FileConverter3D
{
    public interface IValueVisitor
    {
        void Visit(Vertex vertex);
        void Visit(Normal normal);
        void Visit(TextureCoord textureCoord);
        void Visit(Face face);
    }
}
