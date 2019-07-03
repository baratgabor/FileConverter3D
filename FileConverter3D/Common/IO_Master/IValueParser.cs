using System.Collections.Generic;

namespace FileConverter3D
{
    /// <summary>
    /// Parses the specified input type into the specified output type.
    /// </summary>
    public interface IValueParser<TIn>
    {
        bool CanParse(TIn parsable);
        IValue Parse(TIn parsable);
        IEnumerable<IValue> Parse(IEnumerable<TIn> parsables);
    }
}