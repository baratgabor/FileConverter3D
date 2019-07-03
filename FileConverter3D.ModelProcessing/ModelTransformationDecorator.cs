using System;
using System.Collections.Generic;

namespace FileConverter3D
{
    public class ModelTransformationDecorator : IModel
    {
        protected IModel _model;

        private protected Matrix4x4 _matrixScale;
        private protected Matrix4x4 _matrixRotate;
        private protected Matrix4x4 _matrixTranslate;

        private protected Matrix4x4 _matrixMaster;
        private protected bool _rebuildMaster;

        public IEnumerable<Vertex> Vertices => TransformVertices(_model.Vertices);

        public IEnumerable<Normal> Normals => TransformNormals(_model.Normals);

        public IEnumerable<TextureCoord> TextureCoords => _model.TextureCoords;

        public IEnumerable<Face> Faces => _model.Faces;

        public ModelTransformationDecorator(IModel model)
        {
            _model = model;
        }

        public void SetTranslation(IVector3 translation)
        {
            _matrixTranslate = new Matrix4x4();
            _matrixTranslate.SetTranslation(translation);
            _rebuildMaster = true;
        }

        public void SetRotation(IVector3 rotation)
        {
            throw new NotImplementedException();
            //_matrixRotate = new Matrix4x4();
            //_matrixRotate.SetRotation(rotation);
            //_rebuildMaster = true;
        }

        public void SetScale(IVector3 scale)
        {
            _matrixScale = new Matrix4x4();
            _matrixScale.SetScale(scale);
            _rebuildMaster = true;
        }

        protected IEnumerable<Vertex> TransformVertices(IEnumerable<Vertex> vertices)
        {
            throw new NotImplementedException();
            //foreach (var v in vertices)
            //    yield return v * _matrixTranslate * ;
        }

        protected IEnumerable<Normal> TransformNormals(IEnumerable<Normal> normals)
        {
            throw new NotImplementedException();
            //foreach (var n in normals)
            //    yield return _matrix * n;
        }

        public void AddFace(Face face) => _model.AddFace(face);
        public void AddVertex(Vertex vertex) => _model.AddVertex(vertex);
        public void AddNormal(Normal normal) => _model.AddNormal(normal);
        public void AddTextureCoord(TextureCoord textureCoord) => _model.AddTextureCoord(textureCoord);

        public List<Vertex> GetFaceVertices(Face face)
        {
            throw new NotImplementedException();
        }

        public void GetFaceVertices(Face face, List<Vertex> vertexContainer)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<List<Vertex>> GetFaceVertices(IEnumerable<Face> faces, List<Vertex> vertexContainer)
        {
            throw new NotImplementedException();
        }
    }
}
