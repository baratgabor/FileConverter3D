namespace FileConverter3D.Common
{
    /// <summary>
    /// Facade class assuming the responsibility of coupling together file importer components.
    /// Can be injected with components pertaining to different file formats.
    /// </summary>
    /// <typeparam name="TIntermediate">This type is the output of value reader and the input of value parser.</typeparam>
    internal class FileImporter<TIntermediate> : IFileImporter
    {
        protected readonly IFileStreamer _fileStreamer;
        protected readonly IValueReader<TIntermediate> _valueReader;
        protected readonly IValueParser<TIntermediate> _valueParser;
        protected readonly IModelWriter<IModel> _valueWriter;

        public FileImporter(
            IFileStreamer fileStreamer,
            IValueReader<TIntermediate> valueReader,
            IValueParser<TIntermediate> valueParser,
            IModelWriter<IModel> valueWriter)
        {
            _fileStreamer = fileStreamer;
            _valueReader = valueReader;
            _valueParser = valueParser;
            _valueWriter = valueWriter;
        }

        public IModel Import(string filePath) => 
            _valueWriter.Write(
                _valueParser.Parse(
                    _valueReader.Read(
                        _fileStreamer.GetStream(filePath))));
    }
}
