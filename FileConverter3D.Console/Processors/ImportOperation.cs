using System;
using System.IO;

namespace FileConverter3D.Console
{
    class ImportOperation : IInputProcessor
    {
        public string OptionName { get; } = "import";
        public int ArgumentCount { get; } = 2;

        private ConverterState _state;

        public ImportOperation(ConverterState state) => _state = state;

        public ICommandAsync Process(string[] args)
        {
            if (args.Length < ArgumentCount)
                throw new ArgumentException($"Command '{OptionName}' requires {ArgumentCount} arguments.");

            var importType = args[0];
            var path = args[1];

            Action cmd = default;
            if (String.Equals(importType, "objascii", StringComparison.OrdinalIgnoreCase))
                cmd = () => _state.Model = FileConverter3D.Import.ObjAscii(path);
            else if (String.Equals(importType, "stlbinary", StringComparison.OrdinalIgnoreCase))
                cmd = () => _state.Model = FileConverter3D.Import.StlBinary(path);
            else
                throw new ArgumentException("Invalid import type. Supported types: objascii, stlbinary");

            if (!File.Exists(path))
                throw new ArgumentException($"The specified file doesn't exist or inaccessible.");

            return new RelayCommandConsoleConcurrent(OptionName, () => (true, ""), cmd);
        }
    }
}