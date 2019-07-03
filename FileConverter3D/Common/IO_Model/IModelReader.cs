using System.Collections.Generic;

namespace FileConverter3D
{
    interface IModelReader<TOut>
    {
        IEnumerable<TOut> Read(IModel model);
    }
}