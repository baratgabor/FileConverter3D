namespace FileConverter3D.Console
{
    class ConverterState
    {
        public bool OverwriteMode {get; set; }
        public IModel Model { get; set; }
        public IModelTransform ModelTransform { get; set; }
    }
}