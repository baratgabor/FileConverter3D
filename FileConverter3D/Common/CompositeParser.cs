using System;
using System.Collections.Generic;
using System.Linq;

namespace FileConverter3D.Common
{
    /// <summary>
    /// Composite wrapper for value parsers. Finds and uses the first valid parser.
    /// </summary>
    internal class CompositeParser<TParsable> : IValueParser<TParsable>
    {
        protected readonly List<IValueParser<TParsable>> _parsers;

        public CompositeParser(params IValueParser<TParsable>[] parsers)
            => _parsers = new List<IValueParser<TParsable>>(parsers);

        public bool CanParse(TParsable parsable)
            => _parsers.Any(p => p.CanParse(parsable));

        public IValue Parse(TParsable parsable)
        {
            foreach (var p in _parsers)
                if (p.CanParse(parsable))
                    return p.Parse(parsable);

            //TODO: Consider throwing exception in case parse is not possible; returning null is a magic value.
            return null;
        }

        public IEnumerable<IValue> Parse(IEnumerable<TParsable> parsables)
        {
            foreach (var p in parsables)
                yield return Parse(p);
        }
    }
}
