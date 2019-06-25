using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileConverter3D
{
    /// <summary>
    /// Internal representation of 3D file content.
    /// Serves as a central format, to support pairing any importer with any exporter.
    /// </summary>
    public interface IModel :
        //TODO: Segregated interfaces aren't used; consider obsoleting them.
        IVertexSource, INormalSource, ITextureCoordSource, IFaceSource,
        IVertexStore, INormalStore, ITextureCoordStore, IFaceStore
    {
        // Composited
    }
}
