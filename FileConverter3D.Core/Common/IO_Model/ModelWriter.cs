using System;
using System.Collections.Generic;
using System.IO;

namespace FileConverter3D.Core.Common
{
    /// <summary>
    /// Writes values into a new model.
    /// </summary>
    public class ModelWriter : IModelWriter, IValueVisitor
    {
        protected Func<IModel> _modelFactory;
        protected IModel _model;

        public ModelWriter(Func<IModel> modelFactory)
            => _modelFactory = modelFactory ?? throw new ArgumentNullException(nameof(modelFactory));

        public IModel Write(IEnumerable<IValue> values)
        {
            _model = _modelFactory();

            foreach (var v in values)
                v.Accept(this);

            var res = _model;
            _model = null;

            return res;
        }

        public void Visit(Vertex vertex)
            => _model.AddVertex(vertex);

        public void Visit(Normal normal)
            => _model.AddNormal(normal);

        public void Visit(TextureCoord textureCoord)
            => _model.AddTextureCoord(textureCoord);

        public void Visit(Face face)
            => _model.AddFace(face);

        //TODO: Sort out this state shenanigans
        //protected IValueWriter<IModel> ValidateState()
        //{
        //    if (_model == null)
        //        throw new InvalidOperationException($"This class doesn't support individual operation calls. Call {nameof(Write)}() for batch operation.");

        //    return this;
        //}
    }
}