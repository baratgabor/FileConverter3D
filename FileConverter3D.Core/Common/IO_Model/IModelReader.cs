using System.Collections.Generic;

namespace FileConverter3D
{
    public interface IModelReader<TOut>
    {
        IEnumerable<TOut> Read(IModel model);
    }
}