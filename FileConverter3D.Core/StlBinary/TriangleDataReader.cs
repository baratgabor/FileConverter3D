using System.Collections.Generic;
using System.IO;

namespace FileConverter3D.StlBinary
{
    /// <summary>
    /// Reads STL triangle units as raw byte arrays from a stream.
    /// </summary>
    public class TriangleDataReader : IDataReader<byte[]>
    {
        //TODO: Consider using a well-defined struct instead of byte[]; more explicit
        protected const int HeaderLength = 80;
        protected const int TriangleByteLength = 4 * 3 * 4 + 2;

        public IEnumerable<byte[]> Read(Stream stream)
        {
            using (var br = new BinaryReader(stream))
            {
                // Read header content & ignore
                br.ReadBytes(HeaderLength);

                // Read number of triangles in file
                var triangleCount = br.ReadInt32();

                // Read all triangles in file
                for (int i = 0; i < triangleCount; i++)
                    yield return br.ReadBytes(TriangleByteLength);
            }
        }
    }
}