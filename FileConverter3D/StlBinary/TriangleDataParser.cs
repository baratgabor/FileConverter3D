using System;
using System.Collections.Generic;

namespace FileConverter3D.StlBinary
{
    /// <summary>
    /// Parses raw triangle data into IValue instances.
    /// </summary>
    class TriangleDataParser : IValueParser<byte[]>
    {
        protected CompositeValue _cache = new CompositeValue() { Values = new List<IValue>() };
        protected int _vertIndx, _normIndx;

        public bool CanParse(byte[] parsable) => throw new NotImplementedException();

        public IEnumerable<IValue> Parse(IEnumerable<byte[]> parsables)
        {
            _vertIndx = _normIndx = 1;

            foreach (var p in parsables)
                yield return Parse(p);
        }

        public IValue Parse(byte[] parsable)
        {
            //TODO: Consider populating struct StlTriangle via Marshal.PtrToStruct() instead?

            var i = 0;

            _cache.Values.Clear();
            _cache.Values.Add(ReadNormal());
            _cache.Values.Add(ReadVertex());
            _cache.Values.Add(ReadVertex());
            _cache.Values.Add(ReadVertex());
            _cache.Values.Add(new Face(
                (_vertIndx++, null, _normIndx++),
                (_vertIndx++, null, _normIndx),
                (_vertIndx++, null, _normIndx)
            ));

            return _cache;

            float ReadFloat()
            {
                var res = BitConverter.ToSingle(parsable, i);
                i += 4;
                return res;
            }

            Normal ReadNormal() =>
                new Normal(
                    ReadFloat(),
                    ReadFloat(),
                    ReadFloat()
                );

            Vertex ReadVertex() => 
                new Vertex(
                    ReadFloat(),
                    ReadFloat(),
                    ReadFloat()
                );
        }
    }
}