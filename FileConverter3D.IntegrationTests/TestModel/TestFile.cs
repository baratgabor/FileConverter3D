namespace FileConverter3D.Tests
{
    public struct TestFile
    {
        public string FileName { get; set; }
        public IModel ExpectedModel { get; set; }

        public void DecrementModelFaceIndexes()
        {
            for (int i = 0; i < ExpectedModel.Faces.Count; i++)
            {
                for (int j = 0; j < ExpectedModel.Faces[i].Vertices.Count; j++)
                {
                    var fv = ExpectedModel.Faces[i].Vertices[j];
                    ExpectedModel.Faces[i].Vertices[j] = new FaceVertex(
                        vertexIndex: fv.VertexIndex - 1,
                        textureCoordIndex: fv.TextureCoordIndex == null ? null : fv.TextureCoordIndex - 1,
                        normalIndex: fv.NormalIndex == null ? null : fv.NormalIndex - 1
                    );
                }
            }
        }
    }
}
