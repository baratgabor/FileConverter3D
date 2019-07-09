using System;
using System.Collections.Generic;
using System.IO;

namespace FileConverter3D.Console
{
    class ImportOperation : IInputProcessor
    {
        public string OptionName { get; } = "import";
        public int ArgumentCount { get; } = 2;
        public string HelpText => $"import [{String.Join("|", _typeMap.Keys)}] \"Directory\\Model name.ext\"";

        private ConverterState _state;
        private Dictionary<string, Func<string, IModel>> _typeMap = new Dictionary<string, Func<string, IModel>>()
        {
            ["objascii"] = (string path) => FileConverter3D.Import.ObjAscii(path),
            ["stlbinary"] = (string path) => FileConverter3D.Import.StlBinary(path),
        };

        public ImportOperation(ConverterState state) => _state = state;

        public ICommandAsync Process(string[] args)
        {
            if (args.Length < ArgumentCount)
                throw new ArgumentException($"Command '{OptionName}' requires {ArgumentCount} arguments.");

            var importType = args[0];
            var path = args[1];

            if (!_typeMap.TryGetValue(importType, out var cmd))
                throw new ArgumentException($"Invalid {OptionName} type '{importType}'. Supported types: {String.Join(", ", _typeMap.Keys)}");

            if (!File.Exists(path))
                throw new ArgumentException($"The specified file ('{path}') doesn't exist or inaccessible.");

            return new RelayCommandConsoleConcurrent(
                OptionName,
                () => (true, ""),
                () => _state.Model = cmd(path));
        }
    }
}