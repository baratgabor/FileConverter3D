using System.Collections.Generic;

namespace FileConverter3D
{
    /// <summary>
    /// Internal representation of 3D file content.
    /// Serves as a central representation, to support pairing any importer with any exporter.
    /// </summary>
    public interface IModel :
        //TODO: Segregated interfaces aren't used; consider obsoleting them.
        IVertexSource, INormalSource, ITextureCoordSource, IFaceSource,
        IVertexStore, INormalStore, ITextureCoordStore, IFaceStore
    {
        List<Vertex> GetFaceVertices(Face face);
        void GetFaceVertices(Face face, List<Vertex> vertexContainer);
        IEnumerable<List<Vertex>> GetFaceVertices(IEnumerable<Face> faces, List<Vertex> vertexContainer);
    }
}
