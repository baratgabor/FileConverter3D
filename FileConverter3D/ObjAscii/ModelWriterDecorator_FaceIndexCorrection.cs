using System;
using System.Collections.Generic;

namespace FileConverter3D.ObjAscii
{
    /// <summary>
    /// Intercepts faces, checks if they are negatively indexed, and converts them to positive indexing.
    /// </summary>
    class ModelWriterDecorator_FaceIndexCorrection : IModelWriter
    {
        protected IModelWriter _decoratee;
        protected int _vertIndx, _textIndx, _normIndx;

        public ModelWriterDecorator_FaceIndexCorrection(IModelWriter decoratee)
            => _decoratee = decoratee;

        public IModel Write(IEnumerable<IValue> values)
        {
            _vertIndx = _textIndx = _normIndx = 1;
            return _decoratee.Write(Intercept(values));
        }

        protected IEnumerable<IValue> Intercept(IEnumerable<IValue> values)
        {
            foreach (var v in values)
            {
                var vCopy = v; // v is locked by foreach

                switch (vCopy)
                {
                    case Vertex _:
                        _vertIndx++;
                        break;
                    case Normal _:
                        _normIndx++;
                        break;
                    case TextureCoord _:
                        _textIndx++;
                        break;
                    case Face f:
                        if (IsNegativeIndexed(f))
                            vCopy = ConvertToPositiveIndex(f);
                        break;
                    default:
                        break;
                }

                yield return vCopy;
            }

            bool IsNegativeIndexed(Face face)
                => face.Vertices[0].VertexIndex < 0;
        }

        private IValue ConvertToPositiveIndex(Face f)
        {
            var vertNum = f.Vertices.Count;
            for (int i = 0; i < vertNum; i++)
            {
                var v = f.Vertices[i];

                //TODO: Null check for vt & vn
                f.Vertices[i] = new FaceVertex(
                    vertexIndex: v.VertexIndex < 0 ? v.VertexIndex + _vertIndx : v.VertexIndex,
                    textureCoordIndex: v.TextureCoordIndex < 0 ? v.TextureCoordIndex + _textIndx : v.TextureCoordIndex,
                    normalIndex: v.NormalIndex < 0 ? v.NormalIndex + _normIndx : v.NormalIndex
                );
            }

            return f;
        }

        public void Visit(Vertex vertex) => throw new NotImplementedException();
        public void Visit(Normal normal) => throw new NotImplementedException();
        public void Visit(TextureCoord textureCoord) => throw new NotImplementedException();
        public void Visit(Face face) => throw new NotImplementedException();
    }
}