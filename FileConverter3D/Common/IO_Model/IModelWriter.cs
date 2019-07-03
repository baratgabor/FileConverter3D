using System.Collections.Generic;

namespace FileConverter3D
{
    interface IModelWriter
    {
        IModel Write(IEnumerable<IValue> values);
    }
}
