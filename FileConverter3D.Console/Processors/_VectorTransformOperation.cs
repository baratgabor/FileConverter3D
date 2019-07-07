namespace FileConverter3D.Console
{
    abstract class VectorTransformOperation : IInputProcessor
    {
        public abstract string OptionName { get; }
        public abstract int ArgumentCount { get; }

        private ConverterState _state;

        public VectorTransformOperation(ConverterState state) => _state = state;

        protected abstract void AddTransform(IModelTransform t, (float, float, float) vec);

        public ICommandAsync Process(string[] args)
        {
            var vec = ParseVector(args[0], args[1], args[2]);

            return new RelayCommandConsoleConcurrent(
                OptionName,
                () => _state.Model == null ? (false, "A model must be loaded for transform operation.") : (true, ""),
                () => {
                    if (_state.ModelTransform == null)
                        _state.ModelTransform = new ModelMatrixTransform(() => new Matrix4x4());

                    AddTransform(_state.ModelTransform, vec);
                });
        }

        public (float x, float y, float z) ParseVector(string strX, string strY, string strZ)
        {
            if (!float.TryParse(strX, out var x))
                throw new System.ArgumentException("Expected three float values, but the first didn't parse as float.");

            if (!float.TryParse(strY, out var y))
                throw new System.ArgumentException("Expected three float values, but the second didn't parse as float.");

            if (!float.TryParse(strZ, out var z))
                throw new System.ArgumentException("Expected three float values, but the third didn't parse as float.");

            return (x, y, z);
        }
    }
}