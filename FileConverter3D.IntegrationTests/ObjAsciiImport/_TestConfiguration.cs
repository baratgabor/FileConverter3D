using System.Collections.Generic;
using System.IO;

namespace FileConverter3D.Tests.IntegrationTests.ObjAsciiImport
{
    public static class TestConfiguration
    {
        public static string TestFilePath = Path.Combine("TestFiles", "Obj");

        // Float coordinate in cube test files. All vertices are combinations of plus and minus of this value.
        private const float C = 0.5f;

        public static TestFile cube_v_______quads_relIndex => cube_v_______quads_absIndex; // Relative indexes should be converted to absolute

        public static TestFile cube_v_______quads_absIndex => new TestFile()
        {
            FileName = "cube_v_______quads_absIndex.obj",
            ExpectedModel = new TestModel()
            {
                Vertices = new List<Vertex>()
                {
                    new Vertex(-C, -C, C),
                    new Vertex(-C, -C, -C),
                    new Vertex(-C, C, -C),
                    new Vertex(-C, C, C),
                    new Vertex(C, -C, C),
                    new Vertex(C, -C, -C),
                    new Vertex(C, C, -C),
                    new Vertex(C, C, C),
                },
                Normals = new List<Normal>(),
                TextureCoords = new List<TextureCoord>(),
                Faces = new List<Face>()
                {
                    new Face(new FaceVertex(4), new FaceVertex(3), new FaceVertex(2), new FaceVertex(1)),
                    new Face(new FaceVertex(2), new FaceVertex(6), new FaceVertex(5), new FaceVertex(1)),
                    new Face(new FaceVertex(3), new FaceVertex(7), new FaceVertex(6), new FaceVertex(2)),
                    new Face(new FaceVertex(8), new FaceVertex(7), new FaceVertex(3), new FaceVertex(4)),
                    new Face(new FaceVertex(5), new FaceVertex(8), new FaceVertex(4), new FaceVertex(1)),
                    new Face(new FaceVertex(6), new FaceVertex(7), new FaceVertex(8), new FaceVertex(5)),
                }
            }
        };

        public static TestFile cube_v_______tris_absIndex => new TestFile()
        {
            FileName = "cube_v_______tris_absIndex.obj",
            ExpectedModel = new TestModel()
            {
                Vertices = new List<Vertex>()
                {
                    new Vertex(-C, -C, C),
                    new Vertex(C, -C, C),
                    new Vertex(-C, C, C),
                    new Vertex(C, C, C),
                    new Vertex(-C, C, -C),
                    new Vertex(C, C, -C),
                    new Vertex(-C, -C, -C),
                    new Vertex(C, -C, -C),
                },
                Normals = new List<Normal>(),
                TextureCoords = new List<TextureCoord>(),
                Faces = new List<Face>()
                {
                    new Face(new FaceVertex(1), new FaceVertex(2), new FaceVertex(3)),
                    new Face(new FaceVertex(3), new FaceVertex(2), new FaceVertex(4)),
                    new Face(new FaceVertex(3), new FaceVertex(4), new FaceVertex(5)),
                    new Face(new FaceVertex(5), new FaceVertex(4), new FaceVertex(6)),
                    new Face(new FaceVertex(5), new FaceVertex(6), new FaceVertex(7)),
                    new Face(new FaceVertex(7), new FaceVertex(6), new FaceVertex(8)),
                    new Face(new FaceVertex(7), new FaceVertex(8), new FaceVertex(1)),
                    new Face(new FaceVertex(1), new FaceVertex(8), new FaceVertex(2)),
                    new Face(new FaceVertex(2), new FaceVertex(8), new FaceVertex(4)),
                    new Face(new FaceVertex(4), new FaceVertex(8), new FaceVertex(6)),
                    new Face(new FaceVertex(7), new FaceVertex(1), new FaceVertex(5)),
                    new Face(new FaceVertex(5), new FaceVertex(1), new FaceVertex(3)),
                }
            }
        };

