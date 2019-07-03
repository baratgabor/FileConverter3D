using System;
using System.Collections.Generic;

namespace FileConverter3D
{
    public interface ITransformableModel : IModel
    {
        void SetTranslation(IVector3 translation);
        void SetRotation(IVector3 rotation);
        void SetScale(IVector3 scale);
    }

    public class ModelTransformationDecorator : ITransformableModel
    {
        protected IModel _model;
        protected private TRSMatrix _matrix;

        public ModelTransformationDecorator(IModel model)
            => _model = model;

        public void SetTranslation(IVector3 translation)
            => _matrix.SetTranslation(translation);

        public void SetRotation(IVector3 rotation)
            => _matrix.SetRotation(rotation);

        public void SetScale(IVector3 scale)
            => _matrix.SetScale(scale);

        public IEnumerable<Vertex> Vertices => TransformVertices(_model.Vertices);

        public IEnumerable<Normal> Normals => TransformNormals(_model.Normals);

        protected IEnumerable<Vertex> TransformVertices(IEnumerable<Vertex> vertices)
        {
            foreach (var v in vertices)
                yield return _matrix * v;
        }

        protected IEnumerable<Normal> TransformNormals(IEnumerable<Normal> normals)
        {
            foreach (var n in normals)
                yield return _matrix * n;
        }

        // Delegated calls
        public IEnumerable<TextureCoord> TextureCoords => _model.TextureCoords;
        public IEnumerable<Face> Faces => _model.Faces;
        public void AddFace(Face face) => _model.AddFace(face);
        public void AddVertex(Vertex vertex) => _model.AddVertex(vertex);
        public void AddNormal(Normal normal) => _model.AddNormal(normal);
        public void AddTextureCoord(TextureCoord textureCoord) => _model.AddTextureCoord(textureCoord);
        public List<Vertex> GetFaceVertices(Face face) => _model.GetFaceVertices(face);
        public void GetFaceVertices(Face face, List<Vertex> vertexContainer) => _model.GetFaceVertices(face, vertexContainer);
        public IEnumerable<List<Vertex>> GetFaceVertices(IEnumerable<Face> faces, List<Vertex> vertexContainer) => _model.GetFaceVertices(faces, vertexContainer);
    }
}
