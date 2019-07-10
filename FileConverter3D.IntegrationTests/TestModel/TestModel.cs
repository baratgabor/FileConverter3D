using System.Collections.Generic;
namespace FileConverter3D.Tests
{
    public class TestModel : IModel
    {
        public IList<Vertex> Vertices { get; set; }
        public IList<Normal> Normals { get; set; }
        public IList<TextureCoord> TextureCoords { get; set; }
        public IList<Face> Faces { get; set; }

        public void AddFace(Face face) => throw new System.NotImplementedException();
        public void AddNormal(Normal normal) => throw new System.NotImplementedException();
        public void AddTextureCoord(TextureCoord textureCoord) => throw new System.NotImplementedException();
        public void AddVertex(Vertex vertex) => throw new System.NotImplementedException();
        public List<Vertex> GetFaceVertices(Face face) => throw new System.NotImplementedException();
        public void GetFaceVertices(Face face, List<Vertex> vertexContainer) => throw new System.NotImplementedException();
        public IEnumerable<List<Vertex>> GetFaceVertices(IEnumerable<Face> faces, List<Vertex> vertexContainer) => throw new System.NotImplementedException();
    }
}
