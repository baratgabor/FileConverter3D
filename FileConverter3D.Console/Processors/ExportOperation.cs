using System;
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

        public ExportOperation(ConverterState state)
        {
            _state = state;
        }

        public ICommandAsync Process(string[] args)
        {
            var exportType = args[0];
            var path = args[1];

            ValidateFilePath(path);

            Action cmd = default;
            if (String.Equals(exportType, "objascii", StringComparison.OrdinalIgnoreCase))
                cmd = () => FileConverter3D.Export.ObjAscii(_state.Model, path);
            else if (String.Equals(exportType, "stlbinary", StringComparison.OrdinalIgnoreCase))
                cmd = () => FileConverter3D.Export.StlBinary(_state.Model, path);

            return new CompositeCommand(
                // Command for applying transformations, if any set
                new RelayCommandConsoleConcurrent(
                    "apply prior transformations",
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
                    cmd)
            );
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

            if (File.Exists(path))
                throw new ArgumentException("The specified file already exists.");

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