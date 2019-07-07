namespace FileConverter3D.Console
{
    class RotateOperation : VectorTransformOperation
    {
        public override string OptionName { get; } = "rotate";
        public override int ArgumentCount { get; } = 3;

        public RotateOperation(ConverterState state) : base(state) { }

        protected override void AddTransform(IModelTransform t, (float, float, float) vec)
        {
            var (x, y, z) = vec;
            t.AddRotation(x, y, z);
        }
    }
}