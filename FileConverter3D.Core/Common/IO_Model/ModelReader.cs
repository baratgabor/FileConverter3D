using System.Collections.Generic;
using System.Linq;

namespace FileConverter3D.Common
{
    /// <summary>
    /// Enumerates the entire model content as IValue instances.
    /// </summary>
    public class ModelReader : IModelReader<IValue>
    {
        //TODO: Maintenance liability when model is extended; make model itself responsible for content enumeration instead.
        public IEnumerable<IValue> Read(IModel model)
        {
            foreach (var item in model.Vertices)
                yield return item;

            foreach (var item in model.TextureCoords)
                yield return item;

            foreach (var item in model.Normals)
                yield return item;

            foreach (var item in model.Faces)
                yield return item;
        }
    }
}