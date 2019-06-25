using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileConverter3D.IntegrationTests
{
    [TestFixture]
    public class ObjAsciiImporterFactory_Tests
    {
        [TestCase]
        public void Import_ValidContent_ShouldProduceEquivalentModel()
        {
            var testLines = new List<string>()
            {
                "# Comment line",

                "v 1 2 3",
                "v 1.1 2.2 3.3",
                "v 9.9 8.8 7.7",

                "vn 3.1 2.4 1.5",
                "vn 6.9 8.8 9.1",
                "vn 0.9 0.8 0.7",

                "vt 1 2",
                "vt 0.123 0.434",
                "vt 0 0",

                "f 1/1/1 2/2/2 3/3/3 4/4/4",
            };

            var testStr = string.Join(Environment.NewLine, testLines);
            var testStream = GenerateStreamFromString(testStr);

            var expectedModel = new Model();
            expectedModel.AddVertex(new Vertex(1, 2, 3));
            expectedModel.AddVertex(new Vertex(1.1f, 2.2f, 3.3f));
            expectedModel.AddVertex(new Vertex(9.9f, 8.8f, 7.7f));

            expectedModel.AddNormal(new Normal(3.1f, 2.4f, 1.5f));
            expectedModel.AddNormal(new Normal(6.9f, 8.8f, 9.1f));
            expectedModel.AddNormal(new Normal(0.9f, 0.8f, 0.7f));

            expectedModel.AddTextureCoord(new TextureCoord(1, 2));
            expectedModel.AddTextureCoord(new TextureCoord(0.123f, 0.434f));
            expectedModel.AddTextureCoord(new TextureCoord(0, 0));

            var face = new Face();
            face.AddFaceVertex((1, 1, 1));
            face.AddFaceVertex((2, 2, 2));
            face.AddFaceVertex((3, 3, 3));
            face.AddFaceVertex((4, 4, 4));
            expectedModel.AddFace(face);

            IModel resModel = null; //\FileConverter3D.Import.ObjAscii();

            Assert.Multiple(() =>
            {
                Assert.That(resModel.Vertices, Is.EquivalentTo(expectedModel.Vertices));
                Assert.That(resModel.Normals, Is.EquivalentTo(expectedModel.Normals));
                Assert.That(resModel.TextureCoords, Is.EquivalentTo(expectedModel.TextureCoords));
                Assert.That(resModel.Faces, Is.EquivalentTo(expectedModel.Faces));
            });
        }

        public static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
