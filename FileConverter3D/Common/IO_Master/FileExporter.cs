namespace FileConverter3D.Common
{
    internal class FileExporter<TRead, TSerialized> : IFileExporter
    {
        private readonly IModelReader<TRead> _modelReader;
        private readonly IValueSerializer<TRead, TSerialized> _valueSerializer;
        private readonly IDataWriter<TSerialized> _DataWriter;
        private readonly IFileStreamer _fileStreamer;

        public FileExporter(
            IModelReader<TRead> modelReader,
            IValueSerializer<TRead, TSerialized> valueSerializer,
            IDataWriter<TSerialized> fileWriter,
            IFileStreamer fileStreamer
            )
        {
            _modelReader = modelReader;
            _valueSerializer = valueSerializer;
            _DataWriter = fileWriter;
            _fileStreamer = fileStreamer;
        }

        public void Export(string filePath, IModel model) =>
            _DataWriter.Write(
                _fileStreamer.GetStream(filePath),
                _valueSerializer.Serialize(
                    _modelReader.Read(model)));
    }
}
