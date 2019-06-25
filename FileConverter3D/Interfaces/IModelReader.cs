using System.Collections.Generic;

namespace FileConverter3D.Common
{
    interface IModelReader<TResult>
    {
        IEnumerable<TResult> Read(IModel model);
    }
}