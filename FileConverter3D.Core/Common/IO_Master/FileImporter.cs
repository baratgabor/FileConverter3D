using System;
using System.Reflection;

namespace FileConverter3D.Core.Common
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

        public IModel Import(string filePath)
        {
            try
            {
                return
                    _modelWriter.Write(
                        _valueParser.Parse(
                            _valueReader.Read(
                                _fileStreamer.GetStream(filePath))));
            }
            catch(Exception e)
            {
                string addedContext = default;

                foreach (var f in this.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
                    if (f.GetValue(this) is IStateInfoProvider i)
                        addedContext += i.StateInfo;

                if (addedContext != default)
                    throw new Exception($"{e.Message} {addedContext}" , e);
                else
                    throw;
            }
        }
    }
}
