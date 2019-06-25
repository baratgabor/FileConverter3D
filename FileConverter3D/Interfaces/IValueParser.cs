using System.Collections.Generic;

namespace FileConverter3D
{
    public interface IValueParser<TParsable>
    {
        bool CanParse(TParsable parsable);
        IValue Parse(TParsable parsable);
        IEnumerable<IValue> Parse(IEnumerable<TParsable> parsables);
    }
}