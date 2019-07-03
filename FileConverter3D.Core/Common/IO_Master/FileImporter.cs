namespace FileConverter3D.Common
{
    /// <summary>
    /// Facade class assuming the responsibility of coupling together file importer components.
    /// Can be injected with components pertaining to different file formats.
    /// </summary>
    /// <typeparam name="TRead">This type is the output of value reader and the input of value parser.</typeparam>
    public class FileImporter<TRead> : IFileImporter
    {
        protected readonly IFileStreamer _fileStreamer;
        protected readonly IDataReader<TRead> _valueReader;
        protected readonly IValueParser<TRead> _valueParser;
        protected readonly IModelWriter _modelWriter;

        public FileImporter(
            IFileStreamer fileStreamer,
            IDataReader<TRead> valueReader,
            IValueParser<TRead> valueParser,
            IModelWriter modelWriter)
        {
            _fileStreamer = fileStreamer;
            _valueReader = valueReader;
            _valueParser = valueParser;
            _modelWriter = modelWriter;
        }

        public IModel Import(string filePath) => 
            _modelWriter.Write(
                _valueParser.Parse(
                    _valueReader.Read(
                        _fileStreamer.GetStream(filePath))));
    }
}