        public static TestFile cube_v____vn_tris_absIndex => new TestFile()
        {
            FileName = "cube_v____vn_tris_absIndex.obj",
            ExpectedModel = new TestModel()
            {
                Vertices = new List<Vertex>()
                {
                    new Vertex(-C, -C, C),
                    new Vertex(C, -C, C),
                    new Vertex(-C, C, C),
                    new Vertex(C, C, C),
                    new Vertex(-C, C, -C),
                    new Vertex(C, C, -C),
                    new Vertex(-C, -C, -C),
                    new Vertex(C, -C, -C),
                },
                Normals = new List<Normal>()
                {
                    new Normal(0, 0, 1),
                    new Normal(0, 1, 0),
                    new Normal(0, 0, -1),
                    new Normal(0, -1, 0),
                    new Normal(1, 0, 0),
                    new Normal(-1, 0, 0),
                },
                TextureCoords = new List<TextureCoord>(),
                Faces = new List<Face>()
                {
                    new Face(new FaceVertex(1, null, 1), new FaceVertex(2, null, 1), new FaceVertex(3, null, 1)),
                    new Face(new FaceVertex(3, null, 1), new FaceVertex(2, null, 1), new FaceVertex(4, null, 1)),

                    new Face(new FaceVertex(3, null, 2), new FaceVertex(4, null, 2), new FaceVertex(5, null, 2)),
                    new Face(new FaceVertex(5, null, 2), new FaceVertex(4, null, 2), new FaceVertex(6, null, 2)),

                    new Face(new FaceVertex(5, null, 3), new FaceVertex(6, null, 3), new FaceVertex(7, null, 3)),
                    new Face(new FaceVertex(7, null, 3), new FaceVertex(6, null, 3), new FaceVertex(8, null, 3)),

                    new Face(new FaceVertex(7, null, 4), new FaceVertex(8, null, 4), new FaceVertex(1, null, 4)),
                    new Face(new FaceVertex(1, null, 4), new FaceVertex(8, null, 4), new FaceVertex(2, null, 4)),

                    new Face(new FaceVertex(2, null, 5), new FaceVertex(8, null, 5), new FaceVertex(4, null, 5)),
                    new Face(new FaceVertex(4, null, 5), new FaceVertex(8, null, 5), new FaceVertex(6, null, 5)),

                    new Face(new FaceVertex(7, null, 6), new FaceVertex(1, null, 6), new FaceVertex(5, null, 6)),
                    new Face(new FaceVertex(5, null, 6), new FaceVertex(1, null, 6), new FaceVertex(3, null, 6)),
                }
            }
        };

        public static TestFile cube_v_vt____tris_absIndex = new TestFile()
        {
            FileName = "cube_v_vt____tris_absIndex.obj",
            ExpectedModel = new TestModel()
            {
                Vertices = new List<Vertex>()
                {
                    new Vertex(-C, -C, C),
                    new Vertex(C, -C, C),
                    new Vertex(-C, C, C),
                    new Vertex(C, C, C),
                    new Vertex(-C, C, -C),
                    new Vertex(C, C, -C),
                    new Vertex(-C, -C, -C),
                    new Vertex(C, -C, -C),
                },
                Normals = new List<Normal>(),
                TextureCoords = new List<TextureCoord>()
                {
                    new TextureCoord(0, 0),
                    new TextureCoord(1, 0),
                    new TextureCoord(0, 1),
                    new TextureCoord(1, 1),
                },
                Faces = new List<Face>()
                {
                    new Face(new FaceVertex(1, 1), new FaceVertex(2, 2), new FaceVertex(3, 3)),
                    new Face(new FaceVertex(3, 3), new FaceVertex(2, 2), new FaceVertex(4, 4)),

                    new Face(new FaceVertex(3, 1), new FaceVertex(4, 2), new FaceVertex(5, 3)),
                    new Face(new FaceVertex(5, 3), new FaceVertex(4, 2), new FaceVertex(6, 4)),

                    new Face(new FaceVertex(5, 4), new FaceVertex(6, 3), new FaceVertex(7, 2)),
                    new Face(new FaceVertex(7, 2), new FaceVertex(6, 3), new FaceVertex(8, 1)),

                    new Face(new FaceVertex(7, 1), new FaceVertex(8, 2), new FaceVertex(1, 3)),
                    new Face(new FaceVertex(1, 3), new FaceVertex(8, 2), new FaceVertex(2, 4)),

                    new Face(new FaceVertex(2, 1), new FaceVertex(8, 2), new FaceVertex(4, 3)),
                    new Face(new FaceVertex(4, 3), new FaceVertex(8, 2), new FaceVertex(6, 4)),

                    new Face(new FaceVertex(7, 1), new FaceVertex(1, 2), new FaceVertex(5, 3)),
                    new Face(new FaceVertex(5, 3), new FaceVertex(1, 2), new FaceVertex(3, 4)),
                }
            }
        };

