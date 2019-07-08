using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Security.Permissions;

namespace FileConverter3D.Console
{
    class ExportOperation : IInputProcessor
    {
        public string OptionName { get; } = "export";
        public int ArgumentCount { get; } = 2;

        private ConverterState _state;
        private Dictionary<string, Action<IModel, string>> _typeMap = new Dictionary<string, Action<IModel, string>>()
        {
            ["objascii"] = (IModel model, string path) => FileConverter3D.Export.ObjAscii(model, path),
            ["stlbinary"] = (IModel model, string path) => FileConverter3D.Export.StlBinary(model, path),
        };

        public ExportOperation(ConverterState state)
        {
            _state = state;
        }

        public ICommandAsync Process(string[] args)
        {
            var exportType = args[0];
            var path = args[1];

            ValidateFilePath(path);

            if (!_typeMap.TryGetValue(exportType, out var cmd))
                throw new ArgumentException($"Invalid {OptionName} type '{exportType}'. Supported types: {String.Join(", ", _typeMap.Keys)}");

            return new CompositeCommand(
                // Command for applying transformations, if any set
                new RelayCommandConsoleConcurrent(
                    "apply prior transformations (if any)",
                    () => (true, ""), 
                    () => {
                        if (_state.ModelTransform != null && _state.Model != null)
                        {
                            _state.ModelTransform.Apply(_state.Model);
                            _state.ModelTransform = null;
                        }
                    }),
                // Command for executing actual export
                new RelayCommandConsoleConcurrent(
                    OptionName,
                    () => _state.Model == null ? (false, "A model must be loaded for export operation.") : (true, ""),
                    () => {
                        if (_state.OverwriteMode && File.Exists(path))
                            File.Delete(path);
                        cmd(_state.Model, path); }
            ));
        }

        public void ValidateFilePath(string path)
        {
            string dir, file;

            try
            {
                dir = Path.GetDirectoryName(path);
                file = Path.GetFileName(path);
            }
            catch (Exception e)
            {
                throw new ArgumentException("Cannot extract directory or file name from path", e);
            }

            if (!_state.OverwriteMode && File.Exists(path))
                throw new ArgumentException($"The specified file ('{path}') already exists. Use 'overwritemode' to allow overwriting existing files.");

            if (!HaveWritePermToDir(dir))
                throw new ArgumentException("This process has no write privilige to the specified folder.");
        }

        protected bool HaveWritePermToDir(string dirPath)
        {
            PermissionSet permissionSet = new PermissionSet(PermissionState.None);
            permissionSet.AddPermission(
                new FileIOPermission(FileIOPermissionAccess.Write, dirPath));

            if (permissionSet.IsSubsetOf(AppDomain.CurrentDomain.PermissionSet))
                return true;
            else
                return false;
        }
    }
}