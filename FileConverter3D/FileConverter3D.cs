using System;

namespace FileConverter3D
{
    /// <summary>
    /// Exposes file converter services.
    /// </summary>
    public static class FileConverter3D
    {
        // This is a simple static composition root for building the object graphs responsible for file format input/ouput.
        // Reusable common and format-specific components can be mixed & matched.
        // Proxies, decorators, composites, etc. can be easily added to the pipeline.

        public static class Import
        {
            /// <summary>
            /// Imports an OBJ ASCII formatted 3D mesh file.
            /// </summary>
            /// <param name="filePath">The path of the file.</param>
            /// <returns>The imported model.</returns>
            public static IModel ObjAscii(string filePath)
            {
                var objAsciiImporter =
                    new Common.FileImporter<string>(
                        fileStreamer:
                            new Common.ReadingStream(),
                        valueReader:
                            new ObjAscii.LineFilterProxy(
                                new Common.TextLineReader()),
                        valueParser:
                            new Common.CompositeParser<string>(
                                new ObjAscii.VertexParser(),
                                new ObjAscii.NormalParser(),
                                new ObjAscii.TextureCoordParser(),
                                new ObjAscii.FaceParser()),
                        modelWriter:
                            new ObjAscii.ModelWriterDecorator_FaceIndexCorrection(
                                new Common.ModelWriter(() => new Model())
                                ));

                return objAsciiImporter.Import(filePath);
            }

            /// <summary>
            /// Imports a binary STL formatted 3D mesh file.
            /// </summary>
            /// <param name="filePath">The path of the file.</param>
            /// <returns>The imported model.</returns>
            public static IModel StlBinary(string filePath)
            {
                var stlBinaryImporter =
                    new Common.FileImporter<byte[]>(
                        fileStreamer:
                            new Common.ReadingStream(),
                        valueReader:
                            new StlBinary.TriangleDataReader(),
                        valueParser:
                            new StlBinary.TriangleDataParser(),
                        modelWriter:
                            new Common.ModelWriter(() => new Model())
                            );

                return stlBinaryImporter.Import(filePath);
            }
        }

        public static class Export
        {
            /// <summary>
            /// Exports a model as a binary STL formatted file.
            /// </summary>
            /// <param name="model">The model to export.</param>
            /// <param name="filePath">The path of the file to create.</param>
            /// <exception cref="IOException">If the target file already exists.</exception>
            public static void StlBinary(IModel model, string filePath)
            {
                new Common.FileExporter<StlBinary.StlTriangle>(
                    new StlBinary.TriangleExtractor(),
                    new StlBinary.StlBinaryWriter(),
                    new Common.WritingStream())
                .Export(filePath, model);
            }

            /// <summary>
            /// Exports a model as an OBJ ASCII formatted file.
            /// </summary>
            /// <param name="model">The model to export.</param>
            /// <param name="filePath">The path of the file to create.</param>
            /// <exception cref="IOException">If the target file already exists.</exception>
            public static void ObjAscii(IModel model, string filePath)
            {
                new Common.FileExporter<IValue>(
                    new Common.ModelReader(),
                    new ObjAscii.ValueToObjAsciiWriter(),
                    new Common.WritingStream())
                .Export(filePath, model);
            }
        }

        public static class Transform
        {
            public static IModel Translate(IModel model)
            {
                throw new NotImplementedException();
            }

            public static IModel Rotate(IModel model)
            {
                throw new NotImplementedException();
            }
        }

        public static class Analyze
        {
            public static float CalculateSurfaceArea(IModel model)
            {
                throw new NotImplementedException();
            }

            public static float CalculateVolume(IModel model)
            {
                throw new NotImplementedException();
            }

            public static bool Intersect(IModel model, Vector3 point)
            {
                throw new NotImplementedException();
            }
        }
    }
}
