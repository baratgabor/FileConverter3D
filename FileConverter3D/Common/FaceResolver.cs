using System;
using System.Collections.Generic;
using System.Linq;

namespace FileConverter3D
{
    class FaceResolver
    {
        protected IModel _model;

        public FaceResolver(IModel model)
            => _model = model;

        public IEnumerable<(Vertex v, TextureCoord? t, Normal? n)> Resolve(Face face)
        {
            foreach (var fv in face.Vertices)
                yield return ResolveFaceVertex(fv); // Note: Cannot yield return from a try block with catch
            
            (Vertex v, TextureCoord? t, Normal? n) ResolveFaceVertex((int v, int? vt, int? vn) fv)
            {
                try
                {
                    return (
                        _model.Vertices.ElementAt(fv.v - 1),
                        fv.vt == null ? (TextureCoord?)null : _model.TextureCoords.ElementAt((int)fv.vt - 1),
                        fv.vn == null ? (Normal?)null : _model.Normals.ElementAt((int)fv.vn - 1)
                    );
                }
                catch (IndexOutOfRangeException e)
                {
                    throw new ArgumentException("Cannot resolve face. At least one face vertex index is invalid.", e);
                }
            }
        }
    }
}
