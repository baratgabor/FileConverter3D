using System.Collections.Generic;

namespace FileConverter3D
{
    /// <summary>
    /// Serializes the specified input type into the specified output type.
    /// </summary>
    public interface IValueSerializer<TIn, TOut>
    {
        bool CanSerialize(TIn value);
        TOut Serialize(TIn value);
        IEnumerable<TOut> Serialize(IEnumerable<TIn> values);
    }
}