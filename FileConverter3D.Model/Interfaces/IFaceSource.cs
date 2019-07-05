using System.Collections.Generic;

namespace FileConverter3D
{
    public interface IFaceSource
    {
        IList<Face> Faces { get; }
    }
}
