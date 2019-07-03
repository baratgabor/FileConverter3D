using System.Collections.Generic;

namespace FileConverter3D
{
    public interface ITextureCoordSource
    {
        IEnumerable<TextureCoord> TextureCoords { get; }
    }
}
