using System.Collections.Generic;
using System.Linq;

namespace FileConverter3D
{
    public class Model : IModel
    {
        public IList<Vertex> Vertices => _vertices;
        public IList<Normal> Normals => _normals;
        public IList<TextureCoord> TextureCoords => _textureCoords;
        public IList<Face> Faces => _faces;

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

        /// <summary>
        /// Resolves a face into a list of vertices. Allocates a new list.
        /// </summary>
        public List<Vertex> GetFaceVertices(Face face)
            => face.Vertices.Select(v => _vertices[v.VertexIndex]).ToList();

        /// <summary>
        /// Resolves a face into a list of vertices. Populates the specified list.
        /// </summary>
        public void GetFaceVertices(Face face, List<Vertex> vertexContainer)
        {
            foreach (var faceVert in face.Vertices)
                vertexContainer.Add(_vertices[faceVert.VertexIndex]);
        }

        /// <summary>
        /// Resolves a list of faces into lists of vertices. Reuses the specified pre-allocated collection.
        /// </summary>
        public IEnumerable<List<Vertex>> GetFaceVertices(IEnumerable<Face> faces, List<Vertex> vertexContainer)
        {
            foreach (var f in faces)
            {
                vertexContainer.Clear();
                GetFaceVertices(f, vertexContainer);
                yield return vertexContainer;
            }
        }
    }
}