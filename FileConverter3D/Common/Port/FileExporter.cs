namespace FileConverter3D.Common
{
    internal class FileExporter<TIntermediate> : IFileExporter
    {
        private readonly IModelReader<TIntermediate> _modelReader;
        private readonly IFileWriter<TIntermediate> _fileWriter;
        private readonly IFileStreamer _fileStreamer;

        public FileExporter(
            IModelReader<TIntermediate> modelReader,
            IFileWriter<TIntermediate> fileWriter,
            IFileStreamer fileStreamer
            )
        {
            this._modelReader = modelReader;
            this._fileWriter = fileWriter;
            this._fileStreamer = fileStreamer;
        }

        public void Export(string filePath, IModel model)
        {
            _fileWriter.Write(
                _modelReader.Read(model),
                _fileStreamer.GetStream(filePath));
        }
    }
}
