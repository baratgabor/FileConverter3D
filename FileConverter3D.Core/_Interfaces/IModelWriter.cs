using System.Collections.Generic;

namespace FileConverter3D.Core
{
    public interface IModelWriter
    {
        IModel Write(IEnumerable<IValue> values);
    }
}