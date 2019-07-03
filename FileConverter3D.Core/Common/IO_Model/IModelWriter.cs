using System.Collections.Generic;

namespace FileConverter3D
{
    public interface IModelWriter
    {
        IModel Write(IEnumerable<IValue> values);
    }
}