using System;
using Common = FileConverter3D.Core.Common;
using ObjAscii = FileConverter3D.Core.ObjAscii;
using StlBinary = FileConverter3D.Core.StlBinary;

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
                return
                    new Common.FileImporter<string>(
                        new Common.ReadingStream(),
                        new ObjAscii.LineFilterProxy(
                            new Common.TextLineReader()),
                        new Common.ParserChain<string>(
                            new ObjAscii.VertexParser(),
                            new ObjAscii.NormalParser(),
                            new ObjAscii.TextureCoordParser(),
                            new ObjAscii.FaceParser()),
                        new ObjAscii.ModelWriterDecorator_FaceIndexCorrection(
                            new Common.ModelWriter(() => new Model())))

                .Import(filePath);
            }

            /// <summary>
            /// Imports a binary STL formatted 3D mesh file.
            /// </summary>
            /// <param name="filePath">The path of the file.</param>
            /// <returns>The imported model.</returns>
            public static IModel StlBinary(string filePath)
            {
                return
                    new Common.FileImporter<byte[]>(
                        new Common.ReadingStream(),
                        new StlBinary.TriangleDataReader(),
                        new StlBinary.TriangleDataParser(),
                        new Common.ModelWriter(() => new Model()))

                .Import(filePath);
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
                new Common.FileExporter<StlBinary.StlTriangle, byte[]>(
                    new StlBinary.TriangleExtractor(
                        new Common.NaiveTriangulate()),
                    new StlBinary.TriangleSerializer(),
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
                new Common.FileExporter<IValue, string>(
                    new Common.ModelReader(),
                    new ObjAscii.CombinedValueSerializer(),
                    new ObjAscii.ObjAsciiWriter(),
                    new Common.WritingStream())

                .Export(filePath, model);
            }
        }

        public static class Transform
        {
            public static IModel Translate(IModel model, IVector3 translation)
            {
                var transModel = GetTransformableModel(model);
                transModel.SetTranslation(translation);
                return transModel;
            }

            public static IModel Rotate(IModel model, IVector3 eulers)
            {
                var transModel = GetTransformableModel(model);
                transModel.SetRotation(eulers);
                return transModel;
            }

            public static IModel Scale(IModel model, IVector3 scale)
            {
                var transModel = GetTransformableModel(model);
                transModel.SetScale(scale);
                return transModel;
            }

            private static ITransformableModel GetTransformableModel(IModel model)
                => model is ITransformableModel ? 
                    model as ITransformableModel : 
                    new ModelTransformationDecorator(model);
        }

        public static class Analyze
        {
            public static float CalculateSurfaceArea(IModel model)
                => new ModelSurfaceAreaCalculator(
                    new Common.NaiveTriangulate()).Calculate(model);

            public static float CalculateVolume(IModel model)
                => new ModelVolumeCalculator(
                    new Common.NaiveTriangulate()).Calculate(model);

            public static bool Intersect(IModel model, Vector3 point)
                => new PointIsInsideMesh(
                    new Common.NaiveTriangulate(), point).Calculate(model);
        }
    }
}
