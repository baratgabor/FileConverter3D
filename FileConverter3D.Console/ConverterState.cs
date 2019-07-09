using System;

namespace FileConverter3D.Console
{
    class ConverterState
    {
        public bool OverwriteMode { get; set; }
        public IModel Model { get => model; set { model = value; ModelChanged?.Invoke(model); } }
        public IModelTransform ModelTransform { get; set; }

        public event Action<IModel> ModelChanged;
        private IModel model;
    }
}