using System.Collections.Generic;
using System.Linq;

namespace FileConverter3D.Common
{
    /// <summary>
    /// Enumerates the entire model content as IValue instances.
    /// </summary>
    class ModelReader : IModelReader<IValue>
    {
        //TODO: Maintenance liability when model is extended; make model itself responsible for content enumeration instead.
        public IEnumerable<IValue> Read(IModel model) =>
            // Flatten all collections into a single enumeration
            new List<IEnumerable<IValue>>() {
                model.Vertices as IEnumerable<IValue>,
                model.TextureCoords as IEnumerable<IValue>,
                model.Normals as IEnumerable<IValue>,
                model.Faces as IEnumerable<IValue>,
            }.SelectMany(v => v);
    }
}