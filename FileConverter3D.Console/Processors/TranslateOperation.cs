namespace FileConverter3D.Console
{
    class TranslateOperation : VectorTransformOperation
    {
        public override string OptionName { get; } = "translate";
        public override int ArgumentCount { get; } = 3;

        public TranslateOperation(ConverterState state) : base(state) { }

        protected override void AddTransform(IModelTransform t, (float, float, float) vec)
        {
            var (x, y, z) = vec;
            t.AddTranslation(x, y, z);
        }
    }
}