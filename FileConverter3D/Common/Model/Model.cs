using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileConverter3D
{
    public class Model : IModel
    {
        public IEnumerable<Vertex> Vertices => _vertices;
        public IEnumerable<Normal> Normals => _normals;
        public IEnumerable<TextureCoord> TextureCoords => _textureCoords;
        public IEnumerable<Face> Faces => _faces;

        private readonly List<Vertex> _vertices = new List<Vertex>();
        private readonly List<Normal> _normals = new List<Normal>();
        private readonly List<TextureCoord> _textureCoords = new List<TextureCoord>();
        private readonly List<Face> _faces = new List<Face>();

        public void AddVertex(Vertex vertex)
            => _vertices.Add(vertex);

        public void AddNormal(Normal normal)
            => _normals.Add(normal);

        public void AddTextureCoord(TextureCoord textureCoord)
            => _textureCoords.Add(textureCoord);

        public void AddFace(Face face)
            => _faces.Add(face);
    }
}
