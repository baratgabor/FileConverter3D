using System.Collections.Generic;

namespace FileConverter3D.Core
{
    public interface IModelReader<TOut>
    {
        IEnumerable<TOut> Read(IModel model);
    }
}