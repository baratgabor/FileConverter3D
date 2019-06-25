using System.Collections.Generic;

namespace FileConverter3D
{
    public interface IFaceSource
    {
        IEnumerable<Face> Faces { get; }
    }
}