        public static TestFile cube_v_vt_vn_tris_absIndex => new TestFile()
        {
            FileName = "cube_v_vt_vn_tris_absIndex.obj",
            ExpectedModel = new TestModel()
            {
                Vertices = new List<Vertex>()
                {
                    new Vertex(-C, -C, C),
                    new Vertex(C, -C, C),
                    new Vertex(-C, C, C),
                    new Vertex(C, C, C),
                    new Vertex(-C, C, -C),
                    new Vertex(C, C, -C),
                    new Vertex(-C, -C, -C),
                    new Vertex(C, -C, -C),
                },
                Normals = new List<Normal>()
                {
                    new Normal(0, 0, 1),
                    new Normal(0, 1, 0),
                    new Normal(0, 0, -1),
                    new Normal(0, -1, 0),
                    new Normal(1, 0, 0),
                    new Normal(-1, 0, 0),
                },
                TextureCoords = new List<TextureCoord>()
                {
                    new TextureCoord(0, 0),
                    new TextureCoord(1, 0),
                    new TextureCoord(0, 1),
                    new TextureCoord(1, 1),
                },
                Faces = new List<Face>()
                {
                    new Face(new FaceVertex(1, 1, 1), new FaceVertex(2, 2, 1), new FaceVertex(3, 3, 1)),
                    new Face(new FaceVertex(3, 3, 1), new FaceVertex(2, 2, 1), new FaceVertex(4, 4, 1)),

                    new Face(new FaceVertex(3, 1, 2), new FaceVertex(4, 2, 2), new FaceVertex(5, 3, 2)),
                    new Face(new FaceVertex(5, 3, 2), new FaceVertex(4, 2, 2), new FaceVertex(6, 4, 2)),

                    new Face(new FaceVertex(5, 4, 3), new FaceVertex(6, 3, 3), new FaceVertex(7, 2, 3)),
                    new Face(new FaceVertex(7, 2, 3), new FaceVertex(6, 3, 3), new FaceVertex(8, 1, 3)),

                    new Face(new FaceVertex(7, 1, 4), new FaceVertex(8, 2, 4), new FaceVertex(1, 3, 4)),
                    new Face(new FaceVertex(1, 3, 4), new FaceVertex(8, 2, 4), new FaceVertex(2, 4, 4)),

                    new Face(new FaceVertex(2, 1, 5), new FaceVertex(8, 2, 5), new FaceVertex(4, 3, 5)),
                    new Face(new FaceVertex(4, 3, 5), new FaceVertex(8, 2, 5), new FaceVertex(6, 4, 5)),

                    new Face(new FaceVertex(7, 1, 6), new FaceVertex(1, 2, 6), new FaceVertex(5, 3, 6)),
                    new Face(new FaceVertex(5, 3, 6), new FaceVertex(1, 2, 6), new FaceVertex(3, 4, 6)),
                }
            }
        };
    }
}
