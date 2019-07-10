using NUnit.Framework;
using System.IO;

namespace FileConverter3D.Tests.IntegrationTests.ObjAsciiImport
{
    [TestFixture]
    public class Import_ObjAscii_Tests
    {
        static object[] CubeCases =
        {
            new object[] { TestConfiguration.cube_v_vt_vn_tris_absIndex },
            new object[] { TestConfiguration.cube_v_vt____tris_absIndex },
            new object[] { TestConfiguration.cube_v____vn_tris_absIndex },
            new object[] { TestConfiguration.cube_v_______quads_absIndex },
            new object[] { TestConfiguration.cube_v_______quads_relIndex },
            new object[] { TestConfiguration.cube_v_______tris_absIndex },
         };

        [Test, TestCaseSource("CubeCases")]
        public void Import_ObjAscii_ModelContentShouldBeEqualToExpected(TestFile testFile)
        {
            testFile.DecrementModelFaceIndexes(); // Hack. Was too lazy to reindex all the test file faces to 0 based.
            var resModel = FileConverter3D.Import.ObjAscii(GetTestFilePath(testFile.FileName));

            Assert.Multiple(() =>
            {
                Assert.That(resModel.Vertices, Is.EquivalentTo(testFile.ExpectedModel.Vertices), testFile.FileName);
                Assert.That(resModel.Normals, Is.EquivalentTo(testFile.ExpectedModel.Normals), testFile.FileName);
                Assert.That(resModel.TextureCoords, Is.EquivalentTo(testFile.ExpectedModel.TextureCoords), testFile.FileName);
                Assert.That(resModel.Faces, Is.EquivalentTo(testFile.ExpectedModel.Faces), testFile.FileName);
            });
        }

        //const string cube_v_quads_abs = "cube_v_______quads_absIndex.obj";
        //const string cube_v_quads_rel = "cube_v_______quads_relIndex.obj";
        //const string cube_v_tris_abs = "cube_v_______tris_absIndex.obj";
        //const string cube_v_vn_tris_abs = "cube_v____vn_tris_absIndex.obj";
        //const string cube_v_vt_tris_abs = "cube_v_vt____tris_absIndex.obj";
        //const string cube_v_vt_vn_tris_abs = "cube_v_vt_vn_tris_absIndex.obj";

        //    [TestCase(cube_v_quads_abs)]
        //    [TestCase(cube_v_quads_rel)]
        //    [TestCase(cube_v_tris_abs)]
        //    [TestCase(cube_v_vn_tris_abs)]
        //    [TestCase(cube_v_vt_tris_abs)]
        //    [TestCase(cube_v_vt_vn_tris_abs)]
        //    public void Import_ObjAscii_FaceIndexesShouldBeValid(string testFileName)
        //    {
        //        var model = FileConverter3D.Import.ObjAscii(GetTestFilePath(testFileName));

        //        Assert.Multiple(() =>
        //        {
        //            Assert.That(model.Faces.Count > 0, "Imported model should contain faces.");
        //            Assert.That(model.Vertices.Count > 0, "Imported model should contain vertices.");
        //            Assert.That(FaceVertexIndexesValid(), "Face vertices must refer to a valid vertex index.");

        //            if (model.Normals.Count > 0)
        //                Assert.That(FaceVertexNormalIndexesValid(), "Face vertex normals must refer to a valid normal index.");
        //            if (model.TextureCoords.Count > 0)
        //                Assert.That(FaceVertexTextureIndexesValid(), "Face vertex texture coords must refer to a valid texture coord index.");
        //        });

        //        bool FaceVertexIndexesValid()
        //        {
        //            var maxIndex = model.Vertices.Count - 1;
        //            foreach (var f in model.Faces)
        //                foreach (var v in f.Vertices)
        //                    if (v.VertexIndex < 0 || v.VertexIndex > maxIndex)
        //                        return false;

        //            return true;
        //        }

        //        bool FaceVertexNormalIndexesValid()
        //        {
        //            var maxIndex = model.Normals.Count - 1;
        //            foreach (var f in model.Faces)
        //                foreach (var v in f.Vertices)
        //                    if (v.NormalIndex != null && (v.NormalIndex < 0 || v.NormalIndex > maxIndex))
        //                        return false;

        //            return true;
        //        }

        //        bool FaceVertexTextureIndexesValid()
        //        {
        //            var maxIndex = model.TextureCoords.Count - 1;
        //            foreach (var f in model.Faces)
        //                foreach (var v in f.Vertices)
        //                    if (v.TextureCoordIndex != null && (v.TextureCoordIndex < 0 || v.TextureCoordIndex > maxIndex))
        //                        return false;

        //            return true;
        //        }
        //    }

        string GetTestFilePath(string filename)
        {
            return Path.Combine(
                Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                TestConfiguration.TestFilePath,
                filename);
        }
    }
}
