using System;
using System.Collections.Generic;
using System.Linq;

namespace FileConverter3D.Common
{
    /// <summary>
    /// Composite wrapper for value parsers. Finds and uses the first valid parser.
    /// </summary>
    internal class SerializerChain<TIn, TOut> : IValueSerializer<TIn, TOut>
    {
        protected readonly List<IValueSerializer<TIn, TOut>> _serializers;

        public SerializerChain(params IValueSerializer<TIn, TOut>[] serializers)
            => _serializers = new List<IValueSerializer<TIn, TOut>>(serializers);

        public bool CanSerialize(TIn serializable)
            => _serializers.Any(p => p.CanSerialize(serializable));

        public TOut Serialize(TIn serializable)
        {
            foreach (var p in _serializers)
                if (p.CanSerialize(serializable))
                    return p.Serialize(serializable);

            throw new InvalidOperationException($"No serializer found for the specified value.");
        }

        public IEnumerable<TOut> Serialize(IEnumerable<TIn> serializable)
        {
            foreach (var p in serializable)
                yield return Serialize(p);
        }
    }
    /// <summary>
    /// Composite wrapper for value parsers. Finds and uses the first valid parser.
    /// </summary>
    public class ParserChain<TParsable> : IValueParser<TParsable>
    {
        protected readonly List<IValueParser<TParsable>> _parsers;

        public ParserChain(params IValueParser<TParsable>[] parsers)
            => _parsers = new List<IValueParser<TParsable>>(parsers);

        public bool CanParse(TParsable parsable)
            => _parsers.Any(p => p.CanParse(parsable));

        public IValue Parse(TParsable parsable)
        {
            foreach (var p in _parsers)
                if (p.CanParse(parsable))
                    return p.Parse(parsable);

            //TODO: Consider throwing exception in case parse is not possible; returning default/null is a magic value.
            return null;
        }

        public IEnumerable<IValue> Parse(IEnumerable<TParsable> parsables)
        {
            foreach (var p in parsables)
                yield return Parse(p);
        }
    }
}
