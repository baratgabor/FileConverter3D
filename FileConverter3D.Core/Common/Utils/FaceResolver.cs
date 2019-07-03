using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace FileConverter3D.Core
{
    //TODO: Obsolete. Implemented resolve functionality directly in model. In fact, assuming that vertices.ElementAt() will cast to a List and use the indexer is incorrect, because there can be yielding decorators implemented over the model.
    [Obsolete("Obsolete.Implemented resolve functionality directly in model.")]
    class FaceResolver
    {
        protected IModel _model;

        public FaceResolver(IModel model)
            => _model = model;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<(Vertex v, TextureCoord? t, Normal? n)> Resolve(Face face)
        {
            foreach (var fv in face.Vertices)
                yield return ResolveFaceVertex(fv); // Note: Compiler doesn't allow yield return from a try block with catch
            
            (Vertex v, TextureCoord? t, Normal? n) ResolveFaceVertex(FaceVertex fv)
            {
                try
                {
                    return (
                        _model.Vertices.ElementAt(fv.VertexIndex - 1),
                        fv.TextureCoordIndex == null ? (TextureCoord?)null : _model.TextureCoords.ElementAt((int)fv.TextureCoordIndex - 1),
                        fv.NormalIndex == null ? (Normal?)null : _model.Normals.ElementAt((int)fv.NormalIndex - 1)
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
