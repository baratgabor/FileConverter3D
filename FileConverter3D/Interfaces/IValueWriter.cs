using System.Collections.Generic;

namespace FileConverter3D
{
    public interface IModelWriter : IValueVisitor
    {
        IModel Write(IEnumerable<IValue> values);
    }
}
