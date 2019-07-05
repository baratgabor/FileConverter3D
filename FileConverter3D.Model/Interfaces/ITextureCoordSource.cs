using System.Collections.Generic;

namespace FileConverter3D
{
    public interface ITextureCoordSource
    {
        IList<TextureCoord> TextureCoords { get; }
    }
}
