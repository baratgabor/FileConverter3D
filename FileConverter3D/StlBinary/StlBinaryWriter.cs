using System;
using System.Collections.Generic;
using System.IO;

namespace FileConverter3D.StlBinary
{
    class StlBinaryWriter : IFileWriter<StlTriangle>
    {
        protected const int HeaderLength = 80;

        public void Write(IEnumerable<StlTriangle> stlTriangles, Stream destination)
        {
            if (!destination.CanWrite)
                throw new ArgumentException("Destination stream is not writeable.");

            // Note: BinaryWriter is always supposed to be little endian; i.e. correct for binary STL
            using (var bw = new BinaryWriter(destination))
            {
                // Write empty header
                bw.Write(new byte[HeaderLength]);

                // Write placeholder triangle count
                bw.Write(default(int));

                var triangleCount = 0;
                foreach (var st in stlTriangles)
                {
                    triangleCount++;
                    WriteVertex(st.Normal);
                    WriteVertex(st.A);
                    WriteVertex(st.B);
                    WriteVertex(st.C);
                    bw.Write(default(ushort)); // Empty 2 byte
                }

                // Write actual triangle count
                bw.Seek(HeaderLength, SeekOrigin.Begin);
                bw.Write(triangleCount);

                void WriteVertex(Vertex v)
                {
                    bw.Write(v.x);
                    bw.Write(v.y);
                    bw.Write(v.z);
                }
            }
        }
    }
}