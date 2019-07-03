using System;
using System.Collections.Generic;
using System.IO;

namespace FileConverter3D.Core.StlBinary
{
    public class StlBinaryWriter : IDataWriter<byte[]>
    {
        protected const int HeaderLength = 80;

        public void Write(Stream destination, IEnumerable<byte[]> triangleData)
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

                // Write triangles
                uint triangleCount = 0;
                foreach (var triangleBytes in triangleData)
                {
                    bw.Write(triangleBytes);
                    triangleCount++;
                }

                // Seek back and write actual triangle count
                bw.Seek(HeaderLength, SeekOrigin.Begin);
                bw.Write(triangleCount);
            }
        }
    }
}