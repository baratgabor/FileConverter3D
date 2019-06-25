using System.Collections.Generic;

namespace FileConverter3D
{
    public interface IValueWriter<TDestination> : IValueVisitor
    {
        TDestination Write(IEnumerable<IValue> values);
    }
}
