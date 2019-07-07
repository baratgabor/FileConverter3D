namespace FileConverter3D.Console
{
    class ScaleOperation : VectorTransformOperation
    {
        public override string OptionName { get; } = "scale";
        public override int ArgumentCount { get; } = 3;

        public ScaleOperation(ConverterState state) : base(state) { }

        protected override void AddTransform(IModelTransform t, (float, float, float) vec)
        {
            var (x, y, z) = vec;
            t.AddScale(x, y, z);
        }
    }
}